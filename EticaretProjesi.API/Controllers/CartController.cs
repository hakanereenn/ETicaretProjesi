using EticaretProjesi.API.DataAccess;
using EticaretProjesi.API.Entities;
using EticaretProjesi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ETicaretProjesi.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EticaretProjesi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class CartController : ControllerBase
    {
        //GetOrCreate : sepet getir veya oluştur
        //AddToCart : Sepete Ürün Ekleme
        private DatabaseContext _db;
        private IConfiguration _configuration;
        public CartController(DatabaseContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }

        [HttpGet("GetOrCreate/{accountId}")]
        [ProducesResponseType(200, Type = typeof(Resp<CartModel>))]
        public IActionResult GetOrCreate([FromRoute] int accountId)
        {
            Resp<CartModel> response = new Resp<CartModel>();
            Cart cart = _db.Carts.Include(x => x.CartProducts).SingleOrDefault(c => c.AccountId == accountId && c.IsClosed == false);
            if (cart == null)
            {
                cart = new Cart()
                {
                    AccountId = accountId,
                    Date = DateTime.Now,
                    IsClosed = false,
                    CartProducts = new List<CartProduct>()
                };
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
            CartModel data = CartToCartModel(cart);
            response.Data = data;
            return Ok(response);
        }

        private static CartModel CartToCartModel(Cart cart)
        {
            CartModel data = new CartModel()
            {
                Id = cart.Id,
                Date = cart.Date,
                IsClosed = cart.IsClosed,
                AccountId = cart.AccountId,
                CartProducts = new List<CartProductModel>()
            };
            foreach (CartProduct cartProduct in cart.CartProducts)
            {
                data.CartProducts.Add(new CartProductModel { Id = cartProduct.Id, CartId = cartProduct.CartId.Value, UnitPrice = cartProduct.UnitPrice, DiscountedPrice = cartProduct.DiscountedPrice, Quantity = cartProduct.Quantity, ProductId = cartProduct.ProductId.Value });
            }

            return data;
        }

        [HttpPost("AddToCart/{accountId}")]
        public IActionResult AddToCart([FromRoute] int accountId, [FromBody] AddToCartModel model)
        {
            Resp<CartModel> response = new Resp<CartModel>();
            Cart cart = _db.Carts.Include(x => x.CartProducts).SingleOrDefault(c => c.AccountId == accountId && c.IsClosed == false);
            if(cart == null)
            {
                cart = new Cart()
                {
                    AccountId = accountId,
                    Date = DateTime.Now,
                    IsClosed = false,
                    CartProducts = new List<CartProduct>()
                };
                _db.Carts.Add(cart);
                _db.SaveChanges();
            }
            Product product = _db.Products.Find(model.ProductId);
            cart.CartProducts.Add(new CartProduct
            {
                CartId= cart.Id,
                ProductId = product.Id,
                UnitPrice = product.UnitPrice,
                DiscountedPrice = product.DiscountedPrice,
                Quantity = model.Quantity
            });

            _db.SaveChanges();
            CartModel data = CartToCartModel(cart);
            response.Data = data;

            return Ok(response);
        }
    }

}
