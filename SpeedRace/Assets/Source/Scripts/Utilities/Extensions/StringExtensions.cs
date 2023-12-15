using System.Linq;

namespace UnityTools.Extentions
{
    public static class StringExtensions
    {
        public static string Inverse(this string str)
        {
            return new string(str.Reverse().ToArray());
        }
    }
}