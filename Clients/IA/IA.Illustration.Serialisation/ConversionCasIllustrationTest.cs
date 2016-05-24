using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentAssertions;
using IAFG.IA.IL.AF.Illustration.ENUMs;
using IAFG.IA.IL.AP.IllusVie.PDF;
using IAFG.IA.VI.AF.Illustration;
using IAFG.IA.VI.AF.ObjetsPartages;
using IAFG.IA.VI.Contexte;
using IAFG.IA.VI.Contexte.ENUMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration;
using Ploeh.AutoFixture;


namespace IA_T.Illustration.Serialisation.Test
{
    [TestClass]
    public class ConversionCasIllustrationTest
    {

        private static Fixture _auto = new Fixture();

        [TestInitialize]
        public void InitialiserClasseDeTest()
        {
            PatchExternalReferencesWithEverythingOnEarth();
            new DirectoryInfo(@"D:\MyDocs\Dev\Temporaire_Phil\Clients\IA\_SavedFiles").DeleteFilesWhere(x => x.Name.EndsWith(".lck"));
        }

        [TestMethod]
        public void Devrait_AvoirConfigurationDeConversionValide()
        {
            ConvertisseurGenerique.Configuration.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas_PourEvia_Iris2()
        {
            var cle = Guid.NewGuid().ToString();
            var illustration = new IAFG.IA.VI.AF.Illustration.Illustration();
            try
            {
                cle = AccesServices.Contexte.Initialiser(Banniere.IAP, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.EVIA, cle);
                AccesPilotage.Initialiser();
                illustration.Load(@"D:\MyDocs\Dev\Temporaire_Phil\Clients\IA\_SavedFiles\MKEVI999_9512675805_ConversionIRIS_Ent5-I1-P1-D1.evia_IAP");

                PreparerEtVerifierIllustration(illustration, PreparerEtVerifierConceptIris2);

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);

                VerifierCasIllustration(casIllustration, illustration, VerifierCasConceptIris2);
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
            }
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas_PourGenesis_AssuranceRetraire()
        {
            var cle = Guid.NewGuid().ToString();
            var illustration = new IAFG.IA.VI.AF.Illustration.Illustration();
            try
            {
                cle = AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.Genesis, cle);
                AccesPilotage.Initialiser();
                illustration.Load(@"D:\MyDocs\Dev\Temporaire_Phil\Clients\IA\_SavedFiles\Assurance-Retraite.gen69_IA");

                PreparerEtVerifierIllustration(illustration, PreparerEtVerifierConceptAssuranceRetraite);

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);

                VerifierCasIllustration(casIllustration, illustration, VerifierCasConceptAssuranceRetraite);
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
            }
        }

        private void VerifierCasConceptAssuranceRetraite(CasScenario casScenario, Scenario scenario)
        {
            var casConcept = casScenario.ConceptAssuranceRetraite;
            var concept = scenario.Concept.AssuranceRetraite;

            casConcept.Should().NotBeNull();
            casConcept.AgeDepart.Should().Be(concept.AgeDepart);
            casConcept.AgeHeritage.Should().Be(concept.AgeHeritage);
            casConcept.CompteDuMarche.Should().Be(concept.CompteDuMarche);
            casConcept.DeductionCnap.Should().Be(concept.DeductionCnap);
            casConcept.DifferenceOuTaux.Should().Be(concept.DifferenceOuTaux);
            casConcept.DifferenceTaux.Should().ContainInOrder(concept.DifferenceTaux.ToArray());
            casConcept.Duree.Should().Be(concept.Duree);
            casConcept.FrequencePaiement.Should().Be(concept.FrequencePaiement);
            casConcept.IntGuarantit.Should().Be(concept.IntGuarantit);
            casConcept.PretBancaireCumulatif.Should().Be(concept.PretBancaireCumulatif);
            casConcept.RespectRatioPretVsValeurRachat.Should().Be(concept.RespectRatioPretVsValeurRachat);
            casConcept.RespectRatioPretVsValeurRachatPerso.Should().Be(concept.RespectRatioPretVsValeurRachatPerso);
            casConcept.RevenuRetraite.Should().ContainInOrder(concept.RevenuRetraite);
            casConcept.RevenuRetraiteChoix.Should().Be(concept.RevenuRetraiteChoix);
            casConcept.TauxGIA.Should().Be(concept.TauxGIA);
            casConcept.TauxIndex.Should().Be(concept.TauxIndex);
            casConcept.TauxIntDeductible.Should().Be(concept.TauxIntDeductible);
            casConcept.TranfertAuto.Should().Be(concept.TranfertAuto);
            casConcept.ValeurACalculer.Should().Be(concept.ValeurACalculer);
            casConcept.ValeurACalculerComparaison.Should().Be(concept.ValeurACalculerComparaison);
            casConcept.ValeurCible.Should().Be(concept.ValeurCible);
            
        }

