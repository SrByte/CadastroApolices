using Apolices.Web.Models;
using Apolices.Web.Services.IServices;
using Apolices.Web.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Bson;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Apolices.Web.Controllers
{
	public class HomeController : Controller
	{
		private readonly ISeguroService _seguroService;
		private readonly IMemoryCache _cache;



		public HomeController(ISeguroService seguroService)
		{
			_seguroService = seguroService;
		}
		public async Task<IActionResult> Index()
		{

			var seguros = await _seguroService.FindAllSeguros();
			return View(seguros);
		}
		public IActionResult Create()
		{
			var categoryOptions = new List<string>	{
		"Anual",
		"Mensal"
			};

			ViewBag.CategoryId = new SelectList(categoryOptions, "Name");

			return View();
		}


		[HttpPost]
		public async Task<IActionResult> Create(SeguroModel model)

		{
			if (ModelState.IsValid)
			{
				model.Id = ObjectId.GenerateNewId().ToString();
				var response = await _seguroService.CreateSeguro(model);
				if (response != null) return RedirectToAction(
					 nameof(Index));
			}
			return View(model);
		}
		public async Task<IActionResult> Update(string id)
		{

			var model = await _seguroService.FindSeguroById(id);
			if (model == null) return NotFound();

			var lista = await _seguroService.FindAllSeguros();

			//List<SelectListItem> categoryList = (from p in lista.AsEnumerable()
			//									 select new SelectListItem
			//									 {
			//										 Text = p.Name,
			//										 Value = p.Id.ToString()
			//									 }).ToList();

			//categoryList.Find(c => c.Value == model.CategoryId.ToString()).Selected = true;

			//ViewBag.CategoryId = categoryList;

			//ViewBag.Ingredients = await _ingredientService.FindAllIngredients();


			return View(model);

		}

		[HttpPost]
		public async Task<IActionResult> Update(SeguroModel model)
		{

			if (ModelState.IsValid)
			{
				var response = await _seguroService.UpdateSeguro(model);
				if (response != null) return RedirectToAction(
					 nameof(Index));
			}
			return View(model);
		}

		//[Authorize]
		public async Task<IActionResult> Delete(string id)
		{
			var seguros = await _seguroService.FindSeguroById(id);
			return View(seguros);
		}

		[HttpPost]
		//[Authorize(Roles = Role.Admin)]
		public async Task<IActionResult> Delete(SeguroModel model)
		{
			var response = await _seguroService.DeleteSeguroById(model.Id.ToString());
			if (response) return RedirectToAction(
					nameof(Index));
			return View(model);
		}
	}
}