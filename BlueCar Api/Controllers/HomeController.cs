using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueCar.Payments;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace BlueCar_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly IBlueCarsDal bluecarsDal;
        private readonly IReservationsDal reservationsDal;
        private readonly ICompanyDal companyDal;
        private readonly IBlueCarsListDal bluecarslistDal;
        private readonly IBlueCarsLocationDal blueCarsLocationDal;
        private readonly IyzipaySettings iyzipaySettings;
        public HomeController(IBlueCarsDal _bluecarsDal, IReservationsDal _reservationsDal,ICompanyDal _companyDal, IBlueCarsListDal _bluecarslistDal,IBlueCarsLocationDal _blueCarsLocationDal, IOptions<IyzipaySettings> _setting)
        {
            this.bluecarsDal = _bluecarsDal;
            this.reservationsDal = _reservationsDal;
            this.companyDal = _companyDal;
            this.bluecarslistDal = _bluecarslistDal;
            this.blueCarsLocationDal = _blueCarsLocationDal;
            this.iyzipaySettings = (IyzipaySettings)_setting.Value;
        }

        [HttpPost]
        [Route("AddCar/")]
        public IActionResult AddABlueCar([FromQuery] Cars data)
        {
            var resultId = bluecarsDal.Get();
            data.carId = resultId.Count() + 1;
            var result = bluecarsDal.AddAsync(data).Result;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddReservation/")]
        public IActionResult AddReservation([FromQuery] Reservations data)
        {
            var resultId = reservationsDal.Get();
            data.resId = resultId.Count() + 1;
            var result = reservationsDal.AddAsync(data).Result;
            return Ok(result);
        }

        [HttpPost]
        [Route("AddCompany/")]
        public IActionResult AddCompany([FromQuery] Company data)
        {
            var resultId = companyDal.Get();
            data.companyId = resultId.Count() + 1;
            var result = companyDal.AddAsync(data).Result;
            return Ok(result);
        }

        [HttpGet]
        [Route("BlueCars/")]
        public List<ListClass> Get([FromQuery] ReservationQuery query)
        {
            BlueCar.Engine.BlueCarsEngine cars = new BlueCar.Engine.BlueCarsEngine(bluecarsDal,bluecarslistDal,blueCarsLocationDal);
            List<ListClass> result = cars.carList(query);
            return result;
        }

        [HttpPost]
        [Route("AddLocation/")]
        public IActionResult AddLocation([FromQuery] Locations data)
        {
            var resultId = blueCarsLocationDal.Get();
            data.locationId = resultId.Count() + 1;
            var result = blueCarsLocationDal.AddAsync(data).Result;
            return Ok(result);
        }

        [HttpGet]
        [Route("GetLocations/")]
        public String GetLocations([FromQuery] GetLocations data)
        {
            var AllLocation = blueCarsLocationDal.Get();
            var alisyeri = AllLocation.Where(x => x.locationId == Convert.ToInt32(data.alisyeri)).Single();
            var donusyeri = AllLocation.Where(x => x.locationId == Convert.ToInt32(data.alisyeri)).Single();
            GetLocations bsObj = new GetLocations()
            {
                alisyeri = alisyeri.branch.ToString(),
                donusyeri = donusyeri.branch.ToString()
            };
            var customerJson = JsonConvert.SerializeObject(bsObj);
            return customerJson.ToString();
        }

        [HttpGet]
        [Route("AllLocations/")]
        public IActionResult Get()
        {
            var result = blueCarsLocationDal.Get();
            if (result == null)
                return BadRequest("Bad Request");
            return Ok(result);
        }
    }
}
