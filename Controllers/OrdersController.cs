using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using OrderRepository.Repository;

namespace OrderManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IRepository _repository;
        public OrdersController(IRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
        {
            List<Order> orders = new List<Order>();
            
            try
            {
                orders.AddRange((List<Order>)await _repository.GetOrders());
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

            return orders;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            try
            {
                var order = await _repository.GetOrder(id);
                if (order == null)
                {
                    return NotFound();
                }
                return order;
            }
            catch (Exception)
            {
                return StatusCode(500);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            try
            {
                var newOrder = await _repository.CreateOrder(order);
                return CreatedAtAction(nameof(GetOrder), new { id = newOrder.Id }, newOrder);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Order>> UpdateOrder(Order order)
        {
            try
            {
                var updatedOrder = await _repository.UpdateOrder(order);
                return Ok(updatedOrder);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrder(int id)
        {
            try
            {
                await _repository.DeleteOrder(id);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
