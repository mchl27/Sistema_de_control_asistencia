using System;
using System.Collections.Generic;

namespace Sistema_de_asistencia.Models;

public partial class Estudiante
{
    public int Id { get; set; }

    public string? Curso { get; set; }

    public string? Grado { get; set; }

    public virtual Usuario? IdNavigation { get; set; }
}
