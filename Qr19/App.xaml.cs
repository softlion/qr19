using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Qr19.Views;
using XamSvg.Shared;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Qr19
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            XamSvg.Shared.Config.ResourceAssembly = typeof(App).Assembly;
#if DEBUG
            XamSvg.Shared.Config.NativeLogger = new SvgLogger();
#endif
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }

    class SvgLogger : ILogger
    {
        public bool TraceEnabled { get; set; } = true;

        public void Trace(Func<string> s, bool traceEnabled = true, string method = null, int lineNumber = 0)
        {
            //if (TraceEnabled && traceEnabled)
                Console.WriteLine($"SvgTrace {method}:{lineNumber}: {s()}");
        }
    }
}
