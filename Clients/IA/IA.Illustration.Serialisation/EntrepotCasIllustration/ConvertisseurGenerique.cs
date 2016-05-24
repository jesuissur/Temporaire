using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using IAFG.IA.VI.AF.Illustration;
using IAFG.IA.VI.AF.ObjetsPartages;

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
                    cfg.CreateMap(typeof(Vecteur<Decimal>), typeof(List<Decimal>)).ConvertUsing(typeof(ConversionGeneriqueVecteur<Decimal>));
                    cfg.CreateMap<IAFG.IA.VI.AF.Illustration.Illustration, CasIllustration>().
                        ForMember(x => x.DejaAlle, o => o.MapFrom(s => s.PropoInfoGenerale.DejaAlle)).
                        ForMember(x => x.F1, o => o.MapFrom(s => s.PropoInfoGenerale.F1)).
                        ForMember(x => x.LangueCorrespondance, o => o.MapFrom(s => s.PropoInfoGenerale.LangueCorrespondance)).
                        ForMember(x => x.Q4, o => o.MapFrom(s => s.PropoInfoGenerale.Q4)).
                        ForMember(x => x.Q6, o => o.MapFrom(s => s.PropoInfoGenerale.Q6)).
                        ForMember(x => x.TypeProposition, o => o.MapFrom(s => s.PropoInfoGenerale.TypeProposition));
                    cfg.CreateMap<ConceptAssuranceRetraite, CasConceptAssuranceRetraite>();
                    cfg.CreateMap<ConceptIRIS2, CasConceptIris2>();
                    cfg.CreateMap<Scenario, CasScenario>().
                        ForMember(x => x.TypeConcept, o => o.MapFrom(s => s.Concept.TypeDeConcept));
                });
                return config;
            }
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
}