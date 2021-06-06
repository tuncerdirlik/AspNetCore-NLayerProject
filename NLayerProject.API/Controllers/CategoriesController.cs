using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.API.DTOs;
using NLayerProject.API.Filters;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryService.GetAllAsync();
            var output = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            return Ok(output);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var item = await _categoryService.GetByIdAsync(id);
            var output = _mapper.Map<CategoryDto>(item);

            return Ok(output);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetWithProducts(int id)
        {
            var categories = await _categoryService.GetWithProductsByIdAsync(id);
            var output = _mapper.Map<CategoryWithProductDto>(categories);
            return Ok(output);
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(CategoryDto category)
        {
            var entity = _mapper.Map<Category>(category);
            var dbItem = await _categoryService.AddAsync(entity);

            return Created(string.Empty, _mapper.Map<CategoryDto>(dbItem));
        }

        [HttpPut]
        public IActionResult Update(CategoryDto category)
        {
            var entity = _mapper.Map<Category>(category);
            _categoryService.Update(entity);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult Remove(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);

            return NoContent();
        }

        
    }
}
