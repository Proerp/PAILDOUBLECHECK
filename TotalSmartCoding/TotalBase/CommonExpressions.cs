using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

using TotalBase.Enums;

namespace TotalBase
{
    public class CommonExpressions
    {
        public static string PropertyName<T>(Expression<Func<T, object>> expression)
        {
            var body = expression.Body as MemberExpression;

            if (body == null)
            {
                body = ((UnaryExpression)expression.Body).Operand as MemberExpression;
            }

            return body.Member.Name;
        }

        public static string AlphaNumericString(string normalString)
        {
            return Regex.Replace(normalString, @"[^0-9a-zA-Z\*\+\(\)]+", "");
        }

        public static string ComposeCommodityCode(string code, int commodityTypeID)
        {
            code = TotalBase.CommonExpressions.AlphaNumericString(code);

            if (commodityTypeID != (int)GlobalEnums.CommodityTypeID.Vehicles && code.Length >= 9)
                return code.Substring(0, 5) + "-" + code.Substring(5, 3) + "-" + code.Substring(8, code.Length - 8);
            else
                return code;
        }

        public static int GetEntryMonthID()
        {
            return CommonExpressions.GetEntryMonthID(DateTime.Now);
        }

        public static int GetEntryMonthID(DateTime? dateTime)
        {
            if (dateTime == null) dateTime = DateTime.Now;
            return (((DateTime)dateTime).Year - 2013) * 12 + ((DateTime)dateTime).Month - 5;
        }

        public static string IncrementSerialNo(string serialNo) //serialNo is a string with 6 digits
        {//Format 7 digit, then cut 6 right digit: This will reset a 0 when reach the limit of 6 digits
            return CommonExpressions.IncrementSerialNo(serialNo, 1);
        }

        public static string IncrementSerialNo(string serialNo, int  fixedScale) //serialNo is a string with 6 digits
        {//Format 7 digit, then cut 6 right digit: This will reset a 0 when reach the limit of 6 digits
            return (int.Parse(serialNo) + fixedScale).ToString("0000000").Substring(1);
        }

    }

    public class OptionBool
    {
        public bool OptionValue { get; set; }
        public string OptionDescription { get; set; }
    }
}
