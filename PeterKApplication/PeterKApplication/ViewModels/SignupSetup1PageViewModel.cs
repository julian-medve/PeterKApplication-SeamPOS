using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PeterKApplication.Annotations;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Services;
using PeterKApplication.Shared.Dtos;

namespace PeterKApplication.ViewModels
{
    public class SignupSetup1PageViewModel: INotifyPropertyChanged
    {
        private readonly PrivateApiService _privateApiService;
        private ApiExecutionResponse _agentDtoResponse;
        public AgentDto AgentDto { get; set; }

        public ApiExecutionResponse AgentDtoResponse
        {
            get => _agentDtoResponse;
            set => _agentDtoResponse = value;
        }

        public static SignupSetup1PageViewModel Self => null;

        public SignupSetup1PageViewModel(PrivateApiService privateApiService)
        {
            _privateApiService = privateApiService;
        }

        public async Task<bool> SetAgentCode()
        {
            AgentDtoResponse = await ApiHelper.Execute(_privateApiService.Client.OwnerAgentCode(AgentDto));

            return !AgentDtoResponse.HasError;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}