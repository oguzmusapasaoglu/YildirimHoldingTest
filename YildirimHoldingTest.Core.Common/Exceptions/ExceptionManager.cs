using System;
using System.Collections.Generic;
using System.Text;

using YildirimHoldingTest.Core.Common.Helper;

namespace YildirimHoldingTest.Core.Common.Exceptions
{
    public class KnownException : Exception
    {
        public string MethotNameProp { get; set; }
        public string ExceptionMessageProp { get; set; }
        public ErrorTypeEnum ErrorTypeProp { get; set; }
        public Exception ExceptionProp { get; set; }
        public KnownException(string message)
            : base(message)
        {
        }
        public KnownException(ErrorTypeEnum errorType, string message)
            : base(message)
        {
            ErrorTypeProp = errorType;
            ExceptionMessageProp = message;
        }
        public KnownException(ErrorTypeEnum errorType, string methotName, string message, Exception exception)
            : base(message, exception)
        {
            MethotNameProp = methotName;
            ErrorTypeProp = errorType;
            ExceptionMessageProp = message;
            ExceptionProp = exception;
        }
        public KnownException(ErrorTypeEnum errorType, string methotName, string exceptionMessage)
           : base(exceptionMessage)
        {
            MethotNameProp = methotName;
            ExceptionMessageProp = exceptionMessage;
            ErrorTypeProp = errorType;
        }
        public KnownException(ErrorTypeEnum errorType, string methotName, Exception exception)
        {
            ErrorTypeProp = errorType;
            ExceptionMessageProp = exception.Message;
            ExceptionProp = exception;
            MethotNameProp = methotName;
        }
        public string ToShortString()
        {
            var sb = new StringBuilder();
            sb.AppendLine()
                .AppendFormat("ExceptionType      : {0}", ErrorTypeProp).AppendLine()
                .AppendFormat("ExceptionMesssage  : {0}", ExceptionMessageProp).AppendLine()
                .AppendFormat("MethotName         : {0}", MethotNameProp).AppendLine()
                .AppendFormat("ErrorStackTrace    : {0}", ExceptionProp.StackTrace).AppendLine();

            var inner = ExceptionProp;
            while (inner != null)
            {
                sb.AppendFormat("InnerMesssage      : {0}", inner.Message)
                 .AppendLine();
                inner = inner.InnerException;
            }
            return sb.ToString();
        }
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine();
            if (!ErrorTypeProp.IsNullOrEmpty())
                sb.AppendFormat("ExceptionType      : {0}", ErrorTypeProp).AppendLine();
            if (!ExceptionMessageProp.IsNullOrEmpty())
                sb.AppendFormat("ExceptionMesssage  : {0}", ExceptionMessageProp).AppendLine();
            if (!MethotNameProp.IsNullOrEmpty())
                sb.AppendFormat("MethotName         : {0}", MethotNameProp).AppendLine();
            if (ExceptionProp != null)
                sb.AppendFormat("ErrorStackTrace    : {0}", ExceptionProp.StackTrace).AppendLine();

            var inner = ExceptionProp;
            while (inner != null)
            {
                sb.AppendFormat("InnerMesssage      : {0}", inner.Message)
                 .AppendLine();
                inner = inner.InnerException;
            }

            sb.AppendLine().Append("*******************************************").AppendLine()
              .Append(StackTrace);

            while (inner != null)
            {
                sb.AppendLine()
                    .Append("*******************************************")
                    .AppendLine()
                    .Append(inner.StackTrace);
                inner = inner.InnerException;
            }
            return sb.ToString();
        }
    }

    public class FattalException : KnownException
    {
        public FattalException(ErrorTypeEnum errorType, string methotName, string exceptionMessage)
: base(exceptionMessage)
        {
            MethotNameProp = methotName;
            ExceptionMessageProp = exceptionMessage;
            ErrorTypeProp = errorType;
        }
    }

    public class FattalDbException : KnownException
    {
        public FattalDbException(ErrorTypeEnum errorType, string methotName, string exceptionMessage, Exception ex)
: base(exceptionMessage)
        {
            ExceptionProp = ex;
            MethotNameProp = methotName;
            ExceptionMessageProp = exceptionMessage;
            ErrorTypeProp = errorType;
        }
    }

    public class RequestWarningException : KnownException
    {
        public RequestWarningException(ErrorTypeEnum errorTypeProp, string message) : base(message)
        {
            ErrorTypeProp = errorTypeProp;
            ExceptionMessageProp = message;
        }
        public RequestWarningException(ErrorTypeEnum errorTypeProp, string message, Exception exception) : base(message)
        {
            ErrorTypeProp = errorTypeProp;
            ExceptionMessageProp = message;
            ExceptionProp = exception;
        }
    }
}
