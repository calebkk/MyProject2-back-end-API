using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject2.Models;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml.Linq;

namespace MyProject2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly MyDataContext _context;

        public ProductsController(MyDataContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct([FromForm] Product product,List<IFormFile> images)
        {
            if (images != null && images.Count>0 )
            {
                // Read the image data into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await images[0].CopyToAsync(memoryStream);
                    product.PictureData = memoryStream.ToArray();
                }

                // Convert the byte array to a Base64 encoded string
                string base64String = Convert.ToBase64String(product.PictureData);
                // Set the Base64 encoded string to the PictureData property (optional)
                product.PictureBase64String = base64String;
                byte[] pictureData = Convert.FromBase64String(product.PictureBase64String);

                // Assign the byte array to the product's pictureData property
                product.PictureData = pictureData;
            }
            var Product = new Product
            {
                ProductID = product.ProductID,
                Name = product.Name,
                Quantity = product.Quantity,
                Description = product.Description,
                Price = product.Price,               
                PictureData = product.PictureData,
                CategoryID = product.CategoryID
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductID }, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, [FromForm] Product product, List<IFormFile> images)
        {
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            // Get the existing product from the database
            var existingProduct = await _context.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            // Check if an image was uploaded
            if (images != null && images.Count > 0)
            {
                // Read the image data into a byte array
                using (var memoryStream = new MemoryStream())
                {
                    await images[0].CopyToAsync(memoryStream);
                    existingProduct.PictureData = memoryStream.ToArray();
                }
            }

            // Update other properties of the existing product
            existingProduct.Name = product.Name;
            existingProduct.Quantity = product.Quantity;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            // Save the changes to the database
            _context.Entry(existingProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }

    }
}
