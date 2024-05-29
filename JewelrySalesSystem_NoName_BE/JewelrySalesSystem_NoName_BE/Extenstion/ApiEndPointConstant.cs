using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
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

        public static class Login
        {
            public const string LoginEndpoint = ApiEndpoint + "/Login";
        }

        public static class Logout
        {
            public const string LogoutEndpoint = ApiEndpoint + "/Logout";
        }

        public static class Warranty
        {
            public const string WarrantyEndpoint = ApiEndpoint + "/warranty";
            public const string WarrantyEndpointNo = ApiEndpoint + "/warrantyno";
            public const string WarrantyByIdEndpoint = WarrantyEndpoint + "/{id}";
        }

        public static class Stall
        {
            public const string StallEndpoint = ApiEndpoint + "/Stall";
            public const string StallByIdEndpoint = StallEndpoint + "/{id}";
        }
        public static class Account
        {
            public const string AccountEndpoint = ApiEndpoint + "/Account";
            public const string AccountByRoleIdEndpoint = ApiEndpoint + "/AccountByRole";
            public const string TotalAccountEndpoint = ApiEndpoint + "/Account/Total-account";
            public const string ActiveAccountEndpoint = ApiEndpoint + "/Account/Active-account";
            public const string AccountByIdEndpoint = AccountEndpoint + "/id";
        }
        public static class Category
        {
            public const string CategoryEndpoint = ApiEndpoint + "/Category";
            public const string CategoryByIdEndpoint = CategoryEndpoint + "/id";
        }

        public static class Product
        {
            public const string ProductEndpoint = ApiEndpoint + "/Product";
            public const string ProductByIdEndpoint = ProductEndpoint + "/id";
        }

        public static class Promotion
        {
            public const string PromotionEndpoint = ApiEndpoint + "/Promotion";
            public const string PromotionByIdEndpoint = PromotionEndpoint + "/id";
        }
        public static class Material
        {
            public const string MaterialEndpoint = ApiEndpoint + "/Material";
            public const string MaterialByIdEndpoint = MaterialEndpoint + "/id";
        }
        public static class ProductMaterial
        {
            public const string ProductMaterialEndpoint = ApiEndpoint + "/ProductMaterial";
            public const string ProductMaterialByIdEndpoint = ProductMaterialEndpoint + "/id";
        }
        public static class Membership
        {
            public const string MembershipEndpoint = ApiEndpoint + "/membership";
            public const string MembershipByIdEndpoint = MembershipEndpoint + "/{id}";
            public const string MembershipByUserIdEndpoint = MembershipEndpoint + "/userId";
            public const string MembershipExpired = MembershipEndpoint + "/expired";
            public const string MembershipByName = MembershipEndpoint + "/{name}";
            public const string MembershipUserMoney = MembershipEndpoint + "/userMoney";
            public const string MembershipTotal = MembershipEndpoint + "/total";
            public const string MembershipActive = MembershipEndpoint + "/active";
            public const string MembershipUnActive = MembershipEndpoint + "/unavailable";
        }

        public static class Order
        {
            public const string OrderEndpoint = ApiEndpoint + "/order";
            public const string OrderEndpointTest = ApiEndpoint + "/orderTest";
            public const string OrderUpdateEndpoint = OrderEndpoint + "/id";
        }

        public static class ConditionWarranty
        {
            public const string ConditionWarrantyEndpoint = ApiEndpoint + "/condition";
            public const string ConditionWarrantyByIdEndpoint = ConditionWarrantyEndpoint + "/{id}";
        }
    }
}
