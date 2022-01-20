using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PenggunaAPI.Location
{
    public class LocationHelper
    {
        public static Task<float> GetDistance(double penggunaLat, double penggunaLong, 
            double tujuanLat, double tujuanLong)
        {
            var d1 = penggunaLat * (Math.PI / 180.0);
            var num1 = penggunaLong * (Math.PI / 180.0);
            var d2 = tujuanLat * (Math.PI / 180.0);
            var num2 = tujuanLong * (Math.PI / 180.0) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) +
                     Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0);
            var distance = 6378.137 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
            float result = (float)distance;
            return Task.FromResult(result);
        }
    }
}