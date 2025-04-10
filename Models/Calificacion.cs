namespace TiendasAPI.Models
{
    public class Calificacion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public int IdProducto { get; set; }
        public int Puntuacion { get; set; }
        public string Comentario { get; set; }
    }
}
