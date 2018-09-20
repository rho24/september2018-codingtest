using System.Collections.Generic;
using ProductFinder.Domain;

namespace ProductFinder.Repositories
{
    public class PartnerContractRepository : IPartnerContractRepository
    {
        private readonly List<PartnerContract> _partnerContracts = new List<PartnerContract>();

        public void Load(IEnumerable<PartnerContract> contracts)
        {
            _partnerContracts.AddRange(contracts);
        }

        public IEnumerable<PartnerContract> GetAll()
        {
            // Return array so internal list cannot be modified, note entities are mutable as its not deep copying.
            return _partnerContracts.ToArray();
        }
    }
}