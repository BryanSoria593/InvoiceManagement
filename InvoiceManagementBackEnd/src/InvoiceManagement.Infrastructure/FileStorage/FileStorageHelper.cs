using System;
using System.IO;
using System.Threading.Tasks;

namespace InvoiceManagement.Infrastructure.FileStorage;
public static class FileStorageHelper
{
    public static string SaveBase64LogoImage(string base64, string fileName = "logo.png")
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "company");
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, fileName);
        var bytes = Convert.FromBase64String(base64);
        File.WriteAllBytes(filePath, bytes);
        return $"/images/company/{fileName}";
    }
}
