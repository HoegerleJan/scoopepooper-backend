using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scoopepooper_backend.EfCore
{
    [Table("user")]
    public class User
    {
        [Key, Required]
        public int id { get; set; }
        public int editkey { get; set; }
    }
}
