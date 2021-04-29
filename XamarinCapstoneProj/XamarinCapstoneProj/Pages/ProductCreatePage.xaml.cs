using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCapstoneProj.Models;
using Newtonsoft.Json;
using System.IO;
using Xamarin.Essentials;

namespace XamarinCapstoneProj.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductCreatePage : ContentPage
    {
        public ProductCreatePage()
        {
            InitializeComponent();
        }

        private async void CreateNewProductBtn_Clicked(object sender, EventArgs e)
        {
            //Extract & Convert Values
            int ProductId = Convert.ToInt32(ProductIdEntry.Text);
            string ProductName = ProductNameEntry.Text;
            string ProductDesc = ProductDescEntry.Text;
            Decimal ProductPrice = Convert.ToDecimal(ProductPriceSlider.Value);


            //Validation
            if (!string.IsNullOrEmpty(ProductName) 
                && !string.IsNullOrEmpty(ProductDesc)
                && !ProductPrice.Equals(null)
                )
                {
                Products newProducts = new Products()
                {
                    Id = ProductId,
                    ProductName = ProductName,
                    Price = ProductPrice,
                    Description = ProductDesc
                };

                //Path - System.IO  FileSystem - Xamarin Essentials
                //Create File Path
                string dataFilePath = Path.Combine(FileSystem.AppDataDirectory, "ProductListData.json");
                // Type of Storages for Mobile, XML, TXT, JSON, CompactDatabase (Sql CE)

                //Initialize Collection
                List<Products> productList = new List<Products>();

                //Check for previous save
                if (File.Exists(dataFilePath))
                {
                    // i) Yes, previously saved data exists then initialize it
                    string currentDataJson = File.ReadAllText(dataFilePath);
                    // ii) Load previously saved data into a list --> productList(List Model) variable
                    productList = JsonConvert.DeserializeObject<List<Products>>(currentDataJson);

                    // iii) Append to collection with new data
                    productList.Add(newProducts);
                }
                else
                {
                    // If Collection never existed, create a new file in path and add
                    productList.Add(newProducts);
                }

                //Save the updated collection into file
                string productListJson = JsonConvert.SerializeObject(productList);
                File.WriteAllText(dataFilePath, productListJson);

                //Only return to Collection page after successful add
                await this.Navigation.PopAsync();
            }
            //To tap into resources file created
           //XamarinCapstoneProj.Properties.Resources.Author.;

        }

    }
}