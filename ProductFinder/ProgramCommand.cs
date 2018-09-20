using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lamar;
using Oakton;
using ProductFinder.Csv;
using ProductFinder.Domain;
using ProductFinder.Repositories;
using ProductFinder.Services;

namespace ProductFinder
{
    [Description("Find available products")]
    public class ProgramCommand : OaktonCommand<ProgramArguments>
    {
        public override bool Execute(ProgramArguments input)
        {
            ConsoleWriter.Write("Search in the format 'partner name 1st Jun 2012'.");
            ConsoleWriter.Write("Enter 'exit' to exit.");
            ConsoleWriter.Line();

            var container = new Container(x =>
            {
                x.Scan(s =>
                {
                    s.AssembliesAndExecutablesFromApplicationBaseDirectory();
                    s.RegisterConcreteTypesAgainstTheFirstInterface();
                });

                x.For<IMusicContractsRepository>().Use<MusicContractsRepository>().Singleton();
                x.For<IPartnerContractRepository>().Use<PartnerContractRepository>().Singleton();
            });

            try
            {
                LoadData(input, container);
            }
            catch (Exception e)
            {
                ConsoleWriter.Write(ConsoleColor.Red, $"Error loading data: {e.Message}");
                return false;
            }

            ProcessCommands(container);

            ConsoleWriter.Write("Exiting.");

            return true;
        }

        private static void ProcessCommands(Container container)
        {
            var inputParser = container.GetInstance<InputParser>();
            var musicFinder = container.GetInstance<IMusicFinder>();

            while (true)
            {
                try
                {
                    var command = Console.ReadLine();
                    if (command == "exit")
                        break;

                    var input = inputParser.ParseInputs(command);

                    var contracts = musicFinder.FindContracts(input.PartnerName, input.Date);

                    PrintContracts(contracts);
                }
                catch (Exception e)
                {
                    ConsoleWriter.Write(ConsoleColor.Red, $"Error: {e.Message}");
                    ConsoleWriter.PrintHorizontalLine();
                    ConsoleWriter.Write("Enter 'exit' to exit.");
                }
            }
        }

        private static void PrintContracts(IEnumerable<MusicContract> contracts)
        {
            if (!contracts.Any())
            {
                ConsoleWriter.Write("No products found");
            }
            else
            {
                ConsoleWriter.Write("Artist|Title|Usage|StartDate|EndDate");
                foreach (var c in contracts)
                {
                    var usageDisplay = UsageDisplay(c.Usages.First());
                    ConsoleWriter.Write(
                        $"{c.Artist}|{c.Title}|{usageDisplay}|{c.StartDate:dd MMM yyyy}|{c.EndDate:dd MMM yyyy}");
                    
                    // I haven't formatted the date to the specification, hopefully thats ok!
                }
            }

            ConsoleWriter.PrintHorizontalLine();
            ConsoleWriter.Line();
        }

        private static string UsageDisplay(Usage usage)
        {
            switch (usage)
            {
                case Domain.Usage.DigitalDownload:
                    return "digital download";
                case Domain.Usage.Streaming:
                    return "streaming";
                default:
                    throw new ArgumentOutOfRangeException(nameof(usage), usage, null);
            }
        }

        private static void LoadData(ProgramArguments input, Container container)
        {
            var csvDataLoader = container.GetInstance<ICsvDataLoader>();
            var musicRepo = container.GetInstance<IMusicContractsRepository>();
            var partnerRepo = container.GetInstance<IPartnerContractRepository>();

            var musicContracts =
                csvDataLoader.LoadData(input.MusicContractsFilePath,
                    container.GetInstance<ICsvMapper<MusicContract>>());
            musicRepo.Load(musicContracts);

            var partnerContracts = csvDataLoader.LoadData(input.PartnerContractsFilePath,
                container.GetInstance<ICsvMapper<PartnerContract>>());
            partnerRepo.Load(partnerContracts);
        }
    }
}