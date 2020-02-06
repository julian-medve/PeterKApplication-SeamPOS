using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PeterKApplication.Controls;
using PeterKApplication.Helpers;
using PeterKApplication.Models;
using PeterKApplication.Pages.DpsTabbedPages;
using PeterKApplication.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PeterKApplication.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OwnerMasterDetailPage : MasterDetailPage
    {
        public OwnerMasterDetailPage()
        {
            InitializeComponent();
        }
    }
}