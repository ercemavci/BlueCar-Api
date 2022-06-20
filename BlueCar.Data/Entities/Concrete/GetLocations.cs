using System;

namespace BlueCar_Api
{
    public class Locations : MongoDbEntity
    {
        public int locationId { get; set; }
        public string branch { get; set; }
        public string adres { get; set; }
        public string telefon { get; set; }
        public string mail { get; set; }
        public string delivery_type { get; set; }
    }
}
