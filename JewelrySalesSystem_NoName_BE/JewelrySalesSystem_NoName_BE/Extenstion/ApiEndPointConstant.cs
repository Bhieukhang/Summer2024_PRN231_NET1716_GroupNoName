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
            public const string WarrantyEndpointNo = ApiEndpoint + "/warrantyno";
            public const string WarrantyByIdEndpoint = WarrantyEndpoint + "/id";
        }
    }
}
