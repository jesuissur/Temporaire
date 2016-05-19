namespace IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration
{
    public class ConversionCasIllustration
    {
        public CasIllustration Convertir(IAFG.IA.VI.AF.Illustration.Illustration illustration)
        {
            var mapper = ConvertisseurGenerique.Instance;
            return mapper.Map<CasIllustration>(illustration);
        }
    }
}