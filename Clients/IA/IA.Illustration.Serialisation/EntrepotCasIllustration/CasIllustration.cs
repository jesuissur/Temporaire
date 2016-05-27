using System;
using System.Collections.Generic;
using IAFG.IA.IL.AF.Illustration.ENUMs;
using IAFG.IA.VI.AF.PDF.ENUMs;
using IAFG.IA.VI.AF.Proposition;
using IAFG.IA.VI.AF.Proposition.ENUMs;
using IAFG.IA.VI.ENUMs;
using TypeDuree = IAFG.IA.IL.AF.Illustration.ENUMs.TypeDuree;

namespace IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration
{
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
        public bool IsInitialised { get; set; }
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

    public abstract class CasConceptIrisBase
    {
        public int CalculPrimeAdd { get; set; }
        public decimal DeductibleImpot { get; set; }
        public bool DeductionCnap { get; set; }
        public List<Decimal> DesiredLoan { get; set; }
        public bool DesiredLoanDesactivated { get; set; }
        public double FraisGarantie { get; set; }
        public bool IrisExtra { get; set; }
        public bool IsInitialised { get; set; }
        public TypeRemb ProvenanceRembCap { get; set; }
        public TypeRemb ProvenanceRembInt { get; set; }
        public decimal RembBalance { get; set; }
        public TypeRembChoix RembCapChoix { get; set; }
        public int RembCapDuree { get; set; }
        public List<Decimal> RembCapMontant { get; set; }
        public TypeDuree RembCapTypeDuree { get; set; }
        public int RembIntDuree { get; set; }
        public TypeDuree RembIntTypeDuree { get; set; }
        public double SoldeAAtteindre { get; set; }
        public bool TaxePrimeExcedentaire { get; set; }
        public TypeEmprunteur TypeEmprunteur { get; set; }
        public string VehiculeCompte { get; set; }
    }

    public class CasConceptIris : CasConceptIrisBase
    {
        public List<Decimal> IrisAccountRate { get; set; }
        public List<Decimal> IrisBankLoanRate { get; set; }
        public decimal LoanIntRate { get; set; }
        public OptionInterets OptionInterets { get; set; }
        public List<Decimal> RembIntTaux { get; set; }
    }

    public class CasConceptIris2 : CasConceptIrisBase
    {
        public bool FigerSoldePret { get; set; }
        public List<Decimal> ReinvestRendement { get; set; }
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

    public class CasIllustration
    {
        public DateTime DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public string DejaAlle { get; set; }
        public string F1 { get; set; }
        public Langue LangueCorrespondance { get; set; }
        public String NoVersion { get; set; }
        public string PropoInfoGeneraleId { get; set; }
        public string Q4 { get; set; }
        public string Q6 { get; set; }
        public List<CasScenario> Scenarios { get; set; }
        public TypeProposition TypeProposition { get; set; }
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

    public class CasPrimes
    {
        public int AgeFondCibleRetourPrime { get; set; }
        public int AnneeFondCibleRetourPrime { get; set; }
        public TypeReponse CalculRenouvellement { get; set; }
        public int Duree { get; set; }
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

    public class CasScenario
    {
        public CasConceptAssuranceRetraite ConceptAssuranceRetraite { get; set; }
        public CasConceptIris2 ConceptIris2 { get; set; }
        public CasEmpruntBancaire EmpruntBancaire { get; set; }
        public int NbVie { get; set; }
        public int NoScenario { get; set; }
        public CasPrestation Prestation { get; set; }
        public CasPrimes Primes { get; set; }
        public CasTaxation Taxation { get; set; }
        public TypeConcept TypeConcept { get; set; }
        public bool VMaxACalculer { get; set; }
        public CasConceptIris ConceptIris { get; set; }
        public CasProduit Produit { get; set; }
    }

    public class CasProduit
    {
        public CasProduit()
        {
            Universel = new CasPoliceUniversel();
        }

        public Produits GammeProduit { get; set; }
        public CasPoliceUniversel Universel { get; set; }
    }

    public class CasPoliceUniversel
    {
        public CasBoni Boni { get; set; }
        public double CBR { get; set; }
        public double CapitalOptimalValMax { get; set; }
        public CasComparaisonPlacement ComparaisonPlacementAlternatif { get; set; }
    }

    public class CasComparaisonPlacement
    {
        public TypeComparaison TypeComparaison { get; set; }
        public double MontantTemporaire { get; set; }
        public int PlanTemporaire { get; set; }
        public List<decimal> PrimeTemporaire { get; set; }
        public bool IsInitialised { get; set; }
        public CasPlacementAlternatif TauxAlternatifs { get; set; }
    }

    public class CasPlacementAlternatif
    {
        public double InteretProportion { get; set; }
        public List<decimal> InteretTaux { get; set; }
        public double DividendeProportion { get; set; }
        public List<decimal> DividendeTaux { get; set; }
        public double GainCapProportion { get; set; }
        public List<decimal> GainCapTaux { get; set; }
        public double GainCapTauxRealisation { get; set; }
    }

    public class CasBoni
    {
        public OptionVersementBoni OptionVersementBoni { get; set; }
        public Double FacteurHAMA { get; set; }
        public Double FacteurGIA { get; set; }
        public Double FacteurMIA { get; set; }
        public Double PrimeReference { get; set; }
        public TypeBoni TypeBoni { get; set; }
        public double TauxRendementMoyen1An { get; set; }
        public double TauxRendementMoyen2Ans { get; set; }
        public double TauxRendementMoyen3Ans { get; set; }
        public double TauxRendementMoyen4Ans { get; set; }
    }

    public class CasTaxation
    {
        public bool IsInitialised { get; set; }
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
}