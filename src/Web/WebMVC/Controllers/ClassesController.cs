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
    public class ClassesController : Controller
    {
        private readonly GatewayService _gatewayService;

        public ClassesController(GatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }


        // GET: Classes
        public async Task<IActionResult> Index()
        {
            return View(await _gatewayService.GetFromJsonAsync<IEnumerable<Class>>("BasePrice/api/Classes"));
        }

        // GET: Classes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await GetClass(id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Value,Id")] Class @class)
        {
            if (ModelState.IsValid)
            {
                await _gatewayService.PostAsync("BasePrice/api/Classes", @class);
                return RedirectToAction(nameof(Index));
            }
            return View(@class);
        }
        
        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await GetClass(id);
            if (@class == null)
            {
                return NotFound();
            }
            return View(@class);
        }

        // POST: Classes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Description,Value,Id")] Class @class)
        {
            if (id != @class.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _gatewayService.PutAsync("BasePrice/api/Classes", @class);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await GetClass(id) == null)
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
            return View(@class);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @class = await GetClass(id);
            if (@class == null)
            {
                return NotFound();
            }

            return View(@class);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _gatewayService.DeleteAsync("BasePrice/api/Classes/" + id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<Class> GetClass(string id)
        {
            return await _gatewayService.GetFromJsonAsync<Class>("BasePrice/api/Classes/" + id);
        }
    }
}
