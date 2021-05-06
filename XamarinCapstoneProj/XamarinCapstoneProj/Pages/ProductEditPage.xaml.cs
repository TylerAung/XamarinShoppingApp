using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCapstoneProj.Models;

namespace XamarinCapstoneProj.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductEditPage : ContentPage
    {
        public ProductEditPage(Products prod)
        {
            InitializeComponent();
            // this.LblEditName.Text = 
            // _prod.Id = edit
            this.LblEditId.Text = Convert.ToString(prod.Id);
            this.ProductNameEntry.Text = prod.ProductName;
            this.ProductDescEntry.Text = prod.Description;
            this.ProductPriceEntry.Text = Convert.ToString(prod.Price);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           // this.LblEditName.Text = 
        }

        private async void BtnBack_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private async void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            string dataFilePath = Path.Combine(FileSystem.AppDataDirectory, "ProductListData.json");
            if (File.Exists(dataFilePath))
            {
                //Create Collection Model for Data
                List<Products> productList = new List<Products>();
                // i) Yes, previously saved data exists then initialize it
                string currentDataJson = File.ReadAllText(dataFilePath);
                // ii) Load previously saved data into a list --> productList(List Model) variable
                productList = JsonConvert.DeserializeObject<List<Products>>(currentDataJson);

                //productList.FirstOrDefault
                await this.Navigation.PopAsync();
            }
            else
            {
                await this.Navigation.PopAsync();

            }
        }
    }
}