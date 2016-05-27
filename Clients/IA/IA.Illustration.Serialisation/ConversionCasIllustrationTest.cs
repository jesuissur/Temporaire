using System;
using System.IO;
using FluentAssertions;
using IAFG.IA.IL.AF.Illustration.ENUMs;
using IAFG.IA.IL.AP.IllusVie.PDF;
using IAFG.IA.VI.AF.Illustration;
using IAFG.IA.VI.AF.ObjetsPartages;
using IAFG.IA.VI.Contexte.ENUMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration;
using IA_T.Illustration.Serialisation.Test.Utilitaires.Conversion;
using Ploeh.AutoFixture;
using AccesServices = IAFG.IA.VI.Contexte.AccesServices;


namespace IA_T.Illustration.Serialisation.Test
{
    [TestClass]
    public class ConversionCasIllustrationTest
    {
        private static Fixture _auto = new Fixture();

        [TestInitialize]
        public void InitialiserClasseDeTest()
        {
            UtilitairePourConversion.PatchExternalReferencesWithEverythingOnEarth();
            new DirectoryInfo(@"D:\MyDocs\Dev\Temporaire_Phil\Clients\IA\_SavedFiles").DeleteFilesWhere(x => x.Name.EndsWith(".lck"));
            new DirectoryInfo(@"D:\IA_TFS\_Temp\_SavedFiles").DeleteFilesWhere(x => x.Name.EndsWith(".lck"));
        }
        
        [TestMethod]
        public void Devrait_AvoirConfigurationDeConversionValide()
        {
            ConvertisseurGenerique.Configuration.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Devrait_ConvertirCasEnIllustration_PourEvia_Iris2()
        {
            ExecuterSousUnContexte(Banniere.IA, ContexteApplicatif.EVIA, () =>
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                AccesPilotage.Initialiser();
                illustration = UtilitairePourConversion.ObtenirIllustrationDuContenuXml("Evia_Iris2_IA.xml");

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);
                var illustrationApresConversion = sujet.Convertir(casIllustration);
                illustrationApresConversion.Should().NotBeNull();

                VerificationApresConversion.Verifier(illustration, illustrationApresConversion);
            });
        }

        [TestMethod]
        public void Devrait_ConvertirCasEnIllustration_PourGenesis_AssuranceRetraite()
        {
            ExecuterSousUnContexte(Banniere.IA, ContexteApplicatif.Genesis, () =>
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                AccesPilotage.Initialiser();
                illustration = UtilitairePourConversion.ObtenirIllustrationDuContenuXml("Genesis_AssuranceRetraite.xml");

                PreparerEtVerifierIllustration(illustration, PreparerEtVerifierConceptAssuranceRetraite);
                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);
                var illustrationApresConversion = sujet.Convertir(casIllustration);

                VerificationApresConversion.Verifier(illustration, illustrationApresConversion);
            });
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas_PourEvia_Iris2()
        {
            ExecuterSousUnContexte(Banniere.IAP, ContexteApplicatif.EVIA, () =>
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                AccesPilotage.Initialiser();
                illustration = UtilitairePourConversion.ObtenirIllustrationDuContenuXml("Evia_Iris2.xml");

                PreparerEtVerifierIllustration(illustration, PreparerEtVerifierConceptIris2);

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);

                VerificationCasIllustration.VerifierCasPourConceptIris2(casIllustration, illustration);
            });
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas_PourGenesis_AssuranceRetraite()
        {
            ExecuterSousUnContexte(Banniere.IA, ContexteApplicatif.Genesis, () =>
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                AccesPilotage.Initialiser();
                illustration = UtilitairePourConversion.ObtenirIllustrationDuContenuXml("Genesis_AssuranceRetraite.xml");

                PreparerEtVerifierIllustration(illustration, PreparerEtVerifierConceptAssuranceRetraite);

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);

                VerificationCasIllustration.VerifierCasPourConceptAssuranceRetraite(casIllustration, illustration);
            });
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas_PourGenesis_Iris()
        {
            ExecuterSousUnContexte(Banniere.IAP, ContexteApplicatif.Genesis, () =>
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                AccesPilotage.Initialiser();
                illustration = UtilitairePourConversion.ObtenirIllustrationDuContenuXml("Genesis_Iris.xml");

                PreparerEtVerifierIllustration(illustration, PreparerEtVerifierConceptIris);

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);

                VerificationCasIllustration.VerifierCasPourConceptIris(casIllustration, illustration);
            });
        }
        private void ExecuterSousUnContexte(Banniere banniere, ContexteApplicatif contexte, Action action)
        {
            string cle = "";
            try
            {
                cle = AccesServices.Contexte.Initialiser(banniere, ModeExecution.Developpement, Langue.Francais, contexte, cle);
                action();
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
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

        private static void AssignerValeurAleatoireSiAbsente(double valeurChamp, Action<double> assignerValeur)
        {
            AssignerValeurAleatoireSiAbsente(valeurChamp, assignerValeur, x => x.IsDefault());
        }

        private static void AssignerValeurAleatoireSiAbsente<T>(T valeurChamp, Action<T> assignerValeur, Func<T, Boolean> valeurEstAbsente)
        {
            if (valeurEstAbsente(valeurChamp))
                assignerValeur(_auto.Create<T>());
        }

        private static void AssignerValeursAuVecteurSiVide<T>(Vecteur<T> vecteur)
        {
            if (vecteur.ToArray().TrueForAll(x => x.IsDefault()))
            {
                for (int i = 0; i < 3; i++)
                {
                    var valeur = _auto.Create<T>();
                    vecteur.Add(ref valeur);
                }
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

        private static void PreparerEtVerifierConceptIris(Scenario scenario)
        {
            var concept = scenario.Concept.IRIS;

            scenario.Concept.TypeDeConcept.Should().Be(TypeConcept.IRIS);
            concept.Should().NotBeNull();
            AssignerValeurAleatoireSiAbsente(concept.VehiculeCompte, x => concept.VehiculeCompte = x);
            AssignerValeurAleatoireSiAbsente(concept.CalculPrimeAdd, x => concept.CalculPrimeAdd = x);
            AssignerValeurAleatoireSiAbsente(concept.RembCapDuree, x => concept.RembCapDuree = x);
            AssignerValeurAleatoireSiAbsente(concept.RembIntDuree, x => concept.RembIntDuree = x);
            AssignerValeurAleatoireSiAbsente(concept.DeductibleImpot, x => concept.DeductibleImpot = x);
            AssignerValeurAleatoireSiAbsente(concept.RembBalance, x => concept.RembBalance = x);
            AssignerValeursAuVecteurSiVide(concept.DesiredLoan);
            AssignerValeursAuVecteurSiVide(concept.RembCapMontant);
            concept.ProvenanceRembCap = TypeRemb.RetraitPolice; // Autre chose que la valeur par défaut
            concept.ProvenanceRembInt = TypeRemb.DepotsAdditionnels; // Autre chose que la valeur par défaut
            concept.RembCapChoix = TypeRembChoix.Maximum; // Autre chose que la valeur par défaut
            concept.RembCapTypeDuree = TypeDuree.Age;
            concept.RembIntTypeDuree = TypeDuree.Annee;
            concept.TypeEmprunteur = TypeEmprunteur.Corporation;
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

        private static void PreparerEtVerifierConceptIris2IllustrationReguliere(Scenario scenario)
        {
            var concept = scenario.Concept.Iris2IllustrationReguliere;

            //scenario.Concept.TypeDeConcept.Should().Be(TypeConcept.Iris2IllustrationReguliere);
            //concept.Should().NotBeNull();
            //AssignerValeurAleatoireSiAbsente(concept.VehiculeCompte, x => concept.VehiculeCompte = x);
            //AssignerValeurAleatoireSiAbsente(concept.CalculPrimeAdd, x => concept.CalculPrimeAdd = x);
            //AssignerValeurAleatoireSiAbsente(concept.RembCapDuree, x => concept.RembCapDuree = x);
            //AssignerValeurAleatoireSiAbsente(concept.RembIntDuree, x => concept.RembIntDuree = x);
            //AssignerValeurAleatoireSiAbsente(concept.DeductibleImpot, x => concept.DeductibleImpot = x);
            //AssignerValeurAleatoireSiAbsente(concept.RembBalance, x => concept.RembBalance = x);
            //AssignerValeursAuVecteurSiVide(concept.DesiredLoan);
            //AssignerValeursAuVecteurSiVide(concept.RembCapMontant);
            //concept.ProvenanceRembCap = TypeRemb.RetraitPolice; // Autre chose que la valeur par défaut
            //concept.ProvenanceRembInt = TypeRemb.DepotsAdditionnels; // Autre chose que la valeur par défaut
            //concept.RembCapChoix = TypeRembChoix.Maximum; // Autre chose que la valeur par défaut
            //concept.RembCapTypeDuree = TypeDuree.Age;
            //concept.RembIntTypeDuree = TypeDuree.Annee;
            //concept.TypeEmprunteur = TypeEmprunteur.Corporation;
        }

        private static void PreparerEtVerifierEmpruntBancaire(Scenario scenario)
        {
            var empruntBancaire = scenario.InfoTrad.EmpruntBancaire;
            empruntBancaire.Should().NotBeNull();
            AssignerValeurAleatoireSiAbsente(empruntBancaire.Amortissement, x => empruntBancaire.Amortissement = x);
            AssignerValeurAleatoireSiAbsente(empruntBancaire.Solde, x => empruntBancaire.Solde = x);
            AssignerValeursAuVecteurSiVide(empruntBancaire.TauxInteret);
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
            AssignerValeurAleatoireSiAbsente(illustration.PropoInfoGenerale.DejaAlle, x => illustration.PropoInfoGenerale.DejaAlle = x);
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

        private static void PreparerEtVerifierScenario(IAFG.IA.VI.AF.Illustration.Illustration illustration, Action<Scenario> preparerEtVerifierConcept)
        {
            illustration.Scenarios.Should().NotBeNullOrEmpty();
            foreach (Scenario scenario in illustration.Scenarios)
            {
                scenario.NoScenario.IsDefault().Should().BeFalse();
                scenario.VMaxACalculer = true; // Autre chose que la valeur par défaut
                scenario.NbVie.IsDefault().Should().BeFalse();
                PreparerEtVerifierEmpruntBancaire(scenario);
                // TODO Phil T.: À VALIDER
                //scenario.ChangementAssVie.Should().NotBeNull();
                scenario.Concept.Should().NotBeNull();
                scenario.Concept.TypeDeConcept.Should().NotBe(TypeConcept.Aucun);
                Console.WriteLine("CONCEPT:{0}", scenario.Concept.TypeDeConcept);
                preparerEtVerifierConcept(scenario);
                PreparerEtVerifierProduit(scenario);
            }
        }

        private static void PreparerEtVerifierProduit(Scenario scenario)
        {
            scenario.Proposition.Should().NotBeNull();
            scenario.Proposition.InfosPU.Should().NotBeNull();
            scenario.Proposition.InfosPU.Boni.Should().NotBeNull();
            PreparerEtVerifierBoni(scenario);
            AssignerValeursAuVecteurSiVide(scenario.Proposition.InfosPU.Comparaison.PrimeTemporaire);
        }

        private static void PreparerEtVerifierBoni(Scenario scenario)
        {
            var boni = scenario.Proposition.InfosPU.Boni;
            AssignerValeurAleatoireSiAbsente(boni.FacteurGIA, x => boni.FacteurGIA = x);
            AssignerValeurAleatoireSiAbsente(boni.FacteurHAMA, x => boni.FacteurHAMA = x);
            AssignerValeurAleatoireSiAbsente(boni.FacteurMIA, x => boni.FacteurMIA = x);
            AssignerValeurAleatoireSiAbsente(boni.PrimeReference, x => boni.PrimeReference = x);
            AssignerValeurAleatoireSiAbsente(boni.TauxRendementMoyen1An, x => boni.TauxRendementMoyen1An = x);
            AssignerValeurAleatoireSiAbsente(boni.TauxRendementMoyen2Ans, x => boni.TauxRendementMoyen2Ans = x);
            AssignerValeurAleatoireSiAbsente(boni.TauxRendementMoyen3Ans, x => boni.TauxRendementMoyen3Ans = x);
            AssignerValeurAleatoireSiAbsente(boni.TauxRendementMoyen4Ans, x => boni.TauxRendementMoyen4Ans = x);
        }
    }
}
