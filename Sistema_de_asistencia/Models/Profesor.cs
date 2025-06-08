using System;
using System.Collections.Generic;

namespace Sistema_de_asistencia.Models;

public partial class Profesor
{
    public int Id { get; set; }

    public string? Especialidad { get; set; }

    public string? Departamento { get; set; }

    public virtual Usuario IdNavigation { get; set; } = null!;
}
