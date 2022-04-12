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
    public class AircraftsController : Controller
    {
        private readonly GatewayService _gatewayService;

        public AircraftsController(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }



        // GET: Aircrafts
        public async Task<IActionResult> Index()
        {
            return View(await _gatewayService.GetFromJsonAsync<IEnumerable<Aircraft>>("Aircraft/api/Aircrafts"));
        }

        // GET: Aircrafts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aircraft = await _gatewayService.GetFromJsonAsync<Aircraft>("Aircraft/api/Aircrafts/" + id);
            if (aircraft == null)
            {
                return NotFound();
            }

            return View(aircraft);
        }

        // GET: Aircrafts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Aircrafts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Registration,Name,Capacity,Id")] Aircraft aircraft)
        {
            if (ModelState.IsValid)
            {
                await _gatewayService.PostAsync("Aircraft/api/Aircrafts", aircraft);
                return RedirectToAction(nameof(Index));
            }
            return View(aircraft);
        }
        /*
                // GET: Aircrafts/Edit/5
                public async Task<IActionResult> Edit(string id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var aircraft = await _context.Aircraft.FindAsync(id);
                    if (aircraft == null)
                    {
                        return NotFound();
                    }
                    return View(aircraft);
                }

                // POST: Aircrafts/Edit/5
                // To protect from overposting attacks, enable the specific properties you want to bind to.
                // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> Edit(string id, [Bind("Registration,Name,Capacity,Id")] Aircraft aircraft)
                {
                    if (id != aircraft.Id)
                    {
                        return NotFound();
                    }

                    if (ModelState.IsValid)
                    {
                        try
                        {
                            _context.Update(aircraft);
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateConcurrencyException)
                        {
                            if (!AircraftExists(aircraft.Id))
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
                    return View(aircraft);
                }

                // GET: Aircrafts/Delete/5
                public async Task<IActionResult> Delete(string id)
                {
                    if (id == null)
                    {
                        return NotFound();
                    }

                    var aircraft = await _context.Aircraft
                        .FirstOrDefaultAsync(m => m.Id == id);
                    if (aircraft == null)
                    {
                        return NotFound();
                    }

                    return View(aircraft);
                }

                // POST: Aircrafts/Delete/5
                [HttpPost, ActionName("Delete")]
                [ValidateAntiForgeryToken]
                public async Task<IActionResult> DeleteConfirmed(string id)
                {
                    var aircraft = await _context.Aircraft.FindAsync(id);
                    _context.Aircraft.Remove(aircraft);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

                private bool AircraftExists(string id)
                {
                    return _context.Aircraft.Any(e => e.Id == id);
                }*/
    }
}
