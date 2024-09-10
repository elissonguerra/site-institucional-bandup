using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteInstitucionalBandup.Models

[Table("Marcas")]
    public class Marcas
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Id { get; set; }

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
