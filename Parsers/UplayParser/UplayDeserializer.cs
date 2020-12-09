using HtmlAgilityPack;
using Parsers.Core.Models;
using Parsers.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace UplayParser
{
    /// <summary>
    /// Implementation of <see cref="IDeserializer"/> for work with thrm data for Uplay
    /// </summary>
    public class UplayDeserializer : IDeserializer
    {
        /// <summary>
        /// <see cref="IDeserializer.Deserialize(string)"/>
        /// </summary>
        public IEnumerable<GameEntry> Deserialize(string input)
        {
            HtmlDocument document = new HtmlDocument();
            string htmlString = input.ToString();
            document.LoadHtml(htmlString);

            HtmlNodeCollection nodesCollection = document.DocumentNode.SelectNodes("//li[@class='grid-tile cell shrink']");
            if(nodesCollection == null)
            {
                return new GameEntry[] { };
            }
            List<GameEntry> games = new List<GameEntry>();
            foreach (var node in nodesCollection)
                games.Add(_MapEntry(node));

            return games;
        }

        /// <summary>
        /// Get game description by <paramref name="html"/>
        /// </summary>
        /// <param name="html">Target html</param>
        /// <returns>Game description</returns>
        internal string GetGameDescription(string html)
        {
            HtmlDocument document = new HtmlDocument();
            string htmlString = html.ToString();
            document.LoadHtml(htmlString);

            List<string> descriptionParts = document.DocumentNode.SelectSingleNode(".//article[@class='description-content']")
                .Descendants("p")
                .Select(n => _StripString(Regex.Replace(n.InnerHtml, "<br>", "\r\n")))
                .ToList();

            return string.Join("\r\n ", descriptionParts);
        }

        private GameEntry _MapEntry(HtmlNode htmlNode)
        {
            // Price handling
            string basePriceValue = _HandlePriceForRubCurrency(htmlNode.SelectSingleNode(".//span[@class='price-item']")?.InnerHtml);
            int.TryParse(basePriceValue, out int discountedPrice);

            string discountedPriceValue = _HandlePriceForRubCurrency(htmlNode.SelectSingleNode(".//span[@class='price-sales standard-price']")?.InnerHtml);
            int.TryParse(discountedPriceValue, out int basePrice);

            if (discountedPrice == 0)
                discountedPrice = basePrice;

            // Name
            var gameNode = htmlNode.SelectSingleNode(".//div[@class='product-image card-image-wrapper']").ChildNodes[1];
            string name = _StripString(gameNode.Attributes["title"].Value.Split(',')[0] ?? string.Empty);

            // Images
            List<string> pictureURLs = new List<string>();
            _AddImgToListByTargetAttribute(pictureURLs, gameNode, "data-retina-src");
            _AddImgToListByTargetAttribute(pictureURLs, gameNode, "data-desktop-src");
            _AddImgToListByTargetAttribute(pictureURLs, gameNode, "data-tablet-src");
            _AddImgToListByTargetAttribute(pictureURLs, gameNode, "data-mobile-src");
            string thumbnailURL = pictureURLs.ElementAt(1);

            // Platform id and postfix
            string platformSpecificId = htmlNode.ChildNodes[0].Attributes["data-itemid"].Value ?? string.Empty;

            return new GameEntry()
            {
                Name = name,
                DiscountedPrice = discountedPrice,
                BasePrice = basePrice,
                PlatformSpecificId = platformSpecificId,
                PictureURLs = pictureURLs,
                ThumbnailURL = thumbnailURL
            };
        }

        private string _HandlePriceForRubCurrency(string targetStringPrice)
            => targetStringPrice?
            .Replace(".", "")
            .Split(',')[0]
            .Trim() 
            + "00";

        private void _AddImgToListByTargetAttribute(List<string> pictures, HtmlNode node, string attributeName)
        {
            string imgURL = node.Attributes[attributeName].Value.Split('?')[0] ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(imgURL))
                pictures.Add(imgURL);
        }

        private string _StripString(string input)
        {
            string res = Regex.Replace(input, "<.*?>", string.Empty);
            res = Regex.Replace(res, "\t", string.Empty);
            res = Regex.Replace(res, "&.*?;", string.Empty);
            return res;
        }
    }
}
