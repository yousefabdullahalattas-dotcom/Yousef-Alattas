using BootCamp_MVC_EF.Dtos;
using BootCamp_MVC_EF.Services.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BootCamp_MVC_EF.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        
        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetAllOrders();

            return View(orders);
        }

        
        public async Task<IActionResult> Create()
        {
            await LoadCustomersAsync();
            await LoadProductsAsync();

            return View(new OrderCreateDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderCreateDto orderDTO)
        {
            if (ModelState.IsValid)
            {
                await _orderService.AddOrder(orderDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadCustomersAsync();
            await LoadProductsAsync();

            return View(orderDTO);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            await LoadCustomersAsync();
            await LoadProductsAsync();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OrderUpdateDto orderDTO)
        {
            if (ModelState.IsValid)
            {
                var existingOrder =
                    await _orderService.GetOrderById(orderDTO.Id);

                if (existingOrder == null)
                {
                    return NotFound();
                }

                await _orderService.UpdateOrder(orderDTO);

                return RedirectToAction(nameof(Index));
            }

            await LoadCustomersAsync();
            await LoadProductsAsync();

            return View(orderDTO);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _orderService.GetOrderById(id.Value);

            if (order == null)
            {
                return NotFound();
            }

            await _orderService.DeleteOrder(id.Value);

            return RedirectToAction(nameof(Index));
        }
        private async Task LoadCustomersAsync()
        {
            var customers = await _orderService.GetAllCustomers();

            ViewBag.Customers = new SelectList(
                customers,
                "Id",
                "CustomerName"
            );
        }

        private async Task LoadProductsAsync()
        {
            var products = await _orderService.GetAllProducts();

            ViewBag.Products = new SelectList(
                products,
                "Id",
                "ProductName"
            );
        }
    }
}
