using System.Collections.Generic;
using SharedModel.Registration;
using SharedModel.Response;

namespace DataAccessLayer.RegistrationRepository
{
    public interface IRegistrationRepository
    {
        Response RegisterUser(RegistrationDetails registrationDetails);
        List<RegisteredUsers> GetRegisteredUserList(string userName);
        Response ResetPassword(string username, ResetPasswordDetails resetPasswordDetails);
    }
}
