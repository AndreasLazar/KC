using System;
using System.Collections.Generic;

namespace MeineApp.Data
{
    public class Speaker
    {
        public int ID { get; set; }
        public string Vorname { get; set; }
        public string Nachname { get; set; }
        public string KurzZeichen { get; set; }
        public ICollection<KnowledgeCircle> KnowledgeCirles { get; set; }
    }
}