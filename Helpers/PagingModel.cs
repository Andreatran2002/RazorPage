using System;

namespace entity_fr.helpers{
    public class PagingModel{
        public int currentPage { get; set; }
        public int countPages { get; set; }
        public Func<int?,string> generateUrl{set;get;}
    }
}