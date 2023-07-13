
using System.Text.RegularExpressions;
using WeatherApp.services;

namespace WeatherApp;

public partial class WeatherPage : ContentPage
{
    public List<Models.List> WeatherList;
    private double latitude;
    private double longitude;

    public WeatherPage()
	{
		InitializeComponent();
        WeatherList = new List<Models.List>();

        
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await GetLocation();
        await GetWeatherDetailsByLocation(latitude, longitude);
        

    }

    public async Task GetWeatherDetailsByLocation(double latitude, double longitude)
    {
        var result = await APIService.getWeatherByLocation(latitude, longitude);
        updateUI(result);
    }

    private async void ImageButton_Clicked(object sender, EventArgs e)
    {
        var response = await DisplayPromptAsync(title: "", message: "", placeholder: "Search Weather by City", accept: "Search", cancel: "Cancel");

        if (IsValidLocation(response) == true)
        {
            await GetWeatherDataBySearch(response);
        }
        else
        {
            await DisplayAlert("Error","Please enter a valid City Name","OK");
           
        }
    }


   
    public async Task GetWeatherDataBySearch(string city)
    {
        var result = await APIService.getWeather(city);
        updateUI(result);


    }

    public void updateUI(dynamic result)
    {
        foreach (var item in result.list)
        {
            WeatherList.Add(item);
        }

        WeatherView.ItemsSource = WeatherList;
        LBLCity.Text = result.city.name;
        LBLDescription.Text = result.list[0].weather[0].description;
        LBLTemperature.Text = result.list[0].main.temperature + "°C";
        LBLHumidity.Text = result.list[0].main.humidity + "%";
        LBLWind.Text = result.list[0].wind.speed + "km/h";
        ImgWeatherIcon.Source = result.list[0].weather[0].iconUrl;
    }


    public async void OnItemSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Models.List selectedForecast)
        {
            
            await Navigation.PushAsync(new DetailsPage(selectedForecast));
            WeatherView.SelectedItem = null;
        }
    }


    public async Task GetLocation()
    {
        Location location = await Geolocation.Default.GetLastKnownLocationAsync();
        latitude = location.Latitude;
        longitude = location.Longitude;

    }

    public async void TapLocation_Tapped(object sender, EventArgs e)
    {     
        await GetLocation();
        await GetWeatherDetailsByLocation(latitude, longitude);
    }

    private bool IsValidLocation(string city)
    {
        
        if (string.IsNullOrWhiteSpace(city))
        {
            return false;
        }

       
        var invalidCharactersPattern = @"[^A-Za-z\s-]";
        if (Regex.IsMatch(city, invalidCharactersPattern))
        {
            return false;
        }

        var supportedLocations = new List<string> { "Arcadia", "Rosslyn", "Pretoria","Polokwane", "Giyani", "Tzaneen" }; 
        if (!supportedLocations.Contains(city))
        {
            return false;
        }


        return true;
    }

   

}