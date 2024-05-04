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
    public class ProductsController : Controller
    {
        private readonly LojaImpactaContext _context;

        public ProductsController(LojaImpactaContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {   
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products
        public async Task<IActionResult> IndexFilter(string prodName, int priceRange, string? availableOnly)
        {
            Console.WriteLine($"{prodName}, {priceRange}, {availableOnly}");
            List<Product> allProducts = await _context.Product.ToListAsync();
            ViewData["ProdName"] = prodName;
            ViewData["PriceRange"] = priceRange;
            ViewData["AvailableOnly"] = false;

            if(prodName != null)
            {
                allProducts = allProducts.FindAll(i => i.ProductName.ToUpper().Contains(prodName.ToUpper()));
                ViewData["ProdName"] = prodName;
            }   

            if(priceRange != 0) { 
                ViewData["PriceRange"] = priceRange;
                //1 - priceRange > 1 to 500
                if(priceRange == 1)
                {
                    allProducts = allProducts.FindAll(i => i.Price >= 1 && i.Price <= 500);
                }else if(priceRange == 2)
                //2 - priceRange > 500 to 1500
                {
                    allProducts = allProducts.FindAll(i => i.Price >= 501 && i.Price <= 1500);
                }
                else if(priceRange == 3)
                //3 - priceRange > 1500+
                {
                    allProducts = allProducts.FindAll(i => i.Price >= 1501);
                }
            }
            if(availableOnly == "on")
            {
                ViewData["AvailableOnly"] = true;
                allProducts = allProducts.FindAll(i => i.AmountAvailabel > 0);
            }
            return View(nameof(Index), allProducts);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,ImageUrl,Price,Description,AmountAvailabel")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,ImageUrl,Price,Description,AmountAvailabel")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductID == id);
        }
    }
}
