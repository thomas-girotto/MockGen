using MockGen.Model;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Tests.Utils
{
    public class MockSourceGeneratorSpy : MockSourceGenerator
    {
        /// <summary>
        /// Expose the types we want to mock but only in the context of unit tests 
        /// <see cref="MockSourceGenerator.SanityzeMocks(IEnumerable{MockDescriptor})"/> comment
        /// </summary>
        public List<MockDescriptor> TypesToMock = new List<MockDescriptor>();

        protected override IEnumerable<MockDescriptor> SanityzeMocks(IEnumerable<MockDescriptor> allMocksFoundFromSyntax)
        {
            TypesToMock = base.SanityzeMocks(allMocksFoundFromSyntax).ToList();
            return TypesToMock;
        }
    }
}
