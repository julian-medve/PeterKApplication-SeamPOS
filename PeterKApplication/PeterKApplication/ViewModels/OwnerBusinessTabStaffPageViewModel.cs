using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Annotations;
using PeterKApplication.Data;
using PeterKApplication.Helpers;
using PeterKApplication.Shared.Models;

namespace PeterKApplication.ViewModels
{
    public class OwnerBusinessTabStaffPageViewModel : INotifyPropertyChanged
    {
        private List<AppUser> _staffList;
        private string _searchText;
        private List<AppUser> _filteredStaffList;

        public List<AppUser> StaffList
        {
            get => _staffList;
            set
            {
                _staffList = value;
                OnPropertyChanged(nameof(StaffList));
                OnPropertyChanged(nameof(FilteredStaffList));
            }
        }

        public List<AppUser> FilteredStaffList => string.IsNullOrEmpty(SearchText)
            ? StaffList
            : StaffList.Where(s =>
                s.FirstName?.ToLower().Contains(SearchText?.ToLower()) == true ||
                s.LastName?.ToLower().Contains(SearchText?.ToLower()) == true).ToList();

        public static OwnerBusinessTabStaffPageViewModel Self => null;

        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged(nameof(SearchText));
                OnPropertyChanged(nameof(FilteredStaffList));
            }
        }

        public void LoadStaff()
        {
            using (var dbContext = new LocalDbContext())
            {
                StaffList = dbContext.Users.Include(u => u.Orders).ToList();
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