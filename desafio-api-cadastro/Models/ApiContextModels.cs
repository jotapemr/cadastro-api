
using desafio_api_cadastro.Models;
using Microsoft.EntityFrameworkCore;


    public class ApiContextModels : DbContext
    {
        public ApiContextModels(DbContextOptions<ApiContextModels> option) : base(option)
        {

        }

        public DbSet<User> Users { get; set; }
    }

