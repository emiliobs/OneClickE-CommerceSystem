using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository)
        {
            this._productRepository = productRepository;
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
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

                var existProduct = await _productRepository.GetProductByIdAsync(id);
                if (existProduct is null)
                {
                    return BadRequest($"Product with ID {id} not found");
                }

                // Verify category exist
                var categoryExist = await _categoryRepository.GetByIdCategoryAsync(product.CategoryId);
                if (categoryExist is null)
                {
                    return BadRequest($"Category with ID {product.CategoryId} does not exist.");
                }

                existProduct.Name = product.Name;
                existProduct.Description = product.Description;
                existProduct.ImageURL = product.ImageURL;
                existProduct.Price = product.Price;
                existProduct.Qty = product.Qty;
                existProduct.CategoryId = product.CategoryId;

                await _productRepository.UpdateProductAsync(product);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server erro: {ex.Message}");
            }
        }
    }
}