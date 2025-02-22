using System.Text.Json.Serialization;

namespace RentSaaS.API.Models;

public class PaginationInfo
{
    private int _pageSize;
    private int _currentPage;

    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int CurrentPage
    {
        get => _currentPage;
        set => _currentPage = value < 1 ? 1 : value;
    }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = value < 1 ? 10 : value;
    }

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public int TotalItems { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    [JsonIgnore]
    public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    public bool HasPrevious => CurrentPage > 1;

    /// <summary>
    /// Whether there is a next page
    /// </summary>
    public bool HasNext => CurrentPage < TotalPages;

    /// <summary>
    /// First item number of current page
    /// </summary>
    public int FirstItem => (CurrentPage - 1) * PageSize + 1;

    /// <summary>
    /// Last item number of current page
    /// </summary>
    public int LastItem => Math.Min(CurrentPage * PageSize, TotalItems);

    public PaginationInfo()
    {
        CurrentPage = 1;
        PageSize = 10;
        TotalItems = 0;
    }

    public PaginationInfo(int currentPage, int pageSize, int totalItems)
    {
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalItems = totalItems;
    }

    /// <summary>
    /// Creates pagination metadata for API responses
    /// </summary>
    public object GetMetadata()
    {
        return new
        {
            totalItems = TotalItems,
            currentPage = CurrentPage,
            pageSize = PageSize,
            totalPages = TotalPages,
            hasNext = HasNext,
            hasPrevious = HasPrevious,
            firstItem = FirstItem,
            lastItem = LastItem
        };
    }

    /// <summary>
    /// Calculates the number of items to skip for the current page
    /// </summary>
    public int Skip => (CurrentPage - 1) * PageSize;

    /// <summary>
    /// Validates and adjusts pagination parameters if needed
    /// </summary>
    public void Validate()
    {
        if (CurrentPage < 1) CurrentPage = 1;
        if (PageSize < 1) PageSize = 10;
        if (PageSize > 100) PageSize = 100; // Maximum page size limit
    }

    /// <summary>
    /// Creates a new PaginationInfo instance with default values
    /// </summary>
    public static PaginationInfo Default => new PaginationInfo(1, 10, 0);

    /// <summary>
    /// Creates pagination parameters for database queries
    /// </summary>
    public (int skip, int take) GetPaginationParameters()
    {
        return (Skip, PageSize);
    }
}