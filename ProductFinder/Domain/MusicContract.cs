using System;

namespace ProductFinder.Domain
{
    public class MusicContract
    {
        public string Artist { get; set; }
        public string Title { get; set; }
        public Usage[] Usages { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}