using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInstitucionalBandup.Models
{
    [Table("Curriculos")] // Nome da tabela no banco de dados
    public class Curriculo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é de preenchimento obrigatório")]
        public string NomeCompleto { get; set; }

        [Display(Name = "Celular")]
        [Required(ErrorMessage = "O campo celular é de preenchimento obrigatório")]
        public string Celular { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "O campo e-mail é de preenchimento obrigatório")]
        [EmailAddress(ErrorMessage = "E-mail inválido")]
        public string Email { get; set; }

        [Display(Name = "Tipo de Vaga")]
        public string TipoVaga { get; set; } 

        [Display(Name = "Objetivos")]
        [Required(ErrorMessage = "O campo de objetivos é de preenchimento obrigatório")]
        public string Objetivos { get; set; }

        [Display(Name = "Experiências")]
        [Required(ErrorMessage = "O campo de experiências é de preenchimento obrigatório")]
        public string Experiencias { get; set; }

        [Display(Name = "Gênero")]
        public string Genero { get; set; }

        [Display(Name = "Currículo")]
        public string ArquivoCurriculo { get; set; } 

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }
    }
}
