using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JMMTaskApi;
using JMMTaskApi.Controllers.Errors;
using Newtonsoft.Json.Linq;

namespace JMMTaskApi.Controllers
{
    /// <summary>
    ///  SupplierController class contains handlers for handling the Supplier related api endpoints
    /// </summary>
    [Route("v1/api/")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        /// <summary>
        /// Instance Variable _context is used to store dependency injected global database context object for database related operations
        /// </summary>
        private readonly jmmContext _context;

        /// <summary>
        /// The constructor for this class injected with context variable containing the global
        ///  database context
        /// </summary>
        public SuppliersController(jmmContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  Endpoint to get all the suppliers from database
        /// </summary>
        /// <returns>List of Supplies</returns>
        [HttpGet]
        [Route("suppliers")]
        public async Task<ActionResult<IEnumerable<SupplierDTO>>> GetSuppliers()
        {
            var suppliers = from supplier in _context.Supplier
                            select new SupplierDTO()
                            {
                                SId = supplier.SId,
                                SName = supplier.SName,
                                SAddress = supplier.SAddress,
                                SPhone = supplier.SPhone,
                                SIban = supplier.SIban
                            };
            return await suppliers.ToListAsync();
        }

        /// <summary>
        /// Endpoint to get single supplier object from the database
        /// </summary>
        /// <param name="id">id of supplier to fetch</param>
        /// <returns>Supplier Object</returns>
        [HttpGet("{s_id}")]
        [Route("supplier")]
        public async Task<ActionResult<SupplierDTO>> GetSupplier([FromQuery]int s_id)
        {
            var supplier = await _context.Supplier.FindAsync(s_id);

            if (supplier == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }

            SupplierDTO supplierdto = new SupplierDTO()
            {
                SId = supplier.SId,
                SName = supplier.SName,
                SAddress = supplier.SAddress,
                SPhone = supplier.SPhone,
                SIban = supplier.SIban
            };

            return supplierdto;
        }

        /// <summary>
        /// Endpoint to add multiple suppliers to database
        /// </summary>
        /// <returns>List of All Added Supplier Objects with IDs</returns>
        [HttpPost]
        [Route("addsuppliers")]
        public async Task<ActionResult<IList<Supplier>>> PostSupplier([FromBody]IList<Supplier> suppliers)
        {
            foreach (Supplier supplier in suppliers)
            {
                bool supplierexists = await SupplierExistsByObject(supplier);

                if (supplierexists)
                {
                    return BadRequest(new ErrorRecordRepeat());
                }
            }

            _context.Supplier.AddRange(suppliers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupplier", suppliers);
        }

        /// <summary>
        /// Endpoint to delete specific supplier by id
        /// </summary>
        /// <param name="id">id of supplier</param>
        /// <returns>returns deleted Supplie Object</returns>
        [HttpDelete("{s_id}")]
        [Route("deletesupplier")]
        public async Task<ActionResult<SupplierDTO>> DeleteSupplier([FromQuery]int s_id)
        {
            var supplier = await _context.Supplier.FindAsync(s_id);
            if (supplier == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }

            _context.Supplier.Remove(supplier);
            await _context.SaveChangesAsync();

            //Create SupplierDTO Object to send specific fields only
            SupplierDTO supplierdto = new SupplierDTO()
            {
                SId = supplier.SId,
                SName = supplier.SName,
                SAddress = supplier.SAddress,
                SPhone = supplier.SPhone,
                SIban = supplier.SIban
            };

            return supplierdto;
        }

        /// <summary>
        /// Checks if supplier exists by supplier object. This method checks properties of provided objects against DB Records
        /// </summary>
        /// <param name="supplier">Supplier Object</param>
        /// <returns>returns true or false</returns>
        private async Task<bool> SupplierExistsByObject(Supplier supplier)
        {
            return await _context.Supplier.AnyAsync(s => (s.SName == supplier.SName && s.SPhone == supplier.SPhone && s.SAddress == supplier.SAddress && s.SIban == supplier.SIban) );
        }

    }
}
