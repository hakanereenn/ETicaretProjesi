using EticaretProjesi.API.DataAccess;
using EticaretProjesi.API.Entities;
using EticaretProjesi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using ETicaretProjesi.Core.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace EticaretProjesi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private DatabaseContext _db;
        private IConfiguration _configuration;
        public CategoryController(DatabaseContext databaseContext, IConfiguration configuration)
        {
            _db = databaseContext;
            _configuration = configuration;
        }

        [HttpPost("create")]
        [ProducesResponseType(200, Type = typeof(Resp<CategoryModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<CategoryModel>))]

        public IActionResult Create([FromBody] CategoryCreateModel model)
        {
            Resp<CategoryModel> response = new Resp<CategoryModel>();
            //if (ModelState.IsValid)
            //{
            string categoryName = model.Name?.Trim().ToLower();
            if (_db.Categories.Any(x => x.Name.ToLower() == categoryName))
            {
                response.AddError(nameof(model.Name), "Bu kategori adı zaten kullanılıyor");
                return BadRequest(response);
            }
            else
            {
                Category category = new Category()
                {
                    Name = model.Name,
                    Description = model.Description
                };
                _db.Categories.Add(category);
                _db.SaveChanges();
                CategoryModel categoryModel = new CategoryModel()
                {
                    Id = category.Id,
                    Name = category.Name,
                    Description = category.Description,
                };
                response.Data = categoryModel;
                return Ok(response);
            }

        }

        [HttpGet("list")]
        [ProducesResponseType(200, Type = typeof(Resp<List<CategoryModel>>))]

        public IActionResult List()
        {
            Resp<List<CategoryModel>> response = new Resp<List<CategoryModel>>();   
       List<CategoryModel> list=    _db.Categories.Select(X=> new CategoryModel { Id = X.Id , Name=X.Name, Description= X.Description}).ToList();
            response.Data = list;   
            return Ok(response);
        }
        [HttpGet("get/{id}")]
        [ProducesResponseType(200, Type = typeof(Resp<CategoryModel>))]
        [ProducesResponseType(404, Type = typeof(Resp<CategoryModel>))]

        public IActionResult GetById([FromRoute] int id )
        {
            Resp<CategoryModel> response = new Resp<CategoryModel>();
            Category category = _db.Categories.SingleOrDefault(x=> x.Id == id);
            CategoryModel data = null;
            if (category == null)
            {
                response.AddError("*", "Kategori Id'si bulunamadı");
                return NotFound(response);
            }
                data = new CategoryModel { Id = category.Id, Name = category.Name, Description = category.Description };

            response.Data = data;
            return Ok(response);
        }

        [HttpPut("update/{id}")]
        [ProducesResponseType(200, Type = typeof(Resp<CategoryModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<CategoryModel>))]
        [ProducesResponseType(404, Type = typeof(Resp<CategoryModel>))]

        public IActionResult Update([FromRoute] int id, [FromBody] CategoryUpdateModel model)
        {
            Resp<CategoryModel> response = new Resp<CategoryModel>();
            Category category = _db.Categories.Find(id);
            CategoryModel data = null;
            if (category == null)
            {
                response.AddError("*","Kategori Id'si bulunamadı");
                return NotFound(response);
            }
            string categoryName = model.Name?.Trim().ToLower();
             if (_db.Categories.Any(x => x.Name.ToLower() == categoryName && x.Id != id))
            {
                response.AddError(nameof(model.Name), "Bu kategori adı zaten kullanılıyor");
                return BadRequest(response);
            }
            category.Name = model.Name;
            category.Description = model.Description;
            _db.SaveChanges();
            data = new CategoryModel { Id = category.Id, Name = category.Name, Description = category.Description };

            response.Data = data;
            return Ok(response);
        }
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(200, Type = typeof(Resp<bool>))]
        [ProducesResponseType(404, Type = typeof(Resp<bool>))]

        public IActionResult Delete([FromRoute] int id)
        {
            Resp<bool> response = new Resp<bool>();
            Category category = _db.Categories.Find(id);
            if (category == null)
            {
                response.AddError("*", "Kategori Id'si bulunamadı");
             return  NotFound(response);
            }
            _db.Categories.Remove(category);
            _db.SaveChanges();
            response.Data = true;
            return Ok(response);
        }
    }

}
