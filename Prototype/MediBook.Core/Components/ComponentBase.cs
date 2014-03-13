namespace MediBook.Client.Core.Components
{
    public class ComponentBase
    {
        public AppCore Core { get; set; }

        public ComponentBase(AppCore core)
        {
            this.Core = core;
        }
    }
}
