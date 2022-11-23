using FlightMVC.Models;

namespace FlightMVC.Repositories
{
    public interface IPassengersRepository
    {
        IEnumerable<PassengerDetails> GetPassengers();

        PassengerDetails? GetPassenger(string name);

        void AddPassenger(PassengerDetails passengerDetails);

        void UpdatePassenger(PassengerDetails passengerDetails);

        void DeletePassenger(string name);
    }
}
