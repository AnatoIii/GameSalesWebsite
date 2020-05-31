using AngleSharp.Html.Dom;
using HtmlAgilityPack;
using Newtonsoft.Json.Linq;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace SteamParser
{
    /// <summary>
    /// Implementation of <see cref="IDeserializer"/> for work with thrm data for Steam
    /// </summary>
    internal class HtmlDeserializer : IDeserializer
    {
        /// <summary>
        /// <see cref="IDeserializer.Deserialize(string)"/>
        /// </summary>
        public IEnumerable<GameEntry> Deserialize(string json)
        {
            JObject jObject = JObject.Parse(json);
            JToken jToken = jObject["results_html"];

            HtmlDocument document = new HtmlDocument();
            string htmlString = jToken.ToString();
            document.LoadHtml(htmlString);
            HtmlNodeCollection nodesCollection = document.DocumentNode.SelectNodes("//a");

            List<GameEntry> games = new List<GameEntry>();
            foreach (var node in nodesCollection)
                games.Add(_MapEntry(node));

            return games;
        }

        /// <summary>
        /// Get game description by <paramref name="html"/>
        /// </summary>
        /// <param name="html">Target html</param>
        /// <returns></returns>
        public string GetGameDescription(string html)
        {
            HtmlDocument document = new HtmlDocument();
            string htmlString = html.ToString();
            document.LoadHtml(htmlString);

            string descriptionFromDocument = document.GetElementbyId("game_area_description")?.InnerHtml 
                ?? document.DocumentNode.Descendants()
                    .Where(x => x.NodeType == HtmlNodeType.Element)
                    .Where(x => x.Attributes["property"]?.Value == "og:description")
                    .Select(x => x.Attributes["content"]?.Value)
                    .FirstOrDefault();

            return descriptionFromDocument.StripHTML();
        }

        private GameEntry _MapEntry(HtmlNode htmlNode)
        {
            string name = string.Empty;
            string review = string.Empty;
            string gameLinkPostfix = string.Empty;
            string platformSpecificId = string.Empty;
            int basePrice = 0;
            int discountedPrice = 0;
            IEnumerable<string> pictureURLs = null;

            platformSpecificId = htmlNode.Attributes["data-ds-appid"]?.Value ?? htmlNode.Attributes["data-ds-packageid"]?.Value ?? htmlNode.Attributes["data-ds-bundleid"]?.Value;

            HtmlNode imageNode = htmlNode.ChildNodes[1].ChildNodes[0];
            pictureURLs = imageNode.Attributes["srcset"].Value.Split(", ").Select(p => p.Substring(0,p.IndexOf("jpg") + 3));
            string thumbnailURL = pictureURLs.ElementAt(1);

            HtmlNode gameNode = htmlNode.ChildNodes[3];
            name = gameNode.ChildNodes[1].ChildNodes[1].InnerHtml;
            review = gameNode.ChildNodes[5].ChildNodes[1].Attributes["data-tooltip-html"]?.Value.StripHTML();
            discountedPrice = int.Parse(gameNode.ChildNodes[7].Attributes["data-price-final"].Value);

            if (_TryGetChild(gameNode.ChildNodes[7].ChildNodes[3], 1, out HtmlNode fullPriceNode) && fullPriceNode != null)
            {
                basePrice = int.Parse(fullPriceNode.ChildNodes[0].InnerHtml
                    .Replace(" ", "")
                    .Replace("₴", "00"));
            }
            else
            {
                basePrice = discountedPrice;
            }

            return new GameEntry()
            {
                Name = name,
                DiscountedPrice = discountedPrice,
                Review = review,
                BasePrice = basePrice,
                PlatformSpecificId = platformSpecificId,
                PictureURLs = pictureURLs,
                ThumbnailURL = thumbnailURL
            };
        }

        private bool _TryGetChild(HtmlNode targetNode, int childIndex, out HtmlNode child)
        {
            child = null;

            try
            {
                child = targetNode.ChildNodes[childIndex];

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
