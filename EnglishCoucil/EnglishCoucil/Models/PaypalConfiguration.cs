using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EnglishCoucil.Models
{
    public class PaypalConfiguration
    {
        public readonly static string clientId;
        public readonly static string clientSecret;

        static PaypalConfiguration()
        {
            var config = getConfig();
            clientId = "Af83ROYg7SAM0-41ekPx3WHcvIsH8U5PJTfk3avkOzJKjXh14TCHTGfox5-WyMDMvOf9vs3wsDNR0wZY";
            clientSecret = "EDWLm3hMsshhQ0Zo1e7bNar35GMmIL9SE4uI12r2RrVroR_fETQLSY8cF8BlGQqZUWcbmnvZgtZsOBdu";
        }

        private static Dictionary<string, string> getConfig()
        {
            return PayPal.Api.ConfigManager.Instance.GetProperties();
        }

        private static string getAccessToken()
        {
            string accessToken = new OAuthTokenCredential(clientId, clientSecret, getConfig()).GetAccessToken();
            return accessToken;
        }

        public static APIContext getAPIContext()
        {
            APIContext apiContext = new APIContext(getAccessToken());
            apiContext.Config = getConfig();
            return apiContext;
        }
    }
}