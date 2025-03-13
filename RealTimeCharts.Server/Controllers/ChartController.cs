using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using RealTimeCharts.Server.HubConfig;
using RealTimeCharts.Server.TimerFeatures;
using System;

namespace RealTimeCharts.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartController : ControllerBase
    {
        private readonly IHubContext<ChartHub> _hub;
       // private readonly IHubContext<WidgetHub> _hubWidget;
        private readonly TimerManager _timer;

        public ChartController(IHubContext<ChartHub> hub, TimerManager timer)
        {
            _hub = hub;
           // _hubWidget = hubWidget;
            _timer = timer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var random = new Random();

            // Ensure the timer starts only once
            if (!_timer.IsTimerStarted)
            {
                _timer.PrepareChartTimer(() =>
                {
                    var chartData = new
                    {
                        labels = new[] { "Sensor 1", "Sensor 2", "Sensor 3", "Sensor 4", "Sensor 5", "Sensor 6", "Sensor 7", "Sensor 8", "Sensor 9", "Sensor 10" },
                        datasets = new[]
                        {
                            new
                            {
                                label = "Sensor Temperature",
                                backgroundColor = "#f87979",
                                data = GenerateRandomData()
                            }
                        }
                    };

                    // Send chart data via SignalR
                    _hub.Clients.All.SendAsync("TransferChartData", chartData);
                });
            }

            return Ok(new { Message = "Temperature data streaming started" });
        }

        // Helper method to generate random data for chart
        private static int[] GenerateRandomData()
        {
            var random = new Random();
            return new int[]
            {
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100),
                random.Next(10, 100)
            };
        }
    }
}
