using DiplomskiBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DiplomskiBlazor.Client.Services.StatistikaService
{
    public class StatistikaService : IStatistikaService
    {
        public List<Statistika> Statistike { get; set; } = new List<Statistika>();

        private readonly HttpClient _htpp;
        private readonly NavigationManager _navigationManager;

        public StatistikaService(HttpClient htpp, NavigationManager navigationManager)
        {
            _htpp = htpp;
            _navigationManager = navigationManager;
        }

        private async Task SetStatistike(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Statistika>>();
            if (response != null)
            {
                Statistike = response;
                _navigationManager.NavigateTo("statistike");
            }

        }

        public async Task CreateStatistika(Statistika k)
        {
            var result = await _htpp.PostAsJsonAsync("api/statistika", k);
            await SetStatistike(result);
        }

        public async Task DeleteStatistika(int id)
        {
            var result = await _htpp.DeleteAsync($"api/statistika/{id}");
            await SetStatistike(result);
        }

        public async Task<Statistika> GetSingleStatistika(int id)
        {
            var result = await _htpp.GetFromJsonAsync<Statistika>($"api/statistika/{id}");
            if (result != null)
                return result;
            throw new Exception("Statistika nije pronadjena");
        }

        public async Task GetStatistike()
        {
            var result = await _htpp.GetFromJsonAsync<List<Statistika>>("api/statistika/statistike");
            if (result != null)
            {
                Statistike = result;
            }
        }

        public async Task UpdateStatistika(StatistikaDto k)
        {
            var result = await _htpp.PutAsJsonAsync($"api/statistika/{k.statistikaId}", k);
            await SetStatistike(result);
        }

    }
}