using MockGen.Model;
using System.Collections.Generic;

namespace MockGen.Tests.Utils
{
    public class MockSourceGeneratorSpy : MockSourceGenerator
    {
        /// <summary>
        /// Expose the types we want to mock but only in the context of unit tests <see cref="MockSourceGenerator.AddTypeToMock(List{MockDescriptor}, MockDescriptor)"/> comment
        /// </summary>
        public List<MockDescriptor> TypesToMock = new List<MockDescriptor>();

        protected override void AddTypeToMock(List<MockDescriptor> types, MockDescriptor toAdd)
        {
            TypesToMock.Add(toAdd);
            base.AddTypeToMock(types, toAdd);
        }
    }
}
