
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jdfs.BlackJack.Api.Model
{
    /// <summary>
    /// Card category
    /// </summary>
    public enum Suit
    {
        [JsonConverter(typeof(StringEnumConverter))] Clubs,
        [JsonConverter(typeof(StringEnumConverter))] Diamonds,
        [JsonConverter(typeof(StringEnumConverter))] Harts,
        [JsonConverter(typeof(StringEnumConverter))] Spades,
        [JsonConverter(typeof(StringEnumConverter))] Hidden
    }
}
