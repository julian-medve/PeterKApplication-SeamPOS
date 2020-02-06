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
    public class OwnerWalkThroughPageViewModel: INotifyPropertyChanged
    {
        private readonly PrivateApiService _privateApiService;
        private ApiExecutionResponse _uploadBusinessResponse;

        public OwnerWalkThroughPageViewModel(PrivateApiService privateApiService)
        {
            _privateApiService = privateApiService;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task<bool> UploadBusinessSettings(UpdateBusinessDto req)
        {
            UploadBusinessResponse = await ApiHelper.Execute(_privateApiService.Client.UpdateBusiness(req));

            return !UploadBusinessResponse.HasError;
        }

        public ApiExecutionResponse UploadBusinessResponse
        {
            get => _uploadBusinessResponse;
            set
            {
                _uploadBusinessResponse = value;
                OnPropertyChanged(nameof(UploadBusinessResponse));
            }
        }
    }
}