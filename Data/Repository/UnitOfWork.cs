using Fitness_Tracker.Model;

namespace Fitness_Tracker.Data.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Workout> _workouts;
        private IRepository<Exercise> _exercises;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public IRepository<Workout> Workouts => _workouts ??= new Repository<Workout>(_context);
        public IRepository<Exercise> Exercises => _exercises ??= new Repository<Exercise>(_context);
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
