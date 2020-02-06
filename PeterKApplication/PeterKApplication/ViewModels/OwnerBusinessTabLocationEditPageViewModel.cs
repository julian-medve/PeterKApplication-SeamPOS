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
    public class OwnerBusinessTabLocationEditPageViewModel : INotifyPropertyChanged
    {
        private BusinessLocation _locationMember;
        private bool _isEdit;


        public OwnerBusinessTabLocationEditPageViewModel()
        {
        }

        public string PageTitle => IsEdit ? "Edit Location" : "Add Location";
        public static OwnerBusinessTabLocationEditPageViewModel Self => null;


        public BusinessLocation LocationMember
        {
            get => _locationMember;
            set
            {
                _locationMember = value;
                OnPropertyChanged(nameof(LocationMember));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            System.Diagnostics.Debug.Write("OnPropertyChanged for {propertyName} : " + propertyName);
        }

        public bool Save()
        {
            using (var dbContext = new LocalDbContext())
            {
                if (IsEdit)
                {
                    dbContext.BusinessLocations.Update(LocationMember);
                }
                else
                {
                    dbContext.BusinessLocations.Add(LocationMember);
                }

                dbContext.SaveChanges();
            }

            return true;
        }


        public void ExistingMember(BusinessLocation member)
        {
            if (member != null)
            {
                LocationMember = member;
                IsEdit = true;
            }
            else
            {
                LocationMember = new BusinessLocation();
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
                OnPropertyChanged(nameof(PageTitle));
            }
        }
    }
}