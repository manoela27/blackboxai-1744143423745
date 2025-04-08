using System.ComponentModel.DataAnnotations;

namespace CadastroBasico.DTOs
{
    public class UserLevelDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do nível é obrigatório")]
        [Display(Name = "Nome do Nível")]
        [StringLength(50, ErrorMessage = "O nome do nível deve ter no máximo 50 caracteres")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Descrição")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo 200 caracteres")]
        public string? Description { get; set; }
    }
}
