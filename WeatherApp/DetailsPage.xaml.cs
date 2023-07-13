using WeatherApp.services;

namespace WeatherApp;

public partial class DetailsPage : ContentPage
{
    
    public DetailsPage(Models.List selectedForecast)
    {
        InitializeComponent();


        LBLCity.Text = "";
        LBLDescription.Text = "";
        LBLSelectedDate.Text = Convert.ToString(selectedForecast.main.dt);
        LBLTemperature.Text = selectedForecast.main.temperature + "°C";
        LBLHumidity.Text = selectedForecast.main.humidity + "%";
        LBLSunrise.Text = Convert.ToString(selectedForecast.main.sunrise);
        LBLSunset.Text = Convert.ToString(selectedForecast.main.sunset);
        LBLFeelsLike.Text = Convert.ToString(selectedForecast.main.feels_like);
        LBLPressure.Text  = Convert.ToString(selectedForecast.main.pressure);
        LBLSeaLevel.Text = Convert.ToString(selectedForecast.main.sea_level);
        LBLGroundLevel.Text = Convert.ToString(selectedForecast.main.grnd_level);


    }

    private void BtnBack_Clicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new WeatherPage());
    }


}