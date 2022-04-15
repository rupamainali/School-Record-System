using SharedModel.Login;

namespace BusinessLayer.LoginBusiness
{
    public interface ILoginBusiness
    {
        LoginResponse LoginUser(LoginDetails loginDetails);
    }
}
