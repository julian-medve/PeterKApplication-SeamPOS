using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Services;
using PeterKApplication.Shared.Dtos;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages.OwnerTabbedPages.OwnerBusinessTabPages.OwnerBusinessTabSupportPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerBusinessTabSupportMessageSupport : ContentPage
    {
        private ApiExecutionResponse _supportDtoResponse;
        private SupportDto _supportDto = new SupportDto();

        public OwnerBusinessTabSupportMessageSupport()
        {
            InitializeComponent();
        }

        public SupportDto SupportDto
        {
            get => _supportDto;
            set
            {
                _supportDto = value;
                OnPropertyChanged(nameof(SupportDto));
            }
        }

        private async void AppButton_OnOnClicked(object sender, EventArgs e)
        {
            var papi = DependencyService.Resolve<PrivateApiService>();
            SupportDtoResponse = await ApiHelper.Execute(papi.Client.SendSupport(SupportDto));

            if (!SupportDtoResponse.HasError)
            {
                UserDialogs.Instance.Alert(
                    "Successfully sent email to support, we'll reach out to you with email.", "Success", "Ok");
            }

            await Navigation.PopAsync();
        }

        public ApiExecutionResponse SupportDtoResponse
        {
            get => _supportDtoResponse;
            set
            {
                _supportDtoResponse = value;
                OnPropertyChanged(nameof(SupportDtoResponse));
            }
        }
    }
}