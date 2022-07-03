using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaymentAPI.Core.Model;
using EticaretProjesi.MyServices;
using System;
using System.Security.Claims;
using System.Collections.Generic;

namespace PaymentAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class PayController : ControllerBase
    {
        private IConfiguration _configuration;
        public PayController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpPost("authenticate")]
        [ProducesResponseType(200,Type = typeof(AuthResponseModel))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult Authenticate([FromBody] AuthRequestModel model)
        {
            string uid = _configuration["Auth:Uid"] ;
            string pass = _configuration["Auth:Pass"];
            if (model.UserName == uid && model.Password == pass)
            {
              List<Claim> claims = new List<Claim>();   
                claims.Add(new Claim("uid", uid));
                string token =    TokenServices.GenerateToken(_configuration["JwtOptions:Key"],
                    DateTime.Now.AddDays(30),
                    claims,
                    _configuration["JwtOptions:Issuer"],
                    _configuration["JwtOptions:Audience"]) ;
                return Ok(new AuthResponseModel { Token = token });
            }
            else
                 return BadRequest("Kullanıcı Adı Şifre Eşleşmiyor");
        }
        [HttpPost("payment")]
        [ProducesResponseType(200, Type = typeof(PaymentResponseModel))]
        [ProducesResponseType(400, Type = typeof(string))]
        public IActionResult Payment([FromBody] PaymentRequestModel model)
        {
            string cardNo = _configuration["CardTest:No"] ;
            string cardName = _configuration["CardTest:Name"];
            string exp = _configuration["CardTest:Exp"];
            string cvv = _configuration["CardTest:CVV"];
            if (model.CardName== cardName && model.CardNumber == cardNo && exp == model.ExpireDate && cvv == model.CVV )
                return Ok(new PaymentResponseModel { Result="OK" , TransactionId = Guid.NewGuid().ToString()});
            else
                return BadRequest("Kart bilgileri geçersiz. Ödeme alınamadı."); 
        }

        }
}
