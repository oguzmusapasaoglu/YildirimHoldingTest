using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using YildirimHoldingTest.Core.Common.Base;
using YildirimHoldingTest.Core.Common.Exceptions;
using YildirimHoldingTest.Core.Common.Helper;
using YildirimHoldingTest.Core.Data.Interfaces;
using YildirimHoldingTest.Core.Log;
using YildirimHoldingTest.Domain.InvoiceDomain.Entities;
using YildirimHoldingTest.Domain.InvoiceDomain.Interfaces;
using YildirimHoldingTest.Domain.InvoiceDomain.Model;
using YildirimHoldingTest.Domain.Users.Interfaces;

namespace YildirimHoldingTest.Domain.InvoiceDomain.Manager
{
    public class InvoiceHeaderManager : IInvoiceHeaderManager
    {
        private ILogFactory logger;
        private IDbFactory dbFacorty;
        private IConnectionFactory connectionFactory;
        private ICustomerInfoManager customerInfoManager;
        private IUsersInfoManager usersInfoManager;
        public InvoiceHeaderManager(
            ILogFactory _logger,
            IDbFactory _dbFactory,
            IConnectionFactory _connectionFactory,
            ICustomerInfoManager _customerInfoManager,
            IUsersInfoManager _usersInfoManager)
        {
            logger = _logger;
            dbFacorty = _dbFactory;
            connectionFactory = _connectionFactory;
            customerInfoManager = _customerInfoManager;
            usersInfoManager = _usersInfoManager;
        }

        #region V 1.0
        public ServicesResponse<IEnumerable<InvoiceHeaderReponseModel>> GetAllDataByFilterV1(InvoiceHeaderRequestModel request)
        {
            try
            {
                StringBuilder query;
                var parms = new DynamicParameters();
                if (request.InvoiceNo.IsNullOrEmpty() && request.CustomerNo.IsNullOrEmpty() && request.StartDate.IsNullOrEmpty() && request.EndDate.IsNullOrEmpty())
                    query = GetSelectQuery();
                else
                    query = GetWhereQuery(ref parms, request.InvoiceNo, request.CustomerNo, request.StartDate, request.EndDate);

                var returnData = dbFacorty.GetData<InvoiceHeaderReponseModel>(query, parms);
                if (returnData == null && returnData.Count() <= 0)
                    return GlobalHelper.CreateServicesErrorResponse<IEnumerable<InvoiceHeaderReponseModel>>(ExceptionMessageHelper.DataNotFound, ErrorTypeEnum.WarningException);

                return GlobalHelper.CreateServicesResponse(returnData);
            }
            catch (FattalDbException ex)
            {
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<InvoiceHeaderReponseModel>>(ex.ExceptionMessageProp);
            }
            catch (KnownException ex)
            {
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<InvoiceHeaderReponseModel>>(ex.ExceptionMessageProp);
            }
            catch (Exception ex)
            {
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<InvoiceHeaderReponseModel>>();
            }
        }
        private StringBuilder GetSelectQuery()
        {
            return new StringBuilder(@"SELECT
            Invoice.InvoiceHeader.InvoiceNo, Customer.CustomerInfo.CustomerNo,
            Invoice.InvoiceHeader.GrandTotal, Invoice.InvoiceHeader.Currency,
            Invoice.InvoiceHeader.CreatedDate, Users.UsersInfo.UserNo AS CreateBy
            FROM
            Customer.CustomerInfo INNER JOIN
            Invoice.InvoiceHeader ON Customer.CustomerInfo.Id = Invoice.InvoiceHeader.Id INNER JOIN
            Users.UsersInfo ON Customer.CustomerInfo.CreatedBy = Users.UsersInfo.Id AND
            Invoice.InvoiceHeader.CreatedBy = Users.UsersInfo.Id");
        }
        private StringBuilder GetWhereQuery(ref DynamicParameters parameters, string invoiceNo = "", string customerNo = "", DateTime? startdate = null, DateTime? endDate = null)
        {
            var sbSelect = GetSelectQuery();
            var sbWhereConditions = new StringBuilder();
            sbWhereConditions.Append(" WHERE ");
            if (!invoiceNo.IsNullOrEmpty())
            {
                sbWhereConditions.AppendLine("Invoice.InvoiceHeader.InvoiceNo = @InvoiceNo");
                parameters.Add("@InvoiceNo", invoiceNo, System.Data.DbType.String);
            }
            else if (!customerNo.IsNullOrEmpty())
            {
                sbWhereConditions.AppendLine("Customer.CustomerInfo.CustomerNo = @CustomerNo");
                parameters.Add("@CustomerNo", customerNo, System.Data.DbType.String);
            }
            else
            {
                sbWhereConditions.AppendLine("Invoice.InvoiceHeader.CreatedDate BETWEEN @Startdate AND @EndDate)");
                parameters.Add("@Startdate", startdate, System.Data.DbType.DateTime2);
                parameters.Add("@EndDate", endDate, System.Data.DbType.DateTime2);
            }
            return sbSelect.AppendLine().Append(sbWhereConditions);
        }
        #endregion

        #region V 2.0
        public ServicesResponse<IEnumerable<InvoiceHeaderReponseModel>> GetAllDataByFilterV2(InvoiceHeaderRequestModel requestModel)
        {
            var methodName = MethodBase.GetCurrentMethod().GetMethodName();
            try
            {
                var invoiceHeaders = connectionFactory.CreateConnection().GetAll<InvoiceHeaderEntity>().AsQueryable();
                var customers = customerInfoManager.GetAllDataByFilter(null, requestModel.CustomerNo).ResultData;
                var users = usersInfoManager.GetAllData().ResultData;

                #region Filter Condition
                var predicate = PredicateBuilderHelper.True<InvoiceHeaderEntity>();
                if (!requestModel.InvoiceNo.IsNullOrLessOrEqToZero())
                    predicate = predicate.Or(q => q.InvoiceNo == requestModel.InvoiceNo);

                if (!requestModel.CustomerNo.IsNullOrLessOrEqToZero())
                {
                    var cus = customers.FirstOrDefault(q => q.CustomerNo == requestModel.CustomerNo);
                    if (cus != null)
                        predicate = predicate.Or(q => q.CustomerId == cus.Id);
                }
                if (requestModel.StartDate.HasValue && requestModel.EndDate.HasValue)
                    predicate = predicate.Or(q => q.CreatedDate >= requestModel.StartDate.Value && q.CreatedDate <= requestModel.EndDate.Value);
                #endregion

                var result = invoiceHeaders.Where(predicate).Select(q => new InvoiceHeaderReponseModel
                {
                    Currency = q.Currency,
                    Address = customers.FirstOrDefault(p => p.Id == q.Id).CutomerCity,
                    CustomerNo = customers.FirstOrDefault(p => p.Id == q.Id).CustomerNo,
                    GrandTotal = q.GrandTotal,
                    InvoiceNo = q.InvoiceNo,
                    CreatedDate = q.CreatedDate,
                    CreatedBy = users.FirstOrDefault(p => p.Id == q.CreatedUser).UserNo
                }).AsEnumerable();

                return GlobalHelper.CreateServicesResponse(result);
            }
            catch (FattalDbException ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.ExceptionMessageProp, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<InvoiceHeaderReponseModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (KnownException ex)
            {
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<InvoiceHeaderReponseModel>>(ex.ExceptionMessageProp, ex.ErrorTypeProp);
            }
            catch (Exception ex)
            {
                logger.AddErrorLog(LogTypeEnum.Error, methodName, null, ex.Message, ex);
                return GlobalHelper.CreateServicesErrorResponse<IEnumerable<InvoiceHeaderReponseModel>>();
            }
        }
        #endregion
    }
}
