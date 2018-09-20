using Oakton;

namespace ProductFinder
{
    public class ProgramArguments
    {
        [Description("Path to music contracts file")]
        public string MusicContractsFilePath { get; set; }
        
        [Description("Path to partner contracts file")]
        public string PartnerContractsFilePath { get; set; }
    }
}