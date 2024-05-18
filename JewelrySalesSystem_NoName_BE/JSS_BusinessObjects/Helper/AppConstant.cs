using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace JSS_BusinessObjects;

public static class AppConstant
{
    public enum ErrCode
    {
        Internal_Server_Error = 500
    }
    public class ErrMessage
    {
        public const string Internal_Server_Error = "Hệ thống xảy ra lỗi, vui lòng thử lại";
        public const string Opps = "Oops !!! Something Wrong. Try Again.";
        public const string Not_Found_Resource = "The server can not find the requested resource.";
        public const string Bad_Request = "The server could not understand the request due to invalid syntax.";
        public const string Unauthorized = "Unauthorized.";
    }
}

