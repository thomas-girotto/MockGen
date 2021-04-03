using System.Collections.Generic;

namespace MockGen.Model
{
    public class Mock
    {
        private List<Ctor> _ctors = new List<Ctor>();
        private Dictionary<string, int> _numberOfMethodWithSameName = new Dictionary<string, int>();

        public Type TypeToMock { get; set; }

        /// <summary>
        /// Keeps the original type to mock unchanged (as opposed to <see cref="TypeToMock"/> which can be changed to avoid type collision)
        /// </summary>
        public string TypeToMockOriginalName { get; set; }

        public List<Method> Methods { get; set; } = new List<Method>();


        public void AddMethod(Method method)
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

        public List<Ctor> Ctors
        {
            get => _ctors;
            set
            {
                _ctors = (value == null || value.Count == 0)
                    ? new List<Ctor> { Ctor.EmptyCtor }
                    : value;
            }
        }
        public bool IsInterface { get; set; }   
    }
}
