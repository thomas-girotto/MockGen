using System;
using System.Collections.Generic;
using System.Text;

namespace MockGen.Sample
{
    public interface IDependencyOutParams
    {
        bool TryGetById(int id, out SomeModel model);

        bool TryGetByIdWithSeveralOutParameters(int id, out SomeModel outParam1, out SomeModel outParam2);
    }
}
