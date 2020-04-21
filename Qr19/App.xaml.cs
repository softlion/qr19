using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Qr19.Views;
using XamSvg.Shared;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Qr19
{
    public enum AppState
    {
        Sleep,
        Resume
    }

    public partial class App : Application
    {
        public const string AppStateChangedMessage = nameof(AppStateChangedMessage);

        public App()
        {
            InitializeComponent();
            Config.ResourceAssembly = typeof(App).Assembly;
            MainPage = new MainPage();
        }

        //protected override void OnStart()
        //{
        //}

        protected override void OnSleep()
            => MessagingCenter.Send(typeof(string), AppStateChangedMessage, AppState.Sleep);

        protected override void OnResume()
            => MessagingCenter.Send(typeof(string), AppStateChangedMessage, AppState.Resume);
    }
}
