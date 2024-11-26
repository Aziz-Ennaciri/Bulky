using System.Diagnostics;
using System.Security.Claims;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = _unitOfWork.ProductRepo.GetAll(includeProperties:"Category");
            return View(products);
        }
        public IActionResult Details(int productId)
        {
            ShoppingCart shoppingCart = new()
            {
                product = _unitOfWork.ProductRepo.GetFirstIdOrDefault(u => u.Id == productId, includeProperties: "Category"),
                count = 1,
                ProductId = productId
            };
            return View(shoppingCart);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCart shoppingCart)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCart cartFrmDb = _unitOfWork.ShoppingCartRepo.GetFirstIdOrDefault(u => u.ApplicationUserId == userId && u.ProductId== shoppingCart.ProductId);
            if (cartFrmDb != null) {
                //Shooping cart already exists
                cartFrmDb.count += shoppingCart.count;
                _unitOfWork.ShoppingCartRepo.Update(cartFrmDb);
            }
            else
            {   
                //Add to cart
                _unitOfWork.ShoppingCartRepo.Add(shoppingCart);
            }
            TempData["success"] = "Cart updated successfully";
            
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
