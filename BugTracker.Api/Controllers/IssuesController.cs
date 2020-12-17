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
    /// Контроллер АПИ для замечаний
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IssueService _issueService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="issueService"></param>
        public IssuesController(IssueService issueService)
        {
            _issueService = issueService;
        }

        /// <summary>
        /// Получает перечень всех замечаний
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<IssueViewDto>), StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IssueViewDto>>> GetIssues()
        {
            return await _issueService.GetIssues();
        }

        /// <summary>
        /// Получает замечание по заданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор замечания</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IssueViewDto), StatusCodes.Status200OK)]
        [HttpGet("{id}")]
        public async Task<ActionResult<IssueViewDto>> GetIssue(Guid id)
        {
            var issue = await _issueService.GetIssue(id);

            if (issue == null)
            {
                return NotFound();
            }

            return issue;
        }

        /// <summary>
        /// Обновляет существующее замечание
        /// </summary>
        /// <param name="id">Идентификатор замечания</param>
        /// <param name="issue">Данные для обновления</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIssue(Guid id, CreateUpdateIssueDto issue)
        {
            var result = await _issueService.UpdateIssue(id, issue);
            if (result == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        /// <summary>
        /// Создает новое замечаение
        /// </summary>
        /// <param name="issue">Данные необходимые для создания нового замечания</param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IssueViewDto), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<IssueViewDto>> PostIssue(CreateUpdateIssueDto issue)
        {
            var result = await _issueService.CreateIssue(issue);
            return CreatedAtAction(nameof(GetIssue), new { id = result.Id }, result);
        }

        /// <summary>
        /// Удаляет замечание
        /// </summary>
        /// <param name="id">Идентификатор замечания</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(Guid id)
        {
            var result = await _issueService.DeleteIssue(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
