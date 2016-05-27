using System;
using FluentAssertions;
using IAFG.IA.VI.AF.Base;
using IAFG.IA.VI.AF.Illustration;
using IAFG.IA.VI.AF.ObjetsPartages;
using IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration;

namespace IA_T.Illustration.Serialisation.Test.Utilitaires.Conversion
{
    public static class VerificationCasIllustration
    {
        public static void VerifierCasPourConceptIris2(CasIllustration casIllustration, IAFG.IA.VI.AF.Illustration.Illustration illustration)
        {
            VerifierCasIllustration(casIllustration, illustration, VerifierCasConceptIris2);
        }
        public static void VerifierCasPourConceptIris(CasIllustration casIllustration, IAFG.IA.VI.AF.Illustration.Illustration illustration)
        {
            VerifierCasIllustration(casIllustration, illustration, VerifierCasConceptIris);
        }
        public static void VerifierCasPourConceptAssuranceRetraite(CasIllustration casIllustration, IAFG.IA.VI.AF.Illustration.Illustration illustration)
        {
            VerifierCasIllustration(casIllustration, illustration, VerifierCasConceptAssuranceRetraite);
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

        private static void VerifierCasConceptIris2(CasScenario casScenario, Scenario scenario)
        {
            casScenario.ConceptIris2.Should().NotBeNull();
            VerifierCasConceptIrisCommun(casScenario.ConceptIris2, scenario);
            casScenario.ConceptIris2.FigerSoldePret.Should().Be(scenario.Concept.IRIS2.FigerSoldePret);
            casScenario.ConceptIris2.ReinvestRendement.Should().ContainInOrder(scenario.Concept.IRIS2.ReinvestRendement.ToArray());
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

        private static void VerifierCasConceptAssuranceRetraite(CasScenario casScenario, Scenario scenario)
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

        private static void VerifierCasPourProduit(CasScenario casScenario, Scenario scenario)
        {
            casScenario.Produit.Should().NotBeNull();
            casScenario.Produit.GammeProduit.Should().Be(scenario.Proposition.Produit.GammeProduit);
            casScenario.Produit.Universel.Should().NotBeNull();
            casScenario.Produit.Universel.Boni.ShouldBeEquivalentTo(scenario.Proposition.InfosPU.Boni);
            casScenario.Produit.Universel.CBR.Should().Be(scenario.Proposition.InfosPU.CBR);
            casScenario.Produit.Universel.CapitalOptimalValMax.Should().Be(scenario.Proposition.InfosPU.CapitalOptimalValMax);
            VerifierCasPourComparaisonPlacementAlternatif(casScenario, scenario);
        }

        private static void VerifierCasPourComparaisonPlacementAlternatif(CasScenario casScenario, Scenario scenario)
        {
            var placementAlternatif = casScenario.Produit.Universel.ComparaisonPlacementAlternatif;
            var tauxAlternatifs = placementAlternatif.TauxAlternatifs;
            var comparaison = scenario.Proposition.InfosPU.Comparaison;

            placementAlternatif.ShouldBeEquivalentTo(comparaison, o => o.Excluding(x => x.PrimeTemporaire).
                                                                         Excluding(x => x.TauxAlternatifs));
            placementAlternatif.PrimeTemporaire.Should().ContainInOrder(comparaison.PrimeTemporaire.ToArray());
            tauxAlternatifs.ShouldBeEquivalentTo(comparaison.TauxInvAlternatif, o => o.Excluding(x => x.GainCapTaux).
                                                                                       Excluding(x => x.InteretTaux).
                                                                                       Excluding(x => x.DividendeTaux));
            tauxAlternatifs.GainCapTaux.ShouldAllBeEquivalentTo(comparaison.TauxInvAlternatif.GainCapTaux.ToArray());
            tauxAlternatifs.InteretTaux.ShouldAllBeEquivalentTo(comparaison.TauxInvAlternatif.InteretTaux.ToArray());
            tauxAlternatifs.DividendeTaux.ShouldAllBeEquivalentTo(comparaison.TauxInvAlternatif.DividendeTaux.ToArray());
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

    }
}