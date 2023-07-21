using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BusTracking.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {

            var d = UpdateStatus(31.276743847868442, 34.270813085783786);
            ViewBag.k = d.ToString();
            return View();
        }



        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double earthRadius = 6371; // Radius of the Earth in kilometers

            var dLat = DegreeToRadian(lat2 - lat1);
            var dLon = DegreeToRadian(lon2 - lon1);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(DegreeToRadian(lat1)) * Math.Cos(DegreeToRadian(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var distance = earthRadius * c;
            return distance;
        }

        private static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }



        public static double UpdateStatus(double currentLatitude, double currentLongitude)
        {

            var targetLatitude = 31.27652717679175;
            var targetLongitude = 34.27047044601915;
            var thresholdDistance = 0.5; // Distance in kilometers

            var distance = CalculateDistance(currentLatitude, currentLongitude, targetLatitude, targetLongitude);

            //if (distance <= thresholdDistance)
            //{
            //    // Vehicle is approaching the target point
            //    // Perform necessary actions or updates here
            //    // Example: update the status, send notifications, etc.
            //}
            //else
            //{
            //    // Vehicle is not yet approaching the target point
            //}
            return distance;
        }







    }
}