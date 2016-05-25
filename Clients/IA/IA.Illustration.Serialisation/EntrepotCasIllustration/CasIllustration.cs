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
        public CasEmpruntBancaire EmpruntBancaire { get; set; }
        public CasTaxation Taxation { get; set; }
        public CasPrestation Prestation { get; set; }
        public CasPrimes Primes { get; set; }
        public CasProposition Proposition { get; set; }
    }

    public class CasProposition
    {
        public string AddOptAutreContrat { get; set; }
        public int AddOptAutreMontant { get; set; }
        public object AddOptAutrePropo { get; set; }
        public List<CasAdresse> Adresses { get; set; }
        public List<CasAgent> Agents { get; set; }
    }

    public class CasAgent
    {
    }

    public class CasAdresse
    {
        public string RouteRurale { get; set; }
        public string Succursale { get; set; }
        public string CodePostal { get; set; }
        public string Province { get; set; }
        public string Ville { get; set; }
        public string CasePostale { get; set; }
        public string ExtApt { get; set; }
        public string Extension { get; set; }
        public string Rue { get; set; }
        public string NoCivique { get; set; }
    }

    public class CasPrimes
    {
        public int AgeFondCibleRetourPrime { get; set; }
        public int AnneeFondCibleRetourPrime { get; set; }
        public TypeReponse CalculRenouvellement { get; set; }
        public int Duree { get; set; }
        public bool FlagPrimeMinimum { get; set; }
        public decimal FondCible { get; set; }
        public bool HitTarget { get; set; }
        public int Jusqua { get; set; }
        public List<decimal> MontantPrime { get; set; }
        public int PourcentageRetourPrime { get; set; }
        public double PrimeModale { get; set; }
        public List<int> TypePrime { get; set; }
        public TypeDePrimeEcranPrincipal TypePrimeEcranPrincipal { get; set; }
        public TypeDeValeurACalculer ValeurACalculer { get; set; }
    }

    public class CasPrestation
    {
        public List<int> Annee_Deces { get; set; }
        public List<int> Annee_Invalidite { get; set; }
        public List<decimal> Montant_Deces { get; set; }
        public List<decimal> Montant_Invalidite { get; set; }
        public TypeDeces TypePrestationDeces { get; set; }
        public TypeInvalidite TypePrestationInvalidite { get; set; }
    }

    public class CasTaxation
    {
        public List<decimal> TauxMarginalCorp { get; set; }
        public List<decimal> TauxMarginalInd { get; set; }
        public decimal TauxRembCorpDividende { get; set; }
        public decimal TauxTOHCorp { get; set; }
        public List<decimal> TaxeDividendeCorp { get; set; }
        public List<decimal> TaxeDividendeInd { get; set; }
        public List<decimal> TaxeGainCapitalCorp { get; set; }
        public List<decimal> TaxeGainCapitalInd { get; set; }
        public List<decimal> TaxeSurCapitalCorp { get; set; }
    }

    public class CasEmpruntBancaire
    {
        public int Amortissement { get; set; }
        public TypeFrequenceEmprunt Frequence { get; set; }
        public bool IsEmpruntInitialised { get; set; }
        public TypeParametrePaiment ParametrePaiment { get; set; }
        public double Solde { get; set; }
        public List<decimal> TauxInteret { get; set; }
        public TypeEmprunt TypePret { get; set; }
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