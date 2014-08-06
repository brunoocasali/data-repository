using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

/// <summary> DataRepository v1.0.2.7, <para></para>
/// Enthusiast: Bruno Casali entre 07/11/2013 e 10/12/2013   
/// <!--
/// You Wanted the Best and You Got the Best. The Hottest Band in the World, KISS!
/// Background Song Night Gone Wasted by The Band Perry and anothers... :D
/// -->
/// </summary>

public sealed class DataRepository
{
    /// <summary> Connect Data   ||
    /// Esse método não recebe nenhum parâmetro.
    /// </summary>
    /// <returns> retorna sempre uma nova conexão com o MySQL</returns>
    public static MySqlConnection ConnectData()
    {
        try
        {
            return dbConnect.SiteConnection();
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

    /// <summary> Change Registers,
    /// <para>Cria dinâmicamente os INSERTS / DELETES / UPDATES ;D </para> <para>OBS: Os Nomes e os Valores, devem ser colocados na mesma ordem, dentro do array!</para>
    /// </summary>
    /// <param name="names"> Um Array de String para formar os nomes dos parameters.add</param>
    /// <param name="values"> Um Array de Objetos, vai ser qualquer tipo vão os valores dos atributos</param>
    /// <param name="querySQL"> A String de ação do SQL, que pode ser um INSERT, DELETE, UPDATE com todos os nomes dos parâmetros já carregados</param>
    /// <returns>If the operation is OK, (True or False!) ;D</returns>
    public static bool ChangeRecords(string[] names, object[] values, string querySQL)
    {
        try
        {
            using (MySqlConnection coon = DataRepository.ConnectData())
            {
                using (MySqlCommand cmd = new MySqlCommand(querySQL, coon))
                {
                    for (int i = 0; i < names.Length; i++)
                        cmd.Parameters.AddWithValue("@" + names[i], values[i]);

                    coon.Open();
                    return cmd.ExecuteNonQuery() == 0 ? false : true;
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

    /// <summary> Carregar DataTable   ||
    /// SELECTS ;D            --> IMPORTANTE LEMBRAR: Os nomes e os Valores, devem ser colocados na mesma ordem, dentro do array!
    /// </summary>
    /// <param name="names">Recebem todos os nomes dos parâmetros que serão inseridos dentro da QuerySQL</param>
    /// <param name="values"> Aqui entra todos os valores que deverão ser substituídos na QuerySQL</param>
    /// <param name="querySQL"> Recebe aqui a ideia da Query, ou seja o esqueleto dela, sem parâmetros sem nada. somente com os nomes dos parâmetros</param>
    /// <returns> Retorna um Data Table para ser recuperado, pelos métodos de negócio.</returns>
    public static DataTable DataLoad(string[] names, object[] values, string querySQL)
    {
        using (MySqlConnection coon = DataRepository.ConnectData())
        {
            using (MySqlCommand cmd = new MySqlCommand(querySQL, coon))
            {
                for (int i = 0; i < names.Length; i++)
                    cmd.Parameters.AddWithValue("@" + names[i], values[i]);

                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }

    /// <summary> TotalRegistros   ||
    /// Reconhece a Quantidade de elementos de uma tabelaa.
    /// </summary>
    /// <param name="nomeTabela">Requer o nome da tabela como parâmetro.</param>
    /// <returns>O Número de Registros atual!</returns>
    public static int Total(string nomeTabela)
    {
        try
        {
            using (MySqlCommand cmd = new MySqlCommand(string.Format(@"SELECT COUNT(*) AS Total FROM {0};", nomeTabela), DataRepository.ConnectData()))
            {
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    using (DataTable dt = new DataTable())
                    {
                        da.Fill(dt);

                        DataRow dr = dt.Rows[0];
                        return !dr.IsNull("Total") ? Convert.ToInt32(dr["Total"]) : 0;
                    }
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

    /// <summary> Próximo ID Válido ||
    /// Recupera o Próximo ID válido baseado no auto_increment do índice primário da tabela (ID)
    /// </summary>
    /// <param name="nomeTabela">Nome da tabela em questão</param>
    public static int NextValidID(string nomeTabela)
    {
        try
        {
            using (MySqlCommand cmd = new MySqlCommand(@"SELECT AUTO_INCREMENT AS NextID FROM INFORMATION_SCHEMA.TABLES WHERE table_name = @tbl;", DataRepository.ConnectData()))
            {
                cmd.Parameters.AddWithValue("@tbl", nomeTabela);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

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

    /// <summary> Carregar DataTable   ||
    /// SELECTS ;D            --> IMPORTANTE LEMBRAR: Os nomes e os Valores, devem ser colocados na mesma ordem, dentro do array!
    /// </summary>
    /// <param name="names">Recebem todos os nomes dos parâmetros que serão inseridos dentro da QuerySQL</param>
    /// <param name="values"> Aqui entra todos os valores que deverão ser substituídos na QuerySQL</param>
    /// <param name="querySQL"> Recebe aqui a ideia da Query, ou seja o esqueleto dela, sem parâmetros sem nada. somente com os nomes dos parâmetros</param>
    /// <param name="func"> Requer um método que retorne um objeto do tipo T, extenda a classe </param>
    /// <returns> Retorna um Data Table para ser recuperado, pelos métodos de negócio.</returns>
    public static List<T> List<T>(string[] names, object[] values, string querySQL, Func<DataRow, T> func)
    {
        try
        {
            using (DataTable dt = DataLoad(names, values, querySQL))
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
}
