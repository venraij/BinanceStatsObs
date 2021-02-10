using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Controller
{
    public static class BinanceEndpoints
    {
        private static string apiUri = "https://fapi.binance.com";
        private static string recvWindow = "5000";

        public static string apiKey
        {
            get;
            set;
        }

        public static string secretKey
        {
            get;
            set;
        }

        public static void FuturesAccountBalance()
        {
            string uri = apiUri + "/fapi/v2/balance?";

            DateTime baseDate = new DateTime(1970, 1, 1);
            TimeSpan diff = DateTime.Now - baseDate;

            string timestamp = diff.TotalMilliseconds.ToString();
            string parameters = $"recvWindow={recvWindow}&{timestamp}";
            string signature = System.Text.Encoding.UTF8.GetString(HashHMAC(Encoding.ASCII.GetBytes(secretKey), Encoding.ASCII.GetBytes(parameters)));

            uri = $"{uri + parameters}&signature={signature}";

            Console.WriteLine(Request.Get(uri, apiKey));
        }

        private static byte[] HashHMAC(byte[] key, byte[] message)
        {
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        private static void writeToFile()
        {

        }
    }
}
