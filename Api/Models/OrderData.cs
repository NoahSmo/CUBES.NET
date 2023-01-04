using System;

namespace Api
{
    public class OrderData
    {
        public int Id { get; set; }
        public int Id_Client { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
        public string Serial { get; set; }
    }
}
