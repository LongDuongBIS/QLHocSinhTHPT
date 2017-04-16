using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using System.Runtime.Serialization;

namespace QuanLyTruongCap3.Components
{
    public class DataService : DataTable
    {
        private static SqlConnection sqlCon;
        private SqlCommand sqlCMD;
        private SqlDataAdapter sqlReader;

        public static string Str { get; set; } = string.Empty;

        public DataService()
        {
        }

        public DataService(string tableName) : base(tableName)
        {
        }

        public DataService(string tableName, string tableNamespace) : base(tableName, tableNamespace)
        {
        }

        protected DataService(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static void ConnectionString()
        {
            var xmlDoc = XML.XMLReader("Connection.xml");
            var xmlEle = xmlDoc.DocumentElement;

            try
            {
                Str = xmlEle.SelectSingleNode("constatus").InnerText == "true" ? $"Data Source={xmlEle.SelectSingleNode("servername").InnerText};Initial Catalog={xmlEle.SelectSingleNode("database").InnerText};Integrated Security=True;" : $"Data Source={xmlEle.SelectSingleNode("servername").InnerText};Initial Catalog={xmlEle.SelectSingleNode("database").InnerText};User Id={xmlEle.SelectSingleNode("username").InnerText};Password={xmlEle.SelectSingleNode("password").InnerText};";

                Utilities.DatabaseName = xmlEle.SelectSingleNode("database").InnerText;
            }
            catch (Exception)
            {
                MessageBoxEx.Show("Lỗi kết nối đến cơ sở dữ liệu! Xin vui lòng thiết lập lại kết nối...", "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static bool OpenConnection()
        {
            if (Str == string.Empty)
                ConnectionString();
            try
            {
                if (sqlCon == null)
                    sqlCon = new SqlConnection(Str);
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                return true;
            }
            catch (Exception)
            {
                sqlCon.Close();
                return false;
            }
        }

        public void ChangePassword(string userName, string newPassword)
        {
            sqlReader = new SqlDataAdapter();
            using (SqlCommand cmd = new SqlCommand("UPDATE NGUOIDUNG " + "SET MatKhau = @matkhau " + "WHERE TenDNhap = @tendangnhap"))
            {
                cmd.Parameters.Add("tendangnhap", SqlDbType.VarChar).Value = userName;
                cmd.Parameters.Add("matkhau", SqlDbType.VarChar).Value = newPassword;

                if (Str == string.Empty)
                    ConnectionString();

                if (sqlCon == null || sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon = new SqlConnection(Str);
                    sqlCon.Open();
                }

                sqlCMD = new SqlCommand();
                sqlCMD = cmd;
            }

            try
            {
                sqlCMD.Connection = sqlCon;

                sqlReader = new SqlDataAdapter
                {
                    SelectCommand = sqlCMD
                };

                sqlReader.Fill(this);

                using (SqlCommandBuilder buider = new SqlCommandBuilder(sqlReader))
                {
                    sqlReader.Update(this);
                }
            }
            catch (Exception e)
            {
                MessageBoxEx.Show($"Không thể thực thi câu lệnh SQL này!\nLỗi: {e.Message}", "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static void CloseConnection()
        {
            sqlCon.Close();
        }

        public int ExecuteNonQuery()
        {
            var result = 0;
            SqlTransaction trans = null;
            try
            {
                trans = sqlCon.BeginTransaction();

                sqlCMD.Connection = sqlCon;
                sqlCMD.Transaction = trans;

                sqlReader = new SqlDataAdapter
                {
                    SelectCommand = sqlCMD
                };

                using (SqlCommandBuilder builder = new SqlCommandBuilder(sqlReader))
                {
                    result = sqlReader.Update(this);
                    trans.Commit();
                }
            }
            catch
            {
                if (trans != null)
                    trans.Rollback();
                throw;
            }
            return result;
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            var result = 0;
            SqlTransaction trans = null;
            try
            {
                trans = sqlCon.BeginTransaction();

                cmd.Connection = sqlCon;
                cmd.Transaction = trans;
                result = cmd.ExecuteNonQuery();

                this.AcceptChanges();
                trans.Commit();
            }
            catch
            {
                if (trans != null)
                    trans.Rollback();
                throw;
            }
            return result;
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            object result = null;
            SqlTransaction trans = null;
            try
            {
                trans = sqlCon.BeginTransaction();
                cmd.Connection = sqlCon;
                cmd.Transaction = trans;
                result = cmd.ExecuteScalar();

                this.AcceptChanges();
                trans.Commit();
                if (result == DBNull.Value)
                    result = null;
            }
            catch
            {
                if (trans != null)
                    trans.Rollback();
                throw;
            }
            return result;
        }

        public void Load(SqlCommand cmd)
        {
            sqlCMD = cmd;
            try
            {
                sqlCMD.Connection = sqlCon;

                sqlReader = new SqlDataAdapter
                {
                    SelectCommand = sqlCMD
                };

                this.Clear();
                sqlReader.Fill(this);
            }
            catch (Exception e)
            {
                MessageBoxEx.Show($"Không thể thực thi câu lệnh SQL này!\nLỗi: {e.Message}", "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}