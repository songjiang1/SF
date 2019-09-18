using Newtonsoft.Json;
using sys.WebApi.Common;
using sys.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using sys.WebApi;
namespace sys.WebApi.Controllers
{
    public class ProductController : WebApiBaseControllerBase
    {
        [HttpGet]
        [ApiSecurityFilter]
        [AllowAnonymous]
        public HttpResponseMessage GetProduct(string id)
        {
            var product = new Product() { Id = 1, Name = "哇哈哈", Count = 10, Price = 38.8 };
            //ResultMsg resultMsg = null;
            //resultMsg = new ResultMsg();
            //resultMsg.StatusCode = (int)StatusCodeEnum.Success;
            //resultMsg.Msg = StatusCodeEnum.Success.GetEnumText();
            //resultMsg.Data = product;
            //return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
            return  Success(product);
            //return ToJsonResult(product);
        }

        
        [HttpPost]
        [ApiSecurityFilter]
        public HttpResponseMessage addProduct(Product product)
        {
            return Success(product);
            //ResultMsg resultMsg = null;
            //resultMsg = new ResultMsg();
            //resultMsg.StatusCode = (int)StatusCodeEnum.Success;
            //resultMsg.Msg = StatusCodeEnum.Success.GetEnumText();
            //resultMsg.Data = product;
            //return HttpResponseExtension.toJson(JsonConvert.SerializeObject(resultMsg));
        }
    }
}
