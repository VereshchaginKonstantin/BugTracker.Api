using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugTracker.Api.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Api.Controllers
{
    /// <summary>
    /// Контроллер АПИ для вех
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MilestonesController : ControllerBase
    {
        private readonly MilestoneService _milestoneService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="milestoneService"></param>
        public MilestonesController(MilestoneService milestoneService)
        {
            _milestoneService = milestoneService;
        }

        /// <summary>
        /// Получает перечень всех вех
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<MilestoneViewDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MilestoneViewDto>>> GetMilestones()
        {
            return await _milestoneService.GetMilestones();
        }

        /// <summary>
        /// Получает веху по заданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор вехи</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MilestoneViewDto), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<MilestoneViewDto>> GetMilestone(Guid id)
        {
            var milestone = await _milestoneService.GetMilestone(id);

            if (milestone == null)
            {
                return NotFound();
            }

            return milestone;
        }

        /// <summary>
        /// Обновляет существующюю веху
        /// </summary>
        /// <param name="id">Идентификатор вехи</param>
        /// <param name="milestone">Данные для обновления</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMilestone(Guid id, CreateUpdateMilestoneDto milestone)
        {
            var result = await _milestoneService.UpdateMilestone(id, milestone);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Создает новую  веху
        /// </summary>
        /// <param name="milestone">Данные необходимые для создания новой вехи</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(MilestoneViewDto), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<MilestoneViewDto>> PostMilestone(CreateUpdateMilestoneDto milestone)
        {
            var result = await _milestoneService.CreateMilestone(milestone);
            return CreatedAtAction(nameof(GetMilestone), new { id = result.Id }, result);
        }

        /// <summary>
        /// Удаляет веху
        /// </summary>
        /// <param name="id">Идентификатор вехи</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMilestone(Guid id)
        {
            var result = await _milestoneService.DeleteMilestone(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
