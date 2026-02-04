namespace Persistence.Entities
{
    public class IndicatorByCountry
    {
        public required int Id { get; set; }
        public required decimal IndicatorValue { get; set; }
        public required int Year { get; set; }
        public required int CountryId { get; set; } //FK
        public Country? Country { get; set; } //Navigation Property
        public required int MacroindicatorId { get; set; } //FK
        public Macroindicator? Macroindicator{ get; set; } //Navigation Property
    }
}
