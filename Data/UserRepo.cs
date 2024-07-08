using scoopepooper_backend.EfCore;
using scoopepooper_backend.Model;

namespace scoopepooper_backend.Data
{
    public class UserRepo
    {
        private EF_DataContext _context;
        public UserRepo(EF_DataContext context)
        {
            _context = context;
        }

        public IQueryable<UserModel> GetAll()
        {
            List<UserModel> response = new List<UserModel>();
            var dataList = _context.Users.ToList();
            dataList.ForEach(row => response.Add(new UserModel()
            {
                id = row.id,
                editkey = row.editkey,
            }));
            return response.AsQueryable();
        }
        public UserModel Get(int id)
        {
            UserModel response = new UserModel();
            var row = _context.Users.Where(d => d.id.Equals(id)).FirstOrDefault();
            return new UserModel()
            {
                id = row.id,
                editkey= row.editkey,
            };
        }
        public void Delete(int id) 
        {
            var user = _context.Users.Where(d => d.id.Equals(id)).FirstOrDefault();
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public int Create(UserModel model)
        {
            User dbTable = new User();
            dbTable.editkey = model.editkey;
            var obj = _context.Users.Add(dbTable);
            _context.SaveChanges();
            return obj.Entity.id;
        }
    }
}
