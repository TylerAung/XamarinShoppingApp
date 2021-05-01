using System;
using System.Collections.Generic;
using System.Text;

using XamarinCapstoneProj.Helpers.Validators;
using XamarinCapstoneProj.Helpers.Validators.Rules;
using XamarinCapstoneProj.Models;
using Xamarin.Forms;
using System.ComponentModel;

namespace XamarinCapstoneProj.Models.ViewModels
{
    public class ProductsViewModel : INotifyPropertyChanged
    {
        public ValidatableObject<string> ProductName { get; set; } = new ValidatableObject<string>();

        public ProductsViewModel()
        {
            AddValidationRules();
        }

        public void AddValidationRules()
        {

        }

        public ICommand SignUpCommand => Command(async () =>
       {
           if (AreFieldsValid())
           {
               await App.Current.MainPage.DisplayAlert("Welcome", "", "Ok");
           }
       });

        bool AreFieldsValid()
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }


}
