using System;

namespace BlueCar_Api
{
    public class Company : MongoDbEntity
    {
        public int companyId { get; set; }

        public string firmaAd� { get; set; }
        
        public string logourl { get; set; }
    }
}
