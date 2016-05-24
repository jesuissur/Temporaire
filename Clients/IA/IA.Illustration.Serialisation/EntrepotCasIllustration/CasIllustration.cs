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
        public bool VMaxACalculer { get; set; }
        public int NbVie { get; set; }
        public TypeConcept TypeConcept { get; set; }
        public CasConceptIris2 ConceptIris2 { get; set; }
        public CasConceptAssuranceRetraite ConceptAssuranceRetraite { get; set; }
    }

    public class CasConceptAssuranceRetraite
    {
        public int AgeDepart { get; set; }
        public int AgeHeritage { get; set; }
        public decimal CompteDuMarche { get; set; }
        public bool DeductionCnap { get; set; }
        public string DifferenceOuTaux { get; set; }
        public List<decimal> DifferenceTaux { get; set; }
        public int Duree { get; set; }
        public FrequencePaiement FrequencePaiement { get; set; }
        public decimal IntGuarantit { get; set; }
        public decimal PretBancaireCumulatif { get; set; }
        public RespectRatioPretVsValeurRachat RespectRatioPretVsValeurRachat { get; set; }
        public int RespectRatioPretVsValeurRachatPerso { get; set; }
        public List<decimal> RevenuRetraite { get; set; }
        public decimal RevenuRetraiteChoix { get; set; }
        public decimal TauxGIA { get; set; }
        public decimal TauxIndex { get; set; }
        public decimal TauxIntDeductible { get; set; }
        public string TranfertAuto { get; set; }
        public TypeDeValeurACalculerCpt ValeurACalculer { get; set; }
        public TypeValeurAcalculerComparaison ValeurACalculerComparaison { get; set; }
        public string ValeurCible { get; set; }
    }

    public class CasConceptIris2
    {
        public int CalculPrimeAdd { get; set; }
        public int RembCapDuree { get; set; }
        public int RembIntDuree { get; set; }
        public decimal DeductibleImpot { get; set; }
        public decimal RembBalance { get; set; }
        public bool DeductionCnap { get; set; }
        public bool DesiredLoanDesactivated { get; set; }
        public bool FigerSoldePret { get; set; }
        public bool IrisExtra { get; set; }
        public bool TaxePrimeExcedentaire { get; set; }
        public List<Decimal> DesiredLoan { get; set; }
        public List<Decimal> ReinvestRendement { get; set; }
        public List<Decimal> RembCapMontant { get; set; }
        public double FraisGarantie { get; set; }
        public TypeRemb ProvenanceRembCap { get; set; }
        public TypeRemb ProvenanceRembInt { get; set; }
        public TypeRembChoix RembCapChoix { get; set; }
        public TypeDuree RembCapTypeDuree { get; set; }
        public TypeDuree RembIntTypeDuree { get; set; }
        public double SoldeAAtteindre { get; set; }
        public TypeEmprunteur TypeEmprunteur { get; set; }
        public string VehiculeCompteCollateral { get; set; }
    }
}