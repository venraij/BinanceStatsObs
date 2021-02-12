using System;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Controller
{
    public static class BinanceEndpoints
    {
        private static string apiUri = "https://fapi.binance.com"; // Futures API base url of Binance
        private static string recvWindow = "5000";

        // The binance API key
        public static string apiKey
        {
            get;
            set;
        }

        // The Binance API secret
        public static string secretKey
        {
            get;
            set;
        }

        // Grabs all the the futures account info of the account associated with the Api key and Secret key
        public static void FuturesAccountInformation()
        {
            string uri = apiUri + "/fapi/v2/account?";

            string timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString();
            string parameters = $"recvWindow={recvWindow}&timestamp={timestamp}";
            string signature = HashEncode(HashHMAC(StringEncode(secretKey), StringEncode(parameters)));

            // Create the full URI for the request
            uri = $"{uri + parameters}&signature={signature}";

            dynamic response = JObject.Parse(Request.Get(uri, apiKey));

            DirectoryCheck();

            // Write the received data to sepereate TXT files
            File.WriteAllText(@"C:\BinanceStats\Total_PNL.txt", Convert.ToString(response.totalCrossUnPnl));
            File.WriteAllText(@"C:\BinanceStats\Total_Balance.txt", Convert.ToString(response.totalWalletBalance));
            File.WriteAllText(@"C:\BinanceStats\Available_Balance.txt", Convert.ToString(response.availableBalance));
            File.WriteAllText(@"C:\BinanceStats\Total_Maintenance_Margin.txt", Convert.ToString(response.totalMaintMargin));
            File.WriteAllText(@"C:\BinanceStats\Total_Unrealized_Profit.txt", Convert.ToString(response.totalUnrealizedProfit));
            File.WriteAllText(@"C:\BinanceStats\Total_Margin_Balance.txt", Convert.ToString(response.totalMarginBalance));
            File.WriteAllText(@"C:\BinanceStats\Total_Cross_Wallet_Balance.txt", Convert.ToString(response.totalCrossWalletBalance));
            File.WriteAllText(@"C:\BinanceStats\Total_Position_Initial_Margin.txt", Convert.ToString(response.totalPositionInitialMargin));
            File.WriteAllText(@"C:\BinanceStats\Total_Open_Order_Initial_Margin.txt", Convert.ToString(response.totalOpenOrderInitialMargin));
        }

        // Check is if the BinanceStats directory exists
        public static void DirectoryCheck()
        {
            if (!System.IO.Directory.Exists(@"C:\BinanceStats\"))
            {
                System.IO.Directory.CreateDirectory(@"C:\BinanceStats\");
            }
        }

        // Hashes the specifed key and message in HMAC SHA256
        private static byte[] HashHMAC(byte[] key, byte[] message)
        {           
            var hash = new HMACSHA256(key);
            return hash.ComputeHash(message);
        }

        // Encodes a string to ASCII
        private static byte[] StringEncode(string text)
        {
            var encoding = new ASCIIEncoding();
            return encoding.GetBytes(text);
        }

        // Encodes a hash to a string
        private static string HashEncode(byte[] hash)
        {
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }
}
