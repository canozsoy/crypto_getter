namespace Catalog.UtilFunctions {

    public static class UtilClass {
        public static (string, string) formatStrings (string unformattedString) {
            string binanceString = unformattedString.ToUpper();
            string huobiString = unformattedString.ToLower();
            return (binanceString, huobiString);
        }
        
        public async static Task<string> makeNetworkCall(string url) {
            var client = new HttpClient();
            var response = await client.GetStringAsync(url);
            return response;
        }
    }   
}