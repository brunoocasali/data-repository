//-----------------------------------------------------------------------
// <copyright file="DataRepository.cs" company="Cabuum I.T.">
//     Copyright (c) Cabuum Information of Technology. All rights reserved.
// </copyright>
// <author>Bruno Casali</author>
// <email>brunoocasali@gmail.com / brunocasali.wordpress.com / http://about.me/brunocasali </email>
//
// <ver>2.1.7</ver>
//-----------------------------------------------------------------------

namespace DataRepository
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// Provides a simple way to use ADO .NET, without any framework like Entity!
    /// </summary>
    public sealed class DataRepository
    {
        /// <summary>
        /// <para>Alter the registers/records by INSERTS / DELETES / UPDATES ;D </para>
        /// </summary>
        /// <param name="hash">Set of keys and values to be inner on query string by parameters.</param>
        /// <param name="querySQL">Some query to be executed on MySQL. It can be an INSERT or DELETE or UPDATE </param>
        /// <returns>The operation has been executed. It is OK, (True or False!) ;D</returns>
        public static bool ChangeRecords(Dictionary<string, object> hash, string querySQL)
        {
            try
            {
                MySqlCommand cmd = MakeCommand(hash, querySQL);
                cmd.Connection.Open();
                return cmd.ExecuteNonQuery() > 0 ? true : false;
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// How many elements that table has?! Yeah with this method you can get the answer!
        /// </summary>
        /// <param name="tableName">What table you need to know!</param>
        /// <returns>The actual number of rows!</returns>
        public static int Total(string tableName)
        {
            try
            {
                using (DataTable dt = DataLoad(new Dictionary<string, object>(), string.Format(@"SELECT COUNT(*) AS Total FROM {0};", tableName)))
                {
                    DataRow dr = dt.Rows[0];
                    return !dr.IsNull("Total") ? Convert.ToInt32(dr["Total"]) : 0;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get's the next valid ID from inputted tableName. This value is based on primary key or AUTO_INCREMENT fields from table!
        /// </summary>
        /// <param name="tableName">The table you want to know!</param>
        /// <returns>A next valid id from the table requested.</returns>
        public static int NextValidID(string tableName)
        {
            try
            {
                using (DataTable dt = DataLoad(new Dictionary<string, object>() { { "tbl", tableName } }, @"SELECT AUTO_INCREMENT AS NextID FROM INFORMATION_SCHEMA.TABLES WHERE table_name = @tbl;"))
                {
                    DataRow dr = dt.Rows[0];
                    return !dr.IsNull("NextID") ? Convert.ToInt32(dr["NextID"]) : 0;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// This method gives me a new connection on MySQL
        /// </summary>
        /// <returns>A new connection with the parameters inputted in query string for connection! :D</returns>
        public static MySqlConnection ConnectData()
        {
            try
            {
                return new MySqlConnection("Server=HOST;Database=DATABASE;User=USER;Password=PASSWORD;Pooling=true;");
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Get all of rows, that will be satisfies the query.
        /// </summary>
        /// <typeparam name="T">The type of returned list, usually is the same of the class.</typeparam>
        /// <param name="hash">All of keys and yours values, that will be replaced on querySQL</param>
        /// <param name="querySQL">An SELECT wished query!</param>
        /// <param name="func">A method that will be sponsored to fill the values by a DataRow to a Object</param>
        /// <returns>A List with all of the objects of rows.</returns>
        public static List<T> List<T>(Dictionary<string, object> hash, string querySQL, Func<DataRow, T> func)
        {
            try
            {
                using (DataTable dt = DataLoad(hash, querySQL))
                {
                    List<T> lst = new List<T>();

                    foreach (DataRow dr in dt.Rows)
                    {
                        lst.Add(func(dr));
                    }

                    return lst;
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Dynamic load parameters to query.
        /// </summary>
        /// <param name="hash">All of keys and yours values, that will be replaced on querySQL</param>
        /// <param name="querySQL">An SELECT wished query!</param>
        /// <returns>A new DataTable with all of parameters required.</returns>
        private static DataTable DataLoad(Dictionary<string, object> hash, string querySQL)
        {
            try
            {
                MySqlCommand cmd = MakeCommand(hash, querySQL);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Reusing code...
        /// </summary>
        /// <param name="hash">All of keys and yours values, that will be replaced on querySQL</param>
        /// <param name="querySQL">An SELECT wished query!</param>
        /// <returns>Return new command to do something do you need!</returns>
        private static MySqlCommand MakeCommand(Dictionary<string, object> hash, string querySQL)
        {
            using (MySqlConnection coon = DataRepository.ConnectData())
            {
                using (MySqlCommand cmd = new MySqlCommand(querySQL, coon))
                {
                    object obj;
                    foreach (string item in hash.Keys)
                    {
                        hash.TryGetValue(item, out obj);
                        cmd.Parameters.AddWithValue("@" + item, obj);
                    }

                    return cmd;
                }
            }
        }
    }
}