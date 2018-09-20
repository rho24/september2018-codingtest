using System;
using System.Collections.Generic;
using ProductFinder.Domain;

namespace ProductFinder.Repositories
{
    public interface IMusicContractsRepository
    {
        void Load(IEnumerable<MusicContract> contracts);
        IEnumerable<MusicContract> GetAll();
        IEnumerable<MusicContract> GetForUsageAndDate(Usage usage, DateTime date);
    }
}