using HtmlAgilityPack;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pony.Domain.Mazes.Rules
{
    public class MazeRules : IMazeRules
    {
        private readonly IMemoryCache _cache;

        public MazeRules(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool IsPonyNameValidAsync(string name)
        {
            var key = $"pony-{name}";
            var pony = _cache.Get(key);
            if (pony == null)
            {
                HtmlWeb web = new HtmlWeb();
                var doc = web.Load("https://www.ranker.com/list/all-my-little-pony-friendship-is-magic-characters/reference");
                var node1 = doc.DocumentNode.SelectNodes("//*[@id=\"list\"]/h2//div//span[@class=\"listItem__title\"]");
                var node2 = doc.DocumentNode.SelectNodes("//*[@id=\"list\"]/h2//div//a");
                var ponnyNames = (node1.Select(node => node.InnerText)).Concat(node2.Select(node => node.InnerText));
                foreach (var ponyName in ponnyNames)
                {
                    var newKey = $"pony-{name}";
                    _cache.Set(newKey, ponyName);
                }
                return ponnyNames.FirstOrDefault(x => x == name) != null;
            }
            return true;
        }

        public bool isValidDirection(string direction)
        {
            var validDirection = new string[] { "south", "west", "north", "stay", "east" };
            return validDirection.Contains(direction);
        }
    }
}