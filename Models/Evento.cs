namespace TiendasAPI.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public int IdTienda { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
    }
}
