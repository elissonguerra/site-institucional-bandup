using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteInstitucionalBandup.Models
[Table("TrabalheConosco")]
    public class TrabalheConosco
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Id { get; set; }

        [Display(Name = "Nome Completo")]
        [Required(ErrorMessage = "O campo nome é de preenchimento obrigatório")]
        public string NomeCompleto { get; set; }

        [Display(Name = "CPF")]
        [Required(ErrorMessage = "O campo CPF é de preenchimento obrigatório")]
        public string Cpf { get; set; }

        [Display(Name = "RG")]
        [Required(ErrorMessage = "O campo RG é de preenchimento obrigatório")]
        public string Rg { get; set; }

        [Display(Name = "Data de Nascimento")]
        [Required(ErrorMessage = "A data de nascimento é de preenchimento obrigatório")]
        public string DataNascimento { get; set; }

        [Display(Name = "Setor desejado")]
        [Required(ErrorMessage = "A escolha do setor é de preenchimento obrigatório")]
        public string SetorDesejado { get; set; }

        [Display(Name = "Escolaridade")]
        [Required(ErrorMessage = "A escolaridade é de preenchimento obrigatório")]
        public string Escolaridade { get; set; }

        [Display(Name = "Cursos Profissionalizantes")]
        public string CursosProfissionalizantes { get; set; }

        [Display(Name = "Observações")]
        public string Observacoes { get; set; }

        
    }
