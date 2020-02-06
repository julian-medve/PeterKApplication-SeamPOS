using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Helpers;

namespace PeterKApplication.ViewModels
{
    public class OwnerBusinessTabSettingsPageViewModel: INotifyPropertyChanged
    {
        private string _businessName;
        public event PropertyChangedEventHandler PropertyChanged;

        public static OwnerBusinessTabSettingsPageViewModel Self => null;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string BusinessName
        {
            get => _businessName;
            set
            {
                _businessName = value;
                OnPropertyChanged(nameof(BusinessName));
            }
        }

        public void Initialize()
        {
            using (var db = new LocalDbContext())
            {
                BusinessName = db.Businesses?.First()?.Name;
            }
        }
    }
}