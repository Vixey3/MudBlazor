using DiplomskiBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DiplomskiBlazor.Client.Services.KorisnikService
{
    public class KorisnikService : IKorisnikService
    {
        private readonly HttpClient _htpp;
        private readonly NavigationManager _navigationManager;

        public KorisnikService(HttpClient htpp, NavigationManager navigationManager)
        {
            _htpp = htpp;
            _navigationManager = navigationManager;
        }
        public List<Korisnik> Korisnici { get; set; } = new List<Korisnik>();

        public async Task CreateKorisnik(KorisnikDto k)
        {
            var result = await _htpp.PostAsJsonAsync("api/korisnik", k);
            await SetKorisnici(result);
             //GetKorisnici();
            //_navigationManager.NavigateTo("korisnici");
        }

        private async Task SetKorisnici(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Korisnik>>();
            if(response != null)
            {
                Korisnici = response;
            }
            _navigationManager.NavigateTo("korisnici");
        }

        public async Task DeleteKorisnik(int id)
        {
            var result = await _htpp.DeleteAsync($"api/korisnik/{id}");
            await SetKorisnici(result);
        }

        public async Task GetKorisnici()
        {
            var result = await _htpp.GetFromJsonAsync<List<Korisnik>>("api/korisnik/korisnici");
            if (result != null)
            {
                Korisnici = result;
            }

        }
        public async Task<Korisnik> GetSingleKorisnik(int id)
        {
            var result = await _htpp.GetFromJsonAsync<Korisnik>($"api/korisnik/{id}");
            if (result != null)
                return result;
            throw new Exception("Korisnik nije pronadjen");
        }

        public async Task UpdateKorisnik(KorisnikDto k)
        {
            var result = await _htpp.PutAsJsonAsync($"api/korisnik/{k.korisnikId}", k);
            await SetKorisnici(result);
        }

        public async Task AddWorkoutKorisnik(List<int> lista, int id)
        {
            var result = await _htpp.PutAsJsonAsync($"api/korisnik/dodajworkoutkorisniku/{id}", lista);
        }

        public async Task<List<Workout>> DohvatiWorkoutove(int id)
        {
            var result = await _htpp.GetFromJsonAsync<List<Workout>>($"api/korisnik/kw/{id}");
            return result;
        }

        public async Task DeleteWorkout(int idWorkouta, int idKor)
        {
            var result = await _htpp.DeleteAsync($"api/korisnik/obrisiworkoutkorisniku/{idKor}/{idWorkouta}");
        }
    }
}