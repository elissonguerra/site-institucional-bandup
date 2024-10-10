using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInstitucionalBandup.Models;

[Table("Marcas")]
    public class Marca
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Nome da Marca")]
        [Required(ErrorMessage = "Insira o nome da marca")]
        public string NomeMarca { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Insira a descrição")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem")]
        [Required(ErrorMessage = "Insira a imagem")]
        public string Imagem { get; set; }
        
    }
