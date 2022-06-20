using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueCar.Payments;
using Microsoft.Extensions.Options;
using Iyzipay;
using Iyzipay.Model;
using Iyzipay.Request;
using NUnit.Framework;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Cors;

namespace BlueCar_Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IBlueCarsDal bluecarsDal;
        protected Iyzipay.Options options;

        private readonly IyzipaySettings iyzipaySettings;
        public PaymentController(IBlueCarsDal _bluecarsDal, IOptions<IyzipaySettings> _setting)
        {
            this.bluecarsDal = _bluecarsDal;
            this.iyzipaySettings = (IyzipaySettings)_setting.Value;
        }

        public class ThreedsInitialize : IyzipayResource
        {
            [JsonProperty(PropertyName = "threeDSHtmlContent")]
            public String HtmlContent { get; set; }

            public static ThreedsInitialize Create(CreatePaymentRequest request, Iyzipay.Options options)
            {
                ThreedsInitialize response = RestHttpClient.Create().Post<ThreedsInitialize>(options.BaseUrl + "/payment/3dsecure/initialize", GetHttpHeaders(request, options), request);

                if (response != null)
                {
                    response.HtmlContent = DigestHelper.DecodeString(response.HtmlContent);
                }
                return response;
            }
        }
        [HttpPost]
        [Route("payment3d/")]
        public string payment3d([FromQuery] Payments form)
        {
            int say = bluecarsDal.Get().Count() + 10000;
            string ad = form.ad;
            string soyad = form.soyad;
            string cardNumber = form.cardNumber;
            string cvv = form.cvv;
            string ay = form.ay;
            string yil = form.yil;
            decimal tutar = form.tutar;
            //
            string secure3d = "";
            options = new Iyzipay.Options(); 
            options.BaseUrl = iyzipaySettings.BaseUrl;
            options.ApiKey = iyzipaySettings.ApiKey;
            options.SecretKey = iyzipaySettings.SecretKey;

            CreatePaymentRequest request = new CreatePaymentRequest();
            request.Locale = Locale.TR.ToString();
            request.ConversationId = say.ToString();
            request.Price = tutar.ToString();
            request.PaidPrice = tutar.ToString();
            request.Currency = Currency.TRY.ToString();
            request.Installment = 1;
            request.BasketId = say.ToString();
            request.PaymentChannel = PaymentChannel.WEB.ToString();
            request.PaymentGroup = PaymentGroup.PRODUCT.ToString();
            request.CallbackUrl = "https://bluecar.sereryazilim.com" + "/basarili_rezervasyon" + "?res_no=" + say.ToString();

            PaymentCard paymentCard = new PaymentCard();
            paymentCard.CardHolderName = ad + " " + soyad;
            paymentCard.CardNumber = cardNumber;
            paymentCard.ExpireMonth = ay;
            paymentCard.ExpireYear = "20"+yil;
            paymentCard.Cvc = cvv;
            paymentCard.RegisterCard = 0;
            request.PaymentCard = paymentCard;

            Buyer buyer = new Buyer();
            buyer.Id = say.ToString();
            buyer.Name = ad;
            buyer.Surname = soyad;
            buyer.GsmNumber = "+905350000000";
            buyer.Email = "sererprogram@gmail.com";
            buyer.IdentityNumber = say.ToString();
            buyer.LastLoginDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            buyer.RegistrationDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss");
            buyer.RegistrationAddress = "İstanbul";
            buyer.Ip = "";
            buyer.City = "Istanbul";
            buyer.Country = "Turkey";
            buyer.ZipCode = "35000";
            request.Buyer = buyer;

            Address shippingAddress = new Address();
            shippingAddress.ContactName = ad + " " + soyad;
            shippingAddress.City = "Istanbul";
            shippingAddress.Country = "Turkey";
            shippingAddress.Description = "Adres";
            shippingAddress.ZipCode = "35000";
            request.ShippingAddress = shippingAddress;

            Address billingAddress = new Address();
            billingAddress.ContactName = ad+ " "+soyad;
            billingAddress.City = "Istanbul";
            billingAddress.Country = "Turkey";
            billingAddress.Description = "Adres";
            billingAddress.ZipCode = "35000";
            request.BillingAddress = billingAddress;

            List<BasketItem> basketItems = new List<BasketItem>();
            BasketItem firstBasketItem = new BasketItem();
            firstBasketItem.Id = say.ToString();
            firstBasketItem.Name = "BLUECAR";
            firstBasketItem.Category1 = "REZERVASYON";
            firstBasketItem.ItemType = BasketItemType.PHYSICAL.ToString();
            firstBasketItem.Price = tutar.ToString();
            basketItems.Add(firstBasketItem);

            request.BasketItems = basketItems;

            ThreedsInitialize threedsInitialize = ThreedsInitialize.Create(request, options);

            PrintResponse<ThreedsInitialize>(threedsInitialize);

            try
            {
                Assert.AreEqual(Status.SUCCESS.ToString(), threedsInitialize.Status);
                Assert.AreEqual(Locale.TR.ToString(), threedsInitialize.Locale);
                Assert.AreEqual(say.ToString(), threedsInitialize.ConversationId);
                Assert.IsNotNull(threedsInitialize.SystemTime);
                Assert.IsNull(threedsInitialize.ErrorCode);
                Assert.IsNull(threedsInitialize.ErrorMessage);
                Assert.IsNull(threedsInitialize.ErrorGroup);
                Assert.IsNotNull(threedsInitialize.HtmlContent);
            }
            catch
            {
                secure3d = "Lütfen Kart Bilgilerinizi Kontrol Edin!";
            }
       
            if (Status.SUCCESS.ToString() == "success")
            {
                secure3d = threedsInitialize.HtmlContent;
            }
            else
            {
                secure3d = threedsInitialize.ErrorMessage;
            }
          return secure3d;
        }

        [HttpPost]
        [Route("payment/")]
        public void payment(string resNo)
        {
            string paymentId = "";
            string conversationData = "";
            options.BaseUrl = iyzipaySettings.BaseUrl;
            options.ApiKey = iyzipaySettings.ApiKey;
            options.SecretKey = iyzipaySettings.SecretKey;
            try
                {
                    paymentId = Request.Form["paymentId"];
                    conversationData = Request.Form["conversationData"];
                }
                catch
                {
                }
                CreateThreedsPaymentRequest request = new CreateThreedsPaymentRequest();
                request.Locale = Locale.TR.ToString();
                request.ConversationId = resNo.Replace("BLUECAR_", "");
                request.PaymentId = paymentId;
                request.ConversationData = conversationData;
                ThreedsPayment threedsPayment = ThreedsPayment.Create(request, options);
                PrintResponse<ThreedsPayment>(threedsPayment);
                Assert.AreEqual(Status.SUCCESS.ToString(), threedsPayment.Status);
                Assert.AreEqual(Locale.TR.ToString(), threedsPayment.Locale);
                Assert.AreEqual(resNo, threedsPayment.ConversationId);
                Assert.IsNotNull(threedsPayment.SystemTime);
                Assert.IsNull(threedsPayment.ErrorCode);
                Assert.IsNull(threedsPayment.ErrorMessage);
                Assert.IsNull(threedsPayment.ErrorGroup);
                if (threedsPayment.Status == "success")
                {

                }
            }

        protected void PrintResponse<T>(T resource)
        {
            TraceListener consoleListener = new ConsoleTraceListener();
            Trace.Listeners.Add(consoleListener);
            Trace.WriteLine(JsonConvert.SerializeObject(resource, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            }));
        }
    }
}
