

namespace RentSaaS.Application.DTOs.Advertising
{
    public class AdvertizingFileDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public long FileSize { get; set; }
        public DateTime UploadedAt { get; set; }
        public string Url { get; set; }
    }
}
