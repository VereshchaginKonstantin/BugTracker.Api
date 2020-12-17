using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BugTracker.Api.Dto;
using BugTracker.Api.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api
{

    /// <summary>
    /// Сервис работы с замечаниями
    /// </summary>
    public class IssueService
    {
        private readonly BugTrackerDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор сервиса
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="mapper"></param>
        public IssueService(BugTrackerDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Получение списка всех замечаний
        /// </summary>
        /// <returns></returns>
        public async Task<List<IssueViewDto>> GetIssues()
        {
            var items = await _context.Issues.ToListAsync();
            var issue = _mapper.Map<List<IssueViewDto>>(items);
            return issue;
        }

        /// <summary>
        /// Получить замечание
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IssueViewDto> GetIssue(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            return _mapper.Map<IssueViewDto>(issue);
        }

        /// <summary>
        /// Обновляет замечание
        /// </summary>
        /// <param name="id"></param>
        /// <param name="issueCreateUpdateRequestDto"></param>
        /// <returns></returns>
        public async Task<IssueViewDto> UpdateIssue(Guid id, CreateUpdateIssueDto issueCreateUpdateRequestDto)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return null;
            }
            _mapper.Map(issueCreateUpdateRequestDto, issue);

            await _context.SaveChangesAsync();

            return _mapper.Map<IssueViewDto>(issue);
        }

        /// <summary>
        /// Создать замечание
        /// </summary>
        /// <param name="issueCreateDto"></param>
        /// <returns></returns>
        public async Task<IssueViewDto> CreateIssue(CreateUpdateIssueDto issueCreateDto)
        {
            var issue = _mapper.Map<Issue>(issueCreateDto);
            await AutoFillAsync(issue);
            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();
            var issueDto = _mapper.Map<IssueViewDto>(issue);
            return issueDto;
        }

        private async Task AutoFillAsync(Issue issue)
        {
            issue.CreatedAt = DateTime.UtcNow;
            issue.CreatedByUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "sub").Value);
            var milestone = await _context.Milestones.OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
            issue.Milestone = milestone;
        }

        /// <summary>
        /// Удаление замечания
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteIssue(Guid id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return false;
            }
            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}