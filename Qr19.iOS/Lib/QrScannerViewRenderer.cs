using System;
using System.ComponentModel;
using Qr19.iOS.Lib;
using Qr19.Lib;
using Xamarin.Forms;

[assembly: ExportRenderer(typeof(QrScannerView), typeof(QrScannerViewRenderer))]

namespace Qr19.iOS.Lib
{
    public class QrScannerViewRenderer : ZXing.Net.Mobile.Forms.iOS.ZXingScannerViewRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            if (args.PropertyName == nameof(Element.IsTorchOn))
            {
                // THIS IS A NO-OP BY THE LIBRARY. DO IT HERE. https://github.com/Redth/ZXing.Net.Mobile/issues/245
                //if (!Element.HasTorch) return; //Always false..
                var on = Element.IsTorchOn;
                var device = AVFoundation.AVCaptureDevice.GetDefaultDevice(AVFoundation.AVMediaType.Video);
                if (device == null || on == device.TorchActive) 
                    return;

                var e = new {Occurred = false, Message = string.Empty};
                try
                {
                    if (!device.LockForConfiguration(out var error))
                        e = new {Occurred = true, Message = error?.LocalizedDescription};
                    else
                    {
                        if (on)
                        {
                            try
                            {
                                if (!device.SetTorchModeLevel(1, out error))
                                    e = new { Occurred = true, Message = error?.LocalizedDescription };
                            }
                            catch (Exception exception)
                            {
                                e = new { Occurred = true, exception.Message };
                            }
                        }
                        else
                            device.TorchMode = AVFoundation.AVCaptureTorchMode.Off;

                        device.UnlockForConfiguration();
                    }
                }
                catch (Exception exception)
                {
                    e = new {Occurred = true, exception.Message};
                }
                finally
                {
                    if (e.Occurred)
                    {
                        Console.WriteLine(e.Message);
                        Element.IsTorchOn = !Element.IsTorchOn; // something went wrong, restore the value
                    }
                }
            }
            else
            {
                base.OnElementPropertyChanged(sender, args);
            }
        }
    }
}