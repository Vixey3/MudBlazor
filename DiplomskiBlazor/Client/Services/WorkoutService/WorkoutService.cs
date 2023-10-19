using DiplomskiBlazor.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace DiplomskiBlazor.Client.Services.WorkoutService
{
    public class WorkoutService : IWorkoutService
    {
        private readonly HttpClient _htpp;
        private readonly NavigationManager _navigationManager;
        public List<Workout> Workouts { get; set; } = new List<Workout>();

        public WorkoutService(HttpClient htpp, NavigationManager navigationManager)
        {
            _htpp = htpp;
            _navigationManager = navigationManager;
        }

        private async Task SetWorkouts(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<Workout>>();
            Workouts = response;
            _navigationManager.NavigateTo("workouts");
        }

        public async Task CreateWorkout(Workout k)
        {
            var result = await _htpp.PostAsJsonAsync("api/workout", k);
            await SetWorkouts(result);
        }

        public async Task DeleteWorkout(int id)
        {
            var result = await _htpp.DeleteAsync($"api/workout/{id}");
            await SetWorkouts(result);
        }

        public async Task<Workout> GetSingleWorkout(int id)
        {
            var result = await _htpp.GetFromJsonAsync<Workout>($"api/workout/{id}");
            if (result != null)
                return result;
            throw new Exception("Workout nije pronadjen");
        }

        public async Task GetWorkouts()
        {
            var result = await _htpp.GetFromJsonAsync<List<Workout>>("api/workout/workouts");
            if (result != null)
            {
                Workouts = result;
            }
        }

        public async Task UpdateWorkout(WorkoutDto k)
        {
            var result = await _htpp.PutAsJsonAsync($"api/workout/{k.workoutId}", k);
            await SetWorkouts(result);
        }

        public async Task AddVezbeWorkout(List<int> lista, int id)
        {

            var result = await _htpp.PutAsJsonAsync($"api/workout/addvVezbeWorkout/{id}", lista);
            //await SetWorkouts(result);
        }
    }
}