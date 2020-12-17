
using System;
using BugTracker.Api.Model;

namespace BugTracker.Api.Dto
{
    /// <summary>
    /// Данные по замечанию доступные для просмотра
    /// </summary>
    public class IssueViewDto
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Тема задачи
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Уникальный идентификатор автора
        /// </summary>
        public Guid CreatedByUserId { get; set; }

        /// <summary>
        /// Уникальный идентификатор  исполнителя
        /// </summary>
        public Guid? AssignedTo { get; set; }


        /// <summary>
        /// Стаутс задачи
        /// </summary>
        public Status Status { get; set; }

        /// <summary>
        /// Тип задачи
        /// </summary>
        public IssueType IssueType { get; set; }

        /// <summary>
        /// Критичность задачи
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Дата создания задачи
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Срок задачи
        /// </summary>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// номер версии к которой применимо замечание
        /// </summary>
        public string DetectedVersion { get; set; }

        /// <summary>
        /// номер версии в которой замечание устранено
        /// </summary>
        public Guid? ReleasedVersion { get; set; }

        /// <summary>
        /// Веха задачи
        /// </summary>
        public MilestoneViewDto Milestone { get; set; }

    }
}
