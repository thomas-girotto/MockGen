﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 16.0.0.0
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
namespace MockGen.Templates.Setup
{
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    /// <summary>
    /// Class to produce the template output
    /// </summary>
    
    #line 1 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public partial class MethodSetupVoidPnTextTemplate : MethodSetupVoidPnTextTemplateBase
    {
#line hidden
        /// <summary>
        /// Create the template output
        /// </summary>
        public virtual string TransformText()
        {
            this.Write("using System;\r\nusing System.Collections.Generic;\r\nusing System.Linq;\r\nusing MockG" +
                    "en.Matcher;\r\n\r\nnamespace MockGen.Setup\r\n{\r\n    internal class MethodSetupVoid<");
            
            #line 13 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write("> : MethodSetup<");
            
            #line 13 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write(">\r\n    {\r\n        private Stack<ActionSpecification<");
            
            #line 15 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write(">> actionByMatchingCriteria = new Stack<ActionSpecification<");
            
            #line 15 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write(">>();\r\n        private ActionSpecification<");
            
            #line 16 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write("> currentlyConfiguredAction = ActionSpecification<");
            
            #line 16 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write(">.CreateNew();\r\n\r\n        public IMethodSetup<");
            
            #line 18 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write("> ForParameter(");
            
            #line 18 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.ConcatClassParameterByParameterType("Arg", "param")));
            
            #line default
            #line hidden
            this.Write(")\r\n        {\r\n            if (!setupDone)\r\n            {\r\n                actionB" +
                    "yMatchingCriteria.Push(currentlyConfiguredAction);\r\n                currentlyCon" +
                    "figuredAction = ActionSpecification<");
            
            #line 23 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write(">.CreateNew();\r\n\r\n");
            
            #line 25 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
 foreach (var n in Descriptor.EnumerateNumbers) { 
            
            #line default
            #line hidden
            this.Write("                currentlyConfiguredAction.Matcher");
            
            #line 26 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(n));
            
            #line default
            #line hidden
            this.Write(" = ArgMatcher<TParam");
            
            #line 26 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(n));
            
            #line default
            #line hidden
            this.Write(">.Create(param");
            
            #line 26 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(n));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 27 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("            }\r\n            else\r\n            {\r\n");
            
            #line 31 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
 foreach (var n in Descriptor.EnumerateNumbers) { 
            
            #line default
            #line hidden
            this.Write("                matcher");
            
            #line 32 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(n));
            
            #line default
            #line hidden
            this.Write(" = ArgMatcher<TParam");
            
            #line 32 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(n));
            
            #line default
            #line hidden
            this.Write(">.Create(param");
            
            #line 32 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(n));
            
            #line default
            #line hidden
            this.Write(");\r\n");
            
            #line 33 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
 } 
            
            #line default
            #line hidden
            this.Write("            }\r\n\r\n            return this;\r\n        }\r\n\r\n        public override v" +
                    "oid Throws<TException>()\r\n        {\r\n            EnsureConfigurationMethodsAreAl" +
                    "lowed(nameof(Throws));\r\n            currentlyConfiguredAction.MockingAction = ");
            
            #line 42 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.DiscardParameters));
            
            #line default
            #line hidden
            this.Write(" => throw new TException();\r\n        }\r\n\r\n        public override void Throws<TEx" +
                    "ception>(TException exception)\r\n        {\r\n            EnsureConfigurationMethod" +
                    "sAreAllowed(nameof(Throws));\r\n            currentlyConfiguredAction.MockingActio" +
                    "n = ");
            
            #line 48 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.DiscardParameters));
            
            #line default
            #line hidden
            this.Write(" => throw exception;\r\n        }\r\n\r\n        public override void Execute(Action<");
            
            #line 51 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write(@"> callback)
        {
            EnsureConfigurationMethodsAreAllowed(nameof(Execute));
            currentlyConfiguredAction.AdditionalCallback = callback;
        }

        internal override void SetupDone()
        {
            base.SetupDone();
            actionByMatchingCriteria.Push(currentlyConfiguredAction);
            currentlyConfiguredAction = ActionSpecification<");
            
            #line 61 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.GenericTypes));
            
            #line default
            #line hidden
            this.Write(">.CreateNew();\r\n        }\r\n\r\n        public void ExecuteSetup(");
            
            #line 64 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.ParametersTypesWithName));
            
            #line default
            #line hidden
            this.Write(")\r\n        {\r\n            // Register call with given parameter for future assert" +
                    "ions on calls\r\n            calls.Add(");
            
            #line 67 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
 if (Descriptor.NumberOfTypes > 1) {
            
            #line default
            #line hidden
            this.Write("(");
            
            #line 67 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
}
            
            #line default
            #line hidden
            
            #line 67 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.ConcatParameters("param")));
            
            #line default
            #line hidden
            
            #line 67 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
 if (Descriptor.NumberOfTypes > 1) {
            
            #line default
            #line hidden
            this.Write(")");
            
            #line 67 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
}
            
            #line default
            #line hidden
            this.Write(");\r\n            // Execute the configured action according to given parameters\r\n " +
                    "           foreach (var setupAction in actionByMatchingCriteria)\r\n            {\r" +
                    "\n                if (setupAction.Match(");
            
            #line 71 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.ConcatParameters("param")));
            
            #line default
            #line hidden
            this.Write("))\r\n                {\r\n                    setupAction.ExecuteActions(");
            
            #line 73 "D:\Dev\MockGen\MockGen\Templates\Setup\MethodSetupVoidPnTextTemplate.tt"
            this.Write(this.ToStringHelper.ToStringWithCulture(Descriptor.ConcatParameters("param")));
            
            #line default
            #line hidden
            this.Write(");\r\n                    return;\r\n                }\r\n            }\r\n        }\r\n   " +
                    " }\r\n}");
            return this.GenerationEnvironment.ToString();
        }
    }
    
    #line default
    #line hidden
    #region Base class
    /// <summary>
    /// Base class for this transformation
    /// </summary>
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TextTemplating", "16.0.0.0")]
    public class MethodSetupVoidPnTextTemplateBase
    {
        #region Fields
        private global::System.Text.StringBuilder generationEnvironmentField;
        private global::System.CodeDom.Compiler.CompilerErrorCollection errorsField;
        private global::System.Collections.Generic.List<int> indentLengthsField;
        private string currentIndentField = "";
        private bool endsWithNewline;
        private global::System.Collections.Generic.IDictionary<string, object> sessionField;
        #endregion
        #region Properties
        /// <summary>
        /// The string builder that generation-time code is using to assemble generated output
        /// </summary>
        protected System.Text.StringBuilder GenerationEnvironment
        {
            get
            {
                if ((this.generationEnvironmentField == null))
                {
                    this.generationEnvironmentField = new global::System.Text.StringBuilder();
                }
                return this.generationEnvironmentField;
            }
            set
            {
                this.generationEnvironmentField = value;
            }
        }
        /// <summary>
        /// The error collection for the generation process
        /// </summary>
        public System.CodeDom.Compiler.CompilerErrorCollection Errors
        {
            get
            {
                if ((this.errorsField == null))
                {
                    this.errorsField = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errorsField;
            }
        }
        /// <summary>
        /// A list of the lengths of each indent that was added with PushIndent
        /// </summary>
        private System.Collections.Generic.List<int> indentLengths
        {
            get
            {
                if ((this.indentLengthsField == null))
                {
                    this.indentLengthsField = new global::System.Collections.Generic.List<int>();
                }
                return this.indentLengthsField;
            }
        }
        /// <summary>
        /// Gets the current indent we use when adding lines to the output
        /// </summary>
        public string CurrentIndent
        {
            get
            {
                return this.currentIndentField;
            }
        }
        /// <summary>
        /// Current transformation session
        /// </summary>
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session
        {
            get
            {
                return this.sessionField;
            }
            set
            {
                this.sessionField = value;
            }
        }
        #endregion
        #region Transform-time helpers
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void Write(string textToAppend)
        {
            if (string.IsNullOrEmpty(textToAppend))
            {
                return;
            }
            // If we're starting off, or if the previous text ended with a newline,
            // we have to append the current indent first.
            if (((this.GenerationEnvironment.Length == 0) 
                        || this.endsWithNewline))
            {
                this.GenerationEnvironment.Append(this.currentIndentField);
                this.endsWithNewline = false;
            }
            // Check if the current text ends with a newline
            if (textToAppend.EndsWith(global::System.Environment.NewLine, global::System.StringComparison.CurrentCulture))
            {
                this.endsWithNewline = true;
            }
            // This is an optimization. If the current indent is "", then we don't have to do any
            // of the more complex stuff further down.
            if ((this.currentIndentField.Length == 0))
            {
                this.GenerationEnvironment.Append(textToAppend);
                return;
            }
            // Everywhere there is a newline in the text, add an indent after it
            textToAppend = textToAppend.Replace(global::System.Environment.NewLine, (global::System.Environment.NewLine + this.currentIndentField));
            // If the text ends with a newline, then we should strip off the indent added at the very end
            // because the appropriate indent will be added when the next time Write() is called
            if (this.endsWithNewline)
            {
                this.GenerationEnvironment.Append(textToAppend, 0, (textToAppend.Length - this.currentIndentField.Length));
            }
            else
            {
                this.GenerationEnvironment.Append(textToAppend);
            }
        }
        /// <summary>
        /// Write text directly into the generated output
        /// </summary>
        public void WriteLine(string textToAppend)
        {
            this.Write(textToAppend);
            this.GenerationEnvironment.AppendLine();
            this.endsWithNewline = true;
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void Write(string format, params object[] args)
        {
            this.Write(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Write formatted text directly into the generated output
        /// </summary>
        public void WriteLine(string format, params object[] args)
        {
            this.WriteLine(string.Format(global::System.Globalization.CultureInfo.CurrentCulture, format, args));
        }
        /// <summary>
        /// Raise an error
        /// </summary>
        public void Error(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Raise a warning
        /// </summary>
        public void Warning(string message)
        {
            System.CodeDom.Compiler.CompilerError error = new global::System.CodeDom.Compiler.CompilerError();
            error.ErrorText = message;
            error.IsWarning = true;
            this.Errors.Add(error);
        }
        /// <summary>
        /// Increase the indent
        /// </summary>
        public void PushIndent(string indent)
        {
            if ((indent == null))
            {
                throw new global::System.ArgumentNullException("indent");
            }
            this.currentIndentField = (this.currentIndentField + indent);
            this.indentLengths.Add(indent.Length);
        }
        /// <summary>
        /// Remove the last indent that was added with PushIndent
        /// </summary>
        public string PopIndent()
        {
            string returnValue = "";
            if ((this.indentLengths.Count > 0))
            {
                int indentLength = this.indentLengths[(this.indentLengths.Count - 1)];
                this.indentLengths.RemoveAt((this.indentLengths.Count - 1));
                if ((indentLength > 0))
                {
                    returnValue = this.currentIndentField.Substring((this.currentIndentField.Length - indentLength));
                    this.currentIndentField = this.currentIndentField.Remove((this.currentIndentField.Length - indentLength));
                }
            }
            return returnValue;
        }
        /// <summary>
        /// Remove any indentation
        /// </summary>
        public void ClearIndent()
        {
            this.indentLengths.Clear();
            this.currentIndentField = "";
        }
        #endregion
        #region ToString Helpers
        /// <summary>
        /// Utility class to produce culture-oriented representation of an object as a string.
        /// </summary>
        public class ToStringInstanceHelper
        {
            private System.IFormatProvider formatProviderField  = global::System.Globalization.CultureInfo.InvariantCulture;
            /// <summary>
            /// Gets or sets format provider to be used by ToStringWithCulture method.
            /// </summary>
            public System.IFormatProvider FormatProvider
            {
                get
                {
                    return this.formatProviderField ;
                }
                set
                {
                    if ((value != null))
                    {
                        this.formatProviderField  = value;
                    }
                }
            }
            /// <summary>
            /// This is called from the compile/run appdomain to convert objects within an expression block to a string
            /// </summary>
            public string ToStringWithCulture(object objectToConvert)
            {
                if ((objectToConvert == null))
                {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                System.Type t = objectToConvert.GetType();
                System.Reflection.MethodInfo method = t.GetMethod("ToString", new System.Type[] {
                            typeof(System.IFormatProvider)});
                if ((method == null))
                {
                    return objectToConvert.ToString();
                }
                else
                {
                    return ((string)(method.Invoke(objectToConvert, new object[] {
                                this.formatProviderField })));
                }
            }
        }
        private ToStringInstanceHelper toStringHelperField = new ToStringInstanceHelper();
        /// <summary>
        /// Helper to produce culture-oriented representation of an object as a string
        /// </summary>
        public ToStringInstanceHelper ToStringHelper
        {
            get
            {
                return this.toStringHelperField;
            }
        }
        #endregion
    }
    #endregion
}
