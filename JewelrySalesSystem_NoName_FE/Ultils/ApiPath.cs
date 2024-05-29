namespace JewelrySalesSystem_NoName_FE.Ultils
{
    public class ApiPath
    {

        public ApiPath() { }
        public const string host = "https://localhost:";
        public const string port = "44318";
        public const string url = host + port;

        //Warranty
        public const string WarrantyList =  url + "/api/v1/warranty";

        //Membership
        public const string MembershipList = url + "/api/v1/membership";
        public const string MembershipProfile = url + "/api/v1/membership/userId";

        //Promotion
        public const string Promotion = url + "/api/v1/promotion";

        //Category
        public const string CategoryList = url + "/api/v1/Category";


        //Product
        public const string ProductList = url + "/api/v1/Product";

        //account
        public const string AccountList = url + "/api/v1/Account";
        public const string TotalAccount = url + "/api/v1/Account/Total-account";
        public const string ActiveAccount = url + "/api/v1/Account/Active-account";

        //emloyees
        public const string EmployeesList = url + "/api/v1/AccountByRole";

        //stall
        public const string StallList = url + "/api/v1/Stall";
    }
}
