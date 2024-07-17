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
        public const string MembershipUserMoney = url + "/api/v1/membership/userMoney";
        public const string MembershipInfoOrder = url + "/api/v1/membership/phone";

        //Promotion
        public const string Promotion = url + "/api/v1/Promotion";
        public const string PromotionGroup = url + "/api/v1/Promotion/group";

        //Discount
        public const string Discount = url + "/api/v1/discount";
        public const string DiscountConfirm = Discount + "/confirm";

        //Transaction
        public const string Transaction = url + "/api/v1/Transaction";
        public const string TransactionOrder = url + "/api/v1/Transaction/orderId";

        //Dashboard
        public const string Dashboard = url + "/api/v1/Dashboard";
        public const string AccountDashboard = url + "/api/v1/Dashboard/account";

        //Category
        public const string CategoryList = url + "/api/v1/Category";

        //Material
        public const string MaterialList = url + "/api/v1/Material";

        //Product
        public const string ProductList = url + "/api/v1/Product";
        public const string AllProductEndpoint = url + "/api/v1/Product/All";
        public const string ProductDetails = url + "/api/v1/Product/id";
        public const string ProductCodeGetListPromoton = url + "/api/v1/Product/code/promotion";

        public const string SubProductList = url + "/api/v1/Product/subid";
        public const string TotalSubProductList = url + "/api/v1/Product/PurchasePrice";

        //Account
        public const string AccountList = url + "/api/v1/Account";
        public const string CreateAccount = url + "/api/v1/Account/Create";
        public const string TotalAccount = url + "/api/v1/Account/Total-account";
        public const string ActiveAccount = url + "/api/v1/Account/Active-account";
        public const string SearchAccount = url + "/api/v1/Account/search/member";
        public const string FilteredAccounts = url + "/api/v1/Account/Filtered";
        public const string UpdateDeflagAccount = url + "/api/v1/Account/UpdateDeflag";

        //Emloyees
        public const string EmployeesList = url + "/api/v1/Account";

        //stall
        public const string StallList = url + "/api/v1/Stall";

        //Order
        public const string OrderCreate = url + "/api/v1/order";
        public const string OrderByID = url + "/api/v1/order/id";
        public const string OrderCheckPromotion = url + "/api/v1/check";
        public const string GetListOrders = url + "/api/v1/order/GetListOrders";
        public const string OrderListPromotion = url + "/api/v1/orderlist";
        public const string OrderListDetail = url + "/api/v1/order/detail";
        public const string SearchOrders = url + "/api/v1/order/GetListOrders/search";
        public const string SearchOrderCustomer = OrderCreate + "/customer";
        public const string OrderOption = OrderCreate + "/option";
        public const string OrderUpdate = OrderCreate + "/update";
        public const string OrderTotal = url + "/api/v1/static/year";

        //Warranty
        public const string Warranty = url + "/api/v1/warranty";
        public const string WarrantySearch = Warranty + "/search";

        //ConditionWarranty
        public const string ConditionWarrantyList = url + "/api/v1/condition";

        //GoldRate
        public const string GoldRateList = url + "/api/v1/GoldRate";

        //SilverRate
        public const string SilverRateList = url + "/api/v1/ListRate";

        //Role
        public const string RoleList = url + "/api/v1/Role";

        //Diamond
        public const string DiamondList = url + "/api/v1/Diamond";
        public const string DiamondDetails = url + "/api/v1/Diamond/id";

        //PurchasePrice
        public const string PurchasePriceList = url + "/api/v1/PurchasePrice";

        //ProcessPrice
        public const string ProcessPriceList = url + "/api/v1/ProcessPrice";
    }
}
