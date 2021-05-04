using System;
using System.Collections.Generic;
using System.Text;

using XamarinCapstoneProj.Helpers.Validators;
using XamarinCapstoneProj.Helpers.Validators.Rules;
using XamarinCapstoneProj.Models;
using Xamarin.Forms;
using System.ComponentModel;
using System.Windows.Input;

namespace XamarinCapstoneProj.Models.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        public ValidatableObject<int> pvmId { get; set; } = new ValidatableObject<int>();
        public ValidatableObject<string> pvmName { get; set; } = new ValidatableObject<string>();
        public ValidatableObject<Decimal> pvmPrice { get; set; } = new ValidatableObject<Decimal>();
        public ValidatableObject<string> pvmDesc { get; set; } = new ValidatableObject<string>();

        public ProductsViewModel(Products prod)
        {

            // let me fix my mic again
            //pvmId.Value = prod.Id;
            pvmName.Value = prod.ProductName;
            pvmDesc.Value = prod.Description;
            pvmPrice.Value = prod.Price;

            AddValidationRules();
        }

        public void AddValidationRules()
        {
            //Empty Null Field Checker
            pvmId.Validations.Add(new IsNotNullOrEmptyRule<int> { ValidationMessage = "Id is required" });
            pvmName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Product Name is required" });
            pvmPrice.Validations.Add(new IsNotNullOrEmptyRule<Decimal> { ValidationMessage = "Price is required" });
            pvmDesc.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Product Description is required" });

            //Character Count
            pvmId.Validations.Add(new IsLengthValidRule<int> { ValidationMessage = "Product ID can only be 0001 to 9999", MaximunLength = 1, MinimunLength = 4 });
            pvmName.Validations.Add(new IsLengthValidRule<string> { ValidationMessage = "Product name can't be less than 3 Char or more than 50 Characters", MaximunLength = 3, MinimunLength = 50 });
            pvmDesc.Validations.Add(new IsLengthValidRule<string> { ValidationMessage = "Product description must be between 10 to 500 characters", MaximunLength = 10, MinimunLength = 500 });

            //Valid Pricing
            pvmPrice.Validations.Add(new HasValidPriceRule<Decimal> { ValidationMessage = "Product pricing can only be between $1.00 to $300.00" });

        }

        //Method that runs on Creation Add Clicked
        public ICommand CreateCommand =>new Command(async () =>
       {
           if (AreFieldsValid())
           {
               await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
           }
       });

        //Calls Method that run validations and return to Creation Add Method
        bool AreFieldsValid()
        {
            bool isProductIdValid = pvmId.Validate();
            bool isProductNameValid = pvmName.Validate();
            bool isProductPriceValid = pvmPrice.Validate();
            bool isProductDescValid = pvmDesc.Validate();

            return isProductIdValid && isProductNameValid && isProductPriceValid && isProductDescValid;

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


}
