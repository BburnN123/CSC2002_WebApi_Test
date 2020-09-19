using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace TP_WebAPI.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public String Get([FromQuery(Name ="token")] string token)
        {
            UserModel us = new UserModel();
            bool check = false;
            if (String.IsNullOrEmpty(token))
            {
                return "Token required";
            }
            else
            {
                string filename = "user.csv";
                var reader = new StreamReader(System.IO.File.OpenRead(filename));

              
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');
                    if (values[2] == token)
                    {
                        us.username = values[0];
                        us.password = values[1];
                        check = true;
                        break;
                    }
                }

                reader.Close();
            }

            return (check) ? $"{us.username},{us.password}":"Fail";
        }
    }
}
