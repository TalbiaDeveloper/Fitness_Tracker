using Fitness_Tracker.Data.Repository;
using Fitness_Tracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public WorkoutsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWorkouts()
        {
            var workouts = await _unitOfWork.Workouts.GetAllAsync();
            return Ok(workouts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorkoutById(int id)
        {
            var workout = await _unitOfWork.Workouts.GetByIdAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            return Ok(workout);
        }

        [HttpPost]
        public async Task<IActionResult> AddWorkout([FromBody] Workout workout)
        {
            await _unitOfWork.Workouts.AddAsync(workout);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetWorkoutById), new { id = workout.Id }, workout);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout workout)
        {
            if (id != workout.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Workouts.Update(workout);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _unitOfWork.Workouts.GetByIdAsync(id);
            if (workout == null)
            {
                return NotFound();
            }
            _unitOfWork.Workouts.Delete(workout);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}
