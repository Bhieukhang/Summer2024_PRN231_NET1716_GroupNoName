using JewelrySalesSystem_NoName_BE.ZaloPayHelper.Crypto;
using JewelrySalesSystem_NoName_BE.ZaloPayHelper;
using JSS_BusinessObjects.ZaloPay.Config;
using JSS_BusinessObjects.ZaloPay.Request;
using JSS_BusinessObjects.ZaloPay.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using JSS_BusinessObjects.DTO;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly string app_id = "2553";
        private readonly string key1 = "PcY4iZIKFCIdgZvA6ueMcMHHUbRLYjPL";
        private readonly string create_order_url = "https://sb-openapi.zalopay.vn/v2/create";
        private readonly string redirectUrl = "https://localhost:7170/api/Product/GetAllProducts";
        private readonly ZaloPayConfig _zaloPayConfig;
        public PaymentController(IOptions<ZaloPayConfig> zaloPayConfig)
        {
            _zaloPayConfig = zaloPayConfig.Value;
        }
        [HttpPost]
        [Route("CreatePayment")]
        public IActionResult CreatePayment([FromBody] CreatePaymentRequestModel model)
        {
            try
            {
                var request = new CreateZaloPayPayRequest(
                    _zaloPayConfig.AppId,
                    _zaloPayConfig.AppUser,
                    model.AppTransId,
                    model.AppTime,
                    model.Amount,
                    model.Description,
                    model.EmbedData,
                    "zalopayapp",
                    "");
                request.MakeSignature(_zaloPayConfig.Key1);
                var response = request.GetLink(_zaloPayConfig.PaymentUrl);
                if (response.Item1)
                {
                    return Ok(new CreateZaloPayPayResponse { ReturnCode = 1, OrderUrl = response.Item2 });
                }
                else
                {
                    return BadRequest(new CreateZaloPayPayResponse { ReturnCode = 0, ReturnMessage = response.Item2 });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new CreateZaloPayPayResponse { ReturnCode = 0, ReturnMessage = "Internal server error" });
            }
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] UpdateOrderDTO orderDto)
        {

            Random rnd = new Random();
            var embed_data = new { redirecturl = redirectUrl };
            var items = new[] { new { } };
            var param = new Dictionary<string, string>();
            var app_trans_id = rnd.Next(1000000); // Generate a random order's ID.

            param.Add("app_id", app_id);
            param.Add("app_user", "user123");
            param.Add("app_time", Utils.GetTimeStamp().ToString());
            param.Add("amount", orderDto.TotalPrice.ToString());
            param.Add("app_trans_id", DateTime.Now.ToString("yyMMdd") + "_" + app_trans_id); // mã giao dich có định dạng yyMMdd_xxxx
            param.Add("embed_data", JsonConvert.SerializeObject(embed_data));
            param.Add("item", JsonConvert.SerializeObject(items));
            param.Add("description", "Lazada - Thanh toán đơn hàng #" + app_trans_id);
            param.Add("bank_code", "zalopayapp");

            var data = app_id + "|" + param["app_trans_id"] + "|" + param["app_user"] + "|" + param["amount"] + "|"
                + param["app_time"] + "|" + param["embed_data"] + "|" + param["item"];
            param.Add("mac", HmacHelper.Compute(ZaloPayHMAC.HMACSHA256, key1, data));

            var result = await HttpHelper.PostFormAsync(create_order_url, param);
            //var order = _mapper.Map<Order>(orderDTO);
            return Ok(result);
        }

    }
    public class CreatePaymentRequestModel
    {
        public int AppId { get; set; }
        public string AppUser { get; set; }
        public string AppTransId { get; set; }
        public long AppTime { get; set; }
        public long Amount { get; set; }
        public string Description { get; set; }
        public string EmbedData { get; set; }
        public string BankCode { get; set; }
    }
}
