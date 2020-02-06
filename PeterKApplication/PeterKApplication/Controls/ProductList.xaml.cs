using PeterKApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Windows.Input;
using PeterKApplication.Extensions;

namespace PeterKApplication.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductList : ContentView
    {
        public static readonly BindableProperty ProductsProperty = BindableProperty.Create("Products", typeof(List<Product>), typeof(ProductList), null, BindingMode.TwoWay);
        
        public static readonly BindableProperty DefaultImageProperty = BindableProperty.Create("DefaultImage", typeof(string), typeof(ProductList), null, BindingMode.TwoWay);
        
        public static readonly BindableProperty SelectedImageProperty = BindableProperty.Create("SelectedImage", typeof(string), typeof(ProductList), null, BindingMode.TwoWay);

        public static readonly BindableProperty AddOneClickedCommandProperty =
            BindableProperty.Create("AddOneClickedCommand", typeof(ICommand), typeof(ProductList), null,
                BindingMode.TwoWay);

        public static readonly BindableProperty DeleteAllClickedCommandProperty =
            BindableProperty.Create("DeleteAllClickedCommand", typeof(ICommand), typeof(ProductList), null,
                BindingMode.TwoWay);

        public ProductList()
        {
            InitializeComponent();
            
            ItemTapped = new Command<Product>(item =>
            {
                ItemSelected?.Invoke(this, new ProductListEventArgs
                {
                    Product = item
                });
            });

            AddOneCommand = new Command<Product>(item =>
            {
                AddOneClicked?.Invoke(this, new ProductListEventArgs
                {
                    Product = item
                });
            });

            DeleteAllCommand = new Command<Product>(item =>
            {
                DeleteAllClicked?.Invoke(this, new ProductListEventArgs
                {
                    Product = item
                });
            });
        }

        public ICommand ForceCloseCommand { get; set; }

        public List<Product> Products
        {
            get => (List<Product>)GetValue(ProductsProperty);
            set => SetValue(ProductsProperty, value);
        }

        public string DefaultImage
        {
            get => (string)GetValue(DefaultImageProperty);
            set => SetValue(DefaultImageProperty, value);
        }

        public string SelectedImage
        {
            get => (string)GetValue(SelectedImageProperty);
            set => SetValue(SelectedImageProperty, value);
        }

        public Command<Product> ItemTapped { get;set; }
        public Command<Product> AddOneCommand { get; set; }
        public Command<Product> DeleteAllCommand { get; set; }

        public event EventHandler<ProductListEventArgs> ItemSelected;
        public event EventHandler<ProductListEventArgs> AddOneClicked;
        public event EventHandler<ProductListEventArgs> DeleteAllClicked;
    }

    public class ProductListEventArgs: EventArgs
    {
        public Product Product { get; set; }
    }
}