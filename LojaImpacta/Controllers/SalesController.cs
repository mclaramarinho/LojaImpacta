using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LojaImpacta.Data;
using LojaImpacta.Models;

namespace LojaImpacta.Controllers
{
    public class SalesController : Controller
    {
        private readonly LojaImpactaContext _context;

        public SalesController(LojaImpactaContext context)
        {
            _context = context;
        }

        // GET: Sales
        public async Task<IActionResult> Index()
        {
            return View(await _context.Sale.ToListAsync());
        }

        // GET: Sales/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .FirstOrDefaultAsync(m => m.SaleID == id);
            if (sale == null)
            {
                return NotFound();
            }
            
            return View(sale);
        }

        // GET: Sales/Create
        public async Task<IActionResult> Create(int id)
        {
            var prod = await _context.Product.FirstOrDefaultAsync(p => p.ProductID == id);

            var c = await _context.User.ToListAsync();
            var clients = c.FindAll(i => i.AccessLevel == 3);
            SelectList clientsList = new(clients, "UserID", "Name");

            var employees = c.FindAll(i => i.AccessLevel == 1 || i.AccessLevel == 2);
            SelectList employeesList = new(employees, "UserID", "Name");


            ViewData["SelectedProductID"] = prod.ProductID;
            ViewData["SelectedProductName"] = prod.ProductName;
            ViewData["SelectedProductQt"] = prod.AmountAvailabel;
            ViewData["Clientes"] = clientsList;
            ViewData["Vendedores"] = employeesList;

            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SaleID,SalesPersonID,ClientID,ProductID,AmountBought")] Sale sale, int ProductID)
        {
            if (ModelState.IsValid)
            {
                var prod = await _context.Product.FindAsync(ProductID);
                prod.AmountAvailabel -= sale.AmountBought;
                _context.Product.Update(prod);

                sale.SaleID = Guid.NewGuid();
                sale.ProductID = ProductID;
                sale.FinalPrice = prod.Price * sale.AmountBought;
                _context.Add(sale);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sale);
        }

        
        // GET: Sales/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sale = await _context.Sale
                .FirstOrDefaultAsync(m => m.SaleID == id);
            if (sale == null)
            {
                return NotFound();
            }

            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var sale = await _context.Sale.FindAsync(id);
            if (sale != null)
            {
                var prod = await _context.Product.FindAsync(sale.ProductID);
                prod.AmountAvailabel += sale.AmountBought;
                _context.Product.Update(prod);

                _context.Sale.Remove(sale);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SaleExists(Guid id)
        {
            return _context.Sale.Any(e => e.SaleID == id);
        }
    }
}
