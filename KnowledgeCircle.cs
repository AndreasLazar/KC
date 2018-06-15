using System;

namespace MeineApp.Data
{
    public class KnowledgeCircle
    {
        public int ID { get; set; }
        public string Thema { get; set; }
        public DateTime Datum { get; set; }
        public int SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
    }
}