using System;

namespace MockGen.Specs.Generated.Helpers
{
    internal class Arg<TParam>
    {
        private readonly TParam param;
        private readonly Predicate<TParam> predicate;
        internal static Arg<TParam> Any = new Arg<TParam>();
        internal static Arg<TParam> Null = new Arg<TParam>();

        private Arg() { }

        private Arg(TParam param)
        {
            this.param = param;
        }

        private Arg(Predicate<TParam> predicate)
        {
            this.predicate = predicate;
        }

        internal static Arg<TParam> When(Predicate<TParam> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException(nameof(predicate));
            }
            return new Arg<TParam>(predicate);
        }

        internal TParam Value => param;

        internal Predicate<TParam> Predicate => predicate;

        public static implicit operator Arg<TParam>(TParam arg) => new Arg<TParam>(arg);
    }
}
