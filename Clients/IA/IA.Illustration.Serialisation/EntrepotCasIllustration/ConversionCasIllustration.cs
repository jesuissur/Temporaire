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
            return ConvertisseurGenerique.Instance.Map<IAFG.IA.VI.AF.Illustration.Illustration>(casIllustration);
        }
    }
}