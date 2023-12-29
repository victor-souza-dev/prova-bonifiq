namespace ProvaPub.Models.DTOs;
    public class NSerialDTO
    {
        public int Modelo { get; private set; }
        public int Minimo { get; private set; }
        public int Maximo { get; private set; }
        public int Incremento { get; private set; }
        public string? Descricao { get; private set; }
    }