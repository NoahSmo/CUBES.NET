using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Role : Auditable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        
        
        public virtual List<Permission> Permissions { get; set; }
    }
}