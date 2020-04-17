using System;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Qr19.Lib.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using ZXing;

namespace Qr19.ViewModels
{
    public class ScanViewModel : BaseViewModel, IDisposable
    {
        private bool isScanning, isTorchOn;
        private string prevText;
        private bool isValid;
        private string isValidSvg;
        private bool isClear;
        private QrViewModel model;

        public ICommand ScanResultCommand { get; }
        public ICommand ClearCommand { get; }
        public ICommand OpenMapCommand { get; }
        public ICommand ToggleTorchCommand { get; }
        public bool IsScanning { get => isScanning; set => SetProperty(ref isScanning, value); }
        public bool IsTorchOn { get => isTorchOn; set => SetProperty(ref isTorchOn, value); }


        public bool IsValid { get => isValid; set => SetProperty(ref isValid, value); }
        public string IsValidSvg { get => isValidSvg; set => SetProperty(ref isValidSvg, value); }
        public bool IsClear { get => isClear; set => SetProperty(ref isClear, value); }

        public QrViewModel Model { get => model; set => SetProperty(ref model, value); }

        public ScanViewModel()
        {
            Title = "QR COVID 19";
            isScanning = true;
            isClear = true;

            ClearCommand = new Command(() =>
            {
                prevText = null;
                Model = null;
                IsValidSvg = null;
                IsClear = true;
            });

            ToggleTorchCommand = new Command(() => IsTorchOn = !isTorchOn);

            var isInResult = false;
            ScanResultCommand = new Command<Result>(qr =>
            {
                if (isInResult
                    || qr.BarcodeFormat != BarcodeFormat.QR_CODE
                    || String.IsNullOrWhiteSpace(qr.Text) || qr.Text == prevText
                    )
                    return;

                isInResult = true;
                prevText = qr.Text;

                try
                {
                    var culture = CultureInfo.GetCultureInfo("FR-fr");

                    var textItems = qr.Text.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(item => item.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries))
                        .Where(s => s.Length == 2 && s[0] != null && s[1] != null)
                        .ToDictionary(s => s[0].Trim(), s => s[1].Trim());

                    var qrModel = new QrViewModel
                    {
                        DateCreated = DateTime.Parse(textItems["Cree le"].Replace(" ", "").Replace("a", " ").Replace("h", ":"), culture),
                        DateLeaveHome = DateTime.Parse(textItems["Sortie"].Replace(" ", "").Replace("a", " ").Replace("h", ":"), culture),
                        FirstName = textItems["Prenom"],
                        LastName = textItems["Nom"],
                        Address = textItems["Adresse"],
                        //BirthInfo = textItems["Naissance"],
                        Reasons = textItems["Motifs"].Split('-').ToList(),
                    };

                    var birthInfo = textItems["Naissance"];
                    qrModel.BirthDate = DateTime.Parse(birthInfo.Substring(0, birthInfo.IndexOf(' ')), culture);
                    qrModel.BirthPlace = birthInfo.Substring(birthInfo.IndexOf(' '));
                    qrModel.BirthPlace = qrModel.BirthPlace.Substring(qrModel.BirthPlace.IndexOf('a')+2);

                    var isTimeInvalid = (qrModel.Reasons.Count == 1 && qrModel.Reasons.Contains("sport") && (DateTime.Now - qrModel.DateLeaveHome).TotalHours > 1)
                                        || qrModel.DateCreated.Date != DateTime.Now.Date;

                    //Start searching location
                    //https://geo.api.gouv.fr/adresse
                    //https://api-adresse.data.gouv.fr/search/?q=8+bd+du+port

                    Device.InvokeOnMainThreadAsync(() =>
                    {
                        try
                        {
                            IsClear = false;

                            Model = qrModel;
                            IsValid = !isTimeInvalid;
                            IsValidSvg = $"res:images.{(isValid ? "ok" : "nok")}";

                            //TODO: sound ok or nok if isValidSvg has a value

                            isInResult = false;
                        }
                        catch (Exception e)
                        {
                            isInResult = false;
#if DEBUG
                            throw;
#endif
                        }
                    });
                }
                catch (Exception e)
                {
                    isInResult = false;
#if DEBUG
                    throw;
#endif
                }
            });

            OpenMapCommand = new Command(() =>
            {
                if (model != null)
                {
                    var uri = "https://www.google.com/maps/dir/?api=1&origin=position+actuelle&dir_action=preview&destination=" + Uri.EscapeDataString(model.Address);
                    Browser.OpenAsync(uri);
                }
            });
        }


        public void OnAppearing()
        {
            IsScanning = true;
        }

        public void OnDisappearing()
        {
            IsScanning = false;
        }
        public void Dispose()
        {
            IsScanning = false;
        }
    }
}
