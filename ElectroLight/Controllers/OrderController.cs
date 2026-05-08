using ElectroLight.Application.Interfaces.Common;
using ElectroLight.Application.Utilies;
using ElectroLight.Domain.Entities;
using ElectroLight.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ElectroLight.Areas.Admin.Controllers
{
    [Authorize(Roles = SD.Role_Admin)]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.Orders.GetAllBetterVersionAsync(
                include: q => q
                    .Include(o => o.OrderItems)
            );

            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _unitOfWork.Orders.GetBetterVersionAsync(
                o => o.Id == id,
                include: q => q
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
            );

            if (order == null)
                return NotFound();

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateStatus(
            int id,
            OrderStatus status)
        {
            var order = await _unitOfWork.Orders.GetAsync(
                o => o.Id == id
            );

            if (order == null)
                return NotFound();


            if (!IsValidStatusTransition(order.Status, status))
            {
                TempData["error"] = "Invalid status transition.";

                return RedirectToAction(
                    nameof(Details),
                    new { id });
            }

            order.Status = status;

            _unitOfWork.Orders.Update(order);

            await _unitOfWork.SaveChangesAsync();

            TempData["success"] =
                "Order status updated successfully.";

            return RedirectToAction(
                nameof(Details),
                new { id });
        }

        
        private bool IsValidStatusTransition(
            OrderStatus currentStatus,
            OrderStatus newStatus)
        {
            return currentStatus switch
            {
                OrderStatus.Pending =>
                    newStatus == OrderStatus.Processing ||
                    newStatus == OrderStatus.Cancelled,

                OrderStatus.Processing =>
                    newStatus == OrderStatus.Shipped,

                OrderStatus.Shipped =>
                    newStatus == OrderStatus.Delivered,

                _ => false
            };
        }
    }
}