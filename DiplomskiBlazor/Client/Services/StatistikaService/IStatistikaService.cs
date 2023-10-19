namespace DiplomskiBlazor.Client.Services.StatistikaService
{
    public interface IStatistikaService
    {
        List<Statistika> Statistike { get; set; }
        Task GetStatistike();
        Task<Statistika> GetSingleStatistika(int id);

        Task CreateStatistika(Statistika k);
        Task UpdateStatistika(StatistikaDto k);
        Task DeleteStatistika(int id);
    }
}