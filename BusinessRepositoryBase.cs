//-----------------------------------------------------------------------
// <copyright file="BusinessRepositoryBase.cs" company="Cabuum I.T.">
//     Copyright (c) Cabuum Information of Technology. All rights reserved.
// </copyright>
// <author>Bruno Casali</author>
// <email>brunoocasali@gmail.com / brunocasali.wordpress.com / http://about.me/brunocasali </email>
//
// <ver>1.2.0</ver>
//-----------------------------------------------------------------------

namespace DataRepository
{
    using System.Data;

    /// <summary>
    /// This class only for a base classes.
    /// </summary>
    /// <typeparam name="T">The type inherited from a BusinessRepository</typeparam>
    public abstract class BusinessRepositoryBase<T> : BusinessRepository<T>
    {
        /// <summary>
        /// The total of rows, resulted in a search.
        /// </summary>
        /// <param name="search">the query</param>
        /// <returns>The number of rows</returns>
        public abstract int Total(string search);

        /// <summary>
        /// Fill all of the attributes of the class that inheritance of this base.
        /// </summary>
        /// <param name="dataRow">Object of each row, in the table</param>
        /// <param name="obj">Type of object that will be returned</param>
        protected abstract void FillAttributes(DataRow dataRow, T obj);
    }
}