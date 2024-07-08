using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace scoopepooper_backend.EfCore
{
    [Table("entry")]
    public class Entry
    {
        [Key, Required]
        public int id { get; set; }
        public virtual User User { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string nickname { get; set; } = string.Empty;
    }
}
