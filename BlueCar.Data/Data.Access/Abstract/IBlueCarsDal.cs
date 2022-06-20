using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlueCar_Api
{
    public interface IBlueCarsDal : IRepository<Cars, string>
    {

    }

    public interface IReservationsDal : IRepository<Reservations, string>
    {

    }

    public interface ICompanyDal : IRepository<Company, string>
    {

    }

    public interface IBlueCarsListDal: IRepository<ListClass, string>
    {

    }
    public interface IBlueCarsLocationDal : IRepository<Locations, string>
    {

    }
}
