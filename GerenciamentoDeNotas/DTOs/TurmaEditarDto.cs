using System.ComponentModel.DataAnnotations;

namespace GerenciamentoDeNotas.DTOs
{
    public class TurmaEditarDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A data de início é obrigatória.")]
        public DateTime DataDeInicio { get; set; }
    }
}
