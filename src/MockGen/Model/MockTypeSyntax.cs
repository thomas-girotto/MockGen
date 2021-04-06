using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MockGen.Model
{
    public class MockTypeSyntax
    {
        public MockTypeSyntax(string typeName, TypeSyntax syntax)
        {
            TypeName = typeName;
            TypeSyntax = syntax;
        }

        public string TypeName { get; private set; }
        public TypeSyntax TypeSyntax { get; private set; }
    }
}
