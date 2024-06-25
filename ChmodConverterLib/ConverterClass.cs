using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ChmodConverter
{
    public static class ChmodConverter
    {
        public static int ToNumeric(string str)
        {
            if (str.Length != 10)
            {
                throw new FormatException("the specified string must have a length of 10");
            }
            char type = str[0];
            string user = str.Substring(1, 3);
            string group = str.Substring(4, 3);
            string others = str.Substring(7, 3);
            string result = string.Empty;
            List<string> strings = new List<string>(new string[] { user, group, others });
            foreach (var s in strings)
            {
                short perm = 0;
                if ((s[0] != 'r' && s[0] != '-') || (s[1] != 'w' && s[1] != '-') || (s[2] != 'x' && s[2] != '-'))
                {
                    throw new FormatException("string must be in linux permission format, e.g. drwxr-x--x");
                }
                if (s[0] == 'r')
                {
                    perm += 4;
                }
                if (s[1] == 'w')
                {
                    perm += 2;
                }
                if (s[2] == 'x')
                {
                    perm++;
                }
                result += perm.ToString();
            }
            return int.Parse(result);
        }
        public static string ToSymbolic(short input)
        {
            string inputstr = input.ToString();
            string result = "-";
            while (inputstr.Length < 3)
            {
                inputstr = '0' + inputstr;
            }
            if (inputstr.Length > 3)
            {
                throw new FormatException("a three-digit number is required");
            }
            foreach (var c in inputstr)
            {
                string binary = Convert.ToString(c, 2);
                binary = binary.Substring(binary.Length - 3);
                for (var i = 0; i < binary.Length; i++)
                {
                    if (binary[i] == '1')
                    {
                        switch (i)
                        {
                            case 0:
                                result += 'r';
                                break;
                            case 1:
                                result += "w";
                                break;
                            case 2:
                                result += "x";
                                break;
                            default:
                                result += '-';
                                break;
                        }
                    }
                    else result += '-';
                }
            }
            return result;
        }
    }
}
