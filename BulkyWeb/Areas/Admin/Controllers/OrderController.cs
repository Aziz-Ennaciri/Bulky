using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using BulkyModels.ViewModels;
using BulkyUtility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public OrderVM OrderVM { get; set; }

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
            OrderVM = new()
            {
                OrderHeader = _unitOfWork.OrderHeaderRepo.GetFirstIdOrDefault(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetailRepo.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };
            return View(OrderVM);
        }

        [HttpPost]
        [Authorize(Roles =StaticDetails.Role_Admin+","+StaticDetails.Role_Employee)]
        public IActionResult UpdateOrderDetail()
        {
            var orderHeaderFrmDb = _unitOfWork.OrderHeaderRepo.GetFirstIdOrDefault(u => u.Id == OrderVM.OrderHeader.Id);

            orderHeaderFrmDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFrmDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFrmDb.StreetAddress = OrderVM.OrderHeader.StreetAddress;
            orderHeaderFrmDb.City = OrderVM.OrderHeader.City;
            orderHeaderFrmDb.State = OrderVM.OrderHeader.State;
            orderHeaderFrmDb.PostalCode = OrderVM.OrderHeader.PostalCode;

            if(!string.IsNullOrEmpty(OrderVM.OrderHeader.Carrier))
            {
                orderHeaderFrmDb.Carrier = OrderVM.OrderHeader.Carrier;
            }
            if (!string.IsNullOrEmpty(OrderVM.OrderHeader.TrackingNumber))
            {
                orderHeaderFrmDb.TrackingNumber = OrderVM.OrderHeader.TrackingNumber;
            }

            _unitOfWork.OrderHeaderRepo.Update(orderHeaderFrmDb);
            _unitOfWork.Save();

            TempData["success"] = "Order Details Updated successfully";

            return RedirectToAction(nameof(Details),new { orderId=orderHeaderFrmDb.Id});
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
