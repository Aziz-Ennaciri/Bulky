using System.Security.Claims;
using BulkyDataAccess.Repository.Interf;
using BulkyModels.Models;
using BulkyModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Customer.Controllers
{
    [Area("customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartVM ShoppingCartVM { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            ShoppingCartVM = new()
            {
                ShoppingCartList = _unitOfWork.ShoppingCartRepo.GetAll(u => u.ApplicationUserId == userId, includeProperties: "product"),
            };

            foreach (var cart in ShoppingCartVM.ShoppingCartList) { 
                cart.price = GetPriceBasedOnQuantity(cart);
                ShoppingCartVM.OrderTotal += (cart.price * cart.count);
            }

            return View(ShoppingCartVM);
        }


        public IActionResult Plus(int cartId)
        {
            var cartFrmDb = _unitOfWork.ShoppingCartRepo.GetFirstIdOrDefault(u => u.Id == cartId);
            if (cartFrmDb != null)
            {
                cartFrmDb.count += 1;  
                _unitOfWork.ShoppingCartRepo.Update(cartFrmDb);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cartFrmDb = _unitOfWork.ShoppingCartRepo.GetFirstIdOrDefault(u => u.Id == cartId);
            if (cartFrmDb != null)
            {
                if (cartFrmDb.count > 1)
                {
                    cartFrmDb.count -= 1;  
                    _unitOfWork.ShoppingCartRepo.Update(cartFrmDb);
                }
                else
                {
                    _unitOfWork.ShoppingCartRepo.Remove(cartFrmDb);  
                }
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cartFrmDb = _unitOfWork.ShoppingCartRepo.GetFirstIdOrDefault(u => u.Id == cartId);
            if (cartFrmDb != null)
            {
                _unitOfWork.ShoppingCartRepo.Remove(cartFrmDb);  // Remove the cart item completely
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            return View();
        }


        private double GetPriceBasedOnQuantity(ShoppingCart shoppingCart)
        {
            if (shoppingCart.count <= 50)
            {
                return shoppingCart.product.Price;
            }
            else
            {
                if (shoppingCart.count <= 100)
                {
                    return shoppingCart.product.Price50;
                }
                else
                {
                    return shoppingCart.product.Price100;
                }
            }
        }
    }
}
