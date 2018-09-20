using System.Collections.Generic;
using System.Linq;
using ProductFinder.Domain;

namespace ProductFinder.Repositories
{
    public class MusicContractsRepository : IMusicContractsRepository
    {
        private readonly List<MusicContract> _musicContracts = new List<MusicContract>();

        public void Load(IEnumerable<MusicContract> contracts)
        {
            _musicContracts.AddRange(contracts);
        }

        public IEnumerable<MusicContract> GetAll()
        {
            // Return array so internal list cannot be modified, note entities are mutable as its not deep copying.
            return _musicContracts.ToArray();
        }
    }
}