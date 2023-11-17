using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class Student
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key, Column(Order = 0)]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        [Required]
        public DateTime? BirthDay {  get; set; }
        public int GroupId {  get; set; }
        public Group? Group { get; set; }
    }
}