        private static void VerifierCasIllustration(CasIllustration casIllustration, IAFG.IA.VI.AF.Illustration.Illustration illustration, Action<CasScenario, Scenario> verifierCasPourConcept)
        {
            casIllustration.Should().NotBeNull();
            casIllustration.DateCreation.Should().Be(illustration.DateCreation);
            casIllustration.DateModification.Should().Be(illustration.DateModification);
            casIllustration.NoVersion.Should().Be(illustration.NoVersion);
            casIllustration.DejaAlle.Should().Be(illustration.PropoInfoGenerale.DejaAlle);
            casIllustration.F1.Should().Be(illustration.PropoInfoGenerale.F1);
            casIllustration.LangueCorrespondance.Should().Be(illustration.PropoInfoGenerale.LangueCorrespondance);
            casIllustration.Q4.Should().Be(illustration.PropoInfoGenerale.Q4);
            casIllustration.Q6.Should().Be(illustration.PropoInfoGenerale.Q6);
            casIllustration.PropoInfoGeneraleId.Should().Be(illustration.PropoInfoGenerale.Id);
            casIllustration.TypeProposition.Should().Be(illustration.PropoInfoGenerale.TypeProposition);
            casIllustration.Scenarios.Should().HaveSameCount(illustration.Scenarios);
            for (int i = 0; i < illustration.Scenarios.Count; i++)
            {
                var scenario = illustration.Scenarios[i];
                var casScenario = casIllustration.Scenarios[i];

                casScenario.NoScenario.Should().Be(scenario.NoScenario);
                casScenario.VMaxACalculer.Should().Be(scenario.VMaxACalculer);
                casScenario.NbVie.Should().Be(scenario.NbVie);
                casScenario.TypeConcept.Should().Be(scenario.Concept.TypeDeConcept);
                verifierCasPourConcept(casScenario, scenario);
            }
        }

        private static void VerifierCasConceptIris2(CasScenario casScenario, Scenario scenario)
        {
            casScenario.ConceptIris2.Should().NotBeNull();
            casScenario.ConceptIris2.CalculPrimeAdd.Should().Be(scenario.Concept.IRIS2.CalculPrimeAdd);
            casScenario.ConceptIris2.RembCapDuree.Should().Be(scenario.Concept.IRIS2.RembCapDuree);
            casScenario.ConceptIris2.RembIntDuree.Should().Be(scenario.Concept.IRIS2.RembIntDuree);
            casScenario.ConceptIris2.DeductibleImpot.Should().Be(scenario.Concept.IRIS2.DeductibleImpot);
            casScenario.ConceptIris2.RembBalance.Should().Be(scenario.Concept.IRIS2.RembBalance);
            casScenario.ConceptIris2.DeductionCnap.Should().Be(scenario.Concept.IRIS2.DeductionCnap);
            casScenario.ConceptIris2.DesiredLoanDesactivated.Should().Be(scenario.Concept.IRIS2.DesiredLoanDesactivated);
            casScenario.ConceptIris2.FigerSoldePret.Should().Be(scenario.Concept.IRIS2.FigerSoldePret);
            casScenario.ConceptIris2.IrisExtra.Should().Be(scenario.Concept.IRIS2.IrisExtra);
            casScenario.ConceptIris2.TaxePrimeExcedentaire.Should().Be(scenario.Concept.IRIS2.TaxePrimeExcedentaire);
            casScenario.ConceptIris2.DesiredLoan.Should().ContainInOrder(scenario.Concept.IRIS2.DesiredLoan.ToArray());
            casScenario.ConceptIris2.ReinvestRendement.Should().ContainInOrder(scenario.Concept.IRIS2.ReinvestRendement.ToArray());
            casScenario.ConceptIris2.RembCapMontant.Should().ContainInOrder(scenario.Concept.IRIS2.RembCapMontant.ToArray());
            casScenario.ConceptIris2.FraisGarantie.Should().Be(scenario.Concept.IRIS2.FraisGarantie);
            casScenario.ConceptIris2.ProvenanceRembCap.Should().Be(scenario.Concept.IRIS2.ProvenanceRembCap);
            casScenario.ConceptIris2.ProvenanceRembInt.Should().Be(scenario.Concept.IRIS2.ProvenanceRembInt);
            casScenario.ConceptIris2.RembCapChoix.Should().Be(scenario.Concept.IRIS2.RembCapChoix);
            casScenario.ConceptIris2.RembCapTypeDuree.Should().Be(scenario.Concept.IRIS2.RembCapTypeDuree);
            casScenario.ConceptIris2.RembIntTypeDuree.Should().Be(scenario.Concept.IRIS2.RembIntTypeDuree);
            casScenario.ConceptIris2.SoldeAAtteindre.Should().Be(scenario.Concept.IRIS2.SoldeAAtteindre);
            casScenario.ConceptIris2.TypeEmprunteur.Should().Be(scenario.Concept.IRIS2.TypeEmprunteur);
            casScenario.ConceptIris2.VehiculeCompteCollateral.Should().Be(scenario.Concept.IRIS2.VehiculeCompteCollateral);
        }

        private static void PreparerEtVerifierScenario(IAFG.IA.VI.AF.Illustration.Illustration illustration, Action<Scenario> preparerEtVerifierConcept)
        {
            illustration.Scenarios.Should().NotBeNullOrEmpty();
            foreach (Scenario scenario in illustration.Scenarios)
            {
                scenario.NoScenario.IsDefault().Should().BeFalse();
                scenario.VMaxACalculer = true; // Autre chose que la valeur par défaut
                scenario.NbVie.IsDefault().Should().BeFalse();
                // TODO Phil T.: À VALIDER
                //scenario.ChangementAssVie.Should().NotBeNull();
                scenario.Concept.Should().NotBeNull();
                scenario.Concept.TypeDeConcept.Should().NotBe(TypeConcept.Aucun);
                Console.WriteLine("CONCEPT:{0}", scenario.Concept.TypeDeConcept);
                preparerEtVerifierConcept(scenario);
            }
        }

        private static void PreparerEtVerifierConceptAssuranceRetraite(Scenario scenario)
        {
            var concept = scenario.Concept.AssuranceRetraite;

            scenario.Concept.TypeDeConcept.Should().Be(TypeConcept.AssuranceRetraite);
            concept.Should().NotBeNull();

            AssignerValeurAleatoireSiAbsente(concept.AgeHeritage, x => concept.AgeHeritage = x);
            AssignerValeurAleatoireSiAbsente(concept.PretBancaireCumulatif, x => concept.PretBancaireCumulatif = x);
            AssignerValeurAleatoireSiAbsente(concept.TauxIntDeductible, x => concept.TauxIntDeductible = x);
            AssignerValeurAleatoireSiAbsente(concept.ValeurCible, x => concept.ValeurCible = x);
            concept.DeductionCnap = true;
            AssignerValeursAuVecteurSiVide(concept.DifferenceTaux);
            AssignerValeursAuVecteurSiVide(concept.RevenuRetraite);

            concept.AgeDepart.IsDefault().Should().BeFalse();
            concept.AgeHeritage.IsDefault().Should().BeFalse();
            concept.CompteDuMarche.IsDefault().Should().BeFalse();
            concept.DifferenceOuTaux.Should().NotBeNullOrEmpty();
            concept.DifferenceTaux.Should().NotBeNullOrEmpty();
            concept.Duree.IsDefault().Should().BeFalse();
            concept.FrequencePaiement.Should().NotBe(FrequencePaiement.AucunMode);
            concept.IntGuarantit.IsDefault().Should().BeFalse();
            concept.PretBancaireCumulatif.IsDefault().Should().BeFalse();
            concept.RespectRatioPretVsValeurRachat.Should().NotBe(RespectRatioPretVsValeurRachat.Inconnu);
            concept.RespectRatioPretVsValeurRachatPerso.IsDefault().Should().BeFalse();
            concept.RevenuRetraite.Should().NotBeNullOrEmpty();
            concept.RevenuRetraiteChoix.IsDefault().Should().BeFalse();
            concept.TauxGIA.IsDefault().Should().BeFalse();
            concept.TauxIndex.IsDefault().Should().BeFalse();
            concept.TauxIntDeductible.IsDefault().Should().BeFalse();
            concept.TranfertAuto.Should().NotBeNullOrEmpty();
            concept.ValeurCible.Should().NotBeNullOrEmpty();
        }

        private static void PreparerEtVerifierConceptIris2(Scenario scenario)
        {
            var concept = scenario.Concept.IRIS2;

            scenario.Concept.TypeDeConcept.Should().Be(TypeConcept.IRIS2);
            concept.Should().NotBeNull();
            AssignerValeurAleatoireSiAbsente(concept.CalculPrimeAdd, x => concept.CalculPrimeAdd = x);
            AssignerValeurAleatoireSiAbsente(concept.RembCapDuree, x => concept.RembCapDuree = x);
            AssignerValeurAleatoireSiAbsente(concept.RembIntDuree, x => concept.RembIntDuree = x);
            AssignerValeurAleatoireSiAbsente(concept.DeductibleImpot, x => concept.DeductibleImpot = x);
            AssignerValeurAleatoireSiAbsente(concept.RembBalance, x => concept.RembBalance = x);
            AssignerValeursAuVecteurSiVide(concept.DesiredLoan);
            AssignerValeursAuVecteurSiVide(concept.ReinvestRendement);
            AssignerValeursAuVecteurSiVide(concept.RembCapMontant);
            concept.ProvenanceRembCap = TypeRemb.RetraitPolice; // Autre chose que la valeur par défaut
            concept.ProvenanceRembInt = TypeRemb.DepotsAdditionnels; // Autre chose que la valeur par défaut
            concept.RembCapChoix = TypeRembChoix.Maximum; // Autre chose que la valeur par défaut
            concept.RembCapTypeDuree = TypeDuree.Age;
            concept.RembIntTypeDuree = TypeDuree.Annee;
            concept.TypeEmprunteur = TypeEmprunteur.Corporation;
        }

