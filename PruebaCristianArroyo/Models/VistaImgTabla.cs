namespace PruebaCristianArroyo.Models
{
    class MyModel
    {
        public int Id { get; set; }

        public string Nombre { get; set; } = null!;

        public byte[] Imagen { get; set; } = null!;

        public int? IdTabla { get; set; }

        public int? IdCarta { get; set; }

        public int? NumCarta { get; set; }
    }

    class MyViewModel
    {
        public List<MyModel> CartasTablas { get; set; }
    }
}
