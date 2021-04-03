using System;
using System.Collections.Generic;
using System.Linq;

namespace MockGen.Model
{
    public class MockDescriptor
    {
        private List<CtorDescriptor> _ctors = new List<CtorDescriptor>();
        private Dictionary<string, int> _numberOfMethodWithSameName = new Dictionary<string, int>();

        public Type TypeToMock { get; set; }
        /// <summary>
        /// Keeps the original type to mock unchanged (as opposed to <see cref="TypeToMock"/> which can be changed to avoid type collision)
        /// </summary>
        public string TypeToMockOriginalName { get; set; }

        public List<MethodDescriptor> Methods { get; set; } = new List<MethodDescriptor>();


        public void AddMethod(MethodDescriptor method)
        {
            if (_numberOfMethodWithSameName.ContainsKey(method.Name))
            {
                var suffix = _numberOfMethodWithSameName[method.Name];
                method.UniqueName = method.Name + suffix;
                _numberOfMethodWithSameName[method.Name]++;
            }
            else
            {
                method.UniqueName = method.Name;
                _numberOfMethodWithSameName[method.Name] = 1;
            }
            Methods.Add(method);
        }
        
        public List<PropertyDescriptor> Properties { get; set; } = new List<PropertyDescriptor>();

        public List<CtorDescriptor> Ctors
        {
            get => _ctors;
            set
            {
                _ctors = (value == null || value.Count == 0)
                    ? new List<CtorDescriptor> { CtorDescriptor.EmptyCtor }
                    : value;
            }
        }
        public bool IsInterface { get; set; }   
    }
}
