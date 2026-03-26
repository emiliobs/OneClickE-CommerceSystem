using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OneClick.Backend.Repositories;
using OneClick.Shared.Entities;

namespace OneClick.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoryAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Category>> GetByIdCategoryAsync(int id)
        {
            try
            {
                var category = await _categoryRepository.GetByIdCategoryAsync(id);

                if (category is null)
                {
                    return NotFound($"Category with ID {id} not found");
                }

                return Ok(category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostCategoryAsync(Category category)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var createCategory = await _categoryRepository.AddCategoryAsync(category);

                return Ok(createCategory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutCategoryAsync(int id, Category category)
        {
            try
            {
                if (id != category.Id)
                {
                    return BadRequest($"Category with ID: {id} mismatch");
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existCategory = await _categoryRepository.GetByIdCategoryAsync(id);
                if (existCategory is null)
                {
                    return NotFound($"Category with ID: {existCategory} not found");
                }

                existCategory.Name = category.Name;
                await _categoryRepository.UpdateCategoryAsync(existCategory);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCategoryAsync(int id)
        {
            try
            {
                var existCategory = await _categoryRepository.GetByIdCategoryAsync(id);
                if (existCategory is null)
                {
                    return NotFound($"Category with ID: {id} not found");
                }

                var hasProducts = await _categoryRepository.HasProductsAsync(id);
                if (hasProducts)
                {
                    return BadRequest($"Cannot delete Category ID: {id} with associated Product. Remove Prodcuts first.");
                }

                await _categoryRepository.DeleteCategoryAsync(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}