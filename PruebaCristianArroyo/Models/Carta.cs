using System;
using System.Collections.Generic;

namespace PruebaCristianArroyo.Models;

public partial class Carta
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public byte[] Imagen { get; set; } = null!;

    public int? IdTabla { get; set; }
}
