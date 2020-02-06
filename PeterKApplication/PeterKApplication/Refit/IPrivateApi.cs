using System.Collections.Generic;
using System.Threading.Tasks;
using PeterKApplication.Shared.Dtos;
using Refit;

namespace PeterKApplication.Refit
{
    [Headers("Authorization: Bearer")]
    public interface IPrivateApi
    {
        [Post("/Businesses")]
        Task UpdateBusiness([Body] UpdateBusinessDto req);

        [Get("/Owners/refresh")]
        Task<AuthOwnerResDto> RefreshToken();

        [Get("/Sync")]
        Task<SyncDetailsDto> SyncInfo([Body] SyncDetailsDto req);

        [Post("/Sync")]
        Task<SyncDto> Sync([Body] SyncDto req);

        [Post("/Owners/agent")]
        Task OwnerAgentCode(AgentDto agentDto);

        [Post("/Staffs/authenticate")]
        Task<AuthStaffResDto> StaffLogin(AuthStaffReqDto authStaffReqDto);

        [Post("/Support")]
        Task SendSupport([Body] SupportDto supportDto);

        [Get("/KnowledgeBase")]
        Task<List<KnowledgeBaseDto>> GetKnowledgeBase();
    }
}