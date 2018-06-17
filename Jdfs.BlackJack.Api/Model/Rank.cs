using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Jdfs.BlackJack.Api.Model
{
    /// <summary>
    /// Rank category
    /// </summary>
    public enum Rank
    {
        [JsonConverter(typeof(StringEnumConverter)), RankValue(1)] Ace,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(2)] Deuce,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(3)] Three,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(4)] Four,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(5)] Five,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(6)] Six,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(7)] Seven,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(8)] Eight,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(9)] Nine,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(10)] Ten,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(10)] Jack,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(10)] Queen,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(10)] King,
        [JsonConverter(typeof(StringEnumConverter)), RankValue(10)] Hidden
    }
}
