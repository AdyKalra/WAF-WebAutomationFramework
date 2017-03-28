using System;
using System.Linq;

namespace WAF.Framework.HelperClasses
{
    public class NameGenerator
    {
        private static Random random = new Random();
        public static string RandomName(int length)
        {
            const string _chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            return new string(Enumerable.Repeat(_chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string WithSpecialCharacters(int length)
        {
            const string _chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            const string _specialChars = "!@#$%^&*&^%$#@!@#$%^&*&^%$#@^";
            return new string(Enumerable.Repeat(_chars + _specialChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string WithNumbers(int length)
        {
            const string _chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            const string _numbers = "01234567898765432123456789";
            return new string(Enumerable.Repeat(_chars + _numbers, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string WithCombination(int length)
        {
            const string _chars = "AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz";
            const string _numbers = "01234567898765432123456789";
            const string _specialChars = "!@#$%^&*&^%$#@!@#$%^&*&^%$#@^";
            return new string(Enumerable.Repeat(_chars + _numbers + _specialChars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        // To make random email use this code: NameGenerator.RandomName(8) + "@mail.com"
    }
}
