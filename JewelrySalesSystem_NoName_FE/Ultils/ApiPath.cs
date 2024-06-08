namespace JewelrySalesSystem_NoName_FE.Ultils
{
    public class ApiPath
    {

        public ApiPath() { }
        public const string host = "https://localhost:";
        public const string port = "44318";
        public const string url = host + port;

        //Authentication
        public const string Login = url + "/api/v1/Login";
        public const string Logout = url + "/api/v1/Logout";
        public const string Profile = url + "/api/v1/Account/Profile";
        public const string ProfileUpdate = url + "/api/v1/Account/Profile/Update";

        //Membership
        public const string MembershipList = url + "/api/v1/membership";
        public const string MembershipProfile = url + "/api/v1/membership/userId";
        public const string MembershipTotal = url + "/api/v1/membership/total";
        public const string MembershipActive = url + "/api/v1/membership/active";
        public const string MembershipUnActive = url + "/api/v1/membership/unavailable";    

        //Promotion
        public const string Promotion = url + "/api/v1/Promotion";

        //Promotion
        public const string Transaction = url + "/api/v1/Transaction";

        //Dashboard
        public const string Dashboard = url + "/api/v1/Dashboard";

        //Category
        public const string CategoryList = url + "/api/v1/Category";

        //Product
        public const string ProductList = url + "/api/v1/Product";
        public const string ProductCodeGetListPromoton = url + "/api/v1/Product/code/promotion";

        //account
        public const string AccountList = url + "/api/v1/Account";
        public const string CreateAccount = url + "/api/v1/Account/Create";
        public const string TotalAccount = url + "/api/v1/Account/Total-account";
        public const string ActiveAccount = url + "/api/v1/Account/Active-account";

        //emloyees
        public const string EmployeesList = url + "/api/v1/AccountByRole";

        //stall
        public const string StallList = url + "/api/v1/Stall";

        //Order
        public const string OrderCreate = url + "/api/v1/order";
        public const string OrderByID = url + "/api/v1/order";
        public const string OrderCheckPromotion = url + "/api/v1/check";

        //Warranty
        public const string WarrantyList = url + "/api/v1/warranty";

        //ConditionWarranty
        public const string ConditionWarrantyList = url + "/api/v1/condition";
        //GoldRate
        public const string GoldRateList = url + "/api/v1/GoldRate";
    }
}
