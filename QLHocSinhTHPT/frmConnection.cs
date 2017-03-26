using DevComponents.DotNetBar;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QLHocSinhTHPT.Components;
namespace QLHocSinhTHPT
{
    public partial class frmConnection : Office2007Form
    {
        public frmConnection()
        {
            InitializeComponent();
        }

        private void frmConnection_Load(object sender, EventArgs e)
        {
            cmbAuthentication.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cmbAuthentication.SelectedIndex == 0)
                XML.XMLWriter("Connection.xml", txtServer.Text, cmbDatabase.Text, "true");
            else
                XML.XMLWriter("Connection.xml", txtServer.Text, txtUsername.Text, txtPassword.Text, cmbDatabase.Text, "false");

            this.DialogResult = DialogResult.OK;
        }

        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            //Quyền Windows
            if (cmbAuthentication.SelectedIndex == 0)
            {
                cmbDatabase.Items.Clear();
                using (SqlConnection con = new SqlConnection(string.Format("Data Source={0};Initial Catalog=master;Integrated Security=True;", txtServer.Text)))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DATABASES", con))
                    {
                        SqlDataReader reader;

                        try
                        {
                            con.Open();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                cmbDatabase.Items.Add(reader[0].ToString());
                            }
                            MessageBoxEx.Show("Kết nối thành công!", "SUCCESSED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (SqlException sqlEx)
                        {
                            MessageBoxEx.Show(sqlEx.Message, "FAILED", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                                con.Close();
                        }
                    }
                }
            }

            //Quyền SQL Server
            if (cmbAuthentication.SelectedIndex == 1)
            {
                cmbDatabase.Items.Clear();
                using (SqlConnection con = new SqlConnection(string.Format("Data Source={0};Initial Catalog=master;User Id={1};Password={2};", txtServer.Text, txtUsername.Text, txtPassword.Text)))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_DATABASES", con))
                    {
                        SqlDataReader reader;

                        try
                        {
                            con.Open();
                            reader = cmd.ExecuteReader();
                            while (reader.Read())
                            {
                                cmbDatabase.Items.Add(reader[0].ToString());
                            }
                            MessageBoxEx.Show("Kết nối thành công!", "SUCCESSED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (SqlException sqlEx)
                        {
                            MessageBoxEx.Show(sqlEx.Message, "SUCCESSED", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        finally
                        {
                            if (con.State == ConnectionState.Open)
                                con.Close();
                        }
                    }
                }
            }
        }

        private void cmbAuthentication_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbAuthentication.SelectedIndex == 0)
            {
                txtUsername.Enabled = false;
                txtPassword.Enabled = false;
            }
            else
            {
                txtUsername.Enabled = true;
                txtPassword.Enabled = true;
            }
        }
    }
}