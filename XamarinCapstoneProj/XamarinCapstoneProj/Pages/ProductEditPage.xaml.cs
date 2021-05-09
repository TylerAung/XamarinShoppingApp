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

        private void BtnUpdate_Clicked(object sender, EventArgs e)
        {
            //Collect & Initialize Data From Form
            int prodId = Convert.ToInt32(this.LblEditId.Text);
            string prodName = this.ProductNameEntry.Text;
            string prodDesc = this.ProductDescEntry.Text;

            Decimal prodPrice = Convert.ToDecimal(this.ProductPriceEntry.Text);

            if (prodPrice >= 1 && prodPrice <=300)
            {
                var UpdateProd = new Products
                {
                    Id = prodId,
                    ProductName = prodName,
                    Description = prodDesc,
                    Price = prodPrice
                };

                helpUpdate(UpdateProd);
            }
            else
            {
                //Ternary Operator is ultimately an Assignment Operator
                this.ErrName.Text = !string.IsNullOrEmpty(prodName) ?  string.Empty : "Value Can't be Empty";
                this.ErrDesc.Text = !string.IsNullOrEmpty(prodDesc) ? string.Empty : "Value Can't be Empty";
                if (prodPrice.Equals(null))
                {
                    this.ErrPrice.Text = "Value Can't Be Empty";
                }
                else { 
                this.ErrPrice.Text = prodPrice >= 1 && prodPrice <= 300 ? string.Empty : "Value Can't be Less than 0 or More than 300";
                }
            }
        }

        private async void helpUpdate(Products prod)
        {
            if (!string.IsNullOrEmpty(prod.ProductName) &&
                !string.IsNullOrEmpty(prod.Description)
                )
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


                    // update the course object in list
                    productList.First(x => x.Id == prod.Id).ProductName = prod.ProductName;
                    productList.First(x => x.Id == prod.Id).Description = prod.Description;
                    productList.First(x => x.Id == prod.Id).Price = prod.Price;

                    // save course list data
                    string productListJson = JsonConvert.SerializeObject(productList);
                    File.WriteAllText(dataFilePath, productListJson);

                    // remove the previous page, to go back to collections Page
                    Page viewPage = this.Navigation.NavigationStack.First(x => x.GetType() == typeof(ProductViewPage));
                    this.Navigation.RemovePage(viewPage);

                    //productList.FirstOrDefault
                    await this.Navigation.PopAsync();
                }
            }
        }
    }
}