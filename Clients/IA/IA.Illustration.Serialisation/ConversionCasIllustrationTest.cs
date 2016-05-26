using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using FluentAssertions.Equivalency;
using IAFG.IA.IL.AF.Illustration.ENUMs;
using IAFG.IA.IL.AP.IllusVie.PDF;
using IAFG.IA.VI.AF.Base;
using IAFG.IA.VI.AF.Illustration;
using IAFG.IA.VI.AF.ObjetsPartages;
using IAFG.IA.VI.Contexte.ENUMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration;
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
            PatchExternalReferencesWithEverythingOnEarth();
            new DirectoryInfo(@"D:\MyDocs\Dev\Temporaire_Phil\Clients\IA\_SavedFiles").DeleteFilesWhere(x => x.Name.EndsWith(".lck"));
            new DirectoryInfo(@"D:\IA_TFS\_Temp\_SavedFiles").DeleteFilesWhere(x => x.Name.EndsWith(".lck"));
        }

        [TestMethod]
        public void Devrait_AvoirConfigurationDeConversionValide()
        {
            ConvertisseurGenerique.Configuration.AssertConfigurationIsValid();
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas_PourEvia_Iris2()
        {
            string cle = "";
            try
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                cle = AccesServices.Contexte.Initialiser(Banniere.IAP, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.EVIA, cle);
                AccesPilotage.Initialiser();
                illustration = ObtenirIllustrationDuContenuXml("Evia_Iris2.xml");

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
        public void Devrait_ConvertirIllustrationEnCas_PourGenesis_Iris()
        {
            string cle = "";
            try
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                cle = AccesServices.Contexte.Initialiser(Banniere.IAP, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.Genesis, cle);
                AccesPilotage.Initialiser();
                illustration = ObtenirIllustrationDuContenuXml("Genesis_Iris.xml");

                PreparerEtVerifierIllustration(illustration, PreparerEtVerifierConceptIris);

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);

                VerifierCasIllustration(casIllustration, illustration, VerifierCasConceptIris);
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
            }
        }

        [TestMethod]
        public void Devrait_ConvertirCasEnIllustration_PourEvia_Iris2()
        {
            string cle = "";
            try
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                cle = AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.EVIA, cle);
                AccesPilotage.Initialiser();
                illustration = ObtenirIllustrationDuContenuXml("Evia_Iris2_IA.xml");

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);
                var illustrationApresConversion = sujet.Convertir(casIllustration);
                illustrationApresConversion.Should().NotBeNull();
                JournaliserXmlPourUneIllustration(illustrationApresConversion);

                illustrationApresConversion.DateCreation.Should().Be(illustration.DateCreation);
                illustrationApresConversion.DateModification.Should().Be(illustration.DateModification);
                //VerifierCasIllustration(casIllustration, illustration, VerifierCasConceptIris2);
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
            }
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas_PourGenesis_AssuranceRetraite()
        {
            string cle = "";
            try
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                cle = AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.Genesis, cle);
                AccesPilotage.Initialiser();
                illustration = ObtenirIllustrationDuContenuXml("Genesis_AssuranceRetraite.xml");

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

        [TestMethod]
        public void Devrait_ConvertirCasEnIllustration_PourGenesis_AssuranceRetraite()
        {
            string cle = "";
            try
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                cle = AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.Genesis, cle);
                AccesPilotage.Initialiser();
                illustration = ObtenirIllustrationDuContenuXml("Genesis_AssuranceRetraite.xml");

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);
                var illustrationApresConversion = sujet.Convertir(casIllustration);
                illustrationApresConversion.Should().NotBeNull();
                //JournaliserXmlPourUneIllustration(illustrationApresConversion);

                illustrationApresConversion.DateCreation.Should().Be(illustration.DateCreation);
                illustrationApresConversion.DateModification.Should().Be(illustration.DateModification);
                illustrationApresConversion.NoVersion.Should().Be(illustration.NoVersion);
                illustrationApresConversion.PropoInfoGenerale.ShouldBeEquivalentTo(illustration.PropoInfoGenerale, o => o.SansProprietesObjetBase());
                illustrationApresConversion.Scenarios.Should().HaveSameCount(illustration.Scenarios);
                for (int i = 0; i < illustrationApresConversion.Scenarios.Count; i++)
                {
                    var scenarioApresConversion = illustrationApresConversion.Scenarios[i];
                    var scenario = illustration.Scenarios[i];

                    scenarioApresConversion.NbVie.Should().Be(scenario.NbVie);
                    scenarioApresConversion.NoScenario.Should().Be(scenario.NoScenario);
                    scenarioApresConversion.VMaxACalculer.Should().Be(scenario.VMaxACalculer);
                    scenarioApresConversion.Concept.TypeDeConcept.Should().Be(scenario.Concept.TypeDeConcept);
                    scenarioApresConversion.Concept.AssuranceRetraite.ShouldBeEquivalentTo(scenario.Concept.AssuranceRetraite, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.IRIS.ShouldBeEquivalentTo(scenario.Concept.IRIS, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.IRIS2.ShouldBeEquivalentTo(scenario.Concept.IRIS2, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.InvestorPlus.ShouldBeEquivalentTo(scenario.Concept.InvestorPlus, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.InvestorPlus2.ShouldBeEquivalentTo(scenario.Concept.InvestorPlus2, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.Iris2IllustrationReguliere.ShouldBeEquivalentTo(scenario.Concept.Iris2IllustrationReguliere, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.IrisIllustrationReguliere.ShouldBeEquivalentTo(scenario.Concept.IrisIllustrationReguliere, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.ProprietePartagee.ShouldBeEquivalentTo(scenario.Concept.ProprietePartagee, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.ProtectionPatrimoine.ShouldBeEquivalentTo(scenario.Concept.ProtectionPatrimoine, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.TransfertPatrimoine.ShouldBeEquivalentTo(scenario.Concept.TransfertPatrimoine, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.TransfertPatrimoineV2.ShouldBeEquivalentTo(scenario.Concept.TransfertPatrimoineV2, o => o.SansProprietesObjetBase());
                    scenarioApresConversion.Concept.Zerocashflow.ShouldBeEquivalentTo(scenario.Concept.Zerocashflow, o => o.SansProprietesObjetBase());
                }
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
            }
        }

        [TestMethod]
        [Ignore()]
        public void DecouvrirSavedFiles()
        {
            var rep = new DirectoryInfo(@"D:\IA_TFS\_Temp\_SavedFiles");

            var extensions = new List<string>();
            foreach (var fichier in rep.GetFilesWhere("*.*", SearchOption.AllDirectories, x => x.Name.ToUpper().Contains("_")))
                extensions.AddIfNotContains(fichier.Extension);
            extensions.ForEach(x => Console.WriteLine(x));
            //foreach (var fichier in rep.GetFilesWhere("*.*", SearchOption.AllDirectories, x => x.Name.ToUpper().EndsWith("TRA70_IA")))
            //{
            //    string cle = "";
            //    cle = AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.Transition, cle);
            //    try
            //    {
            //        AccesPilotage.Initialiser();
            //        var illustration = new IAFG.IA.VI.AF.Illustration.Illustration(fichier.FullName);
            //        Console.WriteLine("Illustration du fichier:{0}", fichier.Name);
            //        Console.WriteLine(" --> Nb de scenario:{0}", illustration.Scenarios.Count);
            //        foreach (Scenario scenario in illustration.Scenarios)
            //        {
            //            Console.WriteLine("     --> Scenario pour concept:{0}", scenario.Concept.TypeDeConcept);
            //        }
            //    }
            //    finally
            //    {
            //        AccesServices.Contexte.Relacher(cle);
            //    }
            //}
        }

        [TestMethod]
        [Ignore]
        public void EcrireContenuFichier()
        {
            string cle = "";
            try
            {
                IAFG.IA.VI.AF.Illustration.Illustration illustration;

                cle = AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.Genesis, cle);
                AccesPilotage.Initialiser();
                illustration = ObtenirIllustrationDuContenuXml("Genesis_AssuranceRetraite.xml");

                var sujet = new ConversionCasIllustration();
                var casIllustration = sujet.Convertir(illustration);
                var illustrationApresConversion = sujet.Convertir(casIllustration);
                illustrationApresConversion.Should().NotBeNull();
                illustrationApresConversion.Save(@"D:\IA_TFS\_Temp\_SavedFiles\Genesis_AssuranceRetraite.gen69_IA");
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
            }
        }

        [TestMethod]
        [Ignore()]
        public void ObtenirContenuFichier()
        {
            string cle = "";
            try
            {
                var fichier = @"D:\MyDocs\Dev\Temporaire_Phil\Clients\IA\_SavedFiles\MKEVI999_0447632861_Tra-Niv3_Ent7-I3-P3-D3.evia_IA";
                cle = AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.EVIA, cle);
                AccesPilotage.Initialiser();
                JournaliserContenuXml(fichier);
            }
            finally
            {
                AccesServices.Contexte.Relacher(cle);
            }
        }

        private static void JournaliserXmlPourUneIllustration(IAFG.IA.VI.AF.Illustration.Illustration illustration)
        {
            var m = typeof(Base).GetMethod("XML", BindingFlags.Instance | BindingFlags.NonPublic);
            var contenuXml = m.Invoke(illustration, new object[] { });
            Console.WriteLine(contenuXml);
        }

        private IAFG.IA.VI.AF.Illustration.Illustration ObtenirIllustrationDuContenuXml(string nomRessource)
        {
            var assemblyCourante = this.GetType().Assembly;
            var nomCompletRessource = string.Format("IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration.ContenuIllustration.{0}", nomRessource);
            string contenuXml;

            using (var s = assemblyCourante.GetManifestResourceStream(nomCompletRessource))
                contenuXml = s.ReadToEnd();
            var illustration = new IAFG.IA.VI.AF.Illustration.Illustration();
            try
            {
                illustration.Load(contenuXml.Replace("[[ID_OBJET_TETE]]", Guid.NewGuid().ToString()));
            }
            catch (IAErreurDonneesException e)
            {
                // On essaie une seconde fois pour voir si ca regle un probleme intermittant
                Console.WriteLine("Erreur au chargement, on essaie une seconde fois.  Erreur: {0}", e);
                illustration.Load(contenuXml.Replace("[[ID_OBJET_TETE]]", Guid.NewGuid().ToString()));
            }
            return illustration;
        }

        /// <summary>
        /// Permet de journaliser dans la console le contenu décrypter d'un savedFile.  On se sert
        /// de cette méthode la première fois qu'on obtient un savedFile pour les tests unitaires.  Par la suite,
        /// on créer un fichier xml embeddé dans l'assembly de test
        /// </summary>
        private void JournaliserContenuXml(string nomFichier)
        {
            Console.WriteLine(ExtraireContenuXml(nomFichier));
        }

        private string ExtraireContenuXml(string nomFichier)
        {
            var illustration = new IAFG.IA.VI.AF.Illustration.Illustration();
            var t = typeof(ObjetTete).GetMethod("GetXMLFromFile", BindingFlags.NonPublic | BindingFlags.Instance);
            return (string)t.Invoke(illustration, new object[] { nomFichier });
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

        private static void VerifierCasIllustration(CasIllustration casIllustration, IAFG.IA.VI.AF.Illustration.Illustration illustration, Action<CasScenario, Scenario> VerifierCasPourConcept)
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
                VerifierCasPourConcept(casScenario, scenario);
                VerifierCasPourEmpruntBancaire(casScenario, scenario);
                VerifierCasPourTaxation(casScenario, scenario);
                VerifierCasPourPrestation(casScenario, scenario);
                VerifierCasPourPrimes(casScenario, scenario);
                VerifierCasPourProduit(casScenario, scenario);
            }
        }

        private static void VerifierCasPourProduit(CasScenario casScenario, Scenario scenario)
        {
            casScenario.Produit.Should().NotBeNull();
            casScenario.Produit.CodeProduitLus.Should().Be(scenario.Proposition.Produit.CodeProduitLus);
            casScenario.Produit.EstPU.Should().Be(scenario.Proposition.Produit.EstPU);
            casScenario.Produit.GammeProduit.Should().Be(scenario.Proposition.Produit.GammeProduit);
            casScenario.Produit.TypeProduit.Should().Be(scenario.Proposition.Produit.TypeProduit);
            casScenario.Produit.Version.Should().Be(scenario.Proposition.Produit.Version);
            casScenario.Produit.VersionMajeure.Should().Be(scenario.Proposition.Produit.VersionMajeure);
            casScenario.Produit.VersionMineure.Should().Be(scenario.Proposition.Produit.VersionMineure);
        }

        private static void VerifierCasPourPrimes(CasScenario casScenario, Scenario scenario)
        {
            casScenario.Primes.Should().NotBeNull();
            casScenario.Primes.AgeFondCibleRetourPrime.Should().Be(scenario.Primes.AgeFondCibleRetourPrime);
            casScenario.Primes.AnneeFondCibleRetourPrime.Should().Be(scenario.Primes.AnneeFondCibleRetourPrime);
            casScenario.Primes.CalculRenouvellement.Should().Be(scenario.Primes.CalculRenouvellement);
            casScenario.Primes.Duree.Should().Be(scenario.Primes.Duree);
            casScenario.Primes.FondCible.Should().Be(scenario.Primes.FondCible);
            casScenario.Primes.HitTarget.Should().Be(scenario.Primes.HitTarget);
            casScenario.Primes.Jusqua.Should().Be(scenario.Primes.Jusqua);
            casScenario.Primes.MontantPrime.Should().ContainInOrder(scenario.Primes.MontantPrime.ToArray());
            casScenario.Primes.PourcentageRetourPrime.Should().Be(scenario.Primes.PourcentageRetourPrime);
            casScenario.Primes.PrimeModale.Should().Be(scenario.Primes.PrimeModale);
            casScenario.Primes.TypePrime.Should().ContainInOrder(scenario.Primes.TypePrime.ToArray());
            casScenario.Primes.TypePrimeEcranPrincipal.Should().Be(scenario.Primes.TypePrimeEcranPrincipal);
            casScenario.Primes.ValeurACalculer.Should().Be(scenario.Primes.ValeurACalculer);
        }

        private static void VerifierCasPourPrestation(CasScenario casScenario, Scenario scenario)
        {
            casScenario.Prestation.Should().NotBeNull();
            casScenario.Prestation.Annee_Deces.Should().ContainInOrder(scenario.Prestation.Annee_Deces.ToArray());
            casScenario.Prestation.Annee_Invalidite.Should().ContainInOrder(scenario.Prestation.Annee_Invalidite.ToArray());
            casScenario.Prestation.Montant_Deces.Should().ContainInOrder(scenario.Prestation.Montant_Deces.ToArray());
            casScenario.Prestation.Montant_Invalidite.Should().ContainInOrder(scenario.Prestation.Montant_Invalidite.ToArray());
            casScenario.Prestation.TypePrestationDeces.Should().Be(scenario.Prestation.TypePrestationDeces);
            casScenario.Prestation.TypePrestationInvalidite.Should().Be(scenario.Prestation.TypePrestationInvalidite);
        }

        private static void VerifierCasPourTaxation(CasScenario casScenario, Scenario scenario)
        {
            casScenario.Taxation.Should().NotBeNull();
            casScenario.Taxation.TauxMarginalCorp.Should().ContainInOrder(scenario.Taxation.TauxMarginalCorp.ToArray());
            casScenario.Taxation.TauxMarginalInd.Should().ContainInOrder(scenario.Taxation.TauxMarginalInd.ToArray());
            casScenario.Taxation.TauxRembCorpDividende.Should().Be(scenario.Taxation.TauxRembCorpDividende);
            casScenario.Taxation.TauxTOHCorp.Should().Be(scenario.Taxation.TauxTOHCorp);
            casScenario.Taxation.TaxeDividendeCorp.Should().ContainInOrder(scenario.Taxation.TaxeDividendeCorp.ToArray());
            casScenario.Taxation.TaxeDividendeInd.Should().ContainInOrder(scenario.Taxation.TaxeDividendeInd.ToArray());
            casScenario.Taxation.TaxeGainCapitalCorp.Should().ContainInOrder(scenario.Taxation.TaxeGainCapitalCorp.ToArray());
            casScenario.Taxation.TaxeGainCapitalInd.Should().ContainInOrder(scenario.Taxation.TaxeGainCapitalInd.ToArray());
            casScenario.Taxation.TaxeSurCapitalCorp.Should().ContainInOrder(scenario.Taxation.TaxeSurCapitalCorp.ToArray());
        }

        private static void VerifierCasPourEmpruntBancaire(CasScenario casScenario, Scenario scenario)
        {
            var casEmpruntBancaire = casScenario.EmpruntBancaire;
            var empruntBancaire = scenario.InfoTrad.EmpruntBancaire;

            casEmpruntBancaire.Should().NotBeNull();
            casEmpruntBancaire.Amortissement.Should().Be(empruntBancaire.Amortissement);
            casEmpruntBancaire.Frequence.Should().Be(empruntBancaire.Frequence);
            casEmpruntBancaire.IsEmpruntInitialised.Should().Be(empruntBancaire.IsEmpruntInitialised);
            casEmpruntBancaire.ParametrePaiment.Should().Be(empruntBancaire.ParametrePaiment);
            casEmpruntBancaire.Solde.Should().Be(empruntBancaire.Solde);
            casEmpruntBancaire.TauxInteret.Should().ContainInOrder(empruntBancaire.TauxInteret.ToArray());
            casEmpruntBancaire.TypePret.Should().Be(empruntBancaire.TypePret);
        }

        private static void VerifierCasConceptIris2(CasScenario casScenario, Scenario scenario)
        {
            casScenario.ConceptIris2.Should().NotBeNull();
            VerifierCasConceptIrisCommun(casScenario.ConceptIris2, scenario);
            casScenario.ConceptIris2.FigerSoldePret.Should().Be(scenario.Concept.IRIS2.FigerSoldePret);
            casScenario.ConceptIris2.ReinvestRendement.Should().ContainInOrder(scenario.Concept.IRIS2.ReinvestRendement.ToArray());
        }

        private static void VerifierCasConceptIris(CasScenario casScenario, Scenario scenario)
        {
            casScenario.ConceptIris.Should().NotBeNull();
            VerifierCasConceptIrisCommun(casScenario.ConceptIris, scenario);
            casScenario.ConceptIris.IrisAccountRate.Should().ContainInOrder(scenario.Concept.IRIS.IrisAccountRate.ToArray());
            casScenario.ConceptIris.IrisBankLoanRate.Should().ContainInOrder(scenario.Concept.IRIS.IrisBankLoanRate.ToArray());
            casScenario.ConceptIris.LoanIntRate.Should().Be(scenario.Concept.IRIS.LoanIntRate);
            casScenario.ConceptIris.OptionInterets.Should().Be(scenario.Concept.IRIS.OptionInterets);
            casScenario.ConceptIris.RembIntTaux.Should().ContainInOrder(scenario.Concept.IRIS.RembIntTaux.ToArray());
        }

        private static void VerifierCasConceptIrisCommun(CasConceptIrisBase conceptIris, Scenario scenario)
        {
            conceptIris.Should().NotBeNull();
            conceptIris.CalculPrimeAdd.Should().Be(scenario.Concept.IRIS2.CalculPrimeAdd);
            conceptIris.RembCapDuree.Should().Be(scenario.Concept.IRIS2.RembCapDuree);
            conceptIris.RembIntDuree.Should().Be(scenario.Concept.IRIS2.RembIntDuree);
            conceptIris.DeductibleImpot.Should().Be(scenario.Concept.IRIS2.DeductibleImpot);
            conceptIris.RembBalance.Should().Be(scenario.Concept.IRIS2.RembBalance);
            conceptIris.DeductionCnap.Should().Be(scenario.Concept.IRIS2.DeductionCnap);
            conceptIris.DesiredLoanDesactivated.Should().Be(scenario.Concept.IRIS2.DesiredLoanDesactivated);
            conceptIris.IrisExtra.Should().Be(scenario.Concept.IRIS2.IrisExtra);
            conceptIris.TaxePrimeExcedentaire.Should().Be(scenario.Concept.IRIS2.TaxePrimeExcedentaire);
            conceptIris.DesiredLoan.Should().ContainInOrder(scenario.Concept.IRIS2.DesiredLoan.ToArray());
            conceptIris.RembCapMontant.Should().ContainInOrder(scenario.Concept.IRIS2.RembCapMontant.ToArray());
            conceptIris.FraisGarantie.Should().Be(scenario.Concept.IRIS2.FraisGarantie);
            conceptIris.ProvenanceRembCap.Should().Be(scenario.Concept.IRIS2.ProvenanceRembCap);
            conceptIris.ProvenanceRembInt.Should().Be(scenario.Concept.IRIS2.ProvenanceRembInt);
            conceptIris.RembCapChoix.Should().Be(scenario.Concept.IRIS2.RembCapChoix);
            conceptIris.RembCapTypeDuree.Should().Be(scenario.Concept.IRIS2.RembCapTypeDuree);
            conceptIris.RembIntTypeDuree.Should().Be(scenario.Concept.IRIS2.RembIntTypeDuree);
            conceptIris.SoldeAAtteindre.Should().Be(scenario.Concept.IRIS2.SoldeAAtteindre);
            conceptIris.TypeEmprunteur.Should().Be(scenario.Concept.IRIS2.TypeEmprunteur);
            conceptIris.VehiculeCompte.Should().Be(scenario.Concept.IRIS2.VehiculeCompteCollateral);
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
            }
        }

        private static void PreparerEtVerifierEmpruntBancaire(Scenario scenario)
        {
            var empruntBancaire = scenario.InfoTrad.EmpruntBancaire;
            empruntBancaire.Should().NotBeNull();
            AssignerValeurAleatoireSiAbsente(empruntBancaire.Amortissement, x => empruntBancaire.Amortissement = x);
            AssignerValeurAleatoireSiAbsente(empruntBancaire.Solde, x => empruntBancaire.Solde = x);
            AssignerValeursAuVecteurSiVide(empruntBancaire.TauxInteret);
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

        private static void AssignerValeurAleatoireSiAbsente(double valeurChamp, Action<double> assignerValeur)
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

    internal static class ExtensionsFluentAssertions
    {
        public static EquivalencyAssertionOptions<T> SansProprietesObjetBase<T>(this EquivalencyAssertionOptions<T> options) where T:Base
        {
            return options.ExcludingFields().
                           Excluding(x => x.Id).
                           Excluding(x => x.Parent).
                           Excluding(x => x.EstEnChargement).
                           Excluding(x => x.SelectedMemberInfo.Name == "Tete" || 
                                          x.SelectedMemberInfo.Name == "Scenario" ||
                                          x.SelectedMemberInfo.Name == "Illustration" ||
                                          x.SelectedMemberInfo.Name == "IsInitialised");
        }
    }
}
