using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NLayerProject.Core.Models;
using NLayerProject.Core.Services;
using NLayerProject.Web.ApiServices;
using NLayerProject.Web.DTOs;
using NLayerProject.Web.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NLayerProject.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryApiService _categoryApiService;

        private readonly IMapper _mapper;

        public CategoryController(IMapper mapper, CategoryApiService categoryApiService)
        {
            this._categoryApiService = categoryApiService;

            this._mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categoryDto = await _categoryApiService.GetAllAsync(); 
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
            await _categoryApiService.AddAsync(model);

            return RedirectToActionPermanent("Index");
        }

        [ServiceFilter(typeof(NotFoundFilter))]
        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryApiService.GetByIdAsync(id);
            var model = _mapper.Map<CategoryDto>(category);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryDto model)
        {
            await _categoryApiService.UpdateAsync(model);

            return RedirectToActionPermanent("Index");
        }


        public async Task<IActionResult> Delete(int id)
        {
            await _categoryApiService.Remove(id);

            return RedirectToActionPermanent("Index");
        }
    }
}
