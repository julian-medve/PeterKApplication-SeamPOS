using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Acr.UserDialogs;
using PeterKApplication.Annotations;
using PeterKApplication.Services;
using Xamarin.Essentials;

namespace PeterKApplication.ViewModels
{
    public class OwnerBusinessTabSyncPageViewModel: INotifyPropertyChanged
    {
        private readonly SyncService _syncService;
        private bool _autoSyncEnabled;

        public OwnerBusinessTabSyncPageViewModel(SyncService syncService)
        {
            _syncService = syncService;
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public static OwnerBusinessTabSyncPageViewModel Self => null;

        public bool AutoSyncEnabled
        {
            get => _autoSyncEnabled;
            set
            {
                _autoSyncEnabled = value;
                Preferences.Set("AutoSync", value);
                OnPropertyChanged(nameof(AutoSyncEnabled));
                OnPropertyChanged(nameof(AutoSyncEnabledText));
            }
        }

        public string AutoSyncEnabledText 
        { 
            get => _autoSyncEnabled ? "Cloud Sync Activated" : "Cloud Sync Disabled";
            set
            {
                OnPropertyChanged(nameof(AutoSyncEnabledText));
            }
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async Task StartManualSync()
        {
            await _syncService.SyncManual();
        }

        public void Initialize()
        {
            AutoSyncEnabled = Preferences.Get("AutoSync", false);
        }
    }
}