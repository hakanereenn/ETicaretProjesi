using EticaretProjesi.API.DataAccess;
using EticaretProjesi.API.Entities;
using EticaretProjesi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using ETicaretProjesi.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Net.Http;
using PaymentAPI.Core.Model;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EticaretProjesi.MyServices;

namespace EticaretProjesi.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class PaymentController : ControllerBase
    {
        //GetOrCreate : sepet getir veya oluştur
        //AddToCart : Sepete Ürün Ekleme
        private DatabaseContext _db;
        private IConfiguration _configuration;
        public PaymentController(DatabaseContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        [HttpPost("Pay/{cartid}")]
        [ProducesResponseType(200, Type = typeof(Resp<PaymentModel>))]
        [ProducesResponseType(400, Type = typeof(Resp<string>))]
        public IActionResult Pay([FromRoute]int cartId,[FromBody] PayModel model)
        {
            Resp<PaymentModel> result = new Resp<PaymentModel>();
            Cart cart = _db.Carts.Include(x => x.CartProducts).SingleOrDefault(x => x.Id == cartId);
            string paymentApiEndPoint = _configuration["PaymentAPI:EndPoint"]; 
            if (!cart.IsClosed)
            {
                decimal totalPrice = model.TotalPriceOverride ?? cart.CartProducts.Sum(x=> x.Quantity * x.DiscountedPrice);
                HttpClientService client = new HttpClientService(paymentApiEndPoint);
                AuthRequestModel authRequestModel = new AuthRequestModel { UserName = "hakaneren", Password="123123" };
              HttpClientServiceResponse<AuthResponseModel> authResponse =  client.Post<AuthRequestModel, AuthResponseModel>("pay/authenticate",authRequestModel);
                //StringContent content = new StringContent(JsonSerializer.Serialize(authRequestModel), Encoding.UTF8, "application/json");
                //HttpResponseMessage authResponse=  client.PostAsync($"{paymentApiEndPoint}/Pay/Authenticate", content).Result;
                if (authResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
               
                    string token = authResponse.Data.Token; 
                    PaymentRequestModel paymentRequestModel = new PaymentRequestModel
                    {
                        CardNumber = model.CardNumber,
                        CardName = model.CardName,
                        ExpireDate = model.ExpireDate,
                        CVV = model.CVV,
                        TotalPrice = totalPrice,
                    };
                 HttpClientServiceResponse<PaymentResponseModel>  paymentResponse=    client.Post<PaymentRequestModel, PaymentResponseModel>("/pay/payment",paymentRequestModel,token);
                    //StringContent paymentContent = new StringContent(JsonSerializer.Serialize(paymentRequestModel),Encoding.UTF8,"application/json");
                    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme,token) ;
                    // HttpResponseMessage paymentResponse = client.PostAsync($"{paymentApiEndPoint}/pay/payment",paymentContent).Result;
                    if(paymentResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                     
                        if (paymentResponse.Data.Result == "OK")
                        {
                            string transactionId = paymentResponse.Data.TransactionId;
                            Payment payment = new Payment
                            {
                                CartId = cartId,
                                AccountId = cart.AccountId,
                                InvoiceAddress = model.InvoiceAddress,
                                ShippedAddress = model.ShippedAddress,
                                Type = model.Type,
                                TransactionId = transactionId,
                                Date = DateTime.Now,
                                IsCompleted = true,
                                TotalPrice = totalPrice
                            };
                            cart.IsClosed = true;
                            _db.Payments.Add(payment);
                            _db.SaveChanges();
                            PaymentModel data = new PaymentModel
                            {
                                Id = payment.Id,
                                AccountId = payment.AccountId,
                                CartId = payment.CartId,
                                InvoiceAddress= payment.InvoiceAddress,   
                                Date = payment.Date,
                                IsCompleted = payment.IsCompleted,
                                TotalPrice = payment.TotalPrice,
                                ShippedAddress= payment.ShippedAddress,
                               Type = payment.Type
                            };
                            result.Data = data;
                            return Ok(result);
                        }
                        else
                        {
                            Resp<string> paymentOkResult = new Resp<string>();
                            paymentOkResult.AddError("paymentResult","Ödeme Alınamadı");
                            return BadRequest(paymentOkResult);
                        }
                    }
                    else
                    {
                        Resp<string> paymentResult = new Resp<string>();
                        paymentResult.AddError("payment", paymentResponse.ResponseContent);
                        return BadRequest(paymentResult);
                    }
                }
                else
                {
                    Resp<string> authResult = new Resp<string>();
                    authResult.AddError("username", authResponse.ResponseContent);
                    return BadRequest(authResult);
                }
            }
            else
            {
                Payment payment = _db.Payments.SingleOrDefault(x => x.CartId == cartId);

                if (payment == null)
                {
                result.AddError("cart", $"Sepet kapalı ama ödemesi yapılmamış görünmektedir. Olası sorun tespit edildi. Lütfen sistem sağlayıcı ile iletişime geçiniz. Cart Id : {cartId}");
                    return BadRequest(result);
                }
                else
                {
                    PaymentModel data = new PaymentModel
                    {
                        Id = payment.Id,
                        AccountId = payment.AccountId,
                        CartId = payment.CartId,
                        InvoiceAddress = payment.InvoiceAddress,
                        Date = payment.Date,
                        IsCompleted = payment.IsCompleted,
                        TotalPrice = payment.TotalPrice,
                        ShippedAddress = payment.ShippedAddress,
                        Type = payment.Type
                    };
                    result.Data = data;
                    return Ok(result);
                }
            }
        }
    }

}
