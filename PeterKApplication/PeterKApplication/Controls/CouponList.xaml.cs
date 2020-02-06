using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PeterKApplication.Shared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CouponList : ContentView
    {
        public static readonly BindableProperty CouponsProperty = BindableProperty.Create("Products",
            typeof(List<Coupon>), typeof(CouponList), null, BindingMode.TwoWay);

        public static readonly BindableProperty DefaultImageProperty =
            BindableProperty.Create("DefaultImage", typeof(string), typeof(CouponList), null, BindingMode.TwoWay);

        public static readonly BindableProperty SelectedImageProperty =
            BindableProperty.Create("SelectedImage", typeof(string), typeof(CouponList), null, BindingMode.TwoWay);

        public static readonly BindableProperty AddOneClickedCommandProperty =
            BindableProperty.Create("AddOneClickedCommand", typeof(ICommand), typeof(CouponList), null,
                BindingMode.TwoWay);

        public static readonly BindableProperty DeleteAllClickedCommandProperty =
            BindableProperty.Create("DeleteAllClickedCommand", typeof(ICommand), typeof(CouponList), null,
                BindingMode.TwoWay);

        public CouponList()
        {
            InitializeComponent();

            ItemTapped = new Command<Coupon>(item =>
            {
                ItemSelected?.Invoke(this, new CouponListEventArgs
                {
                    Coupon = item
                });
            });

            AddOneCommand = new Command<Coupon>(item =>
            {
                AddOneClicked?.Invoke(this, new CouponListEventArgs
                {
                    Coupon = item
                });
            });

            DeleteAllCommand = new Command<Coupon>(item =>
            {
                DeleteAllClicked?.Invoke(this, new CouponListEventArgs
                {
                    Coupon = item
                });
            });
        }

        public ICommand ForceCloseCommand { get; set; }

        public List<Product> Coupons
        {
            get => (List<Product>) GetValue(CouponsProperty);
            set => SetValue(CouponsProperty, value);
        }

        public string DefaultImage
        {
            get => (string) GetValue(DefaultImageProperty);
            set => SetValue(DefaultImageProperty, value);
        }

        public string SelectedImage
        {
            get => (string) GetValue(SelectedImageProperty);
            set => SetValue(SelectedImageProperty, value);
        }

        public Command<Coupon> ItemTapped { get; set; }
        public Command<Coupon> AddOneCommand { get; set; }
        public Command<Coupon> DeleteAllCommand { get; set; }

        public event EventHandler<CouponListEventArgs> ItemSelected;
        public event EventHandler<CouponListEventArgs> AddOneClicked;
        public event EventHandler<CouponListEventArgs> DeleteAllClicked;
    }

    public class CouponListEventArgs : EventArgs
    {
        public Coupon Coupon { get; set; }
    }
}