using System.ComponentModel.DataAnnotations;

namespace SiteInstitucionalBandup.ViewModels;

public class RegisterVM
{
    [Display(Name = "Nome")]
    [Required(ErrorMessage = "Favor informar o Nome")]
    [StringLength(60, ErrorMessage ="O nome deve possuir no máximo 60 caracteres")]
    public string Name { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Por favor, informe seu email")]
    public string Email { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Data de Nascimento")]
    [Required(ErrorMessage = "Por favor, informe a Data de Nascimento")]
    public DateTime Birthday { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Senha de Acesso")]
    [Required(ErrorMessage = "Por favor, informe sua senha")]
    public string Password { get; set; }
}