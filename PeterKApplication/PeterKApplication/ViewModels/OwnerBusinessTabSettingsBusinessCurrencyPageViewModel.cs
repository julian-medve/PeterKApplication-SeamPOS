using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PeterKApplication.Data;
using PeterKApplication.Annotations;
using PeterKApplication.Helpers;
using PeterKApplication.Services;
using PeterKApplication.Shared.Models;
using System.Threading.Tasks;

namespace PeterKApplication.ViewModels
{
    class OwnerBusinessTabSettingsBusinessCurrencyPageViewModel : INotifyPropertyChanged
    {
        private string _currency;
        private float _tax;

        private List<string> _currencyOptions;
        private string _selectedCurrencyOption;

        private AppUser _me;
        public event PropertyChangedEventHandler PropertyChanged;

        public static OwnerBusinessTabSettingsPageViewModel Self => null;



        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string Currency
        {
            get => _currency;
            set
            {
                _currency = value;
                OnPropertyChanged(nameof(Currency));
            }
        }

        public float Tax 
        {
            get => _tax;
            set
            {
                _tax = value;
                OnPropertyChanged(nameof(Tax));
            }
        }

        public List<string> CurrencyOptions 
        {
            get => _currencyOptions;
            set 
            {
                _currencyOptions = value;
                OnPropertyChanged(nameof(CurrencyOptions));
            }
        }

        public string SelectedCurrencyOption
        {
            get => _selectedCurrencyOption;
            set
            {
                _selectedCurrencyOption = value;
                OnPropertyChanged(nameof(SelectedCurrencyOption));
            }
        }
        public void Initialize()
        {
            using (var db = new LocalDbContext())
            {
               /* BusinessName = db.Businesses?.First()?.Name;*/
            }

            Me = AuthService.CurrentUser();
            Tax = Me.Tax;
            Currency = Me.CurrencyFormat;

            CurrencyOptions = new List<string>() {"Ksh", "USD"};
            SelectedCurrencyOption = Me.CurrencyFormat;

            if (CurrencyOptions.IndexOf(Me.CurrencyFormat) == -1)
                CurrencyOptions.Add(Me.CurrencyFormat);
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

        public async Task Save()
        {
            using (var db = new LocalDbContext())
            {
                Me.Tax = Tax;
                Me.CurrencyFormat = SelectedCurrencyOption;

                db.Users.Update(Me);
                await db.SaveChangesAsync();
            }
        }
    }
}
