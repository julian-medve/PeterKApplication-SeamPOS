using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PeterKApplication.Annotations;
using PeterKApplication.Services;
using Xamarin.Forms;

namespace PeterKApplication.Helpers
{
    public class ApplicationHelper : INotifyPropertyChanged
    {
        public ApplicationHelper()
        {
            Toggle = new Command(() => ToggleMasterMenu());
        }

        public static void ToggleMasterMenu(object sender = null, EventArgs e = null)
        {
            if (AuthService.IsOwner)
            {
                if (Application.Current.MainPage is MasterDetailPage masterDetailPage)
                {
                    masterDetailPage.IsPresented = !masterDetailPage.IsPresented;
                }
            }
        }

        public ICommand Toggle
        {
            get => _toggle;
            set
            {
                _toggle = value;
                OnPropertyChanged(nameof(Toggle));
            }
        }

        public static readonly ApplicationHelper Helper = new ApplicationHelper();
        private ICommand _toggle;
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}