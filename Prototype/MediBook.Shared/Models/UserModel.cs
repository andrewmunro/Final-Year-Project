using System.ComponentModel.DataAnnotations;

namespace MediBook.Shared.Models
{
    public abstract class UserModel
    {
        [Key]
        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}