using Dasync.Collections;
using Ecommerce.Utilities.Image.Constants;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;

namespace Ecommerce.Utilities.Image.Service
{
    public static class FileService
    {
        public static async Task<bool> UploadFiles(List<IFormFile> files, string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            try
            {
                await files.ParallelForEachAsync(async file =>
                {
                    string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
                    string fullPath = Path.Combine(path, fileName);
                    using var stream = new FileStream(fullPath, FileMode.Create);
                    await file.CopyToAsync(stream);
                }, 5);
            }
            catch
            {
                Directory.Delete(path);
                return false;
            }
            return true;
        }

        public static List<string>? GetFileNames(string path)
        {
            var directory = new DirectoryInfo(path);
            if (!directory.Exists) return null;
            var files = directory.EnumerateFiles();
            return files.Select(f => f.Name).ToList();
        }

        public static List<string>? GetFileUrls(string path, string requestPath)
        {
            var directory = new DirectoryInfo(path);
            if (!directory.Exists) return null;
            var files = directory.EnumerateFiles();
            return files.Select(f => string.Format(Url.ImageUrl, requestPath, directory.Name, f.Name)).ToList();
        }
    }
}