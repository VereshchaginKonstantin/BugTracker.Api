using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BugTracker.Api.Model
{

    /// <summary>
    /// Критичность замечания
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Priority
    {
        /// <summary>
        /// Приоритет не установлен
        /// </summary>
        None,

        /// <summary>
        /// низкий приоритет
        /// </summary>
        Low,

        /// <summary>
        /// Высокий приоритет
        /// </summary>
        High,

        /// <summary>
        /// Блокирующее замечание
        /// </summary>
        Blocking
    }
}
