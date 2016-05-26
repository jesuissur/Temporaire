using System.Reflection;
using IAFG.IA.VI.AF.Base;

namespace IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration
{
    public class ConversionCasIllustration
    {
        public CasIllustration Convertir(IAFG.IA.VI.AF.Illustration.Illustration illustration)
        {
            return ConvertisseurGenerique.Instance.Map<CasIllustration>(illustration);
        }

        public IAFG.IA.VI.AF.Illustration.Illustration Convertir(CasIllustration casIllustration)
        {
            ActiverLeModeChargement();
            return ConvertisseurGenerique.Instance.Map<IAFG.IA.VI.AF.Illustration.Illustration>(casIllustration);
        }

        private static void ActiverLeModeChargement()
        {
            typeof(AccesServices).InvokeMember("CreerDictionnaireChargement",
                                               BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.NonPublic, null, null, new object[] {});
        }
    }
}