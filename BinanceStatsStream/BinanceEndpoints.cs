using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Newtonsoft.Json.Linq;

namespace View
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

        public static void FuturesAccountInformation()
        {
            string uri = apiUri + "/fapi/v2/account?";

            string timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string parameters = $"recvWindow={recvWindow}&timestamp={timestamp}";
            string signature = HashEncode(HashHMAC(StringEncode(secretKey), StringEncode(parameters)));

            uri = $"{uri + parameters}&signature={signature}";

            //var jsonObj = JsonSerializer.Serialize(Request.Get(uri, apiKey), new JsonSerializerOptions{ WriteIndented = true });
            dynamic response = JObject.Parse(Request.Get(uri, apiKey));

            DirectoryCheck();

            File.WriteAllText(@"C:\BinanceStats\Total_PNL.txt", Convert.ToString(response.totalCrossUnPnl));
            File.WriteAllText(@"C:\BinanceStats\Total_Balance.txt", Convert.ToString(response.totalWalletBalance));
        }

        private static void DirectoryCheck()
        {
            if (!System.IO.Directory.Exists(@"C:\BinanceStats\"))
            {
                System.IO.Directory.CreateDirectory(@"C:\BinanceStats\");
            }
        }

        private static byte[] HashHMAC(byte[] key, byte[] message)
        {           
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }

        private static void writeToFile()
        {

        }
    }
}
