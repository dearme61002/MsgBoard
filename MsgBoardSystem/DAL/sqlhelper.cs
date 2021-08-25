using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class sqlhelper
    {
        public class sqlcanhelp
        {
            private static string connstring = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
            #region 不帶參數的連接資料庫
            public static int executeNonQuerysql(string sql)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {

                        Console.WriteLine("sucess");

                        using (SqlCommand command = new SqlCommand(sql, con))
                        {
                            con.Open();
                            return command.ExecuteNonQuery();


                        }
                    }
                }
                catch (Exception ex)
                {
                    //writelog("執行executeNonQuerysql方法發生異常:" + ex.Message);
                    throw ex;
                }


            }

            //
            public static object executeScalarsql(string sql)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {

                        Console.WriteLine("sucess");

                        using (SqlCommand command = new SqlCommand(sql, con))
                        {
                            con.Open();
                            return command.ExecuteScalar();


                        }
                    }
                }
                catch (Exception ex)
                {
                    //   writelog("執行executeScalarsql方法發生異常:" + ex.Message);
                    throw ex;
                }

            }
            //
            public static SqlDataReader executeReadesql(string sql)
            {
                try
                {
                    SqlConnection con = new SqlConnection(connstring);


                    Console.WriteLine("sucess");

                    SqlCommand command = new SqlCommand(sql, con);

                    con.Open();
                    return command.ExecuteReader(CommandBehavior.CloseConnection);




                }
                catch (Exception ex)
                {
                    //  writelog("執行executeReadesql(string sql)方法發生異常:" + ex.Message);
                    throw ex;
                }

            }


            #endregion

            #region 帶參數的連接資料庫
            public static int executeNonQuerysql(string sqlOrprocedure, SqlParameter[] param, bool isProcedure)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {


                        Console.WriteLine("sucess");

                        using (SqlCommand command = new SqlCommand(sqlOrprocedure, con))
                        {
                            if (isProcedure)
                            {
                                command.CommandType = CommandType.StoredProcedure;
                            }

                            con.Open();
                            command.Parameters.AddRange(param);
                            return command.ExecuteNonQuery();


                        }
                    }
                }
                catch (Exception ex)
                {
                    //writelog("執行executeNonQuerysql(string sqlOrprocedure ,SqlParameter[] param,bool isProcedure)方法發生異常:" + ex.Message);
                    throw ex;
                }


            }

            //
            public static object executeScalarsql(string sqlOrprocedure, SqlParameter[] param, bool isProcedure)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connstring))
                    {

                        Console.WriteLine("sucess");

                        using (SqlCommand command = new SqlCommand(sqlOrprocedure, con))
                        {
                            if (isProcedure)
                            {
                                command.CommandType = CommandType.StoredProcedure;
                            }

                            con.Open();
                            command.Parameters.AddRange(param);
                            return command.ExecuteScalar();


                        }
                    }
                }
                catch (Exception ex)
                {
                    // writelog("執行executeScalarsql(string sqlOrprocedure, SqlParameter[] param, bool isProcedure)方法發生異常:" + ex.Message);
                    throw ex;
                }

            }
            //
            public static SqlDataReader executeReadesql(string sqlOrprocedure, SqlParameter[] param, bool isProcedure)
            {
                try

                {
                    SqlConnection con = new SqlConnection(connstring);


                    Console.WriteLine("sucess");

                    SqlCommand command = new SqlCommand(sqlOrprocedure, con);

                    if (isProcedure)
                    {
                        command.CommandType = CommandType.StoredProcedure;
                    }

                    con.Open();
                    command.Parameters.AddRange(param);
                    return command.ExecuteReader(CommandBehavior.CloseConnection);

                }
                catch (Exception ex)
                {
                    //  writelog("執行 executeReadesql(string sqlOrprocedure, SqlParameter[] param, bool isProcedure)方法發生異常:" + ex.Message);
                    throw ex;
                }

            }
            #endregion

            //#region write log
            //private static void writelog(string msg)
            //{
            //    FileStream fs = new FileStream(@"C:\Users\changtheworld\Desktop\texlog\projectlog.log", FileMode.Append);
            //    StreamWriter sw = new StreamWriter(fs);
            //    sw.WriteLine(DateTime.Now.ToString() + "----" + msg);
            //    sw.Close();
            //    fs.Close();
            //}
            //#endregion
        }
    }
}
