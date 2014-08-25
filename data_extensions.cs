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
        public static dynamic Verify<T>(this DataRow dr, string nameCollum, Func<object, T> func)
        {
            bool ok = false;
            foreach (DataColumn item in dr.Table.Columns)
            {
                if (item.ColumnName.ToLower().Equals(nameCollum.ToLower()))
                {
                    ok = true;
                    break;
                }
            }

            if (ok)
            {
                if (dr[nameCollum] is DBNull)
                    return default(T);
                else
                    return func(dr[nameCollum]);
            }
            else
                return default(T);
        }
    }
}
