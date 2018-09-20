using System;
using System.Collections.Generic;
using ProductFinder.Domain;
using ProductFinder.Repositories;

namespace ProductFinder.Services
{
    public class MusicFinder : IMusicFinder
    {
        private readonly IPartnerContractRepository _partnerContractRepo;
        private readonly IMusicContractsRepository _musicContractRepo;

        public MusicFinder(IPartnerContractRepository partnerContractRepo, IMusicContractsRepository musicContractRepo)
        {
            _partnerContractRepo = partnerContractRepo;
            _musicContractRepo = musicContractRepo;
        }

        public IEnumerable<MusicContract> FindContracts(string partnerName, DateTime date)
        {
            var partnerContract = _partnerContractRepo.GetByPartnerName(partnerName);
            if(partnerContract == null)
                throw new ArgumentException("Partner does not exist.", nameof(partnerName));

            var contracts = _musicContractRepo.GetForUsageAndDate(partnerContract.Usage, date);

            return contracts;
        }
    }
}