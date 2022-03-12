using Services;
using System;
using System.Threading.Tasks;

/// <summary>
/// c# servicy - osobne obiekty się opakowuje, mamy np. pobieracz co pobiera 
//słówko klucz to było "polimorfizm"
//
//zad utwórz osobny projekt services c# lib -> zaimplementuj 2 obiekty jeden co używa http client a drugi tamtej libki
//mam 2 obiekty które robiąto samo ale w inny sposób
//interface do tych obiektów tzn.żeby dziedziczyły po tym interface
//
//
//jakby http client był tworzony za każdym razem nowy to by braklo socketów http, ale ten co napisałem nie zareaguje na zmiany w dns
//
//mechanizm dependency incjection, http client powinien być używany z service provider dla niego
//.net dependency injection, httpclient factory do wstrzykiwania klienta http
//
//DI do tworzenia obiektów, do pobierania ich z dostawcy
//
//interface dla tego to w webowym tylko - po zakończonym zadaniu nowe zadanie to UI dla tego
//
//elekton framework, vs code tez jest w typescript 
//
//zapytania linq bardzo ważne są, rest, protokół http,
/// 
/// </summary>

namespace Wikirandom
{
    class Program
    {
        static async Task Main()
        {
            Console.WriteLine("This app retrieves <title> tag content from random Wikipedia article. Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine("\nPress 1 to use HttpClient,\nPress 2 to use HTMLAgilityPack\nPress any other key to exit: ");
            var appDriver = Console.ReadLine();
            IGetWikiRandom screen;

            if(appDriver == "1")
            {
                screen = new GetWikiRandomHttpClientService();
            }
            else
            {
                screen = new GetWikiRandomHTMLAgilityPack();
            }
            var wikiReader = new WikiReader(screen);

            try
            {

                await wikiReader.ReadArticle();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occured! Contact your application supporter. Issue details: " + e);
            }

            Console.ReadKey();
        }
    }
}
