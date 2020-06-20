using Newtonsoft.Json.Linq;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace PSStoreParser
{
    internal class Deserializer : IDeserializer
    {
        public IEnumerable<GameEntry> Deserialize(string json)
        {
            JObject jObject = JObject.Parse(json);

            return jObject["included"].Children()
                .Where(c => c["type"].Value<string>() == "game")
                .Select(c => MapEntry(c))
                .Where(e => e.PlatformSpecificId != null);
        }

        private GameEntry MapEntry(JToken jToken)
        {
            string name = jToken["attributes"]["name"].ToObject<string>();
            string platformSpecificId = jToken["id"].ToObject<string>();
            string description = jToken["attributes"]["long-description"].ToObject<string>();
            string thumbnailURL = jToken["attributes"]["thumbnail-url-base"].ToObject<string>();
            string review = "";
            JToken rating = jToken["attributes"]["star-rating"];
            if (rating["score"].Type != JTokenType.Null)
            {
                review = "Score: " + rating["score"].ToObject<string>() + "/5. " +
                                "Total votes: " + rating["total"];
            }
            List<string> pictureURLs = new List<string>();
            var pictures = jToken["attributes"]["media-list"]["screenshots"].Children();
            foreach (var entry in pictures)
            {
                pictureURLs.Add(entry["url"].ToObject<string>());
            }

            JToken skus = jToken["attributes"]["skus"];
            if (skus == null)
            {
                return new GameEntry();
            }

            int discountedPrice = 0;
            int basePrice = 0;
            for (int i = 0; i < skus.Count(); i++)
            {
                discountedPrice = skus[i]["prices"]["non-plus-user"]["actual-price"]["value"].ToObject<int>();
                JToken basePriceJToken = jToken["attributes"]["skus"][0]["prices"]["non-plus-user"]["strikethrough-price"];
                basePrice = basePriceJToken.Type == JTokenType.Null ? discountedPrice : basePriceJToken["value"].ToObject<int>();
                if(basePrice != 0)
                {
                    break;
                }
            }

            return new GameEntry()
            {
                Name = name,
                DiscountedPrice = discountedPrice,
                BasePrice = basePrice,
                PlatformSpecificId = platformSpecificId,
                Description = description,
                PictureURLs = pictureURLs,
                Review = review,
                ThumbnailURL = thumbnailURL
            };
        }
    }
}
