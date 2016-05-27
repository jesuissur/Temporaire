using System;
using FluentAssertions;
using FluentAssertions.Equivalency;
using IAFG.IA.VI.AF.Base;
using IAFG.IA.VI.AF.Illustration;

namespace IA_T.Illustration.Serialisation.Test.Utilitaires.Conversion
{
    internal static class VerificationApresConversion
    {
        public static void Verifier(IAFG.IA.VI.AF.Illustration.Illustration source, IAFG.IA.VI.AF.Illustration.Illustration destination)
        {
            destination.Should().NotBeNull();

            destination.DateCreation.Should().Be(source.DateCreation);
            destination.DateModification.Should().Be(source.DateModification);
            destination.NoVersion.Should().Be(source.NoVersion);
            DoitEtreEquivalent(destination.PropoInfoGenerale, source.PropoInfoGenerale);
            destination.Scenarios.Should().HaveSameCount(source.Scenarios);
            for (int i = 0; i < destination.Scenarios.Count; i++)
            {
                var scenarioDest = destination.Scenarios[i];
                var scenarioSource = source.Scenarios[i];

                scenarioDest.NbVie.Should().Be(scenarioSource.NbVie);
                scenarioDest.NoScenario.Should().Be(scenarioSource.NoScenario);
                scenarioDest.VMaxACalculer.Should().Be(scenarioSource.VMaxACalculer);
                VerifierConcept(scenarioDest, scenarioSource);
                DoitEtreEquivalent(scenarioDest.InfoTrad.EmpruntBancaire, scenarioSource.InfoTrad.EmpruntBancaire);
                DoitEtreEquivalent(scenarioDest.Prestation, scenarioSource.Prestation);
                DoitEtreEquivalent(scenarioDest.Primes, scenarioSource.Primes);
                DoitEtreEquivalent(scenarioDest.Taxation, scenarioSource.Taxation);
                VerifierProduit(scenarioDest, scenarioSource);
            }

        }

        private static void VerifierProduit(Scenario scenarioDest, Scenario scenarioSource)
        {
            var infosDest = scenarioDest.Proposition.InfosPU;
            var infosSource = scenarioSource.Proposition.InfosPU;

            scenarioDest.Proposition.Produit.GammeProduit.Should().Be(scenarioSource.Proposition.Produit.GammeProduit);
            infosDest.CBR.Should().Be(infosSource.CBR);
            infosDest.CapitalOptimalValMax.Should().Be(infosSource.CapitalOptimalValMax);
            DoitEtreEquivalent(infosDest.Boni, infosSource.Boni, o => o.ExcludingNestedObjects().
                                                                        Excluding(x => x.MaximumPremiumGuarantedEAD).
                                                                        Excluding(x => x.BoniFidelite));
            DoitEtreEquivalent(infosDest.Comparaison, infosSource.Comparaison, o=>o.Excluding(x=>x.TauxInvAlternatif));
            DoitEtreEquivalent(infosDest.Comparaison.TauxInvAlternatif, infosSource.Comparaison.TauxInvAlternatif);
        }

        private static void DoitEtreEquivalent<T>(T destination, T source, Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> autresOptions = null) where T : Base
        {
            Func<EquivalencyAssertionOptions<T>, EquivalencyAssertionOptions<T>> options = o => o.SansProprietesObjetBase();
            if (autresOptions != null)
                options = o => autresOptions(o).SansProprietesObjetBase();
            destination.ShouldBeEquivalentTo(source, options);
        }

        private static void VerifierConcept(Scenario scenarioDest, Scenario scenarioSource)
        {
            scenarioDest.Concept.TypeDeConcept.Should().Be(scenarioSource.Concept.TypeDeConcept);
            DoitEtreEquivalent(scenarioDest.Concept.AssuranceRetraite, scenarioSource.Concept.AssuranceRetraite);
            DoitEtreEquivalent(scenarioDest.Concept.IRIS, scenarioSource.Concept.IRIS);
            DoitEtreEquivalent(scenarioDest.Concept.IRIS2, scenarioSource.Concept.IRIS2);
            DoitEtreEquivalent(scenarioDest.Concept.InvestorPlus, scenarioSource.Concept.InvestorPlus);
            DoitEtreEquivalent(scenarioDest.Concept.InvestorPlus2, scenarioSource.Concept.InvestorPlus2);
            DoitEtreEquivalent(scenarioDest.Concept.Iris2IllustrationReguliere, scenarioSource.Concept.Iris2IllustrationReguliere);
            DoitEtreEquivalent(scenarioDest.Concept.IrisIllustrationReguliere, scenarioSource.Concept.IrisIllustrationReguliere);
            DoitEtreEquivalent(scenarioDest.Concept.ProprietePartagee, scenarioSource.Concept.ProprietePartagee);
            DoitEtreEquivalent(scenarioDest.Concept.ProtectionPatrimoine, scenarioSource.Concept.ProtectionPatrimoine);
            DoitEtreEquivalent(scenarioDest.Concept.TransfertPatrimoine, scenarioSource.Concept.TransfertPatrimoine);
            DoitEtreEquivalent(scenarioDest.Concept.TransfertPatrimoineV2, scenarioSource.Concept.TransfertPatrimoineV2);
            DoitEtreEquivalent(scenarioDest.Concept.Zerocashflow, scenarioSource.Concept.Zerocashflow);
        }
    }
}