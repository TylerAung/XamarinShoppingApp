using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCapstoneProj.Models;


namespace XamarinCapstoneProj.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductViewPage : ContentPage
    {
        public ProductViewPage(Products prod)
        {
            InitializeComponent();

            LblProductId.Text = prod.Id.ToString();
            LblProductName.Text = prod.ProductName;
            LblProductDesc.Text= prod.Description;
            LblProductPrice.Text = $"${prod.Price}";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            

        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            bool isDeleteConfirmed =
                await this.DisplayAlert(
                    "Warning!",
                    "Are you sure, you want to delete?",
                    "Yes", "Cancel");

            if (isDeleteConfirmed)
            {
                // Implement Delete data logic
                Remove(Convert.ToInt32(this.LblProductId.Text));
            }
            else
            {
                // Do nothing. Delete operation cancelled by User
            }
        }

        private async void Remove(int ProdId)
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

                //Delete Data from List
                var isRemoveSuccess = productList.Remove(productList.First(x => x.Id == ProdId));

                //Check if still exist
                if (productList.Exists(x => x.Id == ProdId))
                {
                    await this.DisplayAlert("Error!","Fail To Delete","Try Again");
                    return;
                }
                else { 
                //When product don't exist, overwrite to directory
                string productListJson = JsonConvert.SerializeObject(productList);
                File.WriteAllText(dataFilePath, productListJson);
                //Return Back to Collection Page
                await this.Navigation.PopAsync();
                }
            }
            else
            {
                await this.Navigation.PopAsync();
            }
        }

        private async void btnEdit_Clicked(object sender, EventArgs e)
        {
            var editProduct = new Products
            {
                Id = Convert.ToInt32(this.LblProductId.Text),
                ProductName = this.LblProductName.Text,
                Description = this.LblProductDesc.Text,
                Price = Convert.ToDecimal(this.LblProductPrice.Text.Replace("$", String.Empty))

            };
            //Won't work unless done in constructor or onAppearing of class
            //var editPage = new ProductEditPage(editProduct);
            //editPage.BindingContext = editProduct;
            await this.Navigation.PushAsync(new ProductEditPage(editProduct));
        }
    }
}