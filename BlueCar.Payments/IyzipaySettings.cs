using System;
using System.Collections.Generic;
using System.Text;

namespace BlueCar.Payments
{
    public class IyzipaySettings
    {
        public string BaseUrl { get; set; }
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }

        #region Const Values

        public const string BaseUrlValue = nameof(BaseUrl);
        public const string ApiKeyValue = nameof(ApiKey);
        public const string SecretKeyValue = nameof(SecretKey);

        #endregion
    }
}
