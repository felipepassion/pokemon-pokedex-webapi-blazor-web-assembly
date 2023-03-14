using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Tynamix.ObjectFiller;

namespace Pokemon.Api.Utils
{

    public static class RandomGenerator
    {
        public static T[] GenerateMultiplesResponses<T>()
            where T : class
        {
            var entries = new List<T>();
            var count = CreateNumber(2, 8);

            for (int i = 0; i < count; i++)
            {
                var filler = new Filler<T>();
                entries.Add(filler.Create());
            }

            return entries.ToArray();
        }
        public static string CreateString()
        {
            var length = CreateNumber(16, 64);
            return new MnemonicString(1, length, length).GetValue();
        }

        public static string CreateNumberString()
        {
            var length = CreateNumber(16, 64);
            return new MnemonicString(1, length, length).GetValue();
        }

        public static string[] CreateNumberStringCollection(int lengthMin = 1, int lengthMax = 10, int min = 1, int max = 10, params string[] numbersToAvoid)
        {
            var result = new List<string>();
            for (int i = 0; i < CreateNumber(min, max); i++)
            {
                result.Add(CreateNumberString(CreateNumber(lengthMin, lengthMax, numbersToAvoid)));
            }
            return result.ToArray();
        }

        public static string[] CreateStringCollection(int lengthMin = 16, int lengthMax = 64, int min = 1, int max = 10, List<string> strToAvoid = null)
        {
            return CreateStringCollection(lengthMin, lengthMax, min, max, strToAvoid?.ToArray());
        }

        public static string[] CreateStringCollection(int lengthMin = 16, int lengthMax = 64, int min = 1, int max = 10, params string[] strToAvoid)
        {

            var result = new List<string>();

            for (int i = 0; i < CreateNumber(min, max); i++)
            {

                var length = CreateNumber(lengthMin, lengthMax);
                string text = RandomizeString(length, strToAvoid);

                result.Add(text);
            }

            return result.ToArray();
        }

        private static string RandomizeString(int length, params string[] strToAvoid)
        {
            var result = new MnemonicString(1, length, length).GetValue();
            if (strToAvoid?.Contains(result) == true)
                return RandomizeString(length, strToAvoid);
            return result;
        }

        public static string[][] CreateMultidimensionalStringCollection(int min = 1, int max = 10)
        {

            var result = new string[CreateNumber(min, max)][];

            for (int i = 0; i < result.Length; i++)
            {

                result[i] = new string[CreateNumber(min, max)];

                for (int j = 0; j < result[i].Length; j++)
                {
                    var length = CreateNumber(16, 64);
                    var text = new MnemonicString(1, length, length).GetValue();
                }
            }

            return result;
        }

        public static string CreateString(int length)
        {
            return new MnemonicString(1, length, length).GetValue();
        }

        public static string CreateNumberString(int length)
        {
            return string.Join("", CreateNumberCollection(min: length, max: length));
        }

        public static bool CreateBool()
        {
            return Randomizer<bool>.Create();
        }

        public static int CreateNumber(int min = 0, int max = 10, params string[] numbersToAvoid)
        {
            try
            {
                var result = new IntRange(min, max).GetValue();
                if (numbersToAvoid?.Any(x => x.ToString() == result.ToString()) == true)
                    return CreateNumber(min, max, numbersToAvoid);
                return result;

            }
            catch (Exception e)
            {

                throw;
            }
        }

        public static int[] CreateNumberCollection(int lengthMin = 0, int lengthMax = 10, int min = 1, int max = 10, bool avoidRepeat = false, params string[] numbersToAvoid)
        {

            var result = new List<int>();
            int i = 0;
            int repeatUntil = CreateNumber(min, max);

            while (i < repeatUntil)
            {
                var number = CreateNumber(min: lengthMin, max: lengthMax, numbersToAvoid);

                if (avoidRepeat == true && result.Contains(number)) continue;

                result.Add(number);
                i++;
            }

            return result.ToArray();
        }

        public static string[] CreateNumberCollectionAsString(int lengthMin = 0, int lengthMax = 10, int min = 1, int max = 10, bool avoidRepeat = false, List<string> numbersToAvoid = null)
        {
            return CreateNumberCollectionAsString(lengthMin, lengthMax, min, max, avoidRepeat, numbersToAvoid?.ToArray());
        }
        public static string[] CreateNumberCollectionAsString(int lengthMin = 0, int lengthMax = 10, int min = 1, int max = 10, bool avoidRepeat = false, params string[] numbersToAvoid)
        {
            return CreateNumberCollection(lengthMin, lengthMax, min, max, avoidRepeat, numbersToAvoid).Select(x => x.ToString()).ToArray();
        }

        public static DateTime CreateDateTime()
        {
            return new DateTimeRange(DateTime.MinValue, DateTime.MaxValue).GetValue();
        }

        public static DateTime CreateDateTime(DateTime min, DateTime max)
        {
            return new DateTimeRange(min, max).GetValue();
        }

        public static string CreateCulture(CultureTypes types = CultureTypes.SpecificCultures)
        {

            var cultures = CultureInfo.GetCultures(types);
            var names = cultures.Select(p => p.Name);

            return new RandomListItem<string>(names).GetValue();
        }

        public static T CreateEnum<T>() where T : struct
        {

            var values = Enum.GetNames(typeof(T));
            var value = new RandomListItem<string>(values).GetValue();

            return (T)Enum.Parse(typeof(T), value);
        }

        public static Dictionary<TKey, TValue> CreateDictionary<TKey, TValue>()
        {
            try
            {
                return new Filler<Dictionary<TKey, TValue>>().Create();
            }
            catch (Exception)
            {
                return new Dictionary<TKey, TValue>();
            }
        }

        public static T GetValue<T>(params T[] options)
        {
            return new RandomListItem<T>(options).GetValue();
        }
    }
}
