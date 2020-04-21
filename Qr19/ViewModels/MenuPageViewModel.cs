using System.Collections.Generic;
using System.Threading.Tasks;
using Qr19.Lib.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Qr19.ViewModels
{
    public class MenuPageViewModel : BaseViewModel
    {
        private MenuItemModel selectedMenuItem;

        public MenuItemModel SelectedMenuItem { get => selectedMenuItem; set => SetProperty(ref selectedMenuItem, value, onChanged: OnMenuItemChanged); }

        public List<MenuItemModel> MenuItems { get; } = new List<MenuItemModel>
        {
            //new MenuItemModel { Text = "INFOS COVID 19", Url = "https://www.gouvernement.fr/info-coronavirus" },
            //new MenuItemModel { Text = "GENERATEUR D'ATTESTATION", Url = "https://media.interieur.gouv.fr/deplacement-covid-19/" },
            new MenuItemModel { Text = "CONFIDENTIALITÉ", Url = "https://github.com/softlion/qr19" },
            new MenuItemModel { Text = "SPONSORS", Url = "https://github.com/softlion/qr19/sponsors" },
        };

        private async void OnMenuItemChanged()
        {
            if (selectedMenuItem != null)
            {
                await Browser.OpenAsync(selectedMenuItem.Url, BrowserLaunchMode.SystemPreferred);
                await Task.Delay(500);
                SelectedMenuItem = null;
                ((MasterDetailPage)App.Current.MainPage).IsPresented = false;
            }
        }
    }
}
