using System;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace RmatchWebParsTester
{
    public class Pars
    {
       public static string match(string input,string pattern)
        {

            
           string value = Regex.Match(input, pattern).Groups[1].Value;


           return value;
        }
    }
}