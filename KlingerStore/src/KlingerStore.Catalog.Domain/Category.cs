using KlingerStore.Core.Domain.DomainObjects;

namespace KlingerStore.Catalog.Domain
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        public  int Code { get; set; }

        public Category(string name, int code)
        {
            Name = name;
            Code = code;
        }

        public override string ToString()
        {
            return $"{Name} - {Code}";
        }
    }
}
