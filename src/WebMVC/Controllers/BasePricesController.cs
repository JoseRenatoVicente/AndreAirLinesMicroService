using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AndreAirLines.Domain.Entities;
using WebMVC.Data;
using AndreAirLines.Domain.Services;

namespace WebMVC.Controllers
{
    public class BasePricesController : Controller
    {
        private readonly GatewayService _gatewayService;

        public BasePricesController(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }


        // GET: BasePrices
        public async Task<IActionResult> Index()
        {
            return View(await _gatewayService.GetFromJsonAsync<IEnumerable<BasePrice>>("BasePrice/api/BasePrices"));
        }

        // GET: BasePrices/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basePrice = await GetBasePrice(id);
            if (basePrice == null)
            {
                return NotFound();
            }

            return View(basePrice);
        }

        // GET: BasePrices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BasePrices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Value,CreationDate,Id")] BasePrice basePrice)
        {
            if (ModelState.IsValid)
            {
                await _gatewayService.PostAsync("BasePrice/api/BasePrices", basePrice);
                return RedirectToAction(nameof(Index));
            }
            return View(basePrice);
        }

        // GET: BasePrices/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basePrice = await GetBasePrice(id);
            if (basePrice == null)
            {
                return NotFound();
            }
            return View(basePrice);
        }

        // POST: BasePrices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Value,CreationDate,Id")] BasePrice basePrice)
        {
            if (id != basePrice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gatewayService.PutAsync("BasePrice/api/BasePrices", basePrice);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await GetBasePrice(id) == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(basePrice);
        }

        // GET: BasePrices/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var basePrice = await GetBasePrice(id);
            if (basePrice == null)
            {
                return NotFound();
            }

            return View(basePrice);
        }

        // POST: BasePrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _gatewayService.DeleteAsync("BasePrice/api/BasePrices/" + id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<BasePrice> GetBasePrice(string id)
        {
            return await _gatewayService.GetFromJsonAsync<BasePrice>("BasePrice/api/BasePrices/" + id);
        }
    }
}
