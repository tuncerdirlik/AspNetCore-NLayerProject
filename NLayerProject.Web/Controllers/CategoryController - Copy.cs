using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;
using NLayerProject.Web.DTOs;
using NLayerProject.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.Controllers
{
    public class __CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public __CategoryController(ICategoryService categoryService, IMapper mapper)
        {
            this._categoryService = categoryService;
            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categoreis = await _categoryService.GetAllAsync();
            var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(categoreis);

            return View(categoryDto);
        }

        public IActionResult Create()
        {
            var model = new CategoryDto();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDto model)
        {
            Category entity = _mapper.Map<Category>(model);
            await _categoryService.AddAsync(entity);

            return RedirectToActionPermanent("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            var model = _mapper.Map<CategoryDto>(category);

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(CategoryDto model)
        {
            var category = _mapper.Map<Category>(model);
            _categoryService.Update(category);

            return RedirectToActionPermanent("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetByIdAsync(id).Result;
            _categoryService.Remove(category);

            return RedirectToActionPermanent("Index");
        }
    }
}
