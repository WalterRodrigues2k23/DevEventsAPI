using DevEventsAPI.Entities;

namespace DevEventsAPI.Persistense
{
    public class DevEventsDbContext
    {
        public List<DevEvent> DevEvents { get; set; }

        public DevEventsDbContext()
        {
            DevEvents = new List<DevEvent>(); 
        }
    }
}
