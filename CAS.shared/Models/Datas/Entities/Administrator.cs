using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS.shared.Models.Datas.Entities
{
    public class Administrator
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
       

        public Administrator(string username, string password, string email)
        {
            Id = Guid.NewGuid();
            UserName = username;
            Password = password;
            Email = email;
        }

        public Administrator(Guid id, string username, string password, string email)
        {
            Id = id;
            UserName = username;
            Password = password;
            Email = email;
        }

        public Administrator()
        {
            Id = Guid.NewGuid();
            UserName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
        }
    }
}
