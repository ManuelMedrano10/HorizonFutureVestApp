namespace Persistence.Entities
{
    public class MacroindicatorSimulator : Macroindicator
    {
        public required int MacroindicatorId { get; set; }
        public Macroindicator? MacroindicatorType { get; set; }
    }
}
