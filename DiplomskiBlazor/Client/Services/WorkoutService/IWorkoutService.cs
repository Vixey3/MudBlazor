namespace DiplomskiBlazor.Client.Services.WorkoutService
{
    public interface IWorkoutService
    {
        List<Workout> Workouts { get; set; }
        Task GetWorkouts();
        Task<Workout> GetSingleWorkout(int id);
        Task CreateWorkout(Workout k);
        Task UpdateWorkout(WorkoutDto k);
        Task DeleteWorkout(int id);
        Task AddVezbeWorkout(List<int> lista, int id);
    }
}