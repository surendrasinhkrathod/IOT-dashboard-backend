using RealTimeCharts.Server.Models;
using System;

namespace RealTimeCharts.Server.DataStorage
{
    public class DataManager
    {
        public static List<ChartModel> GetData()
        {
            var r = new Random();
            return new List<ChartModel>()
            {
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data1", BackgroundColor = "#5491DA" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data2", BackgroundColor = "#E74C3C" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data3", BackgroundColor = "#82E0AA" },
                new ChartModel { Data = new List<int> { r.Next(1, 40) }, Label = "Data4", BackgroundColor = "#E5E7E9" }
            };
        }

        public static dynamic GetChartData() {
            return new
            {
                labels = new[] { "January", "February", "March", "April", "May", "June", "July" },
                datasets = new[]
                {
                    new
                    {
                        label = "GitHub Commits",
                        backgroundColor = "#f87979",
                        data = GenerateRandomData()
                    }
                }
            };
        }

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
                random.Next(10, 100)
            };
        }
    }
}
