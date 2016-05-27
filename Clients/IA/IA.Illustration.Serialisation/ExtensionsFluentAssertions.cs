using FluentAssertions.Equivalency;
using IAFG.IA.VI.AF.Base;

namespace IA_T.Illustration.Serialisation.Test
{
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
                               x.SelectedMemberInfo.Name == "Proposition");
        }
    }
}