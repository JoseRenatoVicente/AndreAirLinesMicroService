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
using System.Collections;

namespace WebMVC.Controllers
{
    public class AirportsController : Controller
    {
        private readonly GatewayService _gatewayService;

        public AirportsController(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }


        // GET: Airports
        public async Task<IActionResult> Index()
        {
            return View(await _gatewayService.GetFromJsonAsync<IEnumerable<Airport>>("Airport/api/Airports"));
        }

        // GET: Airports/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var airport = await _gatewayService.GetFromJsonAsync<Airport>("Airport/api/Airports/" + id);
            if (airport == null)
            {
                return NotFound();
            }

            return View(airport);
        }

        // GET: Airports/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Airports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IATACode,Name")] Airport airport)
        {
            if (ModelState.IsValid)
            {
                await _gatewayService.PostAsync("Airport/api/Airports", airport);
                return RedirectToAction(nameof(Index));
            }
            return View(airport);
        }
        /*
                // GET: Airports/Edit/5
                public async Task<IActionResult> Edit(string id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var airport = await _context.Airport.FindAsync(id);
                    if (airport == null)
                    {
                        return NotFound();
                    }
                    return View(airport);
                }

                // POST: Airports/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(string id, [Bind("IATACode,Name,Id")] Airport airport)
                {
                    if (id != airport.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(airport);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!AirportExists(airport.Id))
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
                    return View(airport);
                }

                // GET: Airports/Delete/5
                public async Task<IActionResult> Delete(string id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var airport = await _context.Airport
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (airport == null)
                    {
                        return NotFound();
                    }

                    return View(airport);
                }

                // POST: Airports/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(string id)
                {
                    var airport = await _context.Airport.FindAsync(id);
                    _context.Airport.Remove(airport);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                private bool AirportExists(string id)
                {
                    return _context.Airport.Any(e => e.Id == id);
                }*/
    }
}
