using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeNotas.DTOs
{
    public class TurmaDto
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Display(Name = "Data de inicio do périodo letivo")]
        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataDeInicio { get; set; }
    }
}
