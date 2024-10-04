using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInstitucionalBandup.Models;

    public class Eventos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Banner")]
        [Required(ErrorMessage = "Insira o banner")]
        public string Banner { get; set; }

        [Display(nameof = "Nome")]
        [Required(ErrorMessage = "O campo 'nome' é obrigatório")]
        public string Nome { get; set; }

        [Display(nameof = "Descrição")]
        [Required(ErrorMessage = "O campo 'descrição' é obrigatório")]
        public string Descricao { get; set; }

        [Display(nameof = "Imagem")]
        [Required(ErrorMessage = "O campo 'imagem' é obrigatório")]
        public string Imagem { get; set; }

        [Display(nameof = "Link para comprar seu ingresso")]
        public string LinkIngresso { get; set; }
    }
