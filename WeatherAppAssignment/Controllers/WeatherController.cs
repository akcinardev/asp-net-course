using Microsoft.AspNetCore.Mvc;
using WeatherAppAssignment.Models;

namespace WeatherAppAssignment.Controllers
{
    public class WeatherController : Controller
    {
        [Route("/")]
        public IActionResult GetAll()
        {
            List<CityWeather> cityWeathers = new List<CityWeather>()
            {
                new CityWeather(){ CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33},
                new CityWeather(){ CityUniqueCode = "NYC", CityName = "New York City", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60},
                new CityWeather(){ CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82},
            };
            return View(cityWeathers);
        }

        [Route("/weather/{cityCode}")]
        public IActionResult GetOne(string cityCode)
        {
            List<CityWeather> cityWeathers = new List<CityWeather>()
            {
                new CityWeather(){ CityUniqueCode = "LDN", CityName = "London", DateAndTime = Convert.ToDateTime("2030-01-01 8:00"),  TemperatureFahrenheit = 33},
                new CityWeather(){ CityUniqueCode = "NYC", CityName = "New York City", DateAndTime = Convert.ToDateTime("2030-01-01 3:00"),  TemperatureFahrenheit = 60},
                new CityWeather(){ CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = Convert.ToDateTime("2030-01-01 9:00"),  TemperatureFahrenheit = 82},
            };

            CityWeather selectedCity = cityWeathers.Where(city => city.CityUniqueCode == cityCode).FirstOrDefault();

            return View(selectedCity);
        }
    }
}
