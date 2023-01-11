using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Provider
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
