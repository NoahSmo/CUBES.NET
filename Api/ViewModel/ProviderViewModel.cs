using System;
using System.ComponentModel.DataAnnotations;

namespace Api.ViewModels
{
    public class ProviderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
