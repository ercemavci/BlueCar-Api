using BlueCar_Api;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueCar.Data.Data.Access.Concrete
{
   public class CarsMongoDbDal : MongoDbRepositoryBase<Cars>, IBlueCarsDal
    {
        public CarsMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }

    public class ReservationsMongoDbDal : MongoDbRepositoryBase<Reservations>, IReservationsDal
    {
        public ReservationsMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }

    public class CompanyMongoDbDal : MongoDbRepositoryBase<Company>, ICompanyDal
    {
        public CompanyMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
    public class carListMongoDbDal : MongoDbRepositoryBase<ListClass>, IBlueCarsListDal
    {
        public carListMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
    public class LocationMongoDbDal : MongoDbRepositoryBase<Locations>, IBlueCarsLocationDal
    {
        public LocationMongoDbDal(IOptions<MongoDbSettings> options) : base(options)
        {
        }
    }
}
