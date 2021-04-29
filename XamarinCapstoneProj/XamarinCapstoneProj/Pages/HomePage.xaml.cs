using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinCapstoneProj.Models;
using Newtonsoft.Json;
using System.Net.Http;
using XamarinCapstoneProj.Pages;

namespace XamarinCapstoneProj
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                await GetDeviceLocation();

                // double lattitude = 1.3521;
                // double longitude = 103.8198;
                // await this.fGetLocationInfoFromApiAsync(lattitude, longitude);

                GetGreetingText();

            }
            catch (FeatureNotSupportedException)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException ex)
            {
                // Handle permission exception
                await DisplayAlert("Permission Problem", $"{ex.Message}", "ok");
            }
            catch (Exception)
            {
                // any other exception, like Unable to get location
            }
            finally
            {
                //if (string.IsNullOrEmpty(this.GeoLocationLabel01.Text))
                //{
                //    this.GeoLocationLabel01.Text = "unable to get info";
                //}
                //if (string.IsNullOrEmpty(this.GeoLocationLabel02.Text))
                //{
                //    this.GeoLocationLabel02.Text = "unable to get info";
                //}
            }
        }

        private string GetGreetingText()
        {
            if (DateTime.Now.Hour < 6 && DateTime.Now.Hour > 12)
            {
                this.LblGreetings.Text = "Good Morning To You";
            }
            else if (DateTime.Now.Hour < 12 && DateTime.Now.Hour > 17)
            {
                this.LblGreetings.Text = "Good Afternoon To You";
            }
            else if (DateTime.Now.Hour < 17 && DateTime.Now.Hour > 19)
            {
                this.LblGreetings.Text = "Good Evening To You";
            }
            else
            {
                this.LblGreetings.Text = "Good Night To You";
            }
            return null;
        }


        //private async Task fGetDeviceLocationInfoAsync()
        //{
        //    var deviceLocation = await Geolocation.GetLocationAsync();

        //    if (deviceLocation != null)
        //    {
        //        var latitude = deviceLocation.Latitude;
        //        var longitude = deviceLocation.Longitude;

        //        //this.GeoLocationLabel01.Text
        //        //    = $"Latitude: {latitude}\n"
        //        //    + $"Longitude: {longitude}\n"
        //        //    + $"Altitude: {deviceLocation.Altitude}\n"
        //        //    + $"Timestamp: {deviceLocation.Timestamp}";

        //        await fGetLocationInfoFromApiAsync(latitude, longitude);
        //    }
        //}

        //public async Task fGetLocationInfoFromApiAsync(double lattitude, double longitude)
        //{
        //    var apiUrl = "https://wedra.azurewebsites.net/api/location/search";

        //    var locationDataUrl = $"{apiUrl}?lattlong={lattitude},{longitude}";
        //    HttpClient client = new HttpClient();
        //    string locationDataResult
        //        = await client.GetStringAsync(locationDataUrl);
        //    var locationData
        //        = JsonConvert.DeserializeObject<MyLocationDataModel>(locationDataResult);
        //    //this.GeoLocationLabel02.Text
        //    //    = $"Distance: {locationData.Distance}\n"
        //    //      + $"Location: {locationData.Location}\n"
        //    //      + $"Location Type: {locationData.LocationType}\n"
        //    //      + $"Woeid: {locationData.Woeid}\n"
        //    //      + $"LattLong: {locationData.LattLong}";
        //}


        private async Task<Location> GetDeviceLocation()
        {
            // TODO
            var deviceLocation = await Geolocation.GetLastKnownLocationAsync();
            if (deviceLocation != null)
            {
                SetLocationData(deviceLocation);
                await SetWeatherData(deviceLocation);
            }
            
            //SetWeatherData(deviceLocation);
            return null;
        }

        private async Task SetWeatherData(Location deviceLocation)
        {
            try
            {

            
            var locationDataUrl = $"https://wedra.azurewebsites.net/api/location/search?lattlong={deviceLocation.Latitude},{deviceLocation.Longitude}";
            HttpClient client = new HttpClient();
            string locationDataResult = await client.GetStringAsync(locationDataUrl);
            var locationData = JsonConvert.DeserializeObject<WeatherLocationData>(locationDataResult);

            var locationWeatherDataUrl = $"https://wedra.azurewebsites.net/api/location/{locationData.Woeid}"; ;
            string locationWeatherDataResult = await client.GetStringAsync(locationWeatherDataUrl);
            var locationWeatherData = JsonConvert.DeserializeObject<WeatherData>(locationWeatherDataResult);

                this.LblWeatherLocation.Text = $"Weather in {locationData.Title} of Lattitude x Longtitude {locationData.LattLong}";
                this.WeatherDataGrid.IsVisible = true;

                for (int i = 0; i < 3; i++)
                {
                    var weatherStateName = locationWeatherData.ConsolidatedWeather[i].WeatherStateName;
                    var weatherTemp = Math.Floor(locationWeatherData.ConsolidatedWeather[i].TheTemp);
                    var maxTemp = Math.Floor(locationWeatherData.ConsolidatedWeather[i].MaxTemp);
                    var minTemp = Math.Floor(locationWeatherData.ConsolidatedWeather[i].MinTemp);

                    //Application.Current.Resources[typeof(WSDa)]

                    if (i == 0)
                    {
                        this.LblTodayWS.Text = $"Weather Forecast: {weatherStateName} ({weatherTemp}°C)";
                        this.LblTodayTemp.Text = $"Temp: {minTemp}°C - {maxTemp}°C";
                    }
                    else if (i == 1)
                    {
                        this.LblTomorrowWS.Text = $"Weather Forecast: {weatherStateName} ({weatherTemp}°C)";
                        this.LblTomorrowTemp.Text = $"Temp: {minTemp}°C - {maxTemp}°C";
                    }
                    else
                    {
                        this.LblFollowingWS.Text = $"Weather Forecast: {weatherStateName} ({weatherTemp}°C)";
                        this.LblFollowingTemp.Text = $"Temp: {minTemp}°C - {maxTemp}°C";
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void SetLocationData(Location deviceLocation)
        {
            try
            {

            
            Location MartLocation = new Location(1.334713, 103.909060);
            double distance = Location.CalculateDistance(
                           deviceLocation,
                           MartLocation,
                           DistanceUnits.Kilometers);

            this.LblLocation.Text = "You are " + Math.Floor(distance) + " km away from the Mart!";
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async void ProductsBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductListPage());
        }
    }
}