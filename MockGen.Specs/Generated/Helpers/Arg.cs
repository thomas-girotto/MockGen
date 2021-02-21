﻿namespace MockGen.Specs.Generated.Helpers
{
    internal class Arg<TParam>
    {
        private readonly TParam param;
        internal static Arg<TParam> Any = new Arg<TParam>();
        internal static Arg<TParam> Null = new Arg<TParam>();

        private Arg() { }

        private Arg(TParam param)
        {
            this.param = param;
        }

        internal TParam Value => param;

        internal static Arg<TParam> Create(TParam param)
        {
            return param == null
                ? Null
                : new Arg<TParam>(param);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj is Arg<TParam> other)
            {
                return other.Value == null
                    ? false
                    : other.Value.Equals(Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return Value?.GetHashCode() ?? 0;
        }

        public static implicit operator Arg<TParam>(TParam arg) => new Arg<TParam>(arg);
    }
}
