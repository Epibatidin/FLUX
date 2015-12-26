namespace DataAccess.Interfaces
{
    public class VirtualFileFactoryContext
    {
        public string SelectedSource { get; set; }

        public string[] OverrideRootnames { get; set; }
        public int[] SubRoots { get; set; }
    }
}
