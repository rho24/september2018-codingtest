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
    public class ProgramCommand : OaktonAsyncCommand<ProgramArguments>
    {
        public override async Task<bool> Execute(ProgramArguments input)
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

            LoadData(input, container);

            ProcessCommands(container);

            ConsoleWriter.Write("Exiting.");

            return true;
        }

        private static void ProcessCommands(Container container)
        {
            var inputParser = container.GetInstance<InputParser>();
            var musicFinder = container.GetInstance<IMusicFinder>();

            do
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
                    ConsoleWriter.Write(ConsoleColor.Red, "Error:");
                    ConsoleWriter.Write(ConsoleColor.Red, e.Message);
                    ConsoleWriter.PrintHorizontalLine();
                    ConsoleWriter.Write("Enter 'exit' to exit.");
                }
            } while (true);
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
                    ConsoleWriter.Write(
                        $"{c.Artist}|{c.Title}|{c.Usages.First()}|{c.StartDate:dd MMM yyyy}|{c.EndDate:dd MMM yyyy}");
                }
            }
            
            ConsoleWriter.PrintHorizontalLine();
            ConsoleWriter.Line();
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