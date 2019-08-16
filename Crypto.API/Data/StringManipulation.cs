using System;
using System.Collections.Generic;

namespace Crypto.API.Data
{
    internal static class StringManipulation
    {
        
         internal static List<int> AllIndexesOf(this string str, string value) 
        {
                if (String.IsNullOrEmpty(value))
                    throw new ArgumentException("the string to find may not be empty", "value");
                List<int> indexes = new List<int>();
                for (int index = 0;; index += value.Length) {
                    index = str.IndexOf(value, index);
                    if (index == -1)
                        return indexes;
                    indexes.Add(index);
                }
        }


        internal static string getBetween(string strSource, string strStart, string strEnd)
        {
            int Start, End;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                return strSource.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

        public static string Reverse( string s )
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse( charArray );
            return new string( charArray );
        }

    }
  
}