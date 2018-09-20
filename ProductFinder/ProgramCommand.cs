using System.Threading.Tasks;
using Lamar;
using Oakton;

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
                    s.WithDefaultConventions();
                });
            });
            
            
            
            return true;
        }
    }
}