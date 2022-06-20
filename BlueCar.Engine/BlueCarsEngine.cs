using BlueCar_Api;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueCar.Engine
{
    public class BlueCarsEngine
    {
        private readonly IBlueCarsDal bluecarsDal;
        private readonly IBlueCarsListDal bluecarslistDal;
        private readonly IBlueCarsLocationDal bluelocationDal;
        public BlueCarsEngine(IBlueCarsDal _bluecarsDal,IBlueCarsListDal _bluecarslistDal,IBlueCarsLocationDal _blueCarsLocationDal)
        {
            this.bluecarsDal = _bluecarsDal;
            this.bluecarslistDal = _bluecarslistDal;
            this.bluelocationDal = _blueCarsLocationDal;
        }
        List<ListClass> list = new List<ListClass>();
        List<ListClass> pricestatus = new List<ListClass>();
        public List<ListClass> carList(ReservationQuery form)
        {
            int gunfark = gunfarks(form);
            var AllCars = bluecarsDal.Get();
            var AllLocation = bluelocationDal.Get();
            var alisyeri = AllLocation.Where(x => x.locationId == Convert.ToInt32(form.alisyeri)).Single();
            var donusyeri = AllLocation.Where(x => x.locationId == Convert.ToInt32(form.donusyeri)).Single();
            foreach (var result in AllCars)
            {
                list.Add(new ListClass
                {
                    aracmarka = result.aracmarka,
                    aracmodel = result.aracmodel,
                    aracresim = result.aracresim,
                    depozito = result.depozito,
                    gunluk_km = result.gunluk_km,
                    yolcu = result.yolcu,
                    bagaj = result.bagaj,
                    kapi = result.kapi,
                    segment = result.segment,
                    kasatip = result.kasatip,
                    gunluk_fiyat = selectfiyat(gunfark, result.carId),
                    tedarikci_firma = result.firma,
                    vites = result.vites,
                    yakit = result.yakit,
                    gun = gunfark,
                    toplam_fiyat = selectfiyat(gunfark, result.carId) * gunfark,
                    alisyeri = alisyeri.branch,
                    donusyeri = donusyeri.branch
                }); ;
            }
            if (form.price == "low")
                pricestatus = list.OrderBy(x => x.toplam_fiyat).ToList();
            else if (form.price == "top")
                pricestatus = list.OrderByDescending(x => x.toplam_fiyat).ToList();
            return pricestatus.ToList();
        }

        private int gunfarks (ReservationQuery form)
        {
            String[] alistarihi = form.alistarih.Split('.');
            String[] donustarihi = form.donustarih.Split('.');
            //
            DateTime veris = new DateTime(Convert.ToInt32(donustarihi[2]), Convert.ToInt32(donustarihi[1]), Convert.ToInt32(donustarihi[0]), Convert.ToInt32(form.donussaat), Convert.ToInt32(form.donusdakika), 00);
            DateTime alis = new DateTime(Convert.ToInt32(alistarihi[2]), Convert.ToInt32(alistarihi[1]), Convert.ToInt32(alistarihi[0]), Convert.ToInt32(form.alissaat), Convert.ToInt32(form.alisdakika), 00);
            double daysUntilChristmas = veris.Subtract(alis).TotalDays;
            TimeSpan kalangun = veris - alis;
            double gunfarki = kalangun.TotalDays;
            int gunfark = Convert.ToInt32(Math.Ceiling(gunfarki));
            return gunfark;
        }

        private decimal selectfiyat(int gunfark, int carId)
        {
            decimal gunluk_fiyat = 0;
            var AllCars = bluecarsDal.Get();
            var filter = AllCars.Where(x => x.carId == carId).Single();
            if (gunfark == 3 || gunfark < 3)
            {
                gunluk_fiyat = filter.uc;
            }
            else if (gunfark >= 4 && gunfark <= 7)
            {
                gunluk_fiyat = filter.dort_yedi;
            }
            else if (gunfark >= 8 && gunfark <= 15)
            {
                gunluk_fiyat = filter.sekiz_onbes;
            }
            else if (gunfark >= 16 && gunfark <= 29)
            {
                gunluk_fiyat = filter.onalti_yirmidokuz;
            }
            else if (gunfark >= 30)
            {
                gunluk_fiyat = filter.otuzust;
            }
            return gunluk_fiyat;
        }
    }
}
