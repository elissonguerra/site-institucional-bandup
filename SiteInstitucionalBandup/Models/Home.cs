using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInstitucionalBandup.Models

[Table("Home")]
    public class Home
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Banner")]
        [Required(ErrorMessage = "Insira o banner")]
        public string Banner { get; set; }

        [Display(Name = "Quem Somos")]
        [Required(ErrorMessage = 'Preencha o campo "Quem Somos"')]
        public string QuemSomos { get; set; }

        [Display(Name = "O que fazemos")]
        [Required(ErrorMessage = 'Preencha o campo "O que fazemos"')]
        public string OQueFazemos { get; set; }

    
        [Display(Name = "Objetivos")]
        [Required(ErrorMessage = 'Preencha o campo "Objetivos"')]
        public string Objetivos { get; set; }
    }
