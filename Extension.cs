//-----------------------------------------------------------------------
// <copyright file="Extension.cs" company="Cabuum I.T.">
//     Copyright (c) Cabuum Information of Technology. All rights reserved.
// </copyright>
// <author>Bruno Casali</author>
// <email>brunoocasali@gmail.com / brunocasali.wordpress.com</email>
//
// <ver>1.0.1</ver>
//-----------------------------------------------------------------------
 
namespace DataRepository.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// This Class, will provide to you a method to verify which columns are in the DataRow parameter.
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// Avoid to create for the each List method, a new Fill Attributes, with only the mentioned in structured query language attributes.
        /// </summary>
        /// <typeparam name="T">The type that the method will be return.</typeparam>
        /// <param name="dataRow">Each instance from DataRow got in foreach lists.</param>
        /// <param name="nameCollumn">Name that will be verified.</param>
        /// <param name="func">This convert the object type of dataRow[nameColumn] to any specified type, that you want!</param>
        /// <param name="defaultValue">To avoid, null or anyone useless value.</param>
        /// <returns>Return a object converted correctly by a dataRow.</returns>
        public static dynamic Verify<T>(this DataRow dataRow, string nameCollumn, Func<object, T> func, T defaultValue = default(T))
        {
            if (!dataRow.Table.Columns.Contains(nameCollumn))
            {
                return defaultValue;
            }

            return dataRow.IsNull(nameCollumn) ? defaultValue
                                          : func(dataRow[nameCollumn]);
        }
    }
}