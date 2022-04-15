using System;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Configuration;
using SharedModel.Login;

namespace DataAccessLayer.LoginRepository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDataAccessObject _dataAccessObject;
        private readonly string _connectionString;
        public LoginRepository(IDataAccessObject dataAccessObject, IConfiguration configuration)
        {
            _dataAccessObject = dataAccessObject;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public LoginResponse LoginUser(LoginDetails loginDetails)
        {
            LoginResponse loginResponse = new LoginResponse();
            var sql = "EXEC SP_LOGIN_MANAGEMENT ";
            sql += "@Flag='LogIn'";
            sql += $" ,@Username='{loginDetails.Username}'";
            sql += $" ,@Password='{loginDetails.Password}'";
            try
            {
                var dataRow = _dataAccessObject.ExecuteDataRow(sql);
                if (dataRow != null)
                {
                    loginResponse.Username = dataRow["Username"].ToString();
                    loginResponse.Code = dataRow["Code"].ToString();
                    loginResponse.Message = dataRow["Message"].ToString();
                    return loginResponse;
                }
                return loginResponse;
            }
            catch (Exception ex)
            {
                return loginResponse;
            }
        }
    }
}
