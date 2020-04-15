using System.ComponentModel;
using Qr19.ViewModels;
using Xamarin.Forms;

namespace Qr19.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}
