namespace InvoiceManagement.Application.Common.Interfaces;
public interface IFileStorageService
{
    string SaveBase64LogoImage(string base64, string fileName = "logo.png");
}
