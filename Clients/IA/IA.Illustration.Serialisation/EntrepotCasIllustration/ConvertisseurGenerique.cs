using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    cfg.AddGlobalIgnore("Parent");
                    cfg.AddGlobalIgnore("EstVerouille");
                    cfg.AddGlobalIgnore("NomFichierSauvegarde");
                    cfg.AddGlobalIgnore("EstAppelleDuValidateur");
                    cfg.AddGlobalIgnore("EstEnChargement");
                    cfg.AddGlobalIgnore("EstEnSauvegarde");
                    cfg.AddGlobalIgnore("Id");
                    cfg.AddGlobalIgnore("TypeTransactionChg");
                    cfg.AddGlobalIgnore("CalculFait");

                    ConfigurerConvertisseursPourVecteurs(cfg);

                    cfg.CreateMap<IAFG.IA.VI.AF.Illustration.Illustration, CasIllustration>().
                        ForMember(x => x.DejaAlle, o => o.MapFrom(s => s.PropoInfoGenerale.DejaAlle)).
                        ForMember(x => x.F1, o => o.MapFrom(s => s.PropoInfoGenerale.F1)).
                        ForMember(x => x.LangueCorrespondance, o => o.MapFrom(s => s.PropoInfoGenerale.LangueCorrespondance)).
                        ForMember(x => x.Q4, o => o.MapFrom(s => s.PropoInfoGenerale.Q4)).
                        ForMember(x => x.Q6, o => o.MapFrom(s => s.PropoInfoGenerale.Q6)).
                        ForMember(x => x.TypeProposition, o => o.MapFrom(s => s.PropoInfoGenerale.TypeProposition));
                    cfg.CreateMap<CasIllustration, IAFG.IA.VI.AF.Illustration.Illustration>().AfterMap((s, d) =>
                        {
                            d.PropoInfoGenerale.DejaAlle = s.DejaAlle;
                            d.PropoInfoGenerale.F1 = s.F1;
                            d.PropoInfoGenerale.LangueCorrespondance = s.LangueCorrespondance;
                            d.PropoInfoGenerale.Q4 = s.Q4;
                            d.PropoInfoGenerale.Q6 = s.Q6;
                            d.PropoInfoGenerale.TypeProposition = s.TypeProposition;
                            s.Scenarios.ForEach(cas =>
                            {
                                var scenario = d.Scenarios.Add();
                                Instance.Map(cas, scenario);
                            });
                        }).
                        ForMember(x => x.PropoInfoGenerale, o => o.Ignore());

                    cfg.CreateMap<ConceptAssuranceRetraite, CasConceptAssuranceRetraite>().ReverseMap();
                    cfg.CreateMap<ConceptIRIS2, CasConceptIris2>().
                        ForMember(x => x.VehiculeCompte, o=> o.MapFrom(s => s.VehiculeCompteCollateral));
                    cfg.CreateMap<CasConceptIris2, ConceptIRIS2>().
                        ForMember(x => x.VehiculeCompteCollateral, o => o.MapFrom(s => s.VehiculeCompte)).
                        ForMember(x => x.IsInitialised, o => o.Ignore());
                    cfg.CreateMap<ConceptIRIS, CasConceptIris>().ReverseMap();

                    cfg.CreateMap<Produit, CasProduit>();
                    cfg.CreateMap<CasProduit, Produit>();
                    cfg.CreateMap<EmpruntBancaire, CasEmpruntBancaire>().ReverseMap();
                    cfg.CreateMap<Taxation, CasTaxation>().ReverseMap();
                    cfg.CreateMap<Prestation, CasPrestation>();
                    cfg.CreateMap<Primes, CasPrimes>().
                        ForMember(x => x.FlagPrimeMinimum, o => o.Ignore());

                    cfg.CreateMap<CasScenario, Concept>().
                        ForMember(x => x.TypeDeConcept, o => o.MapFrom(s => s.TypeConcept)).
                        ForMember(x => x.IRIS2, o => o.MapFrom(s => s.ConceptIris2)).
                        ForMember(x => x.AssuranceRetraite, o => o.MapFrom(s => s.ConceptAssuranceRetraite));


                    cfg.CreateMap<Scenario, CasScenario>().
                        ForMember(x => x.TypeConcept, o => o.MapFrom(s => s.Concept.TypeDeConcept)).
                        ForMember(x => x.EmpruntBancaire, o => o.MapFrom(s => s.InfoTrad.EmpruntBancaire)).
                        ForMember(x => x.Produit, o => o.MapFrom(s => s.Proposition.Produit));
                    cfg.CreateMap<CasScenario, Scenario>().AfterMap((s, d) =>
                        {
                            Instance.Map(s, d.Concept);
                            Instance.Map(s.Produit, d.Proposition.Produit);
                        }).
                        ForMember(x => x.Proposition, o => o.Ignore()).
                        ForMember(x => x.TabPerso, o => o.Ignore()).
                        ForMember(x => x.GraPerso, o => o.Ignore());
                });
                return config;
            }
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
            return (context.SourceValue == null
                ? new List<T>()
                : ((Vecteur<T>) context.SourceValue).ToArray().ToList());
        }
    }

    public class ConversionGeneriqueListeEnVecteur<T> : ITypeConverter<List<T>, Vecteur<T>>
    {
        public Vecteur<T> Convert(ResolutionContext context)
        {
            Vecteur<T> vecteur = null;
            if (context.SourceValue != null)
            {
                vecteur = (Vecteur<T>) context.DestinationValue;
                var liste = (List<T>)context.SourceValue;

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