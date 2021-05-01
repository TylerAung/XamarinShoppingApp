using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            }
            else
            {
                // Do nothing. Delete operation cancelled by User
            }
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {

        }
    }
}