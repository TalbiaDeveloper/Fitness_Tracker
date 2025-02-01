using Fitness_Tracker.Model;

namespace Fitness_Tracker.Data.Repository
{
    public interface IUnitOfWork: IDisposable
    {
        IRepository<Workout> Workouts { get; }
        IRepository<Exercise> Exercises { get; }
        Task SaveAsync();
    }
}
