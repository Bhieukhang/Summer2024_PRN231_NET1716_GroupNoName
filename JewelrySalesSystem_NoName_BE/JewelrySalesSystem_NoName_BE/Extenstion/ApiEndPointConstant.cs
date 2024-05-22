namespace JewelrySalesSystem_NoName_BE.Extenstion
{
    public static class ApiEndPointConstant
    {
        static ApiEndPointConstant()
        {
        }

        public const string RootEndPoint = "/api";
        public const string ApiVersion = "/v1";
        public const string ApiEndpoint = RootEndPoint + ApiVersion;

        public static class Warranty
        {
            public const string WarrantyEndpoint = ApiEndpoint + "/warranty";
            public const string WarrantyByIdEndpoint = WarrantyEndpoint + "/id";
        }

        public static class Stall  
        {
            public const string StallEndpoint = ApiEndpoint + "/Stall";
            public const string StallByIdEndpoint = StallEndpoint + "/id";
        }
        public static class Account
        {
            public const string AccountEndpoint = ApiEndpoint + "/Account";
            public const string AccountByIdEndpoint = AccountEndpoint + "/id";
        }
    }
}
