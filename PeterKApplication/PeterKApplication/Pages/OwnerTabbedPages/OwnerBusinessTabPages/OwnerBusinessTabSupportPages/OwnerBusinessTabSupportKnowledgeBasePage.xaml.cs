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
    public partial class OwnerBusinessTabSupportKnowledgeBasePage : ContentPage
    {
        private ApiExecutionResponse<List<KnowledgeBaseDto>> _knowledgeBaseResponse;
        private List<KnowledgeBaseDto> _knowledgeBase;
        
        public OwnerBusinessTabSupportKnowledgeBasePage()
        {
            InitializeComponent();
            
            ToggleVisibility = new Command<KnowledgeBaseDto>(Execute);
        }

        private void Execute(KnowledgeBaseDto obj)
        {
            KnowledgeBase = KnowledgeBase.Select(s =>
            {
                if(s.Id == obj.Id) s.IsVisible = !s.IsVisible;
                return s;
            }).ToList();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var papi = DependencyService.Resolve<PrivateApiService>();

            var res = await ApiHelper.Execute(papi.Client.GetKnowledgeBase());
            if (res.HasError)
            {
                UserDialogs.Instance.Alert("There was a problem fetching data", "Error", "Ok");
            }
            else
            {
                KnowledgeBase = res.Response;
            }
        }

        public List<KnowledgeBaseDto> KnowledgeBase
        {
            get => _knowledgeBase;
            set
            {
                _knowledgeBase = value;
                OnPropertyChanged(nameof(KnowledgeBase));
            }
        }

        public Command<KnowledgeBaseDto> ToggleVisibility { get; set; }
    }
}