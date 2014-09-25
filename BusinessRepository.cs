//-----------------------------------------------------------------------
// <copyright file="BusinessRepository.cs" company="Cabuum I.T.">
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
    /// Combined with the DataRepository, is a good choice to don't forget any part of the CRUD operations.
    /// </summary>
    /// <typeparam name="T">Used in the base methods.</typeparam>
    public abstract class BusinessRepository<T>
    {
        /// <summary>
        /// Gets a constant to be reused on selects statements
        /// </summary>
        protected abstract string Fields { get; }
        
        /// <summary>
        /// Add a new instance of the object T, to the database.
        /// </summary>
        /// <returns>Inserted, yes or no.</returns>
        public abstract bool Add();

        /// <summary>
        /// Updates a instance of the object T, to the database.
        /// </summary>
        /// <returns>Updated, yes or no.</returns>
        public abstract bool Update();

        /// <summary>
        /// Remove a instance of the object T, to the database.
        /// </summary>
        /// <returns>Remove, yes or no.</returns>
        public abstract bool Delete();

        /// <summary>
        /// Each class, implements the call of DataRepository.Total("table_name");
        /// </summary>
        /// <returns>How many rows that table has.</returns>
        public abstract int Total();

        /// <summary>
        /// Select and return a object of the type T from the database.
        /// </summary>
        /// <param name="id">The value of primary key.</param>
        /// <returns>A object of the type T</returns>
        public abstract T Select(int id);

        /// <summary>
        /// Fill all of the attributes of the class that inheritance of this base.
        /// </summary>
        /// <param name="dataRow">Object of each row, in the table</param>
        /// <returns>Returns a Object filled</returns>
        protected abstract T FillAttributes(DataRow dataRow);
    }
}