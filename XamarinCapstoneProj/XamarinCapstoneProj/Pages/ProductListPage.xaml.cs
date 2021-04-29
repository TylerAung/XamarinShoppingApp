using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using XamarinCapstoneProj.Pages;
using Xamarin.Forms.Xaml;

namespace XamarinCapstoneProj.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
        {
            InitializeComponent();
        }

        private async void AddNewProductButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductCreatePage());
        }
    }
}