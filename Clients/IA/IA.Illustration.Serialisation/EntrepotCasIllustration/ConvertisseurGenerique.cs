using System;
using AutoMapper;
using IAFG.IA.VI.AF.Illustration;

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
                            _instance = CreerInstance();
                return _instance;
            }
        }

        private static IMapper CreerInstance()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<IAFG.IA.VI.AF.Illustration.Illustration, CasIllustration>().
                    ForMember(x => x.DejaAlle, o => o.MapFrom(s => s.PropoInfoGenerale.DejaAlle)).
                    ForMember(x => x.F1, o => o.MapFrom(s => s.PropoInfoGenerale.F1)).
                    ForMember(x => x.LangueCorrespondance, o => o.MapFrom(s => s.PropoInfoGenerale.LangueCorrespondance)).
                    ForMember(x => x.Q4, o => o.MapFrom(s => s.PropoInfoGenerale.Q4)).
                    ForMember(x => x.Q6, o => o.MapFrom(s => s.PropoInfoGenerale.Q6)).
                    ForMember(x => x.TypeProposition, o => o.MapFrom(s => s.PropoInfoGenerale.TypeProposition));
                cfg.CreateMap<Scenario, CasScenario>();
                cfg.CreateMap<PropoInfoGenerale, CasIllustration>().
                    ForSourceMember(s => s.NoScenario, o => o.Ignore());
                //cfg.RecognizePrefixes("PropoInfoGenerale");
                //cfg.AddMemberConfiguration().AddName<PrePostfixName>(x => x.Prefixes())
            });
            return config.CreateMapper();
        }
    }
}