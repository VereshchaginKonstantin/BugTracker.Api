
using System;
using System.Collections.Generic;

namespace BugTracker.Api.Model
{


    /// <summary>
    /// Веха
    /// </summary>
    public class Milestone
    {
        /// <summary>
        /// Уникальный идентификатор вехи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// наименование вехи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание вехи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Уникальный идентификатор автора вехи
        /// </summary>
        public Guid CreatedByUserId { get; set; }

        /// <summary>
        /// Дата содания вехи
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Срок завершения вехи
        /// </summary>
        public DateTime? Deadline { get; set; }

        /// <summary>
        /// Перечень замечаний в вехе
        /// </summary>
        public ICollection<Issue> Issues { get; set; }
    }
}
