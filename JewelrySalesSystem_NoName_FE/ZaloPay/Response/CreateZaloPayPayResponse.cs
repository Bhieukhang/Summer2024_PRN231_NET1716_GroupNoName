namespace JewelrySalesSystem_NoName_FE.ZaloPay.Response
{
    public class CreateZaloPayPayResponse
    {
        public int ReturnCode { get; set; }
        public string ReturnMessage { get; set; } = string.Empty;
        public string OrderUrl { get; set; } = string.Empty;
    }
}
