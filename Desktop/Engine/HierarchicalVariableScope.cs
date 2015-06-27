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
            if (Local.ContainsVariable(name))
            {
                return true;
            }

            if (Parent == null)
            {
                return false;
            }

            return Parent.ContainsVariable(name);
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
                var variable = Local.TryGetVariable(name);

                if (variable != null)
                {
                    return variable;
                }

                if (Parent == null)
                {
                    return null;
                }

                return Parent.TryGetVariable(name);
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
