using EticaretProjesi.API.DataAccess;
using EticaretProjesi.API.Entities;
using EticaretProjesi.MyServices;
using EticaretProjesi.Core;
using EticaretProjesi.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace EticaretProjesi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles= "Admin")]
    public class AccountController : ControllerBase
    {
        //Applyment : Satıcı Başvuru
        //Register : Üye Kaydı
        //Authenticate : Kimlik Doğrulama
        private DatabaseContext _db;
        private IConfiguration _configuration; 
        public AccountController(DatabaseContext databaseContext , IConfiguration configuration)
        {
            _db = databaseContext;
            _configuration = configuration;
        }
        [HttpPost("merchant/applyment")]
        [ProducesResponseType(200,Type = typeof(Resp<ApplymentAccountResponseModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<ApplymentAccountResponseModel>))]

        public IActionResult Applyment([FromBody] ApplymentAccountRequestModel model)
        {                          
            Resp<ApplymentAccountResponseModel> response = new Resp<ApplymentAccountResponseModel>();
            //if (ModelState.IsValid)
            //{
                model.UserName = model.UserName?.Trim().ToLower();
                if (_db.Accounts.Any(x=> x.UserName.ToLower() ==  model.UserName))
                {
                response.AddError(nameof(model.UserName), "Bu kullanıcı adı zaten kullanılıyor");
                return BadRequest(response);
                }
                else
                {
                    Account account = new Account() {
                        UserName = model.UserName,
                        Password = model.Password,
                        CompanyName = model.CompanyName,
                        ContactEmail = model.ContactEmail,
                        ContactName = model.ContactName,
                        Type = AccountType.Merchant,
                        IsApplyment = true
                    };
                    _db.Accounts.Add(account);
                    _db.SaveChanges();
                    ApplymentAccountResponseModel applymentAccountResponseModel = new ApplymentAccountResponseModel()
                    {
                        Id = account.Id,
                        UserName = account.UserName,
                        CompanyName = account.CompanyName,
                        ContactEmail = account.ContactEmail,
                        ContactName = account.ContactName,

                    };
                response.Data = applymentAccountResponseModel;
                    return Ok(response);
                }
            //}
            //List<string> errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList();
            //return BadRequest(errors);
        }
        [HttpPost("register")]
        [ProducesResponseType(200, Type = typeof(Resp<RegisterResponseModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<RegisterResponseModel>))]
        public IActionResult Register([FromBody] RegisterRequestModel model)
        {
            Resp<RegisterResponseModel> response = new Resp<RegisterResponseModel>();
            model.UserName = model.UserName.Trim().ToLower();
            if (_db.Accounts.Any(x=> x.UserName.ToLower() == model.UserName))
            {
                response.AddError(nameof(model.UserName), "Bu kullanıcı adı zaten kullanılıyor");
                return BadRequest(response);
            }
            else
            {
                Account account = new Account()
                {
                    UserName = model.UserName,
                    Password = model.Password,
                    Type = AccountType.Member,
                };
                _db.Accounts.Add(account);
                _db.SaveChanges();
                RegisterResponseModel registerResponseModel = new RegisterResponseModel()
                {
                    Id = account.Id,
                    UserName = account.UserName,
                };
                response.Data = registerResponseModel;
                return Ok(response);
            }
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200, Type = typeof(Resp<AuthenticateResponseModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<AuthenticateResponseModel>))]
        public IActionResult Authenticate([FromBody] AuthenticateRequestModel model) {
            Resp<AuthenticateResponseModel> response = new Resp<AuthenticateResponseModel>();
            model.UserName= model.UserName?.Trim().ToLower();
            Account account = _db.Accounts.SingleOrDefault(x=> x.UserName.ToLower() == model.UserName && x.Password == model.Password);
            if (account != null)
            {
                if (account.IsApplyment)
                {
                    response.AddError("*", "Henüz Satıcı başvurunuz tamamlanmamıştır.");
                    return BadRequest(response);
                }
                else
                {
                    //Token oluşturulacak
                    string key = _configuration["JwtOptions:Key"];
                    List<Claim> claims = new List<Claim>()
                    {
                        new Claim("id", account.Id.ToString()),
                        new Claim("type",((int) account.Type).ToString()),
                        new Claim(ClaimTypes.Name, account.UserName),
                        new Claim(ClaimTypes.Role, account.Type.ToString()),
                    };
                    DateTime expires= DateTime.Now.AddDays(30);
                    string token = TokenServices.GenerateToken(key,expires,claims);
                    AuthenticateResponseModel data = new AuthenticateResponseModel() { Token = token };
                    response.Data = data;
                    return Ok(response);

                }
            }
            else {
                response.AddError("*","Kullanıcı Adı ya da Şifre eşleşmiyor.");
            return BadRequest(response);
            }

        }

      
    }

}
