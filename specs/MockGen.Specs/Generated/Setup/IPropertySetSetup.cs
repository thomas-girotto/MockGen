namespace MockGen.Setup
{
    internal interface IPropertySetSetup<TParam>
    {
        IMethodSetup<TParam> ForValue(Arg<TParam> param);
    }
}
