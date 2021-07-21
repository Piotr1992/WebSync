using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BMP;
using BmpWebSyncLogic.helpers;


namespace Helpers
{
    public class DBHelper
    {


        public static string ConnectionString()
        {
            return BMP.Securyty.Szyfrant.Decode(ConfigurationManager.AppSettings["DBConn"]);
        }


        public static DataSet RunSqlProc(string procname)
        {
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(DBHelper.ConnectionString()))
            {
                SqlCommand cmd = new SqlCommand();
                SqlDataAdapter da = new SqlDataAdapter();
                cmd = new SqlCommand(procname, con);
                cmd.CommandType = CommandType.StoredProcedure;
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                con.Close();
            }
            return ds;
        }               


        public static DataTable RunSqlQuery(string query, string tableName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;

                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static DataTable RunSqlProcReturnTable(string procName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procName;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static DataTable RunSqlQueryParam(string query, string tableName, string paramName, int paraValue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue(paramName, paraValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static DataTable RunSqlProcParam(string procName, string tableName, string paramName, int paraValue)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue(paramName, paraValue);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static DataTable RunSqlQueryParam(string query, string tableName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);

                    BuildParameters(cmd, parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static DataTable RunSqlProcParam(string procName, string tableName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    BuildParameters(cmd, parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static DataTable RunSqlProcParam(string procName, string tableName, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand
                    {
                        Connection = conn, CommandText = procName, CommandType = CommandType.StoredProcedure
                    };

                    BuildParameters(cmd, parameters);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable(tableName);

                    da.Fill(dt);
                    dt.TableName = tableName;
                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static object RunScalarSqlProcParam(string procName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    BuildParameters(cmd, parameters);

                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }


        public static bool ExecSqlProcParam(string procName, string tableName, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = procName;
                    cmd.CommandType = CommandType.StoredProcedure;

                    BuildParameters(cmd, parameters);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return false;
            }
        }


        private static void BuildParameters(SqlCommand cmd, List<QueryParam> parameters)
        {
            if (parameters != null)
            {
                foreach (QueryParam par in parameters)
                {
                    cmd.Parameters.AddWithValue(par.Name, par.Value);
                }
            }
        }


        private static void BuildParameters(SqlCommand cmd, List<SqlParameter> parameters)
        {
            if (parameters != null)
            {
                foreach (SqlParameter par in parameters)
                {
                    cmd.Parameters.Add(par);
                }
            }
        }


        public static DataTable RunSqlProcSupplementDataProductGroup2(string procName, bool trueOrFalse, DataTable tb)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = procName;
                    cmd.Parameters.AddWithValue("@trueOrFalse", trueOrFalse);
                    cmd.Parameters.AddWithValue("@bmpccta", tb);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    return dt;
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }                       


        public static object RunScalarSqlQueryParam(string query, List<QueryParam> parameters)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    BuildParameters(cmd, parameters);
                    return cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex);
                return null;
            }
        }
      

    }
}