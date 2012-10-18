using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Roomie.Web.Models
{
    public class HomeModel
    {
        [Key]
        public int Id { get; set; }
        
        public string Name { get; set; }
        public string Address { get; set; }
        public string Notes { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public virtual ICollection<UserModel> Owners { get; set; }
        public virtual ICollection<UserModel> Guests { get; set; }

        public HomeModel()
        {
            CreationTimestamp = DateTime.UtcNow;
        }
    }
}
