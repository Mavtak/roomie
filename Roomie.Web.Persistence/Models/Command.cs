namespace Roomie.Web.Persistence.Models
{
    public class Command
    {
        public Argument[] Arguments { get; private set; }
        public string Description { get; private set; }
        public string Group { get; private set; }
        public string Name { get; private set; }

        public Command(Argument[] arguments, string description, string group, string name)
        {
            Arguments = arguments;
            Description = description;
            Group = group;
            Name = name;
        }

        public class Argument
        {
            public string Name { get; private set; }
            public string DefaultValue { get; private set; }
            public bool HasDefaultValue { get; private set; }
            public TypeParameter Type { get; private set; }

            public Argument(string name, string defaultValue, bool hasDefaultValue, TypeParameter type)
            {
                Name = name;
                DefaultValue = defaultValue;
                HasDefaultValue = hasDefaultValue;
                Type = type;
            }

            public class TypeParameter
            {
                public string Description { get; set; }
                public string Name { get; set; }

                public TypeParameter(string description, string name)
                {
                    Description = description;
                    Name = name;
                }
            }
        }
    }
}
