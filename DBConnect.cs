using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;

namespace _12_35_5_14DHBM02 // Đảm bảo đúng namespace
{
    // Lớp này đóng vai trò là cầu nối để các lớp khác sử dụng kết nối.
    public class DBConnect
    {
        // Hàm ExecuteQuery (dùng cho SELECT)
        public DataTable ExecuteQuery(string sql, OracleParameter[] parameters = null)
        {
            OracleConnection conn = Database.Get_Connect();
            DataTable dt = new DataTable();

            if (conn == null) return dt;

            try
            {
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ExecuteQuery: " + ex.Message);
            }
            return dt;
        }

        // Hàm ExecuteNonQuery (dùng cho INSERT, UPDATE, DELETE, GRANT, REVOKE)
        public string ExecuteNonQuery(string sql, OracleParameter[] parameters = null)
        {
            OracleConnection conn = Database.Get_Connect();
            string result = "Thành công.";

            if (conn == null) return "Thất bại: Không có kết nối.";

            try
            {
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == -1) // Chỉ ra lệnh không phải CRUD (GRANT/REVOKE)
                    {
                        result = "Thao tác thành công.";
                    }
                    else
                    {
                        result = $"Thao tác thành công, {rowsAffected} dòng bị ảnh hưởng.";
                    }
                }
            }
            catch (OracleException ex)
            {
                result = $"Thất bại Oracle [{ex.Number}]: {ex.Message}";
                MessageBox.Show(result, "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                result = $"Thất bại: {ex.Message}";
                MessageBox.Show(result, "Lỗi thực thi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }

        // Bổ sung hàm ExecuteQuerySys (dùng cho PhanQuyen)
        public DataTable ExecuteQuerySys(string sql, OracleParameter[] parameters = null)
        {
            OracleConnection conn = Database.Get_ConnectSys();
            DataTable dt = new DataTable();

            if (conn == null) return dt; // Đã xử lý lỗi trong Get_ConnectSys

            try
            {
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }

                    using (OracleDataAdapter adapter = new OracleDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi ExecuteQuerySys: " + ex.Message);
            }
            return dt;
        }

        // Bổ sung hàm ExecuteNonQuerySys
        public string ExecuteNonQuerySys(string sql, OracleParameter[] parameters = null)
        {
            OracleConnection conn = Database.Get_ConnectSys();
            string result = "Thao tác thành công.";

            if (conn == null) return "Thất bại: Không có kết nối SYS.";

            try
            {
                using (OracleCommand cmd = new OracleCommand(sql, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OracleException ex)
            {
                result = $"Thất bại Oracle [{ex.Number}]: {ex.Message}";
                MessageBox.Show(result, "Lỗi thực thi SYS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                result = $"Thất bại: {ex.Message}";
                MessageBox.Show(result, "Lỗi thực thi SYS", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return result;
        }
    }
}