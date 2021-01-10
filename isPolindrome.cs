///
/// Name: isPolindrome          
/// Date: 08.01.2021         
/// Description: Function returns input string is Polindrome or not
///

static string isPolindrome(String str)
        {
            string checkStr;

            str = str.ToLower();

            if (!string.IsNullOrEmpty(str))
            {
                // Reverse order elements of array
                checkStr = new string(str.Reverse().ToArray());
                if (str == checkStr)
                {
                    return str + " is Polindrome";
                }
                else
                {
                    return str + " is not Polindrome";
                }
            }
           return "Wrong value";
        }




