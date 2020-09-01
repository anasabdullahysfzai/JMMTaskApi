using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JMMTaskApi;
using JMMTaskApi.DTOs;
using JMMTaskApi.Controllers.Errors;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Newtonsoft.Json.Linq;

namespace JMMTaskApi.Controllers
{
    /// <summary>
    ///  ProductsController class contains handlers for handling the Products related api endpoints
    /// </summary>
    [Route("v1/api")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        /// <summary>
        /// Instance Variable _context is used to store dependency injected global database context object for database related operations
        /// </summary>
        private readonly jmmContext _context;

        /// <summary>
        /// The constructor for this class injected with context variable containing the global
        ///  database context
        /// </summary>
        public ProductsController(jmmContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Endpoint to get all the Products in database
        /// </summary>
        /// <returns>returns List of ProductDTO Objects</returns>
        [HttpGet]
        [Route("products")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            //Create the Array of ProductDTO Objects with PId , PName , PCode , PStock fields
            var products = from product in _context.Product
                           select new ProductDTO()
                           {
                               PId = product.PId,
                               PName = product.PName,
                               PCode = product.PCode,
                               PPrice = product.PPrice,
                               PStock = product.PStock
                           };
            //Convert the array of ProductDTO to List and Return
            return await products.ToListAsync();
        }
        /// <summary>
        /// Endpoint to get a single product with matching id
        /// </summary>
        /// <param name="id">id of the product to fetch</param>
        /// <returns>returns ProductDTO Object</returns>

        [HttpGet("{p_id}")]
        [Route("product")]
        public async Task<ActionResult<ProductDTO>> GetProduct([FromQuery] int p_id)
        {
            var product = await _context.Product.FindAsync(p_id);

            if (product == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }

            //Create ProductDTO to send only specific fields
            ProductDTO productdto = new ProductDTO()
            {
                PId = product.PId,
                PName = product.PName,
                PCode = product.PCode,
                PPrice = product.PPrice,
                PStock = product.PStock
            };

            return productdto;
        }

        /// <summary>
        ///  Endpoint to update Product by id in db
        /// </summary>
        /// <param name="id">id of the Product to update</param>
        /// <param name="product">Product object with updated properties</param>
        /// <returns>returns updated Product Object</returns>
        [HttpPut("{p_id}")]
        [Route("editproduct")]
        public async Task<IActionResult> PutProduct([FromQuery]int p_id, Product product)
        {
            if (p_id != product.PId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(p_id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Endpoint to Add single product to database
        /// </summary>
        /// <param name="product">Product Object to add in database</param>
        /// <returns>Return Added Product Object</returns>
        [HttpPost]
        [Route("addproduct")]
        public async Task<ActionResult<Product>> PostProduct([FromBody] Product product)
        {

            bool productexists = await ProductExistsByObject(product);

            if (productexists)
            {
                return BadRequest(new ErrorRecordRepeat());
            }

            _context.Product.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.PId }, product);
        }

        /// <summary>
        ///  Endpoint to Add multiple products to database
        /// </summary>
        /// <param name="products">List of Product Objects</param>
        /// <returns>Returns List of Recently added Product Objects with Ids</returns>
        [HttpPost]
        [Route("addproducts")]
        public async Task<ActionResult<IList<Product>>> PostProduct([FromBody] IList<Product> products)
        {

            foreach(Product product in products)
            {
                bool productexists = await ProductExistsByObject(product);

                if (productexists)
                {
                    return BadRequest(new ErrorRecordRepeat());
                }
            }

            _context.Product.AddRange(products);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", products);
        }

        /// <summary>
        /// Endpoint to delete Product from database
        /// </summary>
        /// <param name="id">id of the Product</param>
        /// <returns>returns Deleted Product Object</returns>
        [HttpDelete("{p_id}")]
        [Route("deleteproduct")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct([FromQuery]int p_id)
        {
            var product = await _context.Product.FindAsync(p_id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            await _context.SaveChangesAsync();

            //Create ProductDTO to send only specific fields
            ProductDTO productdto = new ProductDTO()
            {
                PId = product.PId,
                PName = product.PName,
                PCode = product.PCode,
                PPrice = product.PPrice,
                PStock = product.PStock
            };

            return productdto;
        }
        /// <summary>
        ///  This method checks if Product exists in Database by Id
        /// </summary>
        /// <param name="id">id of the Product</param>
        /// <returns>returns deleted Product Object</returns>
        private bool ProductExists(int id)
        {
            return  _context.Product.Any(product => id == product.PId);
        }

        /// <summary>
        /// This method asynchronously checks if Product exists in Database by ID
        /// </summary>
        /// <param name="id">id of the Product</param>
        /// <returns>returns true or false</returns>
        private async Task<bool> ProductExistsAsync(int id)
        {
            return await _context.Product.AnyAsync(product => id == product.PId );
        }

        /// <summary>
        /// This method checks if Product exists in Database by productname
        /// </summary>
        /// <param name="productname">productname of the Product</param>
        /// <returns>returns true or false</returns>
        private async Task<bool> ProductExistsByName(string productname)
        {
            return await _context.Product.AnyAsync(product => productname == product.PName);
        }
        /// <summary>
        /// This method checks if Product exists in Database by matching Properties of Product Object in Parameters
        /// </summary>
        /// <param name="product">Product Object to match Properties with/param>
        /// <returns>returns true or false</returns>
        private async Task<bool> ProductExistsByObject(Product product)
        {
            return await _context.Product.AnyAsync(p => (p.PCode == product.PCode && p.PName == product.PName && p.PPrice == product.PPrice));
        }
    }
}
