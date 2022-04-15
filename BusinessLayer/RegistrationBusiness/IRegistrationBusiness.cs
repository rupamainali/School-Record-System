using System.Collections.Generic;
using SharedModel.Registration;
using SharedModel.Response;

namespace BusinessLayer.RegistrationBusiness
{
    public interface IRegistrationBusiness
    {
        Response RegisterUser(RegistrationDetails registrationDetails);
        List<RegisteredUsers> GetRegisteredUserList(string userName);
        Response ResetPassword(string username, ResetPasswordDetails resetPasswordDetails);
    }
}
