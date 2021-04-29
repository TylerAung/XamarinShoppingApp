using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCapstoneProj.Pages;

namespace XamarinCapstoneProj
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        //private async void Demo01Button_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Page01());
        //}

        //private async void Demo02Button_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new Page02());
        //}
    }
}
