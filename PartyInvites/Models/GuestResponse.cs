using System.ComponentModel.DataAnnotations;

namespace PartyInvites.Models
{
    public class GuestResponse
    {
        [Required(ErrorMessage ="Введите ваше имя")]
        public string? Name {  get; set; }
        [Required(ErrorMessage = "Введите ваш email")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Введите ваш номер телефона")]
        public string? Phone { get; set; }
        [Required(ErrorMessage = "Выберите ваше решение")]
        public bool? WillAttend { get; set; }
    }
}
