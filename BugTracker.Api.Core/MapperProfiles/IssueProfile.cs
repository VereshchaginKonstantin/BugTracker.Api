

using AutoMapper;
using BugTracker.Api.Dto;
using BugTracker.Api.Model;

namespace BugTracker.Api
{
    /// <summary>
    /// Профайл маппинга для замечания
    /// </summary>
    public class IssueProfile : Profile
    {
        /// <summary>
        /// Конструктор профайла маппинга для замечания
        /// </summary>
        public IssueProfile()
        {
            CreateMap<CreateUpdateIssueDto, Issue>();
            CreateMap<Issue, CreateUpdateIssueDto>();
            CreateMap<Issue, IssueViewDto>();
        }
    }
}