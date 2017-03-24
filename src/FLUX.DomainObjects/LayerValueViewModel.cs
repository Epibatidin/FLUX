namespace FLUX.DomainObjects
{
    public class LayerValueViewModel
    {
        public string Value { get; private set; }
        public bool[] ColorCodeActiveFlags { get; private set; }

        public LayerValueViewModel(int count, string value)
        {
            ColorCodeActiveFlags = new bool[count];
            Value = value;
        }
    }
}
