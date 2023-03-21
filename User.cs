using System.ComponentModel.DataAnnotations;

namespace DotNetAPI2
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Currency { get; set; }
    }
}