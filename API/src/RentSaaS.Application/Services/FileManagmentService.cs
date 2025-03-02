using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace RentSaaS.Application.Services;

public class FileManagmentService : IFileManagmentService
{
    private readonly IFileProvider fileProvider;
    public FileManagmentService(IFileProvider fileProvider)
    {
        this.fileProvider = fileProvider;
    }
    public async Task<List<string>> AddFileAsync(IFormFileCollection files, string source)
    {
        var filledFiles = new List<string>();
        var fileDirctory = Path.Combine("wwwroot", "StorageFiles", source);
        if (Directory.Exists(fileDirctory) is not true)
        {
            Directory.CreateDirectory(fileDirctory);
        }
        foreach (var file in files)
        {
            if (file.Length > 0)
            {
                var fileNames = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                var filePath = Path.Combine("StorageFiles", source, fileNames); 

                var root = Path.Combine(fileDirctory, fileNames);

                using (var stream = new FileStream(root, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                filledFiles.Add(filePath);
            }
        }

        return filledFiles;
    }

    public void DeleteFile(string source)
    {
      var file= fileProvider.GetFileInfo(source);
        var root = file.PhysicalPath;
        File.Delete(root);
    }
}
