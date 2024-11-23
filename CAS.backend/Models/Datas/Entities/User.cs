namespace CAS.backend.Models.Datas.Entities
{
    public class User
    {

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string TelNumber { get; set; }

        public User(string username, string password, string email, string telNumber)
        {
            Id = Guid.NewGuid();
            UserName = username;
            Password = password;
            Email = email;
            TelNumber = telNumber;
        }

        public User(Guid id, string username, string password, string email, string telNumber)
        {
            Id = id;
            UserName = username;
            Password = password;
            Email = email;
            TelNumber = telNumber;
        }

        public User()
        {
            Id = Guid.NewGuid();
            UserName = string.Empty;
            Password = string.Empty;
            Email = string.Empty;
            TelNumber = string.Empty;
        }
    }
}
