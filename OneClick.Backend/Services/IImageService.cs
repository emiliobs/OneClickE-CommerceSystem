namespace OneClick.Backend.Services;

public interface IImageService
{
    // Receives a base64 string, uploads to Cloudinary, and returns the Secure URL
    Task<string> UploadBase64ImageAsync(string base64Image);
}