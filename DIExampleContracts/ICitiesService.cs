namespace DIExampleContracts
{
    public interface ICitiesService
    {
        Guid InstanceId { get; }
        List<string> GetCities();
    }
}
