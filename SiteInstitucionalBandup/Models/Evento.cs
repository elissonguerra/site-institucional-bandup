using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteInstitucionalBandup.Models;

    public class Evento
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name = "Banner")]
        [Required(ErrorMessage = "Insira o banner")]
        public string Banner { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "O campo 'nome' é obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "O campo 'descrição' é obrigatório")]
        public string Descricao { get; set; }

        [Display(Name = "Imagem")]
        [Required(ErrorMessage = "O campo 'imagem' é obrigatório")]
        public string Imagem { get; set; }

        [Display(Name = "Link para comprar seu ingresso")]
        public string LinkIngresso { get; set; }
    }
