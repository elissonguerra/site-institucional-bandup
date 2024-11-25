using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInstitucionalBandup.Models
{
    [Table("Curriculos")]
    public class Curriculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é de preenchimento obrigatório")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Gênero")]
        public int GeneroId { get; set; }

        [ForeignKey("GeneroId")]
        public Genero Genero { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo CPF é de preenchimento obrigatório")]
        public string Cpf { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "O campo RG é de preenchimento obrigatório")]
        public string Rg { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "A data de nascimento é de preenchimento obrigatório")]
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "O campo celular é de preenchimento obrigatório")]
        public string Celular { get; set; }

        [Display(Name = "Setor desejado")]
        public int SetorId { get; set; }

        [ForeignKey("SetoresId")]
        public Setor SetorDesejado { get; set; }

        [Display(Name = "Escolaridade")]
        public string Escolaridade { get; set; }

        [Display(Name = "Cursos Profissionalizantes")]
        public string CursosProfissionalizantes { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}
