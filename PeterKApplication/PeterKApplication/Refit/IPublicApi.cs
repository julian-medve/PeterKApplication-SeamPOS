using System.Threading.Tasks;
using PeterKApplication.Shared.Dtos;
using Refit;

namespace PeterKApplication.Refit
{
    public interface IPublicApi
    {
        [Post("/Owners/register")]
        Task<RegisterOwnerResDto> OwnerRegister([Body] RegisterOwnerReqDto req);

        [Post("/Owners/authenticate")]
        Task<AuthOwnerResDto> OwnerAuthentication([Body] AuthOwnerReqDto req);

        [Post("/Owners/sendConfirmationCode")]
        Task<SmsConfirmationResDto> ResendOwnerConfirmationCode([Body] SmsConfirmationReqDto req);

        [Post("/Owners/verifyPhoneNumber")]
        Task<VerifyPhoneNumberResDto> VerifyPhoneNumber([Body] VerifyPhoneNumberReqDto req);
    }
}