using Newtonsoft.Json.Linq;
using Parsers.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace PSStoreParser
{
    static class Deserializer
    {
        public static IEnumerable<GameEntry> Deserialize(string json)
        {
            JObject jObject = JObject.Parse(json);
            return jObject["included"].Children()
                .Where(c => c["type"].Value<string>() == "game")
                .Select(c => MapEntry(c))
                .Where(e => e.PlatformSpecificId != null);
        }
        private static GameEntry MapEntry(JToken jToken)
        {
            string name = jToken["attributes"]["name"].ToObject<string>();
            string platformSpecificId = jToken["id"].ToObject<string>();
            string description = jToken["attributes"]["long-description"].ToObject<string>();
            List<string> pictureURLs = new List<string>();
            var pictures = jToken["attributes"]["media-list"]["screenshots"].Children();
            foreach (var entry in pictures)
            {
                pictureURLs.Add(entry["url"].ToObject<string>());
            }

            JToken skus = jToken["attributes"]["skus"];
            if(skus == null)
            {
                return new GameEntry();
            }
            int discountedPrice = skus[0]["prices"]["non-plus-user"]["actual-price"]["value"].ToObject<int>();
            JToken basePriceJToken = jToken["attributes"]["skus"][0]["prices"]["non-plus-user"]["strikethrough-price"];
            int basePrice = basePriceJToken.Type == JTokenType.Null ? discountedPrice : basePriceJToken["value"].ToObject<int>();

            return new GameEntry()
            {
                Name = name,
                DiscountedPrice = discountedPrice,
                BasePrice = basePrice,
                PlatformSpecificId = platformSpecificId,
                Description = description,
                PictureURLs = pictureURLs
            };
        }
    }
}
