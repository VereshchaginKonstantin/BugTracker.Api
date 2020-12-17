using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BugTracker.Api.Model
{
    /// <summary>
    /// Статус замечания
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Status
    {
        /// <summary>
        /// Создано
        /// </summary>
        Created,

        /// <summary>
        /// назначено
        /// </summary>
        Assigned,

        /// <summary>
        /// В работе
        /// </summary>
        Doing,

        /// <summary>
        /// на тестировании
        /// </summary>
        Testing,

        /// <summary>
        /// Завершено
        /// </summary> 
        Completed,

        /// <summary>
        /// Закрыто
        /// </summary>
        Closed
    }
}
