using Microsoft.AspNetCore.Http;

namespace RentSaaS.Application.Services;

public interface IFileManagmentService
{
    Task<List<string>> AddFileAsync(IFormFileCollection files, string source);
    void DeleteFileAsync(string source);
}
