using FlightMVC.Models;

namespace FlightMVC.Repositories
{
    public class PassengersRepository : IPassengersRepository
    {
        static List<PassengerDetails> _passengers = new();

        static PassengersRepository()
        {
            for (int i = 0; i < 10; i++)
            {
                _passengers.Add(new PassengerDetails($"George_{i}", i+10, "ba1234"));
            }
        }

        public IEnumerable<PassengerDetails> GetPassengers()
        {
            return _passengers.OrderBy(p => p.Name);
        }

        public PassengerDetails? GetPassenger(string name)
        {
            return _passengers.FirstOrDefault(p => p.Name == name);
        }

        public void AddPassenger(PassengerDetails passengerDetails)
        {
            _passengers.Add(passengerDetails);
        }

        public void DeletePassenger(string name)
        {
            var pd = GetPassenger(name);

            if (pd != null)
            {
                _passengers.Remove(pd);
            }
        }

        public void UpdatePassenger(PassengerDetails passengerDetails)
        {
            DeletePassenger(passengerDetails.Name);
            AddPassenger(passengerDetails);
        }
    }
}
