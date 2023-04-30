using Registration.Models;

namespace Registration.Services
{
    public class RegisterService : IRegisterService
    {
        private int? _roleid;
        private readonly RegistrationContext _context;
        public RegisterService(RegistrationContext context)
        {
            _context = context;
        }
        public User Register(User user)
        {
            var promocodes = _context.Promocodes.Where(x => x.Promocode1 == user.Promocode);
            if (promocodes.Any())
            {
                user.RoleId = Convert.ToInt16(Roles.Admin);
            }
            else
            {
                user.RoleId = Convert.ToInt16(Roles.User);
            }
            _roleid = user.RoleId;
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }

        public IEnumerable<User> Singin(string email, string password, string? promocode)
        {
            var list = _context.Users.ToList();
            var promocodeslist = _context.Promocodes.ToList().Where(x => x.Promocode1 == promocode);
            var valid = list.Where(x => x.Email == email && x.Password == password);
            if (valid.Any() && promocodeslist.Any())
            {
                _roleid = Convert.ToInt16(Roles.Admin);
                return list;
            }
            else if (valid.Any())
            {
                return valid;
            }
            return null;
        }
        public IEnumerable<User> UpdateUser(string email, string password, User user)
        {
            var valid = _context.Users.ToList().Find(x => x.Password == password && x.Email == email);
            if (valid == null)
            {
                return null;
            }
            var promocodes = _context.Promocodes.Where(x => x.Promocode1 == user.Promocode);
            if (promocodes.Any())
            {
                user.RoleId = Convert.ToInt16(Roles.Admin);
            }
            else
            {
                user.RoleId = Convert.ToInt16(Roles.User);
            }
            _roleid = user.RoleId;
            valid.FirstName = user.FirstName;
            valid.LastName = user.LastName;
            valid.Email = user.Email;
            valid.Password = user.Password;
            valid.PhoneNumber = user.PhoneNumber;
            valid.UserName = user.UserName;
            valid.RoleId = user.RoleId;
            valid.Promocode = user.Promocode;
            _context.SaveChanges();
            if (_roleid == Convert.ToInt16(Roles.Admin))
            {
                return _context.Users.ToList();
            }
            var  resu = _context.Users.ToList().Where(x=>x.Id==valid.Id);
            return resu;
        }
        public string DeleteUser(string email,string password)
        {
            var valid = _context.Users.ToList().Where(x => x.Password == password && x.Email == email); 
            if (!valid.Any())
            {
                return null;
            }
            _context.Users.Remove(valid.FirstOrDefault());
            _context.SaveChanges();
            return "Your Account Successfully Deleted"; 
        }
    }
    public enum Roles
    {
        User=1,
        Admin=2
    }
}
