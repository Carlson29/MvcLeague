using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MvcLeague.Models
{
        [Index(nameof(userName), IsUnique = true)]
        public class User
        {
            [Key]
            public int userId { get; set; }
            [Required]
            [StringLength(60, MinimumLength = 3)]
            public string userName { get; set; }
            [Required]
            [StringLength(60, MinimumLength = 8)]
            [RegularExpression("^(?=.*[0-9])" + "(?=.*[a-z])(?=.*[A-Z])" + "(?=.*[@#$%^&+=*/])" + ".{8,20}$")]
            public string password { get; set; }
           [Required]
           [DataType(DataType.Date)]
            public DateTime dateOfBirth { get; set; }
    }
  }

