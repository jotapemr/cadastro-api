using desafio_api_cadastro.Models;

namespace desafio_api_cadastro.Repository
{
    public interface IuserRepository
    {
        User GetUserById(int id);
        User GetUsuarioPorLoginSenha(string email, string password);
        public void Save(User user);

        public bool verificateEmail(string email);
    }
}
