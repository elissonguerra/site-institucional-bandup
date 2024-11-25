using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace SiteInstitucionalBandup.Models;
[Table("Setores")]
public class Setor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Display(Name = "Nome")]
    [Required(ErrorMessage = "Insira o setor desejado")]
    public string Nome { get; set; }
}