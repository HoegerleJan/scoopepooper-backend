using scoopepooper_backend.EfCore;

namespace scoopepooper_backend.Model
{
    public class EntryModel
    {
        public int id { get; set; }
        public int user_Id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string nickname { get; set; } = string.Empty;
    }
}
