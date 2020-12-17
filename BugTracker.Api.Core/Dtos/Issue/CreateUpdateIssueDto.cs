
using System;
using System.ComponentModel.DataAnnotations;
using BugTracker.Api.Model;
using BugTracker.Api.Validation;
using Newtonsoft.Json;

namespace BugTracker.Api.Dto
{
    /// <summary>
    /// Данные для создания или обновления замечания.
    /// </summary>
    public class CreateUpdateIssueDto
    {
        /// <summary>
        /// Тема задачи
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Subject { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        [Required]
        [StringLength(1500)]
        public string Description { get; set; }

        /// <summary>
        /// Тип задачи
        /// </summary>
        public IssueType IssueType { get; set; }

        /// <summary>
        /// Критичность задачи
        /// </summary>
        public Priority Priority { get; set; }

        /// <summary>
        /// Срок задачи
        /// </summary>
        [DateMustBeEqualOrGreaterThanCurrentDate(ErrorMessage = "Please verify the Deadline Range")]
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// номер версии к которой применимо замечание
        /// </summary>
        [Required]
        [RegularExpression(@"^(\d+\.)?(\d+\.)?(\*|\d+)$")]
        public string DetectedVersion { get; set; }
    }
}
