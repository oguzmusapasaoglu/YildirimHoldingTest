using ServiceStack.Text;
using System;
using System.Reflection;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Core.Common.Exceptions;

namespace YildirimHoldingTest.Core.Common.Helper
{
    public static class GlobalHelper
    {
        public static string GetMethodName(this MethodBase method)
        {
            string methodName = method.Name;
            string className = method.ReflectedType.Name;
            return className + "." + methodName;
        }
        public static ServicesResponse<TResponse> CreateServicesResponse<TResponse>(TResponse responseData, string responseMessage = "")
        {
            return new ServicesResponse<TResponse>
            {
                ResponseMessage = responseMessage,
                ResultData = responseData,
                IsSuccess = true
            };
        }
        public static ServicesResponse<TResponse> CreateServicesErrorResponse<TResponse>(string exceptionMessage = ExceptionMessageHelper.UnexpectedSystemError, ErrorTypeEnum errorType = ErrorTypeEnum.GeneralExeption)
        {
            var response = new ServicesResponse<TResponse>();
            response.IsSuccess = false;
            response.ErrorType = errorType;
            response.ResponseMessage = exceptionMessage;
            return response;
        }
        public static string CreateResponse(object result)
        {
            return result.ToJson();
        }
        public static string ToJson<T>(this T data)
        {
            return JsonSerializer.SerializeToString<T>(data);
        }
        public static string ToJson(this object data)
        {
            return JsonSerializer.SerializeToString(data);
        }
        public static T FromJson<T>(this string data)
        {
            try
            {
                return JsonSerializer.DeserializeFromString<T>(data);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
