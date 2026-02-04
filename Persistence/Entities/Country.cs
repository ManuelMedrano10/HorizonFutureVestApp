namespace Persistence.Entities
{
    public class Country
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string IsoCode { get; set; }
        //Navigation property
        public ICollection<IndicatorByCountry>? IndicatorsByCountries { get; set; }
    }
}
