using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BugTracker.Api.Model
{

    /// <summary>
    /// Тип замечания
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IssueType
    {
        /// <summary>
        /// новый функционал
        /// </summary>
        Feature,

        /// <summary>
        /// Ошибка
        /// </summary>
        Bug
    }
}
