using Foundation;
using UIKit;

namespace Qr19.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        /// <summary>
        /// You have 17 seconds to return from this method, or iOS will terminate your application.
        /// </summary>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            global::ZXing.Net.Mobile.Forms.iOS.Platform.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}
