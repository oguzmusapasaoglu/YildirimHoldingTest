using System;
using System.Text;
using YildirimHoldingTest.Core.Common.ConfigHelper;
using YildirimHoldingTest.Core.Common.Exceptions;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Data.Interfaces;

namespace YildirimHoldingTest.Core.Log
{
    public class LogFactory : ILogFactory
    {
        private IMongoFactory mongoFactory;
        public LogFactory(IMongoFactory _mongoFactory)
        {
            mongoFactory = _mongoFactory;
        }
        public void AddErrorLog(
            LogTypeEnum parmLogType = LogTypeEnum.Error,
            string parmLogSenderFunc = "",
            int? parmLogUser = null,
            string parmLogMessage = null,
            string parmLogData = null,
            Exception parmException = null)
        {
            var entity = new LogEntity
            {
                LogUser = parmLogUser,
                LogType = (int)parmLogType,
                LogData = parmLogData,
                LogDate = GetDate,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogException = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(entity);
        }

        public void AddErrorLog(
            LogTypeEnum parmLogType = LogTypeEnum.Error,
            string parmLogSenderFunc = "",
            int? parmLogUser = null,
            string parmLogMessage = null,
            Exception parmException = null)
        {
            var entity = new LogEntity
            {
                LogUser = parmLogUser,
                LogType = (int)parmLogType,
                LogDate = GetDate,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogException = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(entity);
        }

        public void AddResponseLog(
            LogTypeEnum parmLogType,
            string parmLogSenderFunc,
            int? parmLogUser,
            string parmLogMessage,
            string parmRequestData,
            long parmRequestDate,
            string parmResponseData,
            long parmResponseDate,
            Exception parmException = null
         )
        {
            var entity = new LogEntity
            {
                LogUser = parmLogUser,
                LogType = (int)parmLogType,
                RequestData = parmRequestData,
                RequestDate = parmRequestDate,
                ResponseData = parmResponseData,
                ResponseDate = parmResponseDate,
                LogDate = GetDate,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogException = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(entity);
        }

        public void AddInfoLog(
            LogTypeEnum parmLogType = LogTypeEnum.Info,
            string parmLogSenderFunc = "",
            int? parmLogUser = null,
            string parmLogMessage = null,
            StringBuilder parmLogData = null,
            Exception parmException = null
            )
        {
            var entity = new LogEntity
            {
                LogUser = parmLogUser,
                LogType = (int)parmLogType,
                LogData = parmLogData.ToString(),
                LogDate = GetDate,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogException = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(entity, true);
        }

        public void AddFattalLog(
            string parmLogSenderFunc,
            string parmLogMessage,
            StringBuilder parmLogData,
            Exception parmException,
            LogTypeEnum parmLogType = LogTypeEnum.Fattal)
        {
            var dto = new LogEntity
            {
                LogType = (int)parmLogType,
                LogData = parmLogData.ToString(),
                LogDate = GetDate,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogException = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }

        public void AddFattalLog(
            string parmLogSenderFunc,
            string parmLogMessage,
            string parmLogData,
            Exception parmException,
            LogTypeEnum parmLogType = LogTypeEnum.Fattal)
        {
            var dto = new LogEntity
            {
                LogType = (int)parmLogType,
                LogData = parmLogData,
                LogDate = GetDate,
                LogMessage = parmLogMessage,
                LogSender = parmLogSenderFunc,
                LogException = (parmException != null) ? parmException.StackTrace : string.Empty
            };
            InsertLog(dto);
        }

        #region Manager
        private void InsertLog(LogEntity data, bool isError = false)
        {
            try
            {
                var collection = mongoFactory.InsertOne((isError) ? AppConfigHelper.GetMongoInfo().ErrorLogCollection : AppConfigHelper.GetMongoInfo().InfoLogCollection, data);
            }
            catch
            {
                throw new KnownException(ErrorTypeEnum.DbOperationException, "LogFactory", ExceptionMessageHelper.UnexpectedLogError);
            }
        }
        private long GetDate => Int64.Parse(DateTime.Now.ToString("yyyyMMddHHmmss"));
        #endregion
    }
}