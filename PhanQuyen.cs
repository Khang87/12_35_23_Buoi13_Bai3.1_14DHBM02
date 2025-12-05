using Oracle.ManagedDataAccess.Client;
using System.Data;
using System;
using System.Windows.Forms; 

// =================================================================
// TẠO CLASS PHAN QUYEN (Hình 47)
// =================================================================
public class PhanQuyen
{
    // Cần thay đổi chuỗi kết nối này bằng thông tin CSDL thực tế của bạn
    private string connectionString = "DATA SOURCE=YourOracleService;USER ID=YourUserID;PASSWORD=YourPassword;";

    // =================================================================
    // HÀM TRUY VẤN USER (Hình 48)
    // =================================================================
    public DataTable SelectAllUsers()
    {
        DataTable dt = new DataTable();
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục truy vấn User trong Package PL/SQL
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_SELECT_ALL_USER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn User: " + ex.Message);
            }
        }
        return dt;
    }

    // =================================================================
    // HÀM TRUY VẤN ROLE (Hình 49 - 50)
    // =================================================================
    public DataTable SelectAllRoles()
    {
        DataTable dt = new DataTable();
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục truy vấn tất cả Role
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_SELECT_ALL_ROLE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn Role: " + ex.Message);
            }
        }
        return dt;
    }

    // =================================================================
    // HÀM TRUY VẤN PROCEDURE, FUNCTION, PACKAGE DỰA TRÊN USER TRUYỀN VÀO (Hình 51 - 52)
    // =================================================================
    public DataTable SelectObjectBySchema(string schemaName)
    {
        DataTable dt = new DataTable();
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục truy vấn P/F/P theo Schema
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_SELECT_OBJECT_BY_SCHEMA", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("P_SCHEMA_NAME", OracleDbType.Varchar2, schemaName, ParameterDirection.Input);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn P/F/P theo Schema: " + ex.Message);
            }
        }
        return dt;
    }

    // =================================================================
    // HÀM TRUY VẤN TABLE (Hình 53 - 54)
    // =================================================================
    public DataTable SelectAllTables(string schemaName)
    {
        DataTable dt = new DataTable();
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục truy vấn tất cả Table theo Schema
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_SELECT_ALL_TABLE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("P_SCHEMA_NAME", OracleDbType.Varchar2, schemaName, ParameterDirection.Input);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn Table: " + ex.Message);
            }
        }
        return dt;
    }

    // =================================================================
    // HÀM TRUY VẤN TẤT CẢ ROLE ĐƯỢC GÁN CHO USER (Hình 55)
    // =================================================================
    public DataTable SelectRoleOfUser(string userName)
    {
        DataTable dt = new DataTable();
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục truy vấn Role đã được gán cho User
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_SELECT_ROLE_OF_USER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, userName, ParameterDirection.Input);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn Role của User: " + ex.Message);
            }
        }
        return dt;
    }

    // =================================================================
    // HÀM CHECK ROLE CÓ ĐƯỢC GÁN CHO USER KHÔNG (Hình 56)
    // =================================================================
    public bool CheckRoleOfUser(string userName, string roleName)
    {
        int result = 0;
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục/hàm kiểm tra Role
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.FUNC_CHECK_ROLE_OF_USER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, userName, ParameterDirection.Input);
                    command.Parameters.Add("P_ROLE_NAME", OracleDbType.Varchar2, roleName, ParameterDirection.Input);

                    // Khai báo biến trả về (giả sử thủ tục này là một Function trả về 1/0)
                    OracleParameter returnValue = new OracleParameter("P_RESULT_OUT", OracleDbType.Int32, ParameterDirection.ReturnValue);
                    command.Parameters.Add(returnValue);

                    command.ExecuteNonQuery();

                    if (returnValue.Value != null && returnValue.Value != DBNull.Value)
                    {
                        result = Convert.ToInt32(returnValue.Value.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kiểm tra Role của User: " + ex.Message);
            }
        }
        return result == 1;
    }

    // =================================================================
    // HÀM TRUY VẤN QUYỀN P/F/P ĐƯỢC GÁN CHO USER (Hình 57)
    // =================================================================
    public DataTable SelectObjectPrivsOfUser(string userName)
    {
        DataTable dt = new DataTable();
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục truy vấn quyền P/F/P của User
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_SELECT_OBJECT_PRIVS_OF_USER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, userName, ParameterDirection.Input);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn quyền P/F/P của User: " + ex.Message);
            }
        }
        return dt;
    }

    // =================================================================
    // HÀM TRUY VẤN QUYỀN CỦA TABLE ĐƯỢC GÁN CHO USER (Hình 58)
    // =================================================================
    public DataTable SelectTablePrivsOfUser(string userName, string tableName)
    {
        DataTable dt = new DataTable();
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục truy vấn quyền Table của User
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_SELECT_TABLE_PRIVS_OF_USER", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, userName, ParameterDirection.Input);
                    command.Parameters.Add("P_TABLE_NAME", OracleDbType.Varchar2, tableName, ParameterDirection.Input);

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy vấn quyền Table của User: " + ex.Message);
            }
        }
        return dt;
    }

    // =================================================================
    // HÀM GÁN VÀ HỦY QUYỀN CỦA P/F/P, TABLE DỰA VÀO BIẾN TRUYỀN VÀO (Hình 59)
    // =================================================================
    public string GrantRevokeObjectPrivs(string grantee, string objectName, string objectType, string privilege, string action)
    {
        string message = "";
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục Grant/Revoke quyền đối tượng
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_GRANT_REVOKE_OBJECT_PRIVS", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("P_GRANTEE", OracleDbType.Varchar2, grantee, ParameterDirection.Input); // User hoặc Role
                    command.Parameters.Add("P_OBJECT_NAME", OracleDbType.Varchar2, objectName, ParameterDirection.Input);
                    command.Parameters.Add("P_OBJECT_TYPE", OracleDbType.Varchar2, objectType, ParameterDirection.Input); // 'TABLE', 'PROCEDURE', 'FUNCTION', 'PACKAGE'
                    command.Parameters.Add("P_PRIVILEGE", OracleDbType.Varchar2, privilege, ParameterDirection.Input); // 'SELECT', 'EXECUTE', ...
                    command.Parameters.Add("P_ACTION", OracleDbType.Varchar2, action, ParameterDirection.Input); // 'GRANT' hoặc 'REVOKE'

                    OracleParameter outputMessage = new OracleParameter("P_MESSAGE_OUT", OracleDbType.Varchar2, 200, ParameterDirection.Output);
                    command.Parameters.Add(outputMessage);

                    command.ExecuteNonQuery();

                    message = outputMessage.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                message = "Lỗi gán/hủy quyền: " + ex.Message;
            }
        }
        return message;
    }

    // =================================================================
    // HÀM GÁN/HỦY ROLE CHO USER (Hình 60)
    // =================================================================
    public string GrantRevokeRoleToUser(string userName, string roleName, string action)
    {
        string message = "";
        using (OracleConnection connection = new OracleConnection(connectionString))
        {
            try
            {
                connection.Open();
                // Gọi thủ tục Grant/Revoke Role
                using (OracleCommand command = new OracleCommand("PKG_PHAN_QUYEN.PRC_GRANT_REVOKE_ROLE", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("P_USER_NAME", OracleDbType.Varchar2, userName, ParameterDirection.Input);
                    command.Parameters.Add("P_ROLE_NAME", OracleDbType.Varchar2, roleName, ParameterDirection.Input);
                    command.Parameters.Add("P_ACTION", OracleDbType.Varchar2, action, ParameterDirection.Input); // 'GRANT' hoặc 'REVOKE'

                    OracleParameter outputMessage = new OracleParameter("P_MESSAGE_OUT", OracleDbType.Varchar2, 200, ParameterDirection.Output);
                    command.Parameters.Add(outputMessage);

                    command.ExecuteNonQuery();

                    message = outputMessage.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                message = "Lỗi gán/hủy Role: " + ex.Message;
            }
        }
        return message;
    }
}