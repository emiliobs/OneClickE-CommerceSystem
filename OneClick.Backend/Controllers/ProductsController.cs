using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers
{
    // The [Route] attribute defines the base URL for all endpoints in this controller. The [ApiController] attribute enables API-specific behaviors,
    // such as automatic model validation and binding source inference. The [Authorize] attribute restricts access to authenticated users with
    // the specified roles (Admin and Customer).
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Customer")]
    public class ProductsController : ControllerBase
    {
        // Dependency injection of the product and category repositories, as well as the configuration for Cloudinary.
        private readonly IProductRepository _productRepository;

        private readonly ICategoryRepository _categoryRepository;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, IConfiguration configuration)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
            this._configuration = configuration;
        }

        // GET: api/Products
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts()
        {
            try
            {
                // Retrieve all products from the repository and return them to the frontend. If an error occurs, return a 500 status code with the error message.
                return Ok(await _productRepository.GetAllProductsAsync());
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"Error getting products: {ex.Message}");
            }
        }

        // GET: api/Products/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetPorductById(int id)
        {
            try
            {
                // Retrieve the product with the specified ID from the repository. If the product is not found, return a 404 Not Found response.
                // If an error occurs, return a 500 status code with the error message.
                var product = await _productRepository.GetProductByIdAsync(id);

                // If the product is not found, return a 404 Not Found response with a message indicating that the product was not found.
                if (product is null)
                {
                    // Log the not found case (not implemented here) and return a 404 Not Found response with a message indicating that the product was not found.
                    return NotFound($"Product with ID {id} not found");
                }

                // If the product is found, return it to the frontend with a 200 OK status code.
                return Ok(product);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // GET: api/Products/ByCategory/5
        [HttpGet("ByCategory/{categoryId:int}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(int categoryId)
        {
            try
            {
                // Verify that the category with the specified ID exists before attempting to retrieve products. If the category does not exist, return a 404 Not Found response.
                var categoryExist = await _categoryRepository.GetByIdCategoryAsync(categoryId);

                // If the category does not exist, return a 404 Not Found response with a message indicating that the category was not found.
                if (categoryExist is null)
                {
                    // Log the not found case (not implemented here) and return a 404 Not Found response with a message indicating that the category was not found.
                    return NotFound($"Category With ID: {categoryId} not found");
                }

                // If the category exists, retrieve the products associated with that category from the repository and return them to the frontend. If an error occurs,
                // return a 500 status code with the error message.
                var prodcut = await _productRepository.GetProductByCategoryIdAsync(categoryId);

                // Return the list of products to the frontend with a 200 OK status code.
                return Ok(prodcut);
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/Products
        [HttpPost]
        [Authorize("Admin")]// Only users with the "Admin" role can access this endpoint to create new products.
        public async Task<IActionResult> PostProductAsync(Product product)
        {
            try
            {
                // Validate the incoming product data. If the model state is invalid, return a 400 Bad Request response with the validation errors.
                if (!ModelState.IsValid)
                {
                    // Log the validation errors (not implemented here) and return a 400 Bad Request response with the validation errors.
                    return BadRequest(ModelState);
                }

                // Verify that the category with the specified ID exists before attempting to create a new product. If the category does not exist, return a 400 Bad Request response.
                var categoryExist = await _categoryRepository.GetByIdCategoryAsync(product.CategoryId);

                // If the category does not exist, return a 400 Bad Request response with a message indicating that the category was not found.
                if (categoryExist is null)
                {
                    // Log the not found case (not implemented here) and return a 400 Bad Request response with a message indicating that the category was not found.
                    return BadRequest($"Category with ID: {product.CategoryId} does not exist");
                }

                // If the category exists, create the new product in the repository and return the created product to the frontend with a 200 OK status code. If an error occurs,
                return Ok(await _productRepository.AddProductAsync(product));
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id:int}")]
        [Authorize("Admin")]// Only users with the "Admin" role can access this endpoint to update existing products.
        public async Task<IActionResult> PutProductAsync(int id, Product product)
        {
            try
            {
                // Validate that the ID in the URL matches the ID of the product being updated. If there is a mismatch, return a 400 Bad Request response.
                if (id != product.Id)
                {
                    // Log the ID mismatch (not implemented here) and return a 400 Bad Request response with a message indicating that there is an ID mismatch.
                    return BadRequest($"Product with ID: {id} mismatch");
                }

                // Validate the incoming product data. If the model state is invalid, return a 400 Bad Request response with the validation errors.
                if (!ModelState.IsValid)
                {
                    // Log the validation errors (not implemented here) and return a 400 Bad Request response with the validation errors.
                    return BadRequest(ModelState);
                }

                // Verify category exist
                var categoryExist = await _categoryRepository.GetByIdCategoryAsync(product.CategoryId);

                // If the category does not exist, return a 400 Bad Request response with a message indicating that the category was not found.
                if (categoryExist is null)
                {
                    // Log the not found case (not implemented here) and return a 400 Bad Request response with a message indicating that the category was not found.
                    return BadRequest($"Category with ID {product.CategoryId} does not exist.");
                }

                // If the category exists, update the product in the repository. If the product to be updated is not found, return a 400 Bad Request
                // response with a message indicating that the product was not found.
                var resultProduct = await _productRepository.UpdateProductAsync(product);

                // If the update operation was not successful (e.g., the product was not found), return a 400 Bad Request response with a message indicating that the product was not found.
                if (resultProduct is null)
                {
                    // Log the not found case (not implemented here) and return a 400 Bad Request response with a message indicating that the product was not found.
                    return BadRequest($"Product with ID {id} not found");
                }

                // If the update is successful, return a 200 OK response to the frontend. If an error occurs, return a 500 status code with the error message.
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"Internal server erro: {ex.Message}");
            }
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:int}")]
        [Authorize("Admin")]// Only users with the "Admin" role can access this endpoint to delete products.
        public async Task<IActionResult> DeletePorductsAsync(int id)
        {
            try
            {
                // Attempt to delete the product with the specified ID from the repository. If the product is not found, return a 404 Not Found response
                // with a message indicating that the product was not found.
                var resultProduct = await _productRepository.DeleteProductAsync(id);

                // If the delete operation was not successful (e.g., the product was not found), return a 404 Not Found response with a message indicating
                // that the product was not found.
                if (resultProduct is null)
                {
                    // Log the not found case (not implemented here) and return a 404 Not Found response with a message indicating that the product was not found.
                    return NotFound($"Prodcut with ID: {id} not found");
                }

                // If the delete is successful, return a 200 OK response to the frontend. If an error occurs, return a 500 status code with the error message.
                return Ok();
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"Internal  error server: {ex.Message}");
            }
        }

        // POST: api/Products/UploadImage
        [HttpPost("UploadImage")]
        [Authorize("Admin")]// Only users with the "Admin" role can access this endpoint to upload product images.
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            // Validate that the file exist is not empty
            if (file is null || file.Length == 0)
            {
                // Log the validation error (not implemented here) and return a 400 Bad Request response with a message indicating that no file was uploaded.
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

                // Create a Cloudinary instance with the account information
                var cloudinary = new Cloudinary(account);

                // Prepare tyhe file stream for upload
                await using var stream = file.OpenReadStream();

                // Set up the upload parameters, including the file description and transformation options. The transformation option is set
                // to automatically crop the image to a 500x500 square.
                var uploadParam = new ImageUploadParams
                {
                    // The File property is set to a new FileDescription, which takes the file name and the file stream as parameters. This tells Cloudinary what file to upload.
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

        // GET: api/Products/Low-stock/5
        [HttpGet("Low-stock/{threshold}")]
        [Authorize(Roles = "Admin")]// Only users with the "Admin" role can access this endpoint to retrieve products that are low in stock based on a specified threshold.
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
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"INternal  server error: {ex.Message}");
            }
        }

        // PUT: api/Products/restock/5
        [HttpPut("restock/{id}")]
        [Authorize(Roles = "Admin")]// Only users with the "Admin" role can access this endpoint to restock a product by specifying the product ID and the quantity to add to the stock.
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
                    // Log the not found case (not implemented here) and return a 404 Not Found response with a message indicating that the product was not found or the update failed.
                    return NotFound($"Product with ID {id} not found or update failed.");
                }

                // If the restock is successful, return a 200 OK response to the frontend with a success message. If an error occurs, return a 500 status code with the error message.
                return Ok("Stock updated successfully!");
            }
            catch (Exception ex)
            {
                // Log the exception (not implemented here) and return a 500 Internal Server Error response with the error message.
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}