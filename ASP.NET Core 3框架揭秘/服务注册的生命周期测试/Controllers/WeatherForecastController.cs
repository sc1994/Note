using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using 服务注册的生命周期测试.Models;
using 服务注册的生命周期测试.Services;

namespace 服务注册的生命周期测试.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger,
            ScopedModel scopedModel,
            SingletonModel singletonModel,
            TransientModel transientModel,
            ScopedService scopedService,
            SingletonService singletonService,
            TransientService transientService)
        {
            _logger = logger;
            System.Console.WriteLine(transientService.ToString());
            System.Console.WriteLine(transientModel.ToString());
            System.Console.WriteLine("====================");
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
