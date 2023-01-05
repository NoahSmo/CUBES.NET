using System;

namespace Api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int Id_Client { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Serial { get; set; }
    }
}
