namespace JewelrySalesSystem_NoName_FE.Ultils
{
    public class ApiPath
    {

        public ApiPath() { }
        public const string host = "https://localhost:";
        public const string port = "44318";
        public const string url = host + port;

        public const string WarrantyList =  url + "/api/v1/warranty";
        public const string MembershipList = url + "/api/v1/membership";

    }
}
