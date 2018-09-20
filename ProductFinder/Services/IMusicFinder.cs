using System;
using System.Collections.Generic;
using ProductFinder.Domain;

namespace ProductFinder.Services
{
    public interface IMusicFinder
    {
        IEnumerable<MusicContract> FindContracts(string partnerName, DateTime date);
    }
}