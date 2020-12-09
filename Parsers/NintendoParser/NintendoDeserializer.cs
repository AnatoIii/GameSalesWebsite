using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NintendoParser
{
    /// <summary>
    /// Implementation of <see cref="IDeserializer"/> for work with thrm data for Nintendo
    /// </summary>
    internal class NintendoDeserializer : IDeserializer
    {
        /// <summary>
        /// <see cref="IDeserializer.Deserialize(string)"/>
        /// </summary>
        public IEnumerable<GameEntry> Deserialize(string json)
        {
            JObject jObject = JObject.Parse(json);

            var games = jObject["results"].First().First().First();

            return games.Children()
                .Where(c => c["type"].Value<string>() == "game")
                .Select(c => MapEntry(c))
                .Where(e => e.PlatformSpecificId != null);
        }

        private GameEntry MapEntry(JToken jToken)
        {
            string name = jToken["title"].ToObject<string>();
            string platformSpecificId = jToken["slug"].ToObject<string>();
            string description = jToken["description"].ToObject<string>();
            int basePrice = (int)jToken["msrp"].ToObject<decimal>() * 100;
            int discountedPrice = (int)jToken["salePrice"].ToObject<decimal>() * 100;
            string thumbnail = jToken["boxArt"] == null ? "" : jToken["boxArt"].ToObject<string>();
            string thumbnailURL = $"https://www.nintendo.com/{thumbnail}";

            string review = "";
            List<string> pictureURLs = new List<string>();

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


        /// <summary>
        /// Get game screenshots by <paramref name="html"/>
        /// </summary>
        /// <param name="gameBaseURL">Game page URL</param>
        /// <param name="slug">Unique game name</param>
        /// <returns>List of screenshots urls</returns>
        public List<string> GetGameScreenshots(string gameBaseURL, string slug)
        {
            var pictureURLs = new List<string>();
            HtmlWeb web = new HtmlWeb();
            HtmlDocument html = web.Load(gameBaseURL + slug);
            HtmlNodeCollection htmlNodes = html.DocumentNode.SelectNodes("//product-gallery-item[@type='image']");
            if (htmlNodes == null) return new List<string>();
            foreach (var node in htmlNodes)
            {
                var imgSrc = node.GetAttributeValue("src", string.Empty);
                if (imgSrc != string.Empty)
                    pictureURLs.Add(imgSrc);
            }
            return pictureURLs;
        }
    }
}
