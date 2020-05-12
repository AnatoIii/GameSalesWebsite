using Newtonsoft.Json.Linq;
using Parsers.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                .ToList();
        }
        private static GameEntry MapEntry(JToken jToken)
        {
            string name = jToken["attributes"]["name"].ToObject<string>();
            string platformSpecificId = jToken["id"].ToObject<string>();
            string description = jToken["attributes"]["long-description"].ToObject<string>();
            int discountedPrice = jToken["attributes"]["skus"][0]["prices"]["non-plus-user"]["actual-price"]["value"].ToObject<int>();

            JToken basePriceJToken = jToken["attributes"]["skus"][0]["prices"]["non-plus-user"]["strikethrough-price"];
            int basePrice = basePriceJToken.Type == JTokenType.Null ? discountedPrice : basePriceJToken["value"].ToObject<int>();

            List<string> pictureURLs = new List<string>();
            var pictures = jToken["attributes"]["media-list"]["screenshots"].Children();
            foreach (var entry in pictures)
            {
                pictureURLs.Add(entry["url"].ToObject<string>());
            }

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
