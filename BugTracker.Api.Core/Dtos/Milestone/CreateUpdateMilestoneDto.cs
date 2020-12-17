
using System;
using System.ComponentModel.DataAnnotations;
using BugTracker.Api.Validation;

namespace BugTracker.Api.Dto
{
    /// <summary>
    /// Данные для создания или обновления вехи
    /// </summary>
    public class CreateUpdateMilestoneDto
    {
        /// <summary>
        /// наименование вехи
        /// </summary>
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        /// <summary>
        /// Описание вехи
        /// </summary>
        [Required]
        [StringLength(1500)]
        public string Description { get; set; }

        /// <summary>
        /// Срок завершения вехи
        /// </summary> 
        [DateMustBeEqualOrGreaterThanCurrentDate(ErrorMessage = "Please verify the Deadline Range")]
        public DateTime? Deadline { get; set; }
    }
}
