using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Pony.Domain.Maze.Rules
{
    public class MazeRules:IMazeRules
    {
        public MazeRules()
        {

        }

        public bool IsPonyNameValidAsync(string name)
        {
            HtmlWeb web = new HtmlWeb();
            var doc =  web.Load("https://www.ranker.com/list/all-my-little-pony-friendship-is-magic-characters/reference");
            var node1 = doc.DocumentNode.SelectNodes("//*[@id=\"list\"]/h2//div//span[@class=\"listItem__title\"]");
            var node2 = doc.DocumentNode.SelectNodes("//*[@id=\"list\"]/h2//div//a");
            var ponnyNames = (node1.Select(node=>node.InnerText)).Concat(node2.Select(node=>node.InnerText));
            return ponnyNames.FirstOrDefault(x => x == name) != null;
        }
        
    }
}
