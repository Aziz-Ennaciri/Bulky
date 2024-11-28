using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using BulkyModels.ViewModels;
using BulkyUtility;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int orderId)
        {
            OrderVM orderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeaderRepo.GetFirstIdOrDefault(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetailRepo.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };
            return View(orderVM);
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeader> objOrderHeaders = _unitOfWork.OrderHeaderRepo.GetAll(includeProperties: "ApplicationUser").ToList();


            switch (status)
            {
                case "pending":
                    objOrderHeaders = objOrderHeaders.Where(u => u.PaymentStatus==StaticDetails.PaymentStatusDelayedPayement);
                    break;
                case "inprocess":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StaticDetails.StatusInProcess);
                    break;
                case "completed":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StaticDetails.StatusShipped);
                    break;
                case "approved":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == StaticDetails.StatusApproved);
                    break;
                default:
                    break;
            }


            return Json(new { data = objOrderHeaders });
        }

        #endregion
    }
}
