using System;
using System.IO;
using InvoiceManagement.Application.Common.Interfaces;

namespace InvoiceManagement.Infrastructure.FileStorage;

public class FileStorageService : IFileStorageService
{
    public string SaveBase64LogoImage(string base64, string fileName = "logo.png")
    {
        var directory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "company");
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);
        var filePath = Path.Combine(directory, fileName);

        // Eliminar prefijo si existe
        var base64Data = base64;
        var commaIndex = base64.IndexOf(',');
        if (commaIndex >= 0)
            base64Data = base64.Substring(commaIndex + 1);

        var bytes = Convert.FromBase64String(base64Data);
        File.WriteAllBytes(filePath, bytes);
        return $"/images/company/{fileName}";
    }
}
