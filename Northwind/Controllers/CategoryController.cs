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

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            try
            {
                var categories = _repository.Category.GetAllCategories();

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


        [HttpGet("{id}", Name = "CategoryById")]
        public IActionResult GetCategoryById(int id)
        {
            try
            {
                var category = _repository.Category.GetCategoryById(id);

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
        public IActionResult GetCategoryWithDetails(int id)
        {
            try
            {
                var category = _repository.Category.GetCategoryWithDetails(id);

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
        public IActionResult CreateCategory([FromBody]CategoryForCreationDto category)
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
                _repository.Save();

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
        public IActionResult UpdateCategory(int id, [FromBody] CategoryForUpdateDto category)
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

                var categoryEntity = _repository.Category.GetCategoryById(id);

                if (categoryEntity == null)
                {
                    _logger.LogError($"Category with id: {id}, hasn't been found in DB");
                    return NotFound();
                }

                _mapper.Map(category, categoryEntity);

                _repository.Category.UpdateCategory(categoryEntity);
                _repository.Save();

                return NoContent();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Somthing went wrong inside UpdateCategory action: {ex.Message}\tStackTrace: {ex.StackTrace}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
