using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinCapstoneProj.Pages;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;
using XamarinCapstoneProj.Models;
using Newtonsoft.Json;

namespace XamarinCapstoneProj.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductListPage : ContentPage
    {
        public ProductListPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            //Get FilePath and initialize the file
            string dataFilePath = Path.Combine(FileSystem.AppDataDirectory, "ProductListData.json");
            //Create Collection Model for Data
            List<Products> productList = new List<Products>();
            // i) Yes, previously saved data exists then initialize it
            string currentDataJson = File.ReadAllText(dataFilePath);
            // ii) Load previously saved data into a list --> productList(List Model) variable
            productList = JsonConvert.DeserializeObject<List<Products>>(currentDataJson);
        }

        private async void AddNewProductButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductCreatePage());
        }
    }
}