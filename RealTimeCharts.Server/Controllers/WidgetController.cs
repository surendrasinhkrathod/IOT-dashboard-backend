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
    public class WidgetController : ControllerBase
    {
        private readonly IHubContext<WidgetHub> _hubWidget;
        private readonly TimerManager _timer;

        public WidgetController(IHubContext<WidgetHub> hubWidget, TimerManager timer)
        {
            _hubWidget = hubWidget;
            _timer = timer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var random = new Random();

            // Ensure the timer starts only once for widget data
            if (!_timer.IsTimerStarted)
            {
                _timer.PrepareWidgetTimer(() =>
                {
                    var widgetData = new
                    {
                        humidity = random.Next(30, 90),
                        pressure = random.Next(950, 1050),
                        aqi = random.Next(0, 500),
                        lightintensity = random.Next(100, 10000)
                    };

                    // Send widget data via SignalR
                    _hubWidget.Clients.All.SendAsync("TransferWidgetData", widgetData);
                });
            }

            return Ok(new { Message = "Widget data streaming started" });
        }
    }
}
