using System;
using System.Collections.Generic;
using System.Data;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using SharedModel.Registration;
using SharedModel.Response;

namespace DataAccessLayer.RegistrationRepository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly IDataAccessObject _dataAccessObject;
        private readonly string _connectionString;
        public RegistrationRepository(IDataAccessObject dataAccessObject, IConfiguration configuration)
        {
            _dataAccessObject = dataAccessObject;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public Response ResetPassword(string username, ResetPasswordDetails resetPasswordDetails)
        {
            Response response = new Response();
            var sql = "EXEC SP_LOGIN_MANAGEMENT ";
            sql += "@Flag='ResetPassword'";
            sql += $" ,@Username='{username}'";
            sql += $" ,@Password='{resetPasswordDetails.CurrentPassword}'";
            sql += $" ,@NewPassword='{resetPasswordDetails.NewPassword}'";
            try
            {
                var dataRow = _dataAccessObject.ExecuteDataRow(sql);
                if (dataRow != null)
                {
                    response.Code = dataRow["Code"].ToString();
                    response.Message = dataRow["Message"].ToString();
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }

        public List<RegisteredUsers> GetRegisteredUserList(string userName)
        {
            List<RegisteredUsers> registeredUsersList = new List<RegisteredUsers>();
            var sql = "EXEC SP_LOGIN_MANAGEMENT ";
            sql += "@Flag='GetAllUsers'";
            sql += $" ,@Username='{userName}'";
            try
            {
                var dataTable = _dataAccessObject.ExecuteDataTable(sql);
                if (dataTable != null)
                {
                    foreach (DataRow user in dataTable.Rows)
                    {
                        RegisteredUsers userDetails = new RegisteredUsers();
                        userDetails.Id = user["Id"].ToString();
                        userDetails.Username = user["Username"].ToString();
                        userDetails.CreatedBy = user["CreatedBy"].ToString();
                        userDetails.CreatedDate = user["CreatedDate"].ToString();
                        registeredUsersList.Add(userDetails);
                    }
                    return registeredUsersList;
                }
                return registeredUsersList;
            }
            catch (Exception ex)
            {
                return registeredUsersList;
            }
        }

        public Response RegisterUser(RegistrationDetails registrationDetails)
        {
            Response response = new Response();
            var sql = "EXEC SP_LOGIN_MANAGEMENT ";
            sql += "@Flag='RegisterUser'";
            sql += $" ,@Username='{registrationDetails.Username}'";
            sql += $" ,@Password='{registrationDetails.Password}'";
            sql += $" ,@CreatedBy='{registrationDetails.CreatedBy}'";
            try
            {
                var dataRow = _dataAccessObject.ExecuteDataRow(sql);
                if (dataRow != null)
                {
                    response.Code = dataRow["Code"].ToString();
                    response.Message = dataRow["Message"].ToString();
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                return response;
            }
        }
    }
}
