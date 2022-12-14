using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ItemsAPI
{
    public class ListConverter
    {
        public static string ListToString(List<string> datalist)
        {
            string datastring = "";
            foreach(string val in datalist)
            {
                string fixedVal = val;

                //$ is used as a delimeter, so no $ in attendees list
                while (fixedVal.Contains('$'))
                {
                    fixedVal = fixedVal.Remove(fixedVal.IndexOf('$'), 1);
                }

                datastring += fixedVal.ToString();
                datastring += '$';
            }
            return datastring;
        } 
        public static List<string> StringToList(string datastring)
        {
            List<string> datalist = new List<string>();
            string currentstring = "";
            foreach(char c in datastring)
            {
                if(c == '$')
                {
                    if(currentstring.Length > 0)
                    {
                        datalist.Add(currentstring);
                    }
                    currentstring = "";
                }
                else
                {
                    currentstring += c;
                }
            }
            return datalist;
        }
    }
}
