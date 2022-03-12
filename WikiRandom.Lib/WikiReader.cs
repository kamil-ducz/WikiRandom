using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikirandom
{
    public class WikiReader
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        private readonly IGetWikiRandom Screen;
        public WikiReader(IGetWikiRandom screen)
        {
            Screen = screen;
        }

        public async void ReadAdHocArticle() 
        {
            var article = await Screen.GetWikiRandom();
            Console.WriteLine(article);
        }
        public async Task<string> LoadArticle()
        {
            var article = await Screen.GetWikiRandom();
            return article;
        }

        public string MySortingKey(string s)
        {
            if (s == null || s == "")
            {
                return "";
            }
            var key = s.Last();
            return key.ToString();
        }

        public void ShowAllArticles(List<string> articlesList)
        {
            Console.WriteLine("Please find below list of all articles loaded: ");
            foreach (var article in articlesList.Select((name, index)=>(name, index)))
            {
                Console.WriteLine($"{article.index+1}. {article.name}");
            }
        }
        /// <summary>
        /// Displays favourite articles followed by criteria entered in the function arguments
        /// </summary>
        /// <param name="articlesList"></param>
        /// <param name="artLenght"></param>
        public void ShowFavArticles(List<string> articlesList, int artLenght)
        {
            var favArticles = articlesList
                .Where(a => a.Length < artLenght)
                .Select(a => a.ToLower())
                .Distinct()
                .OrderByDescending(MySortingKey)
                .Take(10);

            Console.WriteLine("Please find below list of favourite articles: ");
            int i = 1;
            foreach (var favArticle in favArticles)
            {
                Console.WriteLine($"{i}. {favArticle}");
                i++;
            }
        }
        public void ShowFavArticles(List<string> articlesList, int artLenght, string startingLetter) //ShowFavArticles method overload
        {
            var favArticles = articlesList
                .Where(a => a.Length < artLenght && a.StartsWith(startingLetter))
                .Select(a => a.ToLower())
                .Distinct()
                .OrderByDescending(MySortingKey)
                .Take(10);

            foreach (var article in favArticles)
            {
                Console.WriteLine(article);
            }
        }
    }
}
