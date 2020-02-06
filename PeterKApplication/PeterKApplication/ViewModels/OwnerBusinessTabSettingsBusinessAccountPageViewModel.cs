using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Helpers;
using PeterKApplication.Services;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.ViewModels
{
    public class OwnerBusinessTabSettingsBusinessAccountPageViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        private Business _business;
        private AppUser _me;
        public event PropertyChangedEventHandler PropertyChanged;

        /*public OwnerBusinessTabSettingsBusinessAccountPageViewModel(AuthService authService)
        {
            _authService = authService;
        }*/

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Initialize()
        {
            using (var db = new LocalDbContext())
            {
                if (Business == null)
                {
                    var bus = db.Businesses.First();

                    if (bus.BusinessLocation == null)
                    {
                        bus.BusinessLocation = new BusinessLocation();
                    }

                    Business = bus;
                }

                Me = AuthService.CurrentUser();
            }
        }

        public AppUser Me
        {
            get => _me;
            set
            {
                _me = value;
                OnPropertyChanged(nameof(Me));
            }
        }

        public Business Business
        {
            get => _business;
            set
            {
                _business = value;
                OnPropertyChanged(nameof(Business));
            }
        }

        public static OwnerBusinessTabSettingsBusinessAccountPageViewModel Self => null;

        public async Task Save()
        {
            using (var db = new LocalDbContext())
            {
                db.Users.Update(Me);
                db.Businesses.Update(Business);
                await db.SaveChangesAsync();
            }

            Business = null;
        }

        public void SetImage(byte[] eImage)
        {
            Business.Image = new ImageModel
            {
                ImageData = eImage
            };
            
            OnPropertyChanged(nameof(Business));
        }
    }
}