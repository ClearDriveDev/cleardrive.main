namespace ClearDrive.backend.Models.Datas.Entities
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
