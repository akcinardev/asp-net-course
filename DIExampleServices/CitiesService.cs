using DIExampleContracts;

namespace DIExampleServices
{
    public class CitiesService : ICitiesService
    {
        private List<string> _cities;

        private Guid _instanceId;
        public Guid InstanceId {
            get 
            {
                return _instanceId;
            }
        }

        public CitiesService()
        {
            _instanceId = Guid.NewGuid();
            _cities = new List<string>()
            {
                "London",
                "Paris",
                "New York",
                "Tokyo",
                "Rome"
            };
        }

        public List<string> GetCities()
        {
            return _cities;
        }
    }
}
