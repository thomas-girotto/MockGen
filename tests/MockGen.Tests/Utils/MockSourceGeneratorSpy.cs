using MockGen.Model;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Tests.Utils
{
    public class MockSourceGeneratorSpy : MockSourceGenerator
    {
        /// <summary>
        /// Expose the types we want to mock but only in the context of unit tests 
        /// <see cref="MockSourceGenerator.SanityzeMocks(IEnumerable{Mock})"/> comment
        /// </summary>
        public List<Mock> TypesToMock = new List<Mock>();

        protected override IEnumerable<Mock> SanityzeMocks(IEnumerable<Mock> allMocksFoundFromSyntax)
        {
            TypesToMock = base.SanityzeMocks(allMocksFoundFromSyntax).ToList();
            return TypesToMock;
        }
    }
}
