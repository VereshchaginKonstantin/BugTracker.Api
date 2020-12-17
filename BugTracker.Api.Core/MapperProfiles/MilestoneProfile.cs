

using AutoMapper;
using BugTracker.Api.Dto;
using BugTracker.Api.Model;

namespace BugTracker.Api
{
    /// <summary>
    /// Профиль маппинга для вехи
    /// </summary>
    public class MilestoneProfile : Profile
    {

        /// <summary>
        /// Конструктор профиля маппинга для вехи
        /// </summary>
        public MilestoneProfile()
        {
            CreateMap<CreateUpdateMilestoneDto, Milestone>();
            CreateMap<Milestone, CreateUpdateMilestoneDto>();
            CreateMap<Milestone, MilestoneViewDto>();
        }
    }
}