using Android.Content;
using Qr19.Droid.Lib;
using Qr19.Lib;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms.Android;

[assembly: ExportRenderer(typeof(QrScannerView), typeof(QrScannerViewRenderer))]

namespace Qr19.Droid.Lib
{
    public class QrScannerViewRenderer : ZXingScannerViewRenderer
    {
        public QrScannerViewRenderer(Context context) : base(context)
        {
        }
    }
}