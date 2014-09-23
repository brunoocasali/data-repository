using System;
using System.Collections.Generic;
using System.Data;

/// <summary> Versão 0.0.8
/// Made by Bruno Casali with &lt;3 :D! When?! in 9:37 AM, Monday, August 25, 2014
/// contact:    brunocasali.wordpress.com
///             brunoocasali@gmail.com
/// </summary>

namespace DataRepository.Extensions
{
    public static class Extensions
    {
        public static dynamic Verify<T>(this DataRow dr, string nameCollumn, 
                                        Func<object, T> func, T defaultValue = default(T))
        {
            if (!dr.Table.Columns.Contains(nameCollumn))
                return defaultValue;

            return dr.IsNull(nameCollumn) ? defaultValue
                                          : func(dr[nameCollumn]);
        }
    }
}
