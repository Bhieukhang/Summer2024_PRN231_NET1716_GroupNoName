namespace JewelrySalesSystem_NoName_FE.DTOs.Account
{
    public class SearchAccountDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
    }

    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public DateTime TimeStamp { get; set; }
    }

    public class CreateAccountResponse
    {
        public int StatusCode { get; set; }
        public string Error { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
