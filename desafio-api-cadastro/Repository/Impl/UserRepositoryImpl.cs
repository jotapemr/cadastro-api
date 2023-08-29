using desafio_api_cadastro.Models;

namespace desafio_api_cadastro.Repository.Impl
{
    public class UserRepositoryImpl : IuserRepository
    {
        private readonly ApiContextModels _context;

        public UserRepositoryImpl(ApiContextModels context)
        {
            _context = context;
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public User GetUsuarioPorLoginSenha(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.email == email && u.password == password);
        }

        public void Save(User user)
        {
            _context.Add(user);
            _context.SaveChanges();
        }

        public bool verificateEmail(string email)
        {
            return _context.Users.Any(u => u.email == email.ToLower());
        }
    }
}
