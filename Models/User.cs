using System;
using System.Collections.Generic;

namespace Driver_Company_5._0.Models
{
   
    public partial class User
    {
      
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public string? Email { get; set; }
        public string? Phone { get; set; }

        
        public int? RoleId { get; set; }

    
        public string? FirstName { get; set; }

       
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public DateTime? Birthday { get; set; }
        public bool? IsDeleted { get; set; }
        public string? DeletedReason { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; } 
        public DateTime? DeletedAt { get; set; }

        // =================== LIÊN KẾT (RELATIONSHIPS) ===================

        public virtual ICollection<Livestream> Livestreams { get; set; } = new List<Livestream>();
    
        public virtual Role? Role { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    }
}
