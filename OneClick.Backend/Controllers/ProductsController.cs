using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Customer")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, IConfiguration configuration)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._configuration = configuration;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                return Ok(await _productRepository.GetAllProductsAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error getting products: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetPorductById(int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);

                if (product is null)
                {
                    return NotFound($"Product with ID {id} not found");
                }

                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Products/ByCategory/5
        [HttpGet("ByCategory/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(int categoryId)
        {
            try
            {
                var categoryExist = await _categoryRepository.GetByIdCategoryAsync(categoryId);
                if (categoryExist is null)
                {
                    return NotFound($"Category With ID: {categoryId} not found");
                }

                var prodcut = await _productRepository.GetProductByCategoryIdAsync(categoryId);

                return Ok(prodcut);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PostProductAsync(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verify category exist
                var categoryExist = await _categoryRepository.GetByIdCategoryAsync(product.CategoryId);
                if (categoryExist is null)
                {
                    return BadRequest($"Category with ID: {product.CategoryId} does not exist");
                }

                return Ok(await _productRepository.AddProductAsync(product));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PutProductAsync(int id, Product product)
        {
            try
            {
                if (id != product.Id)
                {
                    return BadRequest($"Product with ID: {id} mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Verify category exist
                var categoryExist = await _categoryRepository.GetByIdCategoryAsync(product.CategoryId);
                if (categoryExist is null)
                {
                    return BadRequest($"Category with ID {product.CategoryId} does not exist.");
                }

                var resultProduct = await _productRepository.UpdateProductAsync(product);
                if (resultProduct is null)
                {
                    return BadRequest($"Product with ID {id} not found");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server erro: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePorductsAsync(int id)
        {
            try
            {
                var resultProduct = await _productRepository.DeleteProductAsync(id);
                if (resultProduct is null)
                {
                    return NotFound($"Prodcut with ID: {id} not found");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal  error server: {ex.Message}");
            }
        }

        [HttpPost("UploadImage")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Validate that the file exist is not empty
            if (file is null || file.Length == 0)
            {
                return BadRequest("NoContent file uploaded");
            }

            try
            {
                //Initialize Cloudinary Account using configuration from appsettings.json
                // These keys must match exactly what is in your JSON file.
                var account = new Account
                    (
                       _configuration["CloudinarySettings:CloudName"],
                       _configuration["CloudinarySettings:ApiKey"],
                       _configuration["CloudinarySettings:ApiSecret"]

                    );

                var cloudinary = new Cloudinary(account);

                // Prepare tyhe file stream for upload
                await using var stream = file.OpenReadStream();

                var uploadParam = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),

                    //Transformation: Automatically crop the image to a 500X500 square
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };

                // Execute the upload to Clodinary
                var uploadResult = await cloudinary.UploadAsync(uploadParam);

                // Check for Cloudinary specific errors
                if (uploadResult.Error is not null)
                {
                    return BadRequest(uploadResult.Error.Message);
                }

                // Return the Secure URL (Https) to the forntend
                return Ok(new { Url = uploadResult.SecureUrl.ToString() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Interna server error: {ex.Message}");
            }
        }

        [HttpGet("Low-stock/{threshold}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<Product>>> GetLowStock(int threshold)
        {
            try
            {
                //  Validate that the threshold is a positive integer
                var products = await _productRepository.GetLowStockProductsAsync(threshold);

                // Return the list of low stock products to the frontend
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"INternal  server error: {ex.Message}");
            }
        }

        [HttpPut("restock/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Restock(int id, [FromBody] int quantity)
        {
            try
            {
                // Validate that the quantity is a positive integer
                if (quantity <= 0)
                {
                    return BadRequest("Quantity must be  greater than zero.");
                }

                // Check if the product exist before attempting to restock
                var success = await _productRepository.RestockProductAsync(id, quantity);

                // If the restock operation was not successful, return a NotFound response
                if (!success)
                {
                    return NotFound($"Product with ID {id} not found or update failed.");
                }

                return Ok("Stock updated successfully!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}