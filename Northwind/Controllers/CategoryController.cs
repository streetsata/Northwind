using AutoMapper;
using Contracts;
using Entites.DTO;
using Entites.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IMapper _mapper;

        public CategoryController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the list of all Categories
        /// </summary>
        /// <returns>The list of Categories</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _repository.Category.GetAllCategoriesAsync();

                _logger.LogInfo($"Returned all categories from DB");

                var categoryResult = _mapper.Map<IEnumerable<CategoryDto>>(categories);

                return Ok(categoryResult);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Somthing went wrong inside GetAllCategories action: {ex.Message}\tStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        /// <summary>
        /// Gets Category by id
        /// </summary>
        /// <param name="id">Needed category by id</param>
        /// <returns>Category by id</returns>
        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var category = await _repository.Category.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in DB");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned category with id: {id} from DB");

                    var categoryResult = _mapper.Map<CategoryDto>(category);
                    return Ok(categoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somthing went wrong inside GetCategoryById action: {ex.Message}\tStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/product")]
        public async Task<IActionResult> GetCategoryWithDetails(int id)
        {
            try
            {
                var category = await _repository.Category.GetCategoryWithDetailsAsync(id);

                if (category == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in DB");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned category with details: {id} from DB");

                    var categoryResult = _mapper.Map<CategoryDto>(category);
                    return Ok(categoryResult);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somthing went wrong inside GetCategoryWithDetails action: {ex.Message}\tStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryForCreationDto category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogError("Category object sent from client is null");
                    return BadRequest("Category object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid category object sent from client ");
                    return BadRequest("Invalid object model");
                }

                var categoryEntity = _mapper.Map<Category>(category);

                _repository.Category.CreateCategory(categoryEntity);
                await _repository.SaveAsync();

                var createdCategory = _mapper.Map<CategoryDto>(categoryEntity);

                return CreatedAtRoute("CategoryById", new { id = createdCategory.CategoryID }, createdCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somthing went wrong inside CreateCategory action: {ex.Message}\tStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryForUpdateDto category)
        {
            try
            {
                if (category == null)
                {
                    _logger.LogError("Category object sent from client is null");
                    return BadRequest("Category object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid category object sent from client ");
                    return BadRequest("Invalid object model");
                }

                var categoryEntity = await _repository.Category.GetCategoryByIdAsync(id);

                if (categoryEntity == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in DB");
                    return NotFound();
                }

                _mapper.Map(category, categoryEntity);

                _repository.Category.UpdateCategory(categoryEntity);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Somthing went wrong inside UpdateCategory action: {ex.Message}\tStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var category = await _repository.Category.GetCategoryByIdAsync(id);

                if (category == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in DB");
                    return NotFound();
                }

                if (_repository.Product.ProductsByCategory(id).Any())
                {
                    _logger.LogError($"Cannot delete category with id: {id}. It has related products. Delete those products first");
                    return BadRequest("Cannot delete category. It has related products. If need delete products first");
                }

                _repository.Category.DeleteCategory(category);
                await _repository.SaveAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                string innerEx = ex.InnerException != null ? ex.InnerException.Message : "";
                _logger.LogError($"Somthing went wrong inside DeleteCategory action: {ex.Message}\tStackTrace: {ex.StackTrace}\t" +
                    $"Inner Exception: {innerEx}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
