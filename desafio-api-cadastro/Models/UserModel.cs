﻿
namespace desafio_api_cadastro.Models
{
    public class User
    {
            public int Id { get; set; }
            public string name { get; set; }
            public string email { get; set; }
            public string password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
