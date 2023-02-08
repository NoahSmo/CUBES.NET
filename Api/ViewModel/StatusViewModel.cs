using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class StatusViewModel
    {
        public string Message { get; set; }
        public virtual List<Order> Orders { get; set; }

        public StatusViewModel(Status status)
        {
            Message = status.Message;
            Orders = status.Orders;
        }
    }
}
