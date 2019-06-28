using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;

using TotalModel;
using TotalModel.Helpers;

namespace TotalDTO
{
    public static class SmartLogDTO
    {
        public static bool OnDataLogs { get; set; }

        public static List<string> ExclusiveNames;
        public static List<string> OptionalNames; //LIST NAME NEED TO CHECK IF THE ENTITY HAVE IT (EX: Commodities DON'T HAVE EntryDate) 
        public static List<string> PatternNames;

        public static void Init()
        {
            Type typeNotify = typeof(NotifyPropertyChangeObject);
            List<string> propertyInfos = typeNotify.GetProperties().Select(s => s.Name).ToList();

            propertyInfos.AddRange(new List<string>() { "Caption", "EditedDate", "ViewDetails" });
            propertyInfos.AddRange(typeof(BaseDTO).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Select(s => s.Name).ToList());
            propertyInfos.AddRange(typeof(IBaseModel).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Select(s => s.Name).ToList());
            propertyInfos.AddRange(typeof(IAccessControlAttribute).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance).Select(s => s.Name).ToList());

            SmartLogDTO.ExclusiveNames = propertyInfos.Except(new List<string>() { "EntryDate", "Reference", "Description" }).ToList(); //"PreparedPersonID", "ApproverID", 

            SmartLogDTO.OptionalNames = new List<string>() { "EntryDate" }; //NOW: WE DON'T ADD ANY NAME BY IBaseModel (EXCEPT PROPERTY: "EntryDate") ==> SO OptionalNames: NO NEED THIS STATEMENT: typeof(IBaseModel).GetProperties().Select(s => s.Name).ToList();

            SmartLogDTO.PatternNames = new List<string>() { "BindingList" };
        }

        public static bool CheckProperty(string propertyTypeName, string propertyInfoName)
        {
            return !SmartLogDTO.ExclusiveNames.Contains(propertyInfoName) && !SmartLogDTO.PatternNames.Any(p => propertyTypeName.Contains(p));
        }
    }
}
