using System;

namespace MockGen.Diagnostics
{
    public class AddSourceToBuildContextException : Exception
    {
        public AddSourceToBuildContextException(string fileName, Exception innerException) : base(string.Empty, innerException)
        {
            FileName = fileName;
        }

        public string FileName { get; }
    }
}
