using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Xml;

namespace Core
{
    public class SQLHelper
    {

        #region 配置文件读取
        /// <summary>
        /// 配置文件读取
        /// </summary>
        /// <param name="NodeName"></param>
        /// <param name="EleName"></param>
        /// <returns></returns>
        public static string LoadXML(string NodeName, string EleName)
        {
            string m_sReturn = "";

            string m_sConfigPath = System.AppDomain.CurrentDomain.BaseDirectory + "Config.xml";

            XmlDocument myDoc = new XmlDocument();

            if (File.Exists(m_sConfigPath))
            {
                myDoc.Load(m_sConfigPath);

            }
            else
            {
                return m_sReturn;
            }

            XmlNodeList RootNodeList = myDoc.SelectNodes("//Config");

            if (RootNodeList.Count != 0)
            {
                foreach (XmlNode ConfigNode in RootNodeList)
                {
                    if (ConfigNode.Name == "Config")
                    {
                        XmlNodeList NodeList = ConfigNode.ChildNodes;

                        if (NodeList.Count != 0)
                        {
                            foreach (XmlNode Node in NodeList)
                            {
                                if (Node.Name == NodeName)
                                {
                                    XmlNodeList SubNodeList = Node.ChildNodes;

                                    if (SubNodeList.Count != 0)
                                    {
                                        foreach (XmlNode SubNode in SubNodeList)
                                        {
                                            if (SubNode.Name == EleName)
                                            {
                                                m_sReturn = SubNode.InnerText; ;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return m_sReturn;
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static String SET_CONNECTION(string key)
        {
            return ConfigurationManager.ConnectionStrings[key].ToString();
        }

        #endregion



        public static string Getconnection_string()
        {
            return SET_CONNECTION("connectionString");

        }

        public static int EXECUTE_NONQUERY(string sql_string, CommandType command_type, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, command_type, connection, null, cmd_parms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        public static int EXECUTE_NONQUERY(string sql_string, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, CommandType.Text, connection, null, cmd_parms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        public static int EXECUTE_NONQUERY(string sql_string)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, CommandType.Text, connection, null, null);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }

        public static object EXECUTE_SCALAR(string sql_string, CommandType command_type, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, command_type, connection, null, cmd_parms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        public static object EXECUTE_SCALAR(string sql_string, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, CommandType.Text, connection, null, cmd_parms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        public static object EXECUTE_SCALAR(string sql_string)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, CommandType.Text, connection, null, null);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }


        public static DataSet QUERY_DATE_SET(string sql_string, CommandType command_type, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, command_type, connection, null, cmd_parms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        public static DataSet QUERY_DATE_SET(string sql_string, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, CommandType.Text, connection, null, cmd_parms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }
        public static DataSet QUERY_DATE_SET(string sql_string)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, CommandType.Text, connection, null, null);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

        public static DataTable QUERY_DATE_TABLE(string sql_string, CommandType command_type, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, command_type, connection, null, cmd_parms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds.Tables[0];
                }
            }
        }
        public static DataTable QUERY_DATE_TABLE(string sql_string, params SqlParameter[] cmd_parms)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, CommandType.Text, connection, null, cmd_parms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds.Tables[0];
                }
            }
        }
        public static DataTable QUERY_DATE_TABLE(string sql_string)
        {
            using (SqlConnection connection = new SqlConnection(Getconnection_string()))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, CommandType.Text, connection, null, null);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds.Tables[0];
                }
            }
        }


        public static void PREPARE_COMMAND(SqlCommand cmd, CommandType command_type, SqlConnection conn, SqlTransaction trans, SqlParameter[] cmd_parms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = command_type;
            if (cmd_parms != null)
            {

                foreach (SqlParameter parm in cmd_parms)
                    cmd.Parameters.Add(parm);
            }
        }


        public static DataTable QUERYTABLE(string sql_string)
        {
            String sqltxt = "Data Source=129.9.230.1;Initial Catalog=lsrmyy;User ID=xtxt;password=";
            using (SqlConnection connection = new SqlConnection(sqltxt))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, CommandType.Text, connection, null, null);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds.Tables[0];
                }
            }
        }

        public static int EXECUTE_NONQUERYS(string sql_string, CommandType command_type, params SqlParameter[] cmd_parms)
        {
            String sqltxt = "Data Source=129.9.230.1;Initial Catalog=lsrmyy;User ID=ssa;password='20031118'";
            using (SqlConnection connection = new SqlConnection(sqltxt))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, command_type, connection, null, cmd_parms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException E)
                    {
                        throw new Exception(E.Message);
                    }
                }
            }
        }
        public static DataTable QUERY_DATE_TABLE1(string sql_string, CommandType command_type, params SqlParameter[] cmd_parms)
        {
            //String sqltxt = "Data Source=129.9.230.72;Initial Catalog=study;User ID=ssa;password='20031118'";
            String sqltxt = "Data Source=129.9.230.1;Initial Catalog=lsrmyy;User ID=ssa;password='20031118'";
            using (SqlConnection connection = new SqlConnection(sqltxt))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, command_type, connection, null, cmd_parms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds.Tables[0];
                }
            }
        }

        public static object EXECUTE_SCALARs(string sql_string, CommandType command_type, params SqlParameter[] cmd_parms)
        {
            String sqltxt = "Data Source=129.9.230.1;Initial Catalog=lsrmyy;User ID=ssa;password='20031118'";

            using (SqlConnection connection = new SqlConnection(sqltxt))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = sql_string;
                        PREPARE_COMMAND(cmd, command_type, connection, null, cmd_parms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw new Exception(e.Message);
                    }
                }
            }
        }
        public static DataSet QUERY_DATE_SET1(string sql_string)
        {
            String sqltxt = "Data Source=129.9.230.1;Initial Catalog=lsrmyy;User ID=ssa;password='20031118'";

            using (SqlConnection connection = new SqlConnection(sqltxt))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql_string;
                PREPARE_COMMAND(cmd, CommandType.Text, connection, null, null);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }

    }
}
