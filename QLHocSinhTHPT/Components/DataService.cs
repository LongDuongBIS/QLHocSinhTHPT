using DevComponents.DotNetBar;
using QLHocSinhTHPT.Components;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;

namespace QLHocSinhTHPT.Components
{
    internal class DataService : DataTable
    {
        private static SqlConnection sqlCon;
        public static string str = string.Empty;
        private SqlCommand sqlCMD;
        private SqlDataAdapter sqlReader;

        public static void ConnectionString()
        {
            XmlDocument xmlDoc = XML.XMLReader("Connection.xml");
            XmlElement xmlEle = xmlDoc.DocumentElement;

            try
            {
                if (xmlEle.SelectSingleNode("constatus").InnerText == "true")
                    str = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=True;", xmlEle.SelectSingleNode("servername").InnerText, xmlEle.SelectSingleNode("database").InnerText);
                else
                    str = string.Format("Data Source={0};Initial Catalog={1};User Id={2};Password={3};", xmlEle.SelectSingleNode("servername").InnerText, xmlEle.SelectSingleNode("database").InnerText, xmlEle.SelectSingleNode("username").InnerText, xmlEle.SelectSingleNode("password").InnerText);

                Utilities.DatabaseName = xmlEle.SelectSingleNode("database").InnerText;
            }
            catch
            {
                MessageBoxEx.Show("Lỗi kết nối đến cơ sở dữ liệu! Xin vui lòng thiết lập lại kết nối...", "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public void Load(SqlCommand cmd)
        {
            sqlCMD = cmd;
            try
            {
                sqlCMD.Connection = sqlCon;

                sqlReader = new SqlDataAdapter();
                sqlReader.SelectCommand = sqlCMD;

                this.Clear();
                sqlReader.Fill(this);
            }
            catch (Exception e)
            {
                MessageBoxEx.Show(string.Format("Không thể thực thi câu lệnh SQL này!\nLỗi: {0}", e.Message), "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        public static bool OpenConnection()
        {
            if (str == string.Empty)
                ConnectionString();
            try
            {
                if (sqlCon == null)
                    sqlCon = new SqlConnection(str);
                if (sqlCon.State == ConnectionState.Closed)
                    sqlCon.Open();
                return true;
            }
            catch
            {
                sqlCon.Close();
                return false;
            }
        }

        public void CloseConnection()
        {
            sqlCon.Close();
        }

        public int ExecuteNonQuery()
        {
            int result = 0;
            SqlTransaction trans = null;
            try
            {
                trans = sqlCon.BeginTransaction();

                sqlCMD.Connection = sqlCon;
                sqlCMD.Transaction = trans;

                sqlReader = new SqlDataAdapter();
                sqlReader.SelectCommand = sqlCMD;

                SqlCommandBuilder builder = new SqlCommandBuilder(sqlReader);

                result = sqlReader.Update(this);
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

        public int ExecuteNoneQuery(SqlCommand cmd)
        {
            int result = 0;
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

        public void ChangePassword(string userName, string newPassword)
        {
            sqlReader = new SqlDataAdapter();
            using (SqlCommand cmd = new SqlCommand("UPDATE NGUOIDUNG " + "SET MatKhau = @matkhau " + "WHERE TenDNhap = @tendangnhap"))
            {
                cmd.Parameters.Add("tendangnhap", SqlDbType.VarChar).Value = userName;
                cmd.Parameters.Add("matkhau", SqlDbType.VarChar).Value = newPassword;

                if (str == string.Empty)
                    ConnectionString();

                if (sqlCon == null || sqlCon.State == ConnectionState.Closed)
                {
                    sqlCon = new SqlConnection(str);
                    sqlCon.Open();
                }

                sqlCMD = new SqlCommand();
                sqlCMD = cmd;
            }

            try
            {
                sqlCMD.Connection = sqlCon;

                sqlReader = new SqlDataAdapter();
                sqlReader.SelectCommand = sqlCMD;

                //this.Clear();

                sqlReader.Fill(this);

                SqlCommandBuilder buider = new SqlCommandBuilder(sqlReader);
                sqlReader.Update(this);
            }
            catch (Exception e)
            {
                MessageBoxEx.Show(string.Format("Không thể thực thi câu lệnh SQL này!\nLỗi: {0}", e.Message), "ERROR", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}