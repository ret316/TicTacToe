using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace TicTacToe.WebApi.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Method for getting enum description in attributes
        /// </summary>
        /// <param name="value">Enum</param>
        /// <returns>Description of enum</returns>
        public static string GetDescription(this System.Enum value)
        {
            if (value is null)
                return null;
            Type type = value.GetType();
            string name = System.Enum.GetName(type, value);
            if (name != null)
            {
                FieldInfo field = type.GetField(name);
                if (field != null)
                {
                    DescriptionAttribute attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                    if (attr != null)
                    {
                        return attr.Description;
                    }
                }
            }
            return null;
        }
    }
}
