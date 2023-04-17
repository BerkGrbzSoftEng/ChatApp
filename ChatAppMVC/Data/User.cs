using System.ComponentModel.DataAnnotations.Schema;

namespace ChatAppMVC.Data
{
    [Table("User")]
    public class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime LastLogin { get; set; }
    }
}
