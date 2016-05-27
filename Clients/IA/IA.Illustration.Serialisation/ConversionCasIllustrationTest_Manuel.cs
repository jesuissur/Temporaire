using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using FluentAssertions;
using IAFG.IA.IL.AP.IllusVie.PDF;
using IAFG.IA.VI.AF.Base;
using IAFG.IA.VI.Contexte.ENUMs;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration;
using IA_T.Illustration.Serialisation.Test.Utilitaires.Conversion;
using Ploeh.AutoFixture;
using AccesServices = IAFG.IA.VI.Contexte.AccesServices;


namespace IA_T.Illustration.Serialisation.Test
{
    /// <summary>
    /// Contient des tests qui peuvent être exécutés manuellement afin d'extraire ou écrire des infos sur les savedFiles
    /// </summary>
    [TestClass]
    public class ConversionCasIllustrationTest_Manuel
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
                illustration = UtilitairePourConversion.ObtenirIllustrationDuContenuXml("Genesis_AssuranceRetraite.xml");

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


        private string ExtraireContenuXml(string nomFichier)
        {
            var illustration = new IAFG.IA.VI.AF.Illustration.Illustration();
            var t = typeof(ObjetTete).GetMethod("GetXMLFromFile", BindingFlags.NonPublic | BindingFlags.Instance);
            return (string)t.Invoke(illustration, new object[] { nomFichier });
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

    }
}
