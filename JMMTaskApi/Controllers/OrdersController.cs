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
using Newtonsoft.Json.Linq;

namespace JMMTaskApi.Controllers
{
    /// <summary>
    ///  OrdersController class contains handlers for handling the Order related api endpoints
    /// </summary>
    [Route("v1/api")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        /// <summary>
        /// Instance Variable _context is used to store dependency injected global database context object for database related operations
        /// </summary>
        private readonly jmmContext _context;

        /// <summary>
        /// The constructor for this class injected with context variable containing the global
        ///  database context
        /// </summary>
        /// <param name="context">Database Context Object , injected through IOC Container</param>
        public OrdersController(jmmContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  GetOrderBySupplierId Method handles the /v1/api/ordersbysupplierid route of the api .This method fetches all the purchase orders against provided supplier id
        /// </summary>
        /// <param name="s_id">supplier id of the Supplier</param>
        /// <response code="200">Returns Supplier Orders</response>
        /// <response code="404">Supplier Doesnt Exist</response>
        /// <returns>Returns All purchase orders from supplier whose id is provided</returns>

        [HttpGet("{s_id}")]
        [Route("ordersbysupplierid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<IList<OrderSupplierDTO>>> GetOrdersBySupplierId([FromQuery]int s_id)
        {
            var supplierorders = (from supplier in _context.Supplier
                                  join order in _context.Orders
                                  on supplier.SId equals order.SId
                                  where supplier.SId == s_id
                                  select new OrderSupplierDTO()
                                  {
                                      SId = supplier.SId,
                                      SName = supplier.SName,
                                      SAddress = supplier.SAddress,
                                      SPhone = supplier.SPhone,
                                      SIban = supplier.SIban,
                                      OId = order.OId,
                                      ODate = order.ODate,
                                      OType = order.OType,
                                      products = (from o in _context.Orders
                                                  join op in _context.OrderProduct
                                                  on o.OId equals op.OId
                                                  join p in _context.Product
                                                  on op.PId equals p.PId
                                                  where o.OId == order.OId
                                                  select new ProductDTO()
                                                  {
                                                      PCode = p.PCode,
                                                      PName = p.PName,
                                                      PPrice = p.PPrice,
                                                      PStock = op.OpQuantity,
                                                      
                                                  }
                            ).ToList()
                                  }
                                  );
            if (supplierorders == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }
            return await supplierorders.ToListAsync();
        }
        /// <summary>
        ///  GetOrdersByCustomerId Method handles the /v1/api/ordersbycustomerid route of the api. This method fetches all the sales orders 
        ///  against provided customer id
        /// </summary>
        /// <param name="c_id">customer id of the customer</param>
        /// <response code="200">Return Customer Orders</response>
        /// <response code="404">Return Doesnt Exist</response>
        /// <returns>Returns all orders against the provided customer id</returns>
        [HttpGet("{c_id}")]
        [Route("ordersbycustomerid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<OrderCustomerDTO>>> GetOrdersByCustomerId([FromQuery]int c_id)
        {
            var customerorders = (from customer in _context.Customer
                                  join order in _context.Orders
                                  on customer.CId equals order.CId
                                  where customer.CId == c_id
                                  select new OrderCustomerDTO()
                                  {
                                      CId = customer.CId,
                                      CName = customer.CName,
                                      CAddress = customer.CAddress,
                                      CPhone = customer.CPhone,
                                      OId = order.OId,
                                      ODate = order.ODate,
                                      OType = order.OType,
                                      products = (from o in _context.Orders
                                                  join op in _context.OrderProduct
                                                  on o.OId equals op.OId
                                                  join p in _context.Product
                                                  on op.PId equals p.PId
                                                  where o.OId == order.OId
                                                  select new ProductDTO()
                                                  {
                                                      PCode = p.PCode,
                                                      PName = p.PName,
                                                      PPrice = p.PPrice,
                                                      PStock = op.OpQuantity,
                                                      PId = p.PId
                                                  }
                            ).ToList()
                                  }
                                  ); ;
            if(customerorders == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }
            return await customerorders.ToListAsync();
        }

        /// <summary>
        ///  GetProductsFromOrder Method handles the /v1/api/productsfromorder route of the api. This method returns all the products contained in the order
        ///  whose orde id is provided in the parameters
        /// </summary>
        /// <param name="o_id">order id of the order whose products you want to fetch</param>
        /// <response code="200">Return Order Products</response>
        /// <response code="404">Order Doesnt Exist</response>
        /// <returns>Returns all products against order id</returns>
        [HttpGet("{o_id}")]
        [Route("productsfromorder")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IList<OrderProductsDTO>>> GetProductsFromOrder([FromQuery]int o_id)
        {
            var products = (from order in _context.Orders
                            join orderproduct in _context.OrderProduct
                            on order.OId equals orderproduct.OId
                            join product in _context.Product
                            on orderproduct.PId equals product.PId
                            where order.OId == o_id
                            select new OrderProductsDTO()
                            {
                                PCode = product.PCode,
                                PName = product.PName,
                                PPrice = product.PPrice,
                                PQuantity = orderproduct.OpQuantity
                            }
                            );

            if (products == null)
            {
                return NotFound(new ErrorRecordDoesntExist());
            }

            return await products.ToListAsync();
        }

        /// <summary>
        ///  PostOrders Method handles the /v1/api/createorder route of the api. This method creates the order record in the database.
        /// </summary>
        /// <param name="orders">Order detail provided by the Request Body</param>
        /// <response code="201">Order Created</response>
        /// <response code="400">Orders Object is Null / Insufficient Product Stock / A Product Doesn'nt Exist</response>
        /// <returns>Returns newly created order</returns>
        [HttpPost]
        [Route("createorder")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<JObject>> PostOrders([FromBody]JObject orders)
        {
            if(orders == null)
            {
                BadRequest();
            }

            //Get Order Date
            var o_date = Convert.ToDateTime(orders.GetValue("ODate"));
            //Get Order Type
            var o_type = orders.GetValue("OType").ToString();
            //Create Order Object by looking at order type
            Orders order = new Orders()
            {
                ODate = o_date,
                OType = o_type
            };

            //Set Customer id if order type is sale else set Supplier id if purchase
            if(o_type == "purchase")
            {
                order.SId = Convert.ToInt32(orders.GetValue("SId"));

            }
            else if(o_type == "sale")
            {
                order.CId = Convert.ToInt32(orders.GetValue("CId"));
            }

            //Add Order to the Database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            
            //Get newly created order id
            int OId = order.OId;

            //To store total amount of the products
            int total = 0;

            foreach (JToken item in orders.Last.Values())
            {
                OrderProduct orderproduct = new OrderProduct()
                {
                    OpQuantity = item.Value<int>("OpQuantity"),
                    OId = OId,
                    PId = item.Value<int>("PId")
                };

                //If order type is sale then Check if product stock is sufficient
                if (o_type == "sale")
                {
                    if (!IsProductStockSufficient(orderproduct.PId, orderproduct.OpQuantity))
                    {
                        return BadRequest(new ErrorInsufficientStock());
                    }
                }

                //Add Order to OrderProduct Table
                _context.OrderProduct.Add(orderproduct);


                //Get Product Object 
                Product product = _context.Product.FirstOrDefault(p => p.PId == orderproduct.PId);

                //Return Bad Request if any product doesnt exist
                if(product == null)
                {
                    BadRequest();
                }

                //Calculate total
                total += (product.PPrice*orderproduct.OpQuantity);

                //Change State to Modified
                _context.Entry(product).State = EntityState.Modified;

                //Increment or Decrement stock by checking order type
                if (o_type == "purchase")
                {
                    //Increment Product Stock by Quantity
                    
                    if(product != null)
                    {
                        product.PStock += orderproduct.OpQuantity;
                    }
                }
                else if(o_type == "sale")
                {
                    //Decrement Product Stock by Quantity
                    if (product != null)
                    {
                        product.PStock -= orderproduct.OpQuantity;
                    }
                }
            }

            //Update Order Total
            order.OTotalAmount = total;

            //Save Changes to Database
            await _context.SaveChangesAsync();

            //Add total to Returning Object
            orders.Add("OTotal", total);

            return Created("",orders);
        }

        /// <summary>
        ///  Checks If product stock is sufficient to fulfil product quantity
        /// </summary>
        /// <param name="p_id">Product Id</param>
        /// <param name="op_quantity">Product Order Quantity</param>
        /// <returns>true for sufficient and false for insufficient</returns>
        private bool IsProductStockSufficient(int p_id,int op_quantity)
        {
            Product product  = _context.Product.FirstOrDefault(p => (p.PId == p_id));
            
            if(product == null)
            {
                return false;
            }
            
            if(product.PStock >= op_quantity)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        ///  This method checks if any order containing the order id in parameters , exists
        /// </summary>
        /// <param name="id">order id of the record to check</param>
        /// <returns>Return true if record exists and false if record doesnt exist</returns>
        private bool OrdersExists(int id)
        {
            return _context.Orders.Any(e => e.OId == id);
        }
    }
}
