using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JMMTaskApi;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using JMMTaskApi.Controllers.Errors;

namespace JMMTaskApi.Controllers
{
    /// <summary>
    ///     CustomersController class contains handlers for handling the Endpoints related to Customer Model 
    /// </summary>
    [Route("v1/api/")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        /// <summary>
        /// Instance Variable _context is used to store dependency injected global database context object for database related operations
        /// </summary>
        private readonly jmmContext _context;

        /// <summary>
        ///  The constructor for this class injected with context variable containing the global
        ///  database context
        /// </summary>
        /// <param name="context">Database Context Object , injected through IOC Container</param>
        public CustomersController(jmmContext context)
        {
            _context = context;
        }

        /// <summary>
        ///     GetCustomers Method Handles the /v1/api/customers route of the api.
        /// </summary>
        /// <returns>Returns all the customers from the database</returns>
        [HttpGet]
        [Route("customers")]
        public async Task<ActionResult<IEnumerable<CustomerDTO>>> GetCustomers()
        {
            //Create a array of CustomerDTO Objects with CId , CName , CAddress , CPhone fields
            var customers = from x in _context.Customer
                                          select new CustomerDTO()
                                          {
                                               CId = x.CId,
                                               CName = x.CName,
                                               CAddress = x.CAddress,
                                               CPhone = x.CPhone
                                          };
            return await customers.ToListAsync();
        }

        /// <summary>
        ///     GetCustomer Method handles the /v1/api/customer route of the api . This method fetches the record of the customer
        ///     whose id is provided in parameter.
        /// </summary>
        /// <param name="c_id">id of the customer you want to fetch record of.id is provided by query parameters</param>
        /// <returns>Returns single customer record of the customer/returns>
        [HttpGet("{c_id}")]
        [Route("customer")]
        public async Task<ActionResult<CustomerDTO>> GetCustomer([FromQuery]int c_id)
        {
            var customer = await _context.Customer.FindAsync(c_id);

            

            if (customer == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }
            //Create CustomerDTO object to send only needed fields
            var customerdto = new CustomerDTO()
            {
                CId = customer.CId,
                CName = customer.CName,
                CAddress = customer.CAddress,
                CPhone = customer.CPhone
            };

            return customerdto;
        }

        /// <summary>
        ///  PostCustomer Method handles the /v1/api/addcustomer route of the api . This method adds the new customer to the database. 
        ///
        /// </summary>
        /// <param name="customer">object containing the details of the new customer being created.</param>
        /// <returns>Returns the created customer record</returns>
        [HttpPost]
        [Route("addcustomer")]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody]Customer customer)
        {
            //Check if Customer already Exists with exact same properties
            bool CustomerExists = await CustomerExistsByObject(customer);

            if (CustomerExists)
            {
                return BadRequest(new ErrorRecordRepeat());
            }

            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CId }, customer);
        }

        /// <summary>
        ///  PostCustomers Method handles the /v1/api/addcustomers route of the api . This method adds the multiple new customer to the database. 
        ///
        /// </summary>
        /// <param name="customers">list of objects containing the details of the new customer being created.</param>
        /// <returns>Returns the records of all customers created</returns>
        [HttpPost]
        [Route("addcustomers")]
        public async Task<ActionResult<IList<Customer>>> PostCustomers([FromBody] IList<Customer> customers)
        {
            //Check if Any of the Customer in the list already Exists with exact same properties
            foreach(Customer customer in customers)
            {
                bool CustomerExists = await CustomerExistsByObject(customer);

                if (CustomerExists)
                {
                    return BadRequest(new ErrorRecordRepeat());
                }
            }
            

            _context.Customer.AddRange(customers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", customers);
        }




        /// <summary>
        /// DeleteCustomer Method handles the /v1/api/deletecustomer route of the api. This method deletes the record of the customer whose id is provided in parameters
        /// </summary>
        /// <param name="id">id of the customer whose record is going to be deleted.id is provided by query parameters</param>
        /// <returns>returns CustomerDTO object containing deleted customer details</returns>
        [HttpDelete("{c_id}")]
        [Route("deletecustomer")]
        public async Task<ActionResult<CustomerDTO>> DeleteCustomer([FromQuery]int c_id)
        {
            var customer = await _context.Customer.FindAsync(c_id);
            if (customer == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            //Create CustomerDTO to return
            var customerdto = new CustomerDTO()
            {
                CId = customer.CId,
                CName = customer.CName,
                CAddress = customer.CAddress,
                CPhone = customer.CPhone
            };
            return customerdto;
        }

        /// <summary>
        ///  This method takes customerid and checks whether customer with id exists in database
        /// </summary>
        /// <param name="cid">customer id of the customer</param>
        /// <returns>Returns true if Record Exists and False if Record Doesnt Exist</returns>
        private bool CustomerExists(int cid)
        {
            return _context.Customer.Any(customer => customer.CId == cid);
        }

        /// <summary>
        ///  This method takes customername and checks whether customer with provided name exists in database
        /// </summary>
        /// <param name="customername">customer name of the customer</param>
        /// <returns>Returns true if Record Exists and False if Record Doesnt Exist</returns>
        private async Task<bool> CustomerExistsByName(string customername)
        {
            return await _context.Customer.AnyAsync(customer => customername == customer.CName);
        }

        /// <summary>
        ///  This method takes customer object from query and checks whether customer exists with this properties.
        ///  Difference between this method and CustomerExists,CustomerExistsByName is that this method matches 
        ///  all properties of the customer to check for equlity while other two only check by matching id and name respectively
        /// </summary>
        /// <param name="customer">customer object from query</param>
        /// <returns>Returns true if Record Exists and False if Record Doesnt Exist</returns>
        private async Task<bool> CustomerExistsByObject(Customer customer)
        {
            return await _context.Customer.AnyAsync<Customer>(c => (c.CName == customer.CName && c.CAddress == customer.CAddress && c.CPhone == customer.CPhone));
        }
    }
}
