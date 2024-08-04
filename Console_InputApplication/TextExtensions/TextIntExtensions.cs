using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Расширения с целочисленными данными
/// </summary>
public static class TextIntExtensions
{
    public static bool IsPhoneNumber(this string message)    
    {
        if (message.Length != "7-904-334-1124".Length)
        {
            return false;
        }
        else
        {

            if (message[1] != '-' || message[5] != '-' || message[9] != '-')
            {
                return false;
            }
            else
            {
                if (isNumber(message[0]) == false ||
                    isNumber(message[2]) == false || isNumber(message[3]) == false || isNumber(message[4]) == false ||
                    isNumber(message[6]) == false || isNumber(message[7]) == false || isNumber(message[8]) == false ||
                    isNumber(message[10]) == false || isNumber(message[11]) == false ||
                    isNumber(message[12]) == false || isNumber(message[13]) == false)
                {
                    return false;
                }
            }
        }
        return true;

    }

    private static bool isNumber(char ch)
    {
        return "0123456789".IndexOf(ch) != -1;
    }

    public static bool IsHour(this int number)
    {
        return number > 0 && number <= 24;
    }
    public static bool IsMinute(this int number)
    {
        return number > 0 && number < 60;
    }
    public static bool IsSecond(this int number)
    {
        return number > 0 && number < 60;
    }
    public static bool IsMillisecond(this int number)
    {
        return number > 0 && number < 1000;
    }

    public static bool IsYear(this int number)
    {
        return number > 0 && number < 2100;
    }
    public static bool IsLeapYear(this int number)
    {
        return number.IsYear() && ((number % 4) == 0);
    }
    public static bool IsMonth(this int number)
    {
        return number > 0 && number <= 12;
    }

    public static bool IsDayOfMonth(this int number, int year, int month)
    {
        if (year.IsYear() == false || month.IsMonth() == false)
        {
            return false;
        }
        int minDate = 1;
        int maxDate = (month < 8) ? (30 + month % 2) : (30 + (month % 2 == 1 ? 0 : 1));
        if (month == 2)
        {
            maxDate = year.IsLeapYear() ? 29 : 28;
        }
        return number >= minDate && number <= maxDate;
    }
}