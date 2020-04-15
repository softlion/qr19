using System;
using System.ComponentModel;
using Qr19.ViewModels;
using Xamarin.Forms;

namespace Qr19.Views
{
    [DesignTimeVisible(false)]
    public partial class ScanPage : ContentPage, IDisposable
    {
        ScanViewModel viewModel;

        public ScanPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new ScanViewModel();

            //Scanner.IsScanning = true;
            //Scanner.IsTorchOn = true; //Marche pas sur android ?
        }

        public void Dispose()
        {
            BindingContext = null;
            viewModel.Dispose();
            viewModel = null;
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();

        //    if (viewModel.Items.Count == 0)
        //        viewModel.IsBusy = true;
        //}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            viewModel.OnDisappearing();
        }
    }
}
