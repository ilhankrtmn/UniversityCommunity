using Newtonsoft.Json;
using System.Globalization;

namespace UniversityCommunity.Data
{
    public class Functions
    {

        public static string CleanMsisdn(string msisdn)
        {
            if (msisdn.StartsWith("0")) msisdn = msisdn.Substring(1);
            if (msisdn.StartsWith("90")) msisdn = msisdn.Substring(2);
            msisdn = msisdn.Replace(" ", "");
            msisdn = msisdn.Replace("(", "").Replace(")", "").Replace("+90", "").Trim();
            return msisdn;
        }

        public static double RandomNumber()
        {
            Random random = new Random();
            return random.NextDouble();
        }

        public static string GetResponseCodeMessageDto(int responseCode, string message)
        {
            return JsonConvert.SerializeObject(
                    new
                    {
                        ResponseCode = responseCode,
                        Message = message
                    }, Formatting.None, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore
                    }
                );
        }

        public static string GetResponseCodeMessageDto(dynamic data)
        {
            return JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }
        public static string GetResponseCodeMessageArrayDto(int responseCode, string message)
        {
            var obj = new
            {
                ResponseCode = responseCode,
                Message = message
            };
            return JsonConvert.SerializeObject(new object[]
            {
                   obj
            }, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static string GetResponseCodeMessageArrayDto(dynamic data)
        {

            return JsonConvert.SerializeObject(new object[]
            {
                   data
            }, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
        }

        public static T IsNull<T>(T v1, T defaultValue)
        {
            return v1 == null ? defaultValue : v1;
        }

        public static int GetWeekNumber(DateTime dateTime)
        {
            CultureInfo culture = CultureInfo.CurrentCulture;
            int weekNumber = culture.Calendar.GetWeekOfYear(dateTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            return weekNumber;
        }

        public static List<string> GetStringSplit(string str, string splitChar)
        {
            List<string> splitList = new List<string>();

            if (!string.IsNullOrEmpty(str))
            {
                if (!str.EndsWith(splitChar))
                {
                    str += splitChar;
                }

                string tempWord = "";

                foreach (char c in str)
                {
                    if (c != Convert.ToChar(splitChar))
                    {
                        tempWord += c;
                    }
                    else
                    {
                        tempWord = tempWord.Trim();
                        splitList.Add(tempWord);
                        tempWord = "";
                    }
                }
            }

            return splitList;
        }
    }
}