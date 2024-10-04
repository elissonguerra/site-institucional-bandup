using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInstitucionalBandup.Models;

[Table("Lojas")]
    public class Lojas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Id { get; set; }

        [Display(Name = "Nome da Loja")]
        [Required(ErrorMessage = "Insira o nome da loja")]
        public string NomeLoja { get; set; }

        [Display(Name = "Subtítulo")]
        [Required(ErrorMessage = "Insira o subtítulo")]
        public string Subtitulo { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Insira a descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Banner")]
        [Required(ErrorMessage = "Insira o banner")]
        public string Banner { get; set; }

        [Display(Name = "Link da loja")]
        [Required(ErrorMessage = "Insira o link da loja")]
        public string LinkLoja { get; set; }
    }