        private static void PreparerEtVerifierIllustration(IAFG.IA.VI.AF.Illustration.Illustration illustration, Action<Scenario> preparerEtVerifierConcept)
        {
            illustration.Should().NotBeNull();
            AssignerValeurAleatoireSiAbsente(illustration.NoVersion, x => illustration.NoVersion = x);
            illustration.DateCreation.Should().NotBe(DateTime.MinValue);
            illustration.DateModification.Should().NotBe(DateTime.MinValue);
            illustration.NoVersion.Should().NotBeNullOrWhiteSpace();

            // Préparer Propo Info Generale
            illustration.PropoInfoGenerale.Should().NotBeNull();
            illustration.PropoInfoGenerale.TypeProposition = TypeProposition.Papier;
            AssignerValeurAleatoireSiAbsente(illustration.PropoInfoGenerale.DejaAlle,x => illustration.PropoInfoGenerale.DejaAlle = x);
            AssignerValeurAleatoireSiAbsente(illustration.PropoInfoGenerale.F1, x => illustration.PropoInfoGenerale.F1 = x);
            AssignerValeurAleatoireSiAbsente(illustration.PropoInfoGenerale.Q4, x => illustration.PropoInfoGenerale.Q4 = x);
            AssignerValeurAleatoireSiAbsente(illustration.PropoInfoGenerale.Q6, x => illustration.PropoInfoGenerale.Q6 = x);
            illustration.PropoInfoGenerale.Id.Should().NotBeNullOrWhiteSpace();
            illustration.PropoInfoGenerale.DejaAlle.Should().NotBeNullOrWhiteSpace();
            illustration.PropoInfoGenerale.F1.Should().NotBeNullOrWhiteSpace();
            illustration.PropoInfoGenerale.LangueCorrespondance.ToString().Should().NotBeEmpty();
            illustration.PropoInfoGenerale.Q4.Should().NotBeNullOrWhiteSpace();
            illustration.PropoInfoGenerale.Q6.Should().NotBeNullOrWhiteSpace();

            PreparerEtVerifierScenario(illustration, preparerEtVerifierConcept);
        }

        private static void AssignerValeursAuVecteurSiVide<T>(Vecteur<T> vecteur)
        {
            if (vecteur.ToArray().TrueForAll(x => x.IsDefault()))
            {
                for (int i = 0; i<3; i++)
                {
                    var valeur = _auto.Create<T>();
                    vecteur.Add(ref valeur);
                }
            }
        }

        private static void AssignerValeurAleatoireSiAbsente(string valeurChamp, Action<string> assignerValeur)
        {
            AssignerValeurAleatoireSiAbsente(valeurChamp, assignerValeur, x => x.IsNullOrWhiteSpace());
        }

        private static void AssignerValeurAleatoireSiAbsente(int valeurChamp, Action<int> assignerValeur)
        {
            AssignerValeurAleatoireSiAbsente(valeurChamp, assignerValeur, x => x.IsDefault());
        }

        private static void AssignerValeurAleatoireSiAbsente(decimal valeurChamp, Action<decimal> assignerValeur)
        {
            AssignerValeurAleatoireSiAbsente(valeurChamp, assignerValeur, x => x.IsDefault());
        }

        private static void AssignerValeurAleatoireSiAbsente<T>(T valeurChamp, Action<T> assignerValeur, Func<T, Boolean> valeurEstAbsente)
        {
            if (valeurEstAbsente(valeurChamp))
                assignerValeur(_auto.Create<T>());
        }

        private static void PatchExternalReferencesWithEverythingOnEarth()
        {
            var emplacementCourant = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string sourceReferences = Path.Combine(emplacementCourant, @"..\..\..\UnitTestProject3\_Ref");
            foreach (var nomFichier in Directory.GetFiles(sourceReferences))
            {
                var nouveauNomFichier = Path.Combine(emplacementCourant, Path.GetFileName(nomFichier));
                if (!File.Exists(nouveauNomFichier))
                    File.Copy(nomFichier, nouveauNomFichier);
            }
        }
    }
}
