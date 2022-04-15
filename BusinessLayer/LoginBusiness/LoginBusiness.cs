using DataAccessLayer.LoginRepository;
using SharedModel.Helpers;
using SharedModel.Login;

namespace BusinessLayer.LoginBusiness
{
    public class LoginBusiness : ILoginBusiness
    {
        private readonly ILoginRepository _loginRepository;

        public LoginBusiness(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;
        }

        public LoginResponse LoginUser(LoginDetails loginDetails)
        {
            loginDetails.Password = StringHelpers.MD5Hash(loginDetails.Password);
            return _loginRepository.LoginUser(loginDetails);
        }
    }
}
