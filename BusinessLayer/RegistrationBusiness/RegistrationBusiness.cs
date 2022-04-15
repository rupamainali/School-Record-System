using System.Collections.Generic;
using DataAccessLayer.RegistrationRepository;
using SharedModel.Helpers;
using SharedModel.Registration;
using SharedModel.Response;

namespace BusinessLayer.RegistrationBusiness
{
    public class RegistrationBusiness : IRegistrationBusiness
    {
        private readonly IRegistrationRepository _registrationRepository;

        public RegistrationBusiness(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public Response ResetPassword(string username, ResetPasswordDetails resetPasswordDetails)
        {
            resetPasswordDetails.CurrentPassword = StringHelpers.MD5Hash(resetPasswordDetails.CurrentPassword);
            resetPasswordDetails.NewPassword = StringHelpers.MD5Hash(resetPasswordDetails.NewPassword);
            resetPasswordDetails.ConfirmNewPassword = StringHelpers.MD5Hash(resetPasswordDetails.ConfirmNewPassword);
            return _registrationRepository.ResetPassword(username, resetPasswordDetails);
        }

        public List<RegisteredUsers> GetRegisteredUserList(string userName)
        {
            return _registrationRepository.GetRegisteredUserList(userName);
        }

        public Response RegisterUser(RegistrationDetails registrationDetails)
        {
            registrationDetails.Password = StringHelpers.MD5Hash(registrationDetails.Password);
            return _registrationRepository.RegisterUser(registrationDetails);
        }
    }
}
