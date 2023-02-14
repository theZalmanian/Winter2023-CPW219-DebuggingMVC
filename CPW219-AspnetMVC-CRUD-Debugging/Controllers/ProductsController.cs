using CPW219_AspnetMVC_CRUD_Debugging.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CPW219_AspnetMVC_CRUD_Debugging.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            // If all data is valid
            if (ModelState.IsValid)
            {
                // Prepare INSERT Statement
                _context.Product.AddAsync(product);

                // Execute query asynchronously
                await _context.SaveChangesAsync();

                // Prepare success message
                TempData["Message"] = $"{product.Name} was created successfully";

                // Send them back to the Product catalog
                return RedirectToAction("Index");
            }
            
            // If all Product data not valid
            return View(product);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Update(product);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            // Get the specified Product from the DB using it's ID
            Product? currProduct = await _context.Product.FindAsync(id);

            // If the specified product is null
            if (currProduct == null)
            {
                // Display 404 error
                return NotFound();
            }

            // Otherwise display Product information
            return View(currProduct);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Get the specified Product from the DB using it's ID
            Product? currProduct = await _context.Product.FindAsync(id);

            // If the specified product is not null
            if (currProduct != null)
            {
                // Prepare DELETE Statement
                _context.Product.Remove(currProduct);

                // Execute query asynchronously
                await _context.SaveChangesAsync();

                // Prepare success message
                TempData["Message"] = $"{currProduct.Name} was deleted successfully";

                // Send them back to the Product catalog
                return RedirectToAction("Index");
            }

            // Otherwise, prepare error message
            TempData["Message"] = "This product has already been deleted";

            // Send them back to the Product catalog
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.ProductId == id);
        }
    }
}
