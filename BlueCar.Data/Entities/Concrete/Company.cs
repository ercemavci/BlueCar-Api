using System;

namespace BlueCar_Api
{
    public class Company : MongoDbEntity
    {
        public int companyId { get; set; }

        public string firmaAdý { get; set; }
        
        public string logourl { get; set; }
    }
}
