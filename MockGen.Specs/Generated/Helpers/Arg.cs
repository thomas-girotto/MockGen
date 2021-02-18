using System;

namespace MockGen.Specs.Generated.Helpers
{
    internal struct Arg<TParam>
    {
        private readonly TParam param;
        internal static Arg<TParam> Any = new Arg<TParam>();
        internal TParam Value => param;

        public Arg(TParam param)
        {
            this.param = param;
        }

        public static implicit operator Arg<TParam>(TParam arg) => new Arg<TParam>(arg);
    }
}
