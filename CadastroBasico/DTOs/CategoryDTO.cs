using System.ComponentModel.DataAnnotations;

namespace CadastroBasico.DTOs
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }
    }
}
