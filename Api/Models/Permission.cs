using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Permission : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        
        
        public virtual List<Role>? Roles { get; set; }
    }
}