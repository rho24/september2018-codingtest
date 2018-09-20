using System.Linq;
using System.Threading.Tasks;
using Lamar;
using Oakton;
using ProductFinder.Csv;
using ProductFinder.Domain;
using ProductFinder.Repositories;

namespace ProductFinder
{
    [Description("Find available products")]
    public class ProgramCommand : OaktonAsyncCommand<ProgramArguments>
    {
        public override async Task<bool> Execute(ProgramArguments input)
        {
            ConsoleWriter.Write(input.MusicContractsFilePath);
            ConsoleWriter.Write(input.PartnerContractsFilePath);

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

            var csvDataLoader = container.GetInstance<ICsvDataLoader>();
            var musicRepo = container.GetInstance<IMusicContractsRepository>();

            var musicContracts = csvDataLoader.LoadData(input.MusicContractsFilePath, container.GetInstance<ICsvMapper<MusicContract>>());
            musicRepo.Load(musicContracts);
            
            ConsoleWriter.Write(musicRepo.GetAll().First().Title);
            
            return true;
        }
    }
}