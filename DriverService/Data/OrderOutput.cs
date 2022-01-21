using DriverService.Models;
using System;

namespace DriverService.Data
{
    public class OrderOutput
    {
        public OrderOutput(Order order)
        {
            DriverId = (int)order.DriverId;
            PenggunaId = order.PenggunaId;
            Status = order.Status;
            Created = order.Created;
        }

        public int DriverId { get; set; }
        public int PenggunaId { get; set; }
        public string Status { get; set; }
        public DateTime Created { get; set; }
    }
}
