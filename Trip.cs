using System;

namespace commuteFormGen {
    public class Trip {
        public DateTime Date { get; set; }
        public string Enter { get; set; }
        public string Leave { get; set; }
        public ushort Coast { get; set; }

        public string ShortDate => Date.ToString("dd日 dddd");

        public string Money => Coast.ToString("C0");
    }
}
