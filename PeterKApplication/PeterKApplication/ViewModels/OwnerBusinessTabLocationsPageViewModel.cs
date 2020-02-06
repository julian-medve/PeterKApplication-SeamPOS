using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace PeterKApplication.ViewModels
{
    public class OwnerBusinessTabLocationsPageViewModel : INotifyPropertyChanged
    {

        private List<BusinessLocation> _locationList;

        public OwnerBusinessTabLocationsPageViewModel()
        {
            
        }

        public List<BusinessLocation> LocationList 
        {
            get => _locationList;
            set
            {
                _locationList = value;
                OnPropertyChanged(nameof(LocationList));
            }
        }

        
        public static OwnerBusinessTabStaffPageViewModel Self => null;


        public void LoadLocations()
        {
            using (var dbContext = new LocalDbContext())
            {
                LocationList = dbContext.BusinessLocations.ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}