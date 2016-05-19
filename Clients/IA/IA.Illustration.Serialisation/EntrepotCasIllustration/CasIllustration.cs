using System;
using System.Collections.Generic;
using IAFG.IA.IL.AF.Illustration.ENUMs;
using IAFG.IA.VI.ENUMs;

namespace IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration
{
    public class CasIllustration
    {
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public String NoVersion { get; set; }
        public string DejaAlle { get; set; }
        public string F1 { get; set; }
        public Langue LangueCorrespondance { get; set; }
        public string Q4 { get; set; }
        public string Q6 { get; set; }
        public string PropoInfoGeneraleId { get; set; }
        public TypeProposition TypeProposition { get; set; }
        public List<CasScenario> Scenarios { get; set; }
    }

    public class CasScenario
    {
        public int NoScenario { get; set; }
    }
}