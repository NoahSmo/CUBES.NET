using System;
using System.ComponentModel.DataAnnotations;
using Api.Models;

namespace Api.ViewModels
{
    public class StatusViewModel
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public virtual List<Order> Orders { get; set; }

        public StatusViewModel(Status status)
        {
            Id = status.Id;
            Message = status.Message;
            Orders = status.Orders;
        }
    }
}
