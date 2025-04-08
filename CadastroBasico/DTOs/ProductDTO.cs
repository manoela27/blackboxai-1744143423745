using System.ComponentModel.DataAnnotations;

namespace CadastroBasico.DTOs
{
    public class ProductDTO
    {
        [Required(ErrorMessage = "O código é obrigatório")]
        [Display(Name = "Código")]
        public string Code { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        [Display(Name = "Preço")]
        public decimal Price { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }
    }
}
