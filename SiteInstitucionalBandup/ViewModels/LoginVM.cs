using System.ComponentModel.DataAnnotations;

namespace SiteInstitucionalBandup.ViewModels;
public class LoginVM
{
    [Display(Name = "Email ou Nome de Usuário")]
    [Required(ErrorMessage = "Por favor, informe seu email ou nome de usuário")]
    public string Email { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Senha de Acesso")]
    [Required(ErrorMessage = "Por favor, informe sua senha")]
    public string Password { get; set; }

    [Display(Name = "Manter Conectado?")]
    public bool RememberMe { get; set; } = false;
    public string ReturnUrl { get; set; }
    
}