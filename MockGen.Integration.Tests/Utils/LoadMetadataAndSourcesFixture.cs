using Xunit;

namespace MockGen.Integration.Tests.Utils
{
    [CollectionDefinition("Load both metadata and sources")]
    public class LoadMetadataAndSourcesFixture : ICollectionFixture<LoadMetadataReferenceFixture>, ICollectionFixture<LoadCommonSpecsFilesFixture>
    {
    }
}
