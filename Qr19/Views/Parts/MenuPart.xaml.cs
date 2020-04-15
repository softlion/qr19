using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Qr19.Views.Parts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPart : Grid
    {
        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(MenuPart));

        public MenuPart()
        {
            InitializeComponent();
        }

        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        //public ImageSource Icon { get => (ImageSource)GetValue(IconImageSourceProperty); set => SetValue(IconImageSourceProperty, value); }
    }
}