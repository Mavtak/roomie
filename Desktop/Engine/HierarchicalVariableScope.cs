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

        public void DeclareOrUpdateVariable(string name, string value)
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
                    Local.DeclareLocalVariable(name, value);
                }
            }
        }

        public VariableParameter TryGetVariable(string name)
        {
            lock (this)
            {
                var variable = Local.TryGetLocalVariable(name);

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

        public IParameter ReadParameter(string name)
        {
            var variable = GetVariable(name);
            var result = variable.Interpolate(this);

            return result;
        }

        public bool ContainsVariable(string name)
        {
            if (Local.ContainsLocalVariable(name))
            {
                return true;
            }

            if (Parent == null)
            {
                return false;
            }

            return Parent.ContainsVariable(name);
        }
    }
}
