using EticaretProjesi.API.DataAccess;
using EticaretProjesi.API.Entities;
using EticaretProjesi.Core;
using ETicaretProjesi.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace EticaretProjesi.API.Controllers
{
    [Route("[controller]")]
    //[Authorize(Roles = "Admin,Merchant")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private DatabaseContext _db;
        private IConfiguration _configuration;
        public ProductController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _db = databaseContext;
            _configuration = configuration;
        }

        [HttpPost("create")]
        [ProducesResponseType(200, Type = typeof(Resp<ProductModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<ProductModel>))]

        public IActionResult Create([FromBody] ProductCreateModel model)
        {
            Resp<ProductModel> response = new Resp<ProductModel>();

            string productName = model.Name?.Trim().ToLower();
            if (_db.Products.Any(x => x.Name.ToLower() == productName))
            {
                response.AddError(nameof(model.Name), "Bu ürün adı zaten kullanılıyor");
                return BadRequest(response);
            }
            else
            {
               int accountId= int.Parse(HttpContext.User.FindFirst("id").Value);
                Product product = new Product()
                {
                    Name = model.Name,
                    Description = model.Description,
                    UnitPrice = model.UnitPrice,
                    Discontiuned = model.Discontiuned,
                    DiscountedPrice = model.UnitPrice,
                    CategoryId = model.CategoryId,
                    AccountId = accountId
                };
                _db.Products.Add(product);
                _db.SaveChanges();
                product = _db.Products.Include(x=> x.Category).Include(x=> x.Account).SingleOrDefault(x => x.Id == product.Id);
                ProductModel data = new ProductModel()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    UnitPrice = product.UnitPrice,
                    Discontiuned = product.Discontiuned,
                    DiscountedPrice = product.UnitPrice,
                    AccountId = product.AccountId,
                    CategoryId = product.CategoryId,
                    CategoryName=product.Category.Name,
                    AccountCompanyName=product.Account.CompanyName
                };
                
                response.Data = data;
                return Ok(response);
            }

        }

        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(Resp<List<ProductModel>>))]

        public IActionResult List()
        {
            Resp<List<ProductModel>> response = new Resp<List<ProductModel>>();
            int accountId = int.Parse(HttpContext.User.FindFirst("id").Value);
            List<ProductModel> list = _db.Products.Where(x=> x.AccountId == accountId).Include(x => x.Category).Include(x => x.Account).Select(X => new ProductModel { Id = X.Id, Name = X.Name, Description = X.Description,
                UnitPrice = X.UnitPrice,
                Discontiuned = X.Discontiuned,
                DiscountedPrice = X.UnitPrice,
                AccountId = X.AccountId,
                CategoryId = X.CategoryId,
                CategoryName = X.Category.Name,
                AccountCompanyName = X.Account.CompanyName
            }).ToList();
            response.Data = list;
            return Ok(response);
        }
        [HttpGet("list/{accountId}")]
        [ProducesResponseType(200, Type = typeof(Resp<List<ProductModel>>))]
        public IActionResult ListByAccountId([FromRoute] int accountId)
        {
            Resp<List<ProductModel>> response = new Resp<List<ProductModel>>();
            List<ProductModel> products = _db.Products.Where(x => x.AccountId == accountId).Include(x => x.Category).Include(x => x.Account).Select(X => new ProductModel
            {
                Id = X.Id,
                Name = X.Name,
                Description = X.Description,
                UnitPrice = X.UnitPrice,
                Discontiuned = X.Discontiuned,
                DiscountedPrice = X.UnitPrice,
                AccountId = X.AccountId,
                CategoryId = X.CategoryId,
                CategoryName = X.Category.Name,
                AccountCompanyName = X.Account.CompanyName
            }).ToList();
           

            response.Data = products;
            return Ok(response);
        }
        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(Resp<ProductModel>))]
        [ProducesResponseType(404, Type = typeof(Resp<ProductModel>))]

        public IActionResult GetById([FromRoute] int id)
        {
            Resp<ProductModel> response = new Resp<ProductModel>();
            int accountId = int.Parse(HttpContext.User.FindFirst("id").Value);
            Product product = _db.Products.SingleOrDefault(x => x.Id == id && x.AccountId == accountId);
            ProductModel data = null;
            if (product == null)
            {
                response.AddError("*", "Urun Id'si bulunamadı");
                return NotFound(response);
            }
            data = new ProductModel { Id = product.Id, Name = product.Name, Description = product.Description,
                UnitPrice = product.UnitPrice,
                Discontiuned = product.Discontiuned,
                DiscountedPrice = product.UnitPrice,
                AccountId = product.AccountId,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                AccountCompanyName = product.Account.CompanyName
            };

            response.Data = data;
            return Ok(response);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(200, Type = typeof(Resp<ProductModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<ProductModel>))]
        [ProducesResponseType(404, Type = typeof(Resp<ProductModel>))]

        public IActionResult Update([FromRoute] int id, [FromBody] ProductUpdateModel model)
        {
            Resp<ProductModel> response = new Resp<ProductModel>();
            int accountId = int.Parse(HttpContext.User.FindFirst("id").Value);
            string role = HttpContext.User.FindFirst(ClaimTypes.Role).Value;

            Product product = _db.Products.SingleOrDefault(x=> x.Id == id && (role =="Admin" || (x.AccountId == accountId && role != "Admin")));
            ProductModel data = null;
            if (product == null)
            {
                response.AddError("*", "Ürün Id'si bulunamadı");
                return NotFound(response);
            }
            string productName = model.Name?.Trim().ToLower();
            if (_db.Products.Any(x => x.Name.ToLower() == productName && x.Id != id ))
            {
                response.AddError(nameof(model.Name), "Bu Ürün adı zaten kullanılıyor");
                return BadRequest(response);
            }
            product.Name = model.Name;
            product.Description = model.Description;
            product.UnitPrice = model.UnitPrice ;
            product.Discontiuned = model.Discontiuned;
            product.DiscountedPrice = model.DiscountedPrice;              
            product.CategoryId = model.CategoryId;

            _db.SaveChanges();
            product = _db.Products.Include(x=> x.Category).Include(y=> y.Account).SingleOrDefault(x=> x.Id == id);
            data = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                UnitPrice = product.UnitPrice,
                Discontiuned = product.Discontiuned,
                DiscountedPrice = product.UnitPrice,
                AccountId = product.AccountId,
                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                AccountCompanyName = product.Account.CompanyName
            };

            response.Data = data;
            return Ok(response);
        }

        [HttpDelete("delete/{id}")]
        [ProducesResponseType(200, Type = typeof(Resp<ProductModel>))]
        [ProducesResponseType(404, Type = typeof(Resp<ProductModel>))]

        public IActionResult Delete([FromRoute] int id)
        {
            Resp<ProductModel> response = new Resp<ProductModel>();
            int accountId = int.Parse(HttpContext.User.FindFirst("id").Value);
            string role = HttpContext.User.FindFirst(ClaimTypes.Role).Value;

            Product product = _db.Products.SingleOrDefault(x => x.Id == id && (role == "Admin" || (x.AccountId == accountId && role != "Admin")));
            if (product == null)
            {
                response.AddError("*", "Ürün Id'si bulunamadı");
                return NotFound(response);
            }
            _db.Products.Remove(product);
            _db.SaveChanges();
            return Ok(response);
        }
    }
}
