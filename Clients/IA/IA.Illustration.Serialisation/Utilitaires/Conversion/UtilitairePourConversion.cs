using System;
using System.IO;
using System.Reflection;
using IAFG.IA.VI.AF.Base;

namespace IA_T.Illustration.Serialisation.Test.Utilitaires.Conversion
{
    internal static class UtilitairePourConversion
    {
        public static IAFG.IA.VI.AF.Illustration.Illustration ObtenirIllustrationDuContenuXml(string nomRessource)
        {
            var assemblyCourante = typeof(UtilitairePourConversion).Assembly;
            var nomCompletRessource = string.Format("IA_T.Illustration.Serialisation.Test.ContenuIllustrationPourTest.{0}", nomRessource);
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

        public static void PatchExternalReferencesWithEverythingOnEarth()
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