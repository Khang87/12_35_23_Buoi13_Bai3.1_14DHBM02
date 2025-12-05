using System;
using System.Data;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Windows.Forms;

namespace _12_35_5_14DHBM02
{
    public class Create_User
    {
        OracleConnection conn;

        public Create_User(OracleConnection conn)
        {
            this.conn = conn;
        }

        // Hàm kiểm tra user đã tồn tại chưa
        public int Pro_CheckUser(string UserName)
        {
            try
            {
                // Đảm bảo gọi đúng package (sys.pkg_cruser)
                string functionName = "sys.pkg_cruser.fun_check_account";

                using (OracleCommand cmd = new OracleCommand(functionName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter resultParam = new OracleParameter();
                    resultParam.ParameterName = "Result";
                    resultParam.OracleDbType = OracleDbType.Int32;
                    resultParam.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(resultParam);

                    OracleParameter userParam = new OracleParameter();
                    userParam.ParameterName = "user";
                    userParam.OracleDbType = OracleDbType.Varchar2;
                    userParam.Value = UserName.ToUpper();
                    userParam.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(userParam);

                    cmd.ExecuteNonQuery();

                    if (resultParam.Value != null && resultParam.Value != DBNull.Value)
                    {
                        // ODP.NET returns OracleDecimal for numeric return values
                        OracleDecimal dec = (OracleDecimal)resultParam.Value;
                        if (!dec.IsNull)
                        {
                            return dec.ToInt32();
                        }
                    }
                    return -1;
                }
            }
            catch (OracleException oraEx)
            {
                MessageBox.Show($"Lỗi Oracle: {oraEx.Message}\nCode: {oraEx.Number}",
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        // Hàm tạo hoặc đổi mật khẩu user
        public bool Pro_CreateUser(string UserName, string PassWord)
        {
            try
            {
                // Đảm bảo gọi đúng package (sys.pkg_cruser)
                string procedureName = "sys.pkg_cruser.Pro_CrUser";

                using (OracleCommand cmd = new OracleCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    OracleParameter userParam = new OracleParameter();
                    userParam.ParameterName = "username";
                    userParam.OracleDbType = OracleDbType.Varchar2;
                    userParam.Value = UserName.ToUpper();
                    userParam.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(userParam);

                    OracleParameter passParam = new OracleParameter();
                    passParam.ParameterName = "pass";
                    passParam.OracleDbType = OracleDbType.Varchar2;
                    passParam.Value = PassWord;
                    passParam.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(passParam);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (OracleException oraEx)
            {
                MessageBox.Show($"Lỗi Oracle: {oraEx.Message}\nCode: {oraEx.Number}",
                    "Lỗi Database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}