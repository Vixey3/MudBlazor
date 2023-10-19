namespace DiplomskiBlazor.Client.Services.KorisnikService
{
    public interface IKorisnikService
    {
        List<Korisnik> Korisnici { get; set; }
        Task GetKorisnici();
        Task<Korisnik> GetSingleKorisnik(int id);
        Task<List<Workout>> DohvatiWorkoutove(int id);
        Task CreateKorisnik(KorisnikDto k);
        Task UpdateKorisnik(KorisnikDto k);
        Task DeleteKorisnik(int id);
        Task DeleteWorkout(int idWorkouta, int idKor);
        Task AddWorkoutKorisnik(List<int> lista, int id);
    }
}