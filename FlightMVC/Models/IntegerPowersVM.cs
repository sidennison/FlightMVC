namespace FlightMVC.Models
{
    public record IntegerPowersVM(int Number)
    {
        public int Square => (int)Math.Pow(Number, 2);
        public int Cube => (int)Math.Pow(Number, 3);
    }
}
