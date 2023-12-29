namespace ProvaPub.Models.Entities
{
    public class NSerial
    {
        public Guid IdNSerial { get; private set; }
            public int Modelo {  get; private set; }
            public int Serial { get; private set; }
            public int Minimo {  get; private set; }
            public int Maximo {  get; private set; }
            public int Incremento {  get; private set; }
            public string? Descricao {  get; private set; }
            public DateTime DataAlteracaoLog {  get; private set; }

        public NSerial(int modelo, int minimo, int maximo, int incremento, string? descricao = "")
        {
            IdNSerial = Guid.NewGuid();
            Modelo = modelo;
            Serial = minimo;
            Minimo = minimo;
            Maximo = maximo;
            Incremento = incremento;
            Descricao = descricao;
            DataAlteracaoLog = DateTime.Now;
        }

        public int getSerial()
        {
            return Serial + Incremento;
        }
    }
}
