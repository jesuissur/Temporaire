using System;
using System.IO;
using System.Reflection;
using FluentAssertions;
using IAFG.IA.IL.AF.Illustration.ENUMs;
using IAFG.IA.IL.AP.IllusVie.PDF;
using IAFG.IA.VI.AF.Illustration;
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
            new DirectoryInfo(@"D:\IA_TFS\_Temp\SavedFiles").DeleteFilesWhere(x => x.Name.EndsWith(".lck"));
            IAFG.IA.VI.Contexte.AccesServices.Contexte.Initialiser(Banniere.IA, ModeExecution.Developpement, Langue.Francais, ContexteApplicatif.Genesis, "");
            AccesPilotage.Initialiser();
        }

        [TestMethod]
        public void Devrait_ConvertirIllustrationEnCas()
        {
            var illustration = new IAFG.IA.VI.AF.Illustration.Illustration();
            illustration.Load(@"D:\IA_TFS\_Temp\SavedFiles\MKGEN999_601520.gen69_IA");
            //illustration.Load(@"D:\IA_TFS\_Temp\SavedFiles\MKGEN999_601013.gen69_IAP");
            //illustration.Load(@"D:\IA_TFS\_Temp\SavedFiles\MKEVI999_9512675805_ConversionIRIS_Ent5-I1-P1-D1.evia_IAP");


            illustration.Should().NotBeNull();
            AssignerValeurAleatoireSiAbsente(illustration.NoVersion, x => illustration.NoVersion = x);
            illustration.DateCreation.Should().NotBe(DateTime.MinValue);
            illustration.DateModification.Should().NotBe(DateTime.MinValue);
            illustration.NoVersion.Should().NotBeNullOrWhiteSpace();
            
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

            illustration.Scenarios.Should().NotBeNullOrEmpty();
            foreach (Scenario scenario in illustration.Scenarios)
            {
                scenario.NoScenario.IsDefault().Should().BeFalse();
                scenario.VMaxACalculer = true; // Autre chose que la valeur par défaut
                scenario.NbVie.IsDefault().Should().BeFalse();
            }

            var sujet = new ConversionCasIllustration();
            var casIllustration = sujet.Convertir(illustration);

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
            for (int i =0; i<illustration.Scenarios.Count; i++)
            {
                var scenario = illustration.Scenarios[i];
                var casScenario = casIllustration.Scenarios[i];

                casScenario.NoScenario.Should().Be(scenario.NoScenario);
                casScenario.VMaxACalculer.Should().Be(scenario.VMaxACalculer);
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
