using Microsoft.AspNetCore.SignalR;
using RealTimeCharts.Server.Models;

namespace RealTimeCharts.Server.HubConfig
{
    public class ChartHub : Hub
    {
        // Broadcast chart data under the "TransferChartData" event
        public async Task BroadcastChartData(List<ChartModel> data)
        {
            await Clients.All.SendAsync("TransferChartData", data);
        }
    }

    public class WidgetHub : Hub
    {
        // Method to send widget data
        public async Task BroadcastWidgetData(object widgetData)
        {
            await Clients.All.SendAsync("TransferWidgetData", widgetData);
        }
    }
}
