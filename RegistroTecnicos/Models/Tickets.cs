using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroTecnicos.Models;

public class Tickets
{
    [Key]
    public int TicketId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public DateOnly Fecha { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public string Prioridad { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public int ClienteId { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "Solo se permiten letras")]
    public string Asunto { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    [RegularExpression(@"^[a-zA-z\s]+$", ErrorMessage = "Solo se permiten letras")]
    public string Descripcion { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public TimeSpan TiempoInvertido { get; set; }

    [Required(ErrorMessage = "Campo Obligatorio")]
    public int TecnicoId { get; set; }

    [ForeignKey("ClienteId")]
    public Clientes Clientes { get; set; }

    [ForeignKey("TecnicoId")]
    public Tecnicos Tecnicos { get; set; }

}
