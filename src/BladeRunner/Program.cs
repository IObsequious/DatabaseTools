using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BladeRunner.JsonModel.Jurisdictions;
using BladeRunner.Sql;
using BladeRunner.Web;


namespace BladeRunner
{
    internal static class Program
    {
        public const string JurisdictionsURL = @"http://commonstandardsproject.com/api/v1/jurisdictions";

        public static int Main(params string[] args)
        {
            if ((args?.Length ?? 0) > 0)
            {
                Console.SetWindowSize(160, 50);

                switch (args[0].ToLower())
                {
                    case "jurisdiction":
                        ProcessJurisdictions();
                        return 0;
                        break;
                    case "standard":
                        ProcessStandards();
                        return 0;
                        break;
                    default:
                        ShowHelp();
                        return 1;
                        break;
                }

            }
            else
            {
                ShowHelp();
                return 1;
            }
        }


        private static void ShowHelp()
        {
            Console.WriteLine();
            Console.WriteLine("SYNTAX: BladeRunner [ jurisdiction | standard ]");
            Console.WriteLine();
        }

        private static void ProcessJurisdictions()
        {
 //           Console.WriteLine("Getting jurisdictions...");

            JurisdictionCollection collection = Requester.GetJurisdictions();

//            Console.WriteLine("Importing jurisdictions...");

            JurisdictionDBImporter.Import(collection);

            int standardSetsCount = collection.SelectMany(j => j.Data.StandardSets).Count();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Complete!");
            Console.WriteLine("Import task was successful. {0} jurisdictions and {1} standard sets imported.", collection.Count, standardSetsCount);
            Console.ResetColor();
        }

        private static void ProcessStandards()
        {

            int standardSetsCount = 0;

            int standardsCount = 0;

            var list = Requester.GetJurisdictions().SelectMany(j => j.Data.StandardSets).ToList();

            var total = list.Count;

            for (int i = 0; i < total; i++)
            {
                var current = list[i];
                var standardSet = Requester.GetStandardSet(current);

                standardSetsCount++;
                
                standardsCount += standardSet.Data.Standards.Count;

                int percent = (int)Math.Round(i * ((decimal)100 / total));

                ConsoleEx.WriteProgress("Importing Standard Sets", $"Importing standard set {i} of {total}", standardSet.Data.ToString(), percent, 0);

                StandardDBImporter.Import(standardSet);

            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine("Complete!");
            Console.WriteLine("Import task was successful. {0} standard sets and {1} standards were imported.", standardSetsCount, standardsCount);
            Console.ResetColor();

        }

    }

}
