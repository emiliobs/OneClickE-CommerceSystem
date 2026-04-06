using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace OneClick.Backend.Services;

public class ImageService : IImageService
{
    private readonly IConfiguration _configuration;

    public ImageService(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public async Task<string> UploadBase64ImageAsync(string base64Image)
    {
        //  Initialize Cloudinary using your exact logic
        var account = new Account(
            _configuration["CloudinarySettings:CloudName"],
            _configuration["CloudinarySettings:ApiKey"],
            _configuration["CloudinarySettings:ApiSecret"]
        );

        var cloudinary = new Cloudinary(account);

        // Cloudinary can read Base64 strings natively!
        var cloudinaryFormatBase64 = base64Image;
        if (!cloudinaryFormatBase64.StartsWith("data:image"))
        {
            cloudinaryFormatBase64 = $"data:image/jpeg;base64,{base64Image}";
        }

        // 3. Prepare the parameters
        var uploadParam = new ImageUploadParams
        {
            File = new FileDescription(cloudinaryFormatBase64),
            Transformation = new Transformation().Height(500).Width(500).Crop("fill")
        };

        //  Execute upload
        var uploadResult = await cloudinary.UploadAsync(uploadParam);

        if (uploadResult.Error is not null)
        {
            throw new Exception($"Cloudinary Error: {uploadResult.Error.Message}");
        }

        // Return the String URL
        return uploadResult.SecureUrl.ToString();
    }
}