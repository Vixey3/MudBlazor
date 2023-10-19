using DiplomskiBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DiplomskiBlazor.Client.Services.VezbaService
{
    public class VezbaService : IVezbaService
    {
        private readonly HttpClient _htpp;
        private readonly NavigationManager _navigationManager;

        public List<Vezba> Vezbe { get; set; } = new List<Vezba>();
        public List<DeoTela> DeloviTela { get; set; } = new List<DeoTela>();

        public VezbaService(HttpClient htpp, NavigationManager navigationManager)
        {
            _htpp = htpp;
            _navigationManager = navigationManager;
        }

        private async Task SetVezbe(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Vezba>>();
            if (response == null)
            {
                return;
                // DODAJ KOD
            }
            Vezbe = response;
            _navigationManager.NavigateTo("vezbe");
        }
        public async Task CreateVezba(Vezba v)
        {
            var result = await _htpp.PostAsJsonAsync("api/vezba", v);
            await SetVezbe(result);
        }

        public async Task DeleteVezba(int id)
        {
            var result = await _htpp.DeleteAsync($"api/vezba/{id}");
            await SetVezbe(result);
        }

        public async Task<Vezba> GetSingleVezba(int id)
        {
            var result = await _htpp.GetFromJsonAsync<Vezba>($"api/vezba/{id}");
            if (result != null)
                return result;
            throw new Exception("Vezba nije pronadjena");
        }

        public async Task GetVezbe()
        {
            var result = await _htpp.GetFromJsonAsync<List<Vezba>>("api/vezba/vezbe");
            if (result != null)
            {
                Vezbe = result;
            }
        }

        public async Task UpdateVezba(VezbaDto v)
        {
            var result = await _htpp.PutAsJsonAsync($"api/vezba/{v.vezbaId}", v);
            await SetVezbe(result);
        }

        public async Task GetDeloviTela()
        {
            var result = await _htpp.GetFromJsonAsync<List<DeoTela>>("api/deotela/delovitela");
            if (result != null)
            {
                DeloviTela = result;
            }
        }

        /*public async Task SetujDeoTela()
		{
			foreach (var v in Vezbe)
			{
				foreach (var d in DeloviTela)
				{
					if(v.deoTelaId==d.deoTelaId)
					{
						v.deoTela = d;
					}
				}
			}
		}*/
    }
}