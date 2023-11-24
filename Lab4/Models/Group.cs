using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab4.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Speciality { get; set; }
        public string? Faculty { get; set; }
        List<Student> Students { get; set; } = new();
    }
}
