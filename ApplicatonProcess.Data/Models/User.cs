using System.ComponentModel.DataAnnotations;


namespace ApplicatonProcess.Data.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
