using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class Ctor
    {
        public static Ctor EmptyCtor = new Ctor();
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
        public List<Parameter> ParametersWithoutOutParams => Parameters.Where(p => !p.IsOutParameter).ToList();
        public List<Parameter> OutParameters => Parameters.Where(p => p.IsOutParameter).ToList();
        
        public string ParameterTypes => string.Join(", ", Parameters.Select(p => p.Type.Name));
        
        public string ParameterTypesWithoutOutParameters => string.Join(", ", ParametersWithoutOutParams.Select(p => p.Type.Name));
    }
}
