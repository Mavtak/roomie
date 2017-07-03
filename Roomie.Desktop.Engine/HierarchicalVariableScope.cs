using Roomie.Desktop.Engine.Exceptions;
using Roomie.Desktop.Engine.Parameters;

namespace Roomie.Desktop.Engine
{
    public class HierarchicalVariableScope
    {
        public HierarchicalVariableScope Parent { get; private set; }
        public LocalVariableScope Local { get; private set; }

        public HierarchicalVariableScope(HierarchicalVariableScope parent = null)
        {
            Parent = parent;
            Local = new LocalVariableScope();
        }

        public HierarchicalVariableScope CreateLowerScope()
        {
            return new HierarchicalVariableScope(this);
        }

        public bool ContainsVariable(string name)
        {
            var currentScope = this;

            while (currentScope != null)
            {
                if (currentScope.Local.ContainsVariable(name))
                {
                    return true;
                }

                currentScope = currentScope.Parent;
            }

            return false;
        }

        public void SetVariable(string name, string value)
        {
            lock (this)
            {
                var variable = TryGetVariable(name);

                if (variable != null)
                {
                    variable.Update(value);
                }
                else
                {
                    Local.DeclareVariable(name, value);
                }
            }
        }

        public VariableParameter TryGetVariable(string name)
        {
            lock (this)
            {
                var currentScope = this;

                while (currentScope != null)
                {
                    var variable = currentScope.Local.TryGetVariable(name);

                    if (variable != null)
                    {
                        return variable;
                    }

                    currentScope = currentScope.Parent;
                }

                return null;
            }
        }

        public VariableParameter GetVariable(string name)
        {
            var result = TryGetVariable(name);

            if(result == null)
            {
                throw new VariableException("Variable " + name + " not set");
            }

            return result;
        }
    }
}
