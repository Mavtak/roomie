﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Roomie.Web.Persistence.Models
{
    [Table("NetworkGuestModels")]
    public class NetworkGuestModel
    {
        public int Id { get; set; }
        public UserModel User { get; set; }
        public NetworkModel Network { get; set; }
    }
}
