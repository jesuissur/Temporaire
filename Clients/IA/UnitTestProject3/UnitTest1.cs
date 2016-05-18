using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using FluentAssertions;
using IAFG.IA.VI.AF.Illustration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {

        [TestInitialize]
        public void InitialiserClasseDeTest()
        {
            PatchExternalReferencesWithEverythingOnEarth();
            File.Delete(@"D:\IA_TFS\_Temp\SavedFiles\3d0e722fd9b12b2120ad6b557d4992fb.lck");
        }

        [TestMethod]
        public void TestMethod1()
        {
            var illustration = new Illustration();
            illustration.Load(@"D:\IA_TFS\_Temp\SavedFiles\MKEVI999_9512675805_ConversionIRIS_Ent5-I1-P1-D1.evia_IAP");
            var debugSerialisationJson = new MemoryTraceWriter();
            var optionsJson = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.All,
                                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                                                           ContractResolver = new FieldsOnlyContratResolver(),
                                                           TraceWriter = debugSerialisationJson};
            //optionsJson.Converters.Add(new Converter());
            try
            {
                var json = JsonConvert.SerializeObject(illustration, optionsJson);

                var illustration2 = JsonConvert.DeserializeObject<Illustration>(json, optionsJson);
                illustration.ShouldBeEquivalentTo(illustration2, x => x.IgnoringCyclicReferences().ExcludingProperties());
            }
            finally
            {
                //Console.WriteLine(debugSerialisationJson);
            }
        }

        private static void PatchExternalReferencesWithEverythingOnEarth()
        {
            var emplacementCourant = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string sourceReferences = Path.Combine(emplacementCourant, @"..\..\_Ref");
            foreach (var nomFichier in Directory.GetFiles(sourceReferences))
            {
                var nouveauNomFichier = Path.Combine(emplacementCourant, Path.GetFileName(nomFichier));
                if (!File.Exists(nouveauNomFichier))
                    File.Copy(nomFichier, nouveauNomFichier);
            }
        }

    }

    public class Converter : JsonConverter
    {
        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException("Inutile car la propriété CanWrite empêche l'appel de la méthode WriteJson");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.StartObject)
            {
                //var objetJson = JObject.Load(reader);
                var t = serializer.Deserialize(reader);
            }
            
            return null;
        }

        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(Scenario));
        }
    }
}
