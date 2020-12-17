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
    /// Сервис работы с вехами
    /// </summary>
    public class MilestoneService
    {
        private readonly BugTrackerDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        /// <summary>
        /// Создает сервис
        /// </summary>
        /// <param name="context"></param>
        /// <param name="httpContextAccessor"></param>
        /// <param name="mapper"></param>
        public MilestoneService(BugTrackerDbContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Получает список вех
        /// </summary>
        /// <returns></returns>
        public async Task<List<MilestoneViewDto>> GetMilestones()
        {
            var items = await _context.Milestones.ToListAsync();
            var milestone = _mapper.Map<List<MilestoneViewDto>>(items);
            return milestone;
        }

        /// <summary>
        /// Получить веху
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MilestoneViewDto> GetMilestone(Guid id)
        {
            var milestone = await _context.Milestones.Include(x => x.Issues).FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<MilestoneViewDto>(milestone);
        }

        /// <summary>
        /// Обновляет веху
        /// </summary>
        /// <param name="id"></param>
        /// <param name="milestoneCreateUpdateRequestDto"></param>
        /// <returns></returns>
        public async Task<MilestoneViewDto> UpdateMilestone(Guid id, CreateUpdateMilestoneDto milestoneCreateUpdateRequestDto)
        {
            var milestone = await _context.Milestones.FindAsync(id);
            if (milestone == null)
            {
                return null;
            }
            _mapper.Map(milestoneCreateUpdateRequestDto, milestone);

            await _context.SaveChangesAsync();

            return _mapper.Map<MilestoneViewDto>(milestone);
        }

        /// <summary>
        /// Создать веху
        /// </summary>
        /// <param name="milestoneCreateDto"></param>
        /// <returns></returns>
        public async Task<MilestoneViewDto> CreateMilestone(CreateUpdateMilestoneDto milestoneCreateDto)
        {
            var milestone = _mapper.Map<Milestone>(milestoneCreateDto);
            AutoFill(milestone);
            _context.Milestones.Add(milestone);
            await _context.SaveChangesAsync();
            var milestoneDto = _mapper.Map<MilestoneViewDto>(milestone);
            return milestoneDto;
        }

        /// <summary>
        /// Заполнение автоматических полей
        /// </summary>
        /// <param name="milestone"></param>
        /// <returns></returns>
        private void AutoFill(Milestone milestone)
        {
            milestone.CreatedAt = DateTime.UtcNow;
            milestone.CreatedByUserId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "sub").Value);
        }

        /// <summary>
        /// Удаляет веху
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteMilestone(Guid id)
        {
            var milestone = await _context.Milestones.FindAsync(id);
            if (milestone == null)
            {
                return false;
            }
            _context.Milestones.Remove(milestone);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}