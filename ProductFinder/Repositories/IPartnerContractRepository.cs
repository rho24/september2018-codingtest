using System.Collections.Generic;
using ProductFinder.Domain;

namespace ProductFinder.Repositories
{
    public interface IPartnerContractRepository
    {
        void Load(IEnumerable<PartnerContract> contracts);
        IEnumerable<PartnerContract> GetAll();
        PartnerContract GetByPartnerName(string partnerName);
    }
}