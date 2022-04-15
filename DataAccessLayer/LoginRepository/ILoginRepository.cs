using SharedModel.Login;

namespace DataAccessLayer.LoginRepository
{
    public interface ILoginRepository
    {
        LoginResponse LoginUser(LoginDetails loginDetails);
    }
}
