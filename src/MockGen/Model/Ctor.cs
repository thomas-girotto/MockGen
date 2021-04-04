using System;
using System.Collections.Generic;

namespace MockGen.Model
{
    public class Ctor
    {
        public static Ctor EmptyCtor = new Ctor();
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}
