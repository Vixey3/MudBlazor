using DiplomskiBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DiplomskiBlazor.Client.Services.DeoTelaService
{
    public class DeoTelaService : IDeoTelaService
    {
        private readonly HttpClient _htpp;
        private readonly NavigationManager _navigationManager;

        public List<DeoTela> DeloviTela { get; set; } = new List<DeoTela>();

        public DeoTelaService(HttpClient htpp, NavigationManager navigationManager)
        {
            _htpp = htpp;
            _navigationManager = navigationManager;
        }

        private async Task SetDeloviTela(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<DeoTela>>();
            if (response == null)
            {
                return;
                // DODAJ KOD
            }
            DeloviTela = response;
            _navigationManager.NavigateTo("delovitela");
        }

        public async Task CreateDeoTela(DeoTela k)
        {
            var result = await _htpp.PostAsJsonAsync("api/vezba", k);
            await SetDeloviTela(result);
        }

        public async Task DeleteDeoTela(int id)
        {
            var result = await _htpp.DeleteAsync($"api/deotela/{id}");
            await SetDeloviTela(result);
        }

        public async Task GetDeoTela()
        {
            var result = await _htpp.GetFromJsonAsync<List<DeoTela>>("api/deotela/delovitela");
            if (result != null)
            {
                DeloviTela = result;
            }
        }

        public async Task<DeoTela> GetDeoTela(int id)
        {
            var result = await _htpp.GetFromJsonAsync<DeoTela>($"api/deotela/{id}");
            if (result != null)
                return result;
            throw new Exception("Deo tela nije pronadjen");
        }

        public async Task UpdateDeoTela(DeoTela k)
        {
            var result = await _htpp.PutAsJsonAsync($"api/deotela/{k.deoTelaId}", k);
            await SetDeloviTela(result);
        }

    }
}