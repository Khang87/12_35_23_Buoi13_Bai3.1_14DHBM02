using Oracle.ManagedDataAccess.Client;
using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

namespace _12_35_5_14DHBM02
{
    public static class Database
    {
        private static OracleConnection connSys;   // kết nối SYSUSER
        private static OracleConnection connUser;  // kết nối user thường

        private static string host = "localhost";
        private static string port = "1521";
        private static string sid = "orcl";   // mặc định orcl, đổi nếu cần ví dụ (ORLPDB)

        private static string user;
        private static string pass;
        private static string dbaPrivilege = "";

        // Lưu thông tin kết nối đầy đủ
        public static void Set_Database(string pHost, string pPort, string pSid, string pUser, string pPass, string pPrivilege = "")
        {
            host = pHost;
            port = pPort;
            sid = pSid;
            user = pUser;
            pass = pPass;
            dbaPrivilege = pPrivilege;
        }

        // Overload chỉ lưu user và pass (dùng khi đổi mật khẩu)
        public static void Set_Database(string pUser, string pPass)
        {
            user = pUser;
            pass = pPass;
        }

        // Kết nối SYSUSER
        // Kết nối SYSUSER với Easy Connect Syntax
        public static bool ConnectSys()
        {
            // Sử dụng Easy Connect Syntax: host:port/service_name
            string connStr = $"Data Source={host}:{port}/{sid};User Id={user};Password={pass};";

            // Hoặc thử với DESCRIPTION đầy đủ
            // string connStr = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={sid})));User Id={user};Password={pass};";

            Console.WriteLine("Connection String: " + connStr);

            try
            {
                connSys = new OracleConnection(connStr);
                connSys.Open();

                // Test query đơn giản
                using (OracleCommand cmd = new OracleCommand("SELECT 'Connected Successfully' FROM DUAL", connSys))
                {
                    string result = cmd.ExecuteScalar().ToString();
                    Console.WriteLine("Test Query Result: " + result);
                }

                return true;
            }
            catch (OracleException oraEx)
            {
                string errorMsg = $"Lỗi Oracle [{oraEx.Number}]: {oraEx.Message}";
                Console.WriteLine(errorMsg);

                // Thử với SID thay vì SERVICE_NAME
                if (oraEx.Number == 12514 || oraEx.Number == 12505)
                {
                    Console.WriteLine("Thử với SID thay vì SERVICE_NAME...");
                    string connStrWithSid = $"Data Source={host}:{port}/orcl2;User Id={user};Password={pass};";
                    try
                    {
                        connSys = new OracleConnection(connStrWithSid);
                        connSys.Open();
                        Console.WriteLine("Kết nối thành công với SID!");
                        return true;
                    }
                    catch (Exception ex2)
                    {
                        errorMsg += $"\nCũng không thể kết nối với SID: {ex2.Message}";
                    }
                }

                MessageBox.Show(errorMsg, "Lỗi kết nối Oracle",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                string errorMsg = $"Lỗi kết nối: {ex.Message}";
                Console.WriteLine(errorMsg);
                MessageBox.Show(errorMsg, "Lỗi kết nối",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Lấy kết nối SYSUSER
        public static OracleConnection Get_ConnectSys()
        {
            if (connSys == null || connSys.State != ConnectionState.Open)
            {
                if (!ConnectSys()) return null;
            }
            return connSys;
        }

        // Đóng kết nối SYSUSER
        public static void Close_ConnectSYS()
        {
            if (connSys != null && connSys.State == ConnectionState.Open)
            {
                connSys.Close();
            }
        }

        // Kết nối user thường
        public static bool Connect()
        {
            string connStr =
                $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={host})(PORT={port}))(CONNECT_DATA=(SERVICE_NAME={sid})));User Id={user};Password={pass};";

            if (dbaPrivilege.ToUpper() == "SYSDBA" || dbaPrivilege.ToUpper() == "SYSOPER")
            {
                connStr += $"DBA Privilege={dbaPrivilege};";
            }

            try
            {
                connUser = new OracleConnection(connStr);
                connUser.Open();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối user: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        // Lấy kết nối user thường
        public static OracleConnection Get_Connect()
        {
            return connUser;
        }

        // Kiểm tra trạng thái user
        public static string Get_StaTus(string user)
        {
            try
            {
                if (connSys == null || connSys.State != ConnectionState.Open)
                {
                    if (!ConnectSys()) return "";
                }

                // Sửa: Truy vấn trực tiếp từ dba_users thay vì gọi hàm không tồn tại
                string query = "SELECT account_status FROM dba_users WHERE username = :username";
                OracleCommand cmd = new OracleCommand(query, connSys);
                cmd.Parameters.Add("username", OracleDbType.Varchar2).Value = user.ToUpper();

                object result = cmd.ExecuteScalar();

                if (result != null)
                {
                    return result.ToString();
                }
                return "NOT_EXIST";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra trạng thái: " + ex.Message);
                return "ERROR";
            }
        }
    }
}
