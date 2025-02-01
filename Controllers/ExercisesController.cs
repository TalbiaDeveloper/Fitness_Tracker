using Fitness_Tracker.Data.Repository;
using Fitness_Tracker.Model;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ExercisesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllExercises()
        {
            var exercises = await _unitOfWork.Exercises.GetAllAsync();
            return Ok(exercises);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetExerciseById(int id)
        {
            var exercise = await _unitOfWork.Exercises.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            return Ok(exercise);
        }

        [HttpPost]
        public async Task<IActionResult> AddExercise([FromBody] Exercise exercise)
        {
            await _unitOfWork.Exercises.AddAsync(exercise);
            await _unitOfWork.SaveAsync();
            return CreatedAtAction(nameof(GetExerciseById), new { id = exercise.Id }, exercise);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExercise(int id, [FromBody] Exercise exercise)
        {
            if (id != exercise.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Exercises.Update(exercise);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExercise(int id)
        {
            var exercise = await _unitOfWork.Exercises.GetByIdAsync(id);
            if (exercise == null)
            {
                return NotFound();
            }
            _unitOfWork.Exercises.Delete(exercise);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
}