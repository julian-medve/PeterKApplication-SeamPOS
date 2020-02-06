using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Helpers;
using PeterKApplication.Services;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.ViewModels
{
    public class OwnerBusinessTabStaffEditProfilePageViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;

        private List<BusinessLocation> _locations;

        private AppUser _staffMember;
        private string _staffFullName;
        private bool _isEdit;
        private string _selectedLocationOption;

        public OwnerBusinessTabStaffEditProfilePageViewModel(AuthService authService)
        {
            _authService = authService;
            /*using (var dbContext = new LocalDbContext())
            {
                List<BusinessLocation> businessLocations = dbContext.BusinessLocations.ToList();

                _locationOptions = businessLocations;

                List<string> listLocations = new List<string>();
                foreach (BusinessLocation item in businessLocations)
                {
                    listLocations.Add(item.Name);
                }

                LocationOptions = listLocations;
            }*/
        }

        public void Initialize()
        {
            using (var db = new LocalDbContext())
            {
                Locations = db.BusinessLocations.ToList();

                if (StaffMember.BusinessLocationId != null) {

                    var location = db.BusinessLocations.First(f => f.Id.Equals(StaffMember.BusinessLocationId));
                    SelectedLocationOption = location.Name;

                    OnPropertyChanged(nameof(SelectedLocationOption));
                }
            }
        }


        public static OwnerBusinessTabStaffEditProfilePageViewModel Self => null;

        public List<string> LocationOptions => Locations?.Select(c => c.Name)?.ToList();


        public string SelectedLocationOption
        {
            get => _selectedLocationOption;
            set
            {
                _selectedLocationOption = value;
                OnPropertyChanged(nameof(SelectedLocationOption));

                var location = Locations.First(f => f.Name.ToLower().Equals(SelectedLocationOption.ToLower()));
                StaffMember.BusinessLocationId = location.Id;
                OnPropertyChanged(nameof(StaffMember));
            }
        }

        public List<BusinessLocation> Locations
        {
            get => _locations;
            set
            {
                _locations = value;
                OnPropertyChanged(nameof(Locations));
                OnPropertyChanged(nameof(LocationOptions));
            }
        }


        public string StaffFullName
        {
            get => _staffFullName;
            set
            {
                _staffFullName = value;
                OnPropertyChanged(nameof(StaffFullName));
            }
        }


        public AppUser StaffMember
        {
            get => _staffMember;
            set
            {
                _staffMember = value;
                OnPropertyChanged(nameof(StaffMember));

                _staffFullName = StaffMember.FirstName + ' ' +  StaffMember.LastName;
                OnPropertyChanged(nameof(StaffFullName));

            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Save()
        {
            string[] split = StaffFullName.Split(new Char[] { ' ' });

            switch (split.Length) {

                case 0: StaffMember.FirstName = "";
                        StaffMember.LastName = ""; 
                        break;

                case 1: StaffMember.FirstName = split[0]; 
                        StaffMember.LastName = ""; 
                        break;

                case 2: StaffMember.FirstName = split[0];
                        StaffMember.LastName  = split[1];
                        break;
            }
            
            StaffMember.UserName = StaffFullName;

            using (var dbContext = new LocalDbContext())
            {
                if (SelectedLocationOption != null)
                {
                    var location = dbContext.BusinessLocations.First(f => f.Name.ToLower().Equals(SelectedLocationOption.ToLower()));
                    StaffMember.BusinessLocationId = location.Id;
                }
                else
                    StaffMember.BusinessLocationId = null;

                if (IsEdit)
                {
                    dbContext.Users.Update(StaffMember);
                }
                else
                {
                    StaffMember.BusinessId = AuthService.BusinessId;

                    // Default value for Currency format and tax
                    StaffMember.Tax = 10;

                    dbContext.Users.Add(StaffMember);
                }

                dbContext.SaveChanges();
            }

            return true;
        }

        public bool Delete()
        {
            return true;
        }

        public bool Disable()
        {
            return true;
        }

        public void ExistingMember(AppUser member)
        {
            if (member != null)
            {
                StaffMember = member;
                IsEdit = true;
            }
            else
            {
                StaffMember = new AppUser();
                IsEdit = false;
            }
        }

        public bool IsEdit
        {
            get => _isEdit;
            set
            {
                _isEdit = value;
                OnPropertyChanged(nameof(IsEdit));
            }
        }

        public void SetStaffMemberImage(byte[] eImage)
        {
            StaffMember.Image = eImage;
            OnPropertyChanged(nameof(StaffMember));
        }
    }
}