using Newtonsoft.Json;
using WeatherApp.Models;


namespace WeatherApp.services
{
    public static class APIService
    {

        public static async Task<Root> getWeatherByLocation(double latitude,double longitude)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync(String.Format("http://api.openweathermap.org/data/2.5/forecast?lat={0}&lon={1}&units=metric&appid=0e7498926d6982015d3875ece785419d", latitude,longitude));
            return JsonConvert.DeserializeObject<Root>(response);
        }   

        public static async Task<Root> getWeather(string city)
        {    
            var httpClient = new HttpClient();
            if(city != null)
            {
                var response = await httpClient.GetStringAsync(String.Format("https://api.openweathermap.org/data/2.5/forecast?q={0}&units=metric&appid=0e7498926d6982015d3875ece785419d", city));
                return JsonConvert.DeserializeObject<Root>(response);
            }
            else
            {
                return null;
            }
            
           
        }
    }
}
