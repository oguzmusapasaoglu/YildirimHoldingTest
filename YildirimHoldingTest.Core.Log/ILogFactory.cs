using System;
using System.Text;
using YildirimHoldingTest.Core.Common.Helper;

namespace YildirimHoldingTest.Core.Log
{
    public interface ILogFactory
    {
        void AddErrorLog(
              LogTypeEnum parmLogType = LogTypeEnum.Error,
              string parmLogSenderFunc = "",
              int? parmLogUser = null,
              string parmLogMessage = null,
              string parmLogData = null,
              Exception parmException = null);

        void AddErrorLog(
           LogTypeEnum parmLogType = LogTypeEnum.Error,
           string parmLogSenderFunc = "",
           int? parmLogUser = null,
           string parmLogMessage = null,
           Exception parmException = null);

        void AddResponseLog(
           LogTypeEnum parmLogType,
           string parmLogSenderFunc,
           int? parmLogUser,
           string parmLogMessage,
           string parmRequestData,
           long parmRequestDate,
           string parmResponseData,
           long parmResponseDate,
           Exception parmException = null
        );

        void AddInfoLog(
           LogTypeEnum parmLogType = LogTypeEnum.Info,
           string parmLogSenderFunc = "",
           int? parmLogUser = null,
           string parmLogMessage = null,
           StringBuilder parmLogData = null,
           Exception parmException = null
           );

        void AddFattalLog(
           string parmLogSenderFunc,
           string parmLogMessage,
           StringBuilder parmLogData,
           Exception parmException,
           LogTypeEnum parmLogType = LogTypeEnum.Fattal);

        void AddFattalLog(
           string parmLogSenderFunc,
           string parmLogMessage,
           string parmLogData,
           Exception parmException,
           LogTypeEnum parmLogType = LogTypeEnum.Fattal);
    }
}
