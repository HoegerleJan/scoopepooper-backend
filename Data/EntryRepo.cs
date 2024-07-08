using scoopepooper_backend.EfCore;
using scoopepooper_backend.Model;

namespace scoopepooper_backend.Data
{
    public class EntryRepo
    {
        private EF_DataContext _context;
        public EntryRepo(EF_DataContext context)
        {
            _context = context;
        }

        public IQueryable<EntryModel> GetAll()
        {
            List<EntryModel> response = new List<EntryModel>();
            var dataList = _context.Entries.ToList();
            dataList.ForEach(row => response.Add(new EntryModel()
            {
                id = row.id,
                user_Id = row.User.id,
                first_name = row.first_name,
                last_name = row.last_name,
                nickname = row.nickname,
            }));
            return response.AsQueryable();
        }
        public EntryModel Get(int id)
        {
            EntryModel response = new EntryModel();
            var row = _context.Entries.Where(d => d.id.Equals(id)).FirstOrDefault();
            return new EntryModel()
            {
                id = row.id,
                user_Id = row.User.id,
                first_name = row.first_name,
                last_name = row.last_name,
                nickname = row.nickname,
            };
        }
        public void Delete(int id)
        {
            var entry = _context.Entries.Where(d => d.id.Equals(id)).FirstOrDefault();
            if (entry != null)
            {
                _context.Entries.Remove(entry);
                _context.SaveChanges();
            }
        }

        public void Create(EntryModel model)
        {
            Entry dbTable = new Entry();
            dbTable.User = _context.Users.Where(f => f.id.Equals(model.user_Id)).FirstOrDefault();
            _context.Entries.Add(dbTable);
            _context.SaveChanges();
        }

        public void Update(EntryModel model) 
        {
            Entry dbTable = new Entry();
            if (model.id > 0)
            {
                //PUT
                dbTable = _context.Entries.Where(d => d.id.Equals(model.id)).FirstOrDefault();
                if (dbTable != null)
                {
                    dbTable.first_name = model.first_name;
                    dbTable.last_name = model.last_name;
                    dbTable.nickname = model.nickname;
                }
                _context.SaveChanges();
            }
            else
            {
                Create(model);
            }
        }
    }
}
