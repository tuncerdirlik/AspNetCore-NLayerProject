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
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAllAsync();
            var output = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(output);
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            var output = _mapper.Map<ProductDto>(product);

            return Ok(output);
        }

        [HttpGet("{id}/category")]
        public async Task<IActionResult> GetWithCategory(int id)
        {
            var product = await _productService.GetWithCategoryByIdAsync(id);
            var output = _mapper.Map<ProductWithCategoryDto>(product);

            return Ok(output);
        }

        [ValidationFilter]
        [HttpPost]
        public async Task<IActionResult> Save(ProductDto product)
        {
            var entity = _mapper.Map<Product>(product);
            var dbItem = await _productService.AddAsync(entity);

            return Created(string.Empty, _mapper.Map<ProductDto>(dbItem));
        }

        [HttpPut]
        public IActionResult Update(ProductDto product)
        {
            var entity = _mapper.Map<Product>(product);
            _productService.Update(entity);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var product = _productService.GetByIdAsync(id).Result;
            _productService.Remove(product);

            return NoContent();
        }
    }
}
