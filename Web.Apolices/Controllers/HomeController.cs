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
            var categoryOptions = new List<string>  {
        "Anual",
        "Mensal"
            };

            ViewBag.frequenciaPagamento = new SelectList(categoryOptions, "Name");



            return View();
        }

        private object RestornaLista(string lista, bool cobertura = true)
        {
            var listaCoberturas = new List<CoberturaModel>();
            var listaBeneficiarios = new List<BeneficiarioModel>();

            CoberturaModel? coberturaModel;
            BeneficiarioModel? beneficiarioModel;


            string[] items = lista.Split('|');


            switch (cobertura)
            {
                case true:

                    foreach (var par in items)
                    {
                        string[] chaveValor = par.Split(';');

                        coberturaModel = new CoberturaModel
                        {
                            Id = ObjectId.GenerateNewId().ToString(),
                            Descricao = chaveValor[0],
                            CapitalSegurado = chaveValor[1]
                        };
                        listaCoberturas.Add(coberturaModel);
                        coberturaModel = null;
                    }
                    return listaCoberturas;

                default:
                    foreach (var par in items)
                    {
                        string[] chaveValor = par.Split(';');

                        beneficiarioModel = new BeneficiarioModel
                        {
                            Id = ObjectId.GenerateNewId().ToString(),
                            Nome = chaveValor[0],
                            PercentualBeneficio = chaveValor[1]
                        };
                        listaBeneficiarios.Add(beneficiarioModel);
                        beneficiarioModel = null;
                    }
                    return listaBeneficiarios;
            }


        }
        [HttpPost]
        public async Task<IActionResult> Create(SeguroModel model, string frequenciaPagamento, string hddCobertura, string hddBeneficiario)
        {
            model.FrequenciaPagamento = frequenciaPagamento;
            model.Coberturas = (List<CoberturaModel>)RestornaLista(hddCobertura);
            model.Beneficiarios = (List<BeneficiarioModel>)RestornaLista(hddBeneficiario,false);


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

            var categoryOptions = new List<string>  {
                "Anual",
                "Mensal"
            };

            ViewBag.frequenciaPagamento = new SelectList(categoryOptions, model.FrequenciaPagamento);


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