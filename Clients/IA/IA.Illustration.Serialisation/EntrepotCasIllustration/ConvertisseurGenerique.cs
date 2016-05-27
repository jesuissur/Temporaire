using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IAFG.IA.VI.AF.Illustration;
using IAFG.IA.VI.AF.ObjetsPartages;
using IAFG.IA.VI.AF.Proposition;

namespace IA_T.Illustration.Serialisation.Test.EntrepotCasIllustration
{
    public static class ConvertisseurGenerique
    {
        private static readonly Object _barriere = new Object();
        private static IMapper _instance;

        public static IMapper Instance
        {
            get
            {
                if (_instance == null)
                    lock (_barriere)
                        if (_instance == null)
                            _instance = Configuration.CreateMapper();
                return _instance;
            }
        }

        public static MapperConfiguration Configuration
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                {
                    IgnorerProprietesObjetBase(cfg);
                    ConfigurerConvertisseursPourVecteurs(cfg);

                    ConfigurerIllustration(cfg);
                    ConfigurerScenario(cfg);
                    ConfigurerConcepts(cfg);
                    ConfigurerProduit(cfg);
                    cfg.CreateMap<EmpruntBancaire, CasEmpruntBancaire>().ReverseMap();
                    cfg.CreateMap<Taxation, CasTaxation>().ReverseMap();
                    cfg.CreateMap<Prestation, CasPrestation>();
                    cfg.CreateMap<Primes, CasPrimes>().ReverseMap();
                });
                return config;
            }
        }

        private static void ConfigurerProduit(IMapperConfiguration cfg)
        {
            cfg.CreateMap<Produit, CasProduit>().
                ForMember(x => x.Universel, o=>o.MapFrom(s => s.Proposition.InfosPU));
            cfg.CreateMap<CasProduit, Produit>().AfterMap((s, d) =>
                                                          {
                                                              Instance.Map(s.Universel, d.Proposition.InfosPU);
                                                          });
            cfg.CreateMap<InfosPU, CasPoliceUniversel>().
                ForMember(x=>x.ComparaisonPlacementAlternatif, o=>o.MapFrom(s=>s.Proposition.InfosPU.Comparaison));
            cfg.CreateMap<CasPoliceUniversel, InfosPU>().
                ForMember(x => x.Comparaison, o => o.MapFrom(s => s.ComparaisonPlacementAlternatif));
            cfg.CreateMap<BoniContrat, CasBoni>().ReverseMap();
            cfg.CreateMap<Comparaison, CasComparaisonPlacement>()
                .ForMember(x=>x.TauxAlternatifs, o=>o.MapFrom(s=>s.TauxInvAlternatif));
            cfg.CreateMap<CasComparaisonPlacement, Comparaison>()
                .ForMember(x => x.TauxInvAlternatif, o => o.MapFrom(s => s.TauxAlternatifs));
            cfg.CreateMap<PlacementAlternatif, CasPlacementAlternatif>().ReverseMap();
        }

        private static void ConfigurerScenario(IMapperConfiguration cfg)
        {
            cfg.CreateMap<Scenario, CasScenario>().
                ForMember(x => x.TypeConcept, o => o.MapFrom(s => s.Concept.TypeDeConcept)).
                ForMember(x => x.EmpruntBancaire, o => o.MapFrom(s => s.InfoTrad.EmpruntBancaire)).
                ForMember(x => x.Produit, o => o.MapFrom(s => s.Proposition.Produit));
            cfg.CreateMap<CasScenario, Scenario>().AfterMap(ConvertirAvecInstancesExistantes).
                ForMember(x => x.TabPerso, o => o.Ignore()).
                ForMember(x => x.GraPerso, o => o.Ignore());
        }

        private static void ConfigurerConcepts(IMapperConfiguration cfg)
        {
            cfg.CreateMap<ConceptAssuranceRetraite, CasConceptAssuranceRetraite>().ReverseMap();
            cfg.CreateMap<ConceptIRIS2, CasConceptIris2>().
                ForMember(x => x.VehiculeCompte, o => o.MapFrom(s => s.VehiculeCompteCollateral));
            cfg.CreateMap<CasConceptIris2, ConceptIRIS2>().
                ForMember(x => x.VehiculeCompteCollateral, o => o.MapFrom(s => s.VehiculeCompte));
            cfg.CreateMap<ConceptIRIS, CasConceptIris>().ReverseMap();
            cfg.CreateMap<CasScenario, Concept>().
                ForMember(x => x.TypeDeConcept, o => o.MapFrom(s => s.TypeConcept)).
                ForMember(x => x.IRIS2, o => o.MapFrom(s => s.ConceptIris2)).
                ForMember(x => x.AssuranceRetraite, o => o.MapFrom(s => s.ConceptAssuranceRetraite));
        }

        private static void ConfigurerIllustration(IMapperConfiguration cfg)
        {
            cfg.CreateMap<IAFG.IA.VI.AF.Illustration.Illustration, CasIllustration>().
                ForMember(x => x.DejaAlle, o => o.MapFrom(s => s.PropoInfoGenerale.DejaAlle)).
                ForMember(x => x.F1, o => o.MapFrom(s => s.PropoInfoGenerale.F1)).
                ForMember(x => x.LangueCorrespondance, o => o.MapFrom(s => s.PropoInfoGenerale.LangueCorrespondance)).
                ForMember(x => x.Q4, o => o.MapFrom(s => s.PropoInfoGenerale.Q4)).
                ForMember(x => x.Q6, o => o.MapFrom(s => s.PropoInfoGenerale.Q6)).
                ForMember(x => x.TypeProposition, o => o.MapFrom(s => s.PropoInfoGenerale.TypeProposition));
            cfg.CreateMap<CasIllustration, IAFG.IA.VI.AF.Illustration.Illustration>().AfterMap((s, d) =>
                {
                    ConvertirPropoInfoGenerale(d, s);
                    ConvertirScenarios(d, s);
                });
        }

        private static void ConvertirScenarios(IAFG.IA.VI.AF.Illustration.Illustration illustration, CasIllustration casIllustration)
        {
            casIllustration.Scenarios.ForEach(cas =>
            {
                var scenario = illustration.Scenarios.Add();
                Instance.Map(cas, scenario);
            });
        }

        private static void ConvertirPropoInfoGenerale(IAFG.IA.VI.AF.Illustration.Illustration illustration, CasIllustration casIllustration)
        {
            illustration.PropoInfoGenerale.DejaAlle = casIllustration.DejaAlle;
            illustration.PropoInfoGenerale.F1 = casIllustration.F1;
            illustration.PropoInfoGenerale.LangueCorrespondance = casIllustration.LangueCorrespondance;
            illustration.PropoInfoGenerale.Q4 = casIllustration.Q4;
            illustration.PropoInfoGenerale.Q6 = casIllustration.Q6;
            illustration.PropoInfoGenerale.TypeProposition = casIllustration.TypeProposition;
        }

        private static void IgnorerProprietesObjetBase(IMapperConfiguration cfg)
        {
            cfg.AddGlobalIgnore("Parent");
            cfg.AddGlobalIgnore("EstVerouille");
            cfg.AddGlobalIgnore("NomFichierSauvegarde");
            cfg.AddGlobalIgnore("EstAppelleDuValidateur");
            cfg.AddGlobalIgnore("EstEnChargement");
            cfg.AddGlobalIgnore("EstEnSauvegarde");
            cfg.AddGlobalIgnore("Id");
            cfg.AddGlobalIgnore("TypeTransactionChg");
            cfg.AddGlobalIgnore("CalculFait");
        }

        /// <summary>
        /// Puisque les objets d'affaires sous une Illustration empêchent la construction d'une instance, 
        /// on effectue la conversion en utilisant les instances créées dans les constructeurs privés
        /// </summary>
        private static void ConvertirAvecInstancesExistantes(CasScenario casScenario, Scenario scenario)
        {
            Instance.Map(casScenario, scenario.Concept);
            Instance.Map(casScenario.Produit, scenario.Proposition.Produit);
            Instance.Map(casScenario.EmpruntBancaire, scenario.InfoTrad.EmpruntBancaire);
            Instance.Map(casScenario.Primes, scenario.Primes);
            Instance.Map(casScenario.Taxation, scenario.Taxation);
        }

        private static void ConfigurerConvertisseursPourVecteurs(IMapperConfiguration cfg)
        {
            ConfigurerConvertisseursPourTypeVecteur<Decimal>(cfg);
            ConfigurerConvertisseursPourTypeVecteur<int>(cfg);
        }

        private static void ConfigurerConvertisseursPourTypeVecteur<T>(IMapperConfiguration cfg)
        {
            cfg.CreateMap(typeof(Vecteur<T>), typeof(List<T>)).ConvertUsing(typeof(ConversionGeneriqueVecteur<T>));
            cfg.CreateMap(typeof(List<T>), typeof(Vecteur<T>)).ConvertUsing(typeof(ConversionGeneriqueListeEnVecteur<T>));
        }
    }

    public class ConversionGeneriqueVecteur<T> : ITypeConverter<Vecteur<T>, List<T>>
    {
        public List<T> Convert(ResolutionContext context)
        {
            return (context.SourceValue == null ? new List<T>() : ((Vecteur<T>) context.SourceValue).ToArray().ToList());
        }
    }

    public class ConversionGeneriqueListeEnVecteur<T> : ITypeConverter<List<T>, Vecteur<T>>
    {
        public Vecteur<T> Convert(ResolutionContext context)
        {
            var vecteur = context.DestinationValue as Vecteur<T>;
            var liste = context.SourceValue as List<T>;

            if (liste != null && vecteur != null)
            {
                foreach (T element in liste)
                {
                    var elementParReference = element;
                    vecteur.Add(ref elementParReference);
                }
            }
            return vecteur;
        }
    }
}