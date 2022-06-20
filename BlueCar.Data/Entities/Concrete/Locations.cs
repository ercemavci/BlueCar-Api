using System;

namespace BlueCar_Api
{
    public class GetLocations : MongoDbEntity
    {
        public string alisyeri { get; set; }
        public string donusyeri { get; set; }
    }
}
