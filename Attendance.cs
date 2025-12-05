using Oracle.ManagedDataAccess.Client;
using System.Data;
using System;
using System.Windows.Forms;
using _12_35_5_14DHBM02; 
// Giả định Class DBConnect đã được định nghĩa
public class Attendance
{
    // Khai báo đối tượng kết nối và thực thi (Hình 102)
    private DBConnect db = new DBConnect();

    // =================================================================
    // CÁC HÀM XỬ LÝ DỮ LIỆU
    // =================================================================

    // Hàm Select (Hình 103)
    public DataTable Select()
    {
        string sql = "SELECT ATTENDANCEID, EMPLOYEEID, ATTENDANCEDATE, STATUS FROM ATTENDANCE";

        DataTable dt = db.ExecuteQuery(sql);

        return dt;
    }

    // Hàm Insert (Hình 104)
    public string Insert(string attendanceID, string employeeID, DateTime attendanceDate, string status)
    {
        string sql = "INSERT INTO ATTENDANCE (ATTENDANCEID, EMPLOYEEID, ATTENDANCEDATE, STATUS) " +
                     "VALUES (:p_attID, :p_empID, :p_date, :p_status)";

        OracleParameter[] parameters = new OracleParameter[]
        {
            new OracleParameter("p_attID", attendanceID),
            new OracleParameter("p_empID", employeeID),
            new OracleParameter("p_date", attendanceDate),
            new OracleParameter("p_status", status)
        };

        return db.ExecuteNonQuery(sql, parameters);
    }

    // Hàm Update (Hình 105)
    public string Update(string attendanceID, string employeeID, DateTime attendanceDate, string status)
    {
        string sql = "UPDATE ATTENDANCE SET " +
                     "EMPLOYEEID = :p_empID, " +
                     "ATTENDANCEDATE = :p_date, " +
                     "STATUS = :p_status " +
                     "WHERE ATTENDANCEID = :p_attID";

        OracleParameter[] parameters = new OracleParameter[]
        {
            new OracleParameter("p_empID", employeeID),
            new OracleParameter("p_date", attendanceDate),
            new OracleParameter("p_status", status),
            new OracleParameter("p_attID", attendanceID)
        };

        return db.ExecuteNonQuery(sql, parameters);
    }

    // Hàm Delete (Hình 106)
    public string Delete(string attendanceID)
    {
        string sql = "DELETE FROM ATTENDANCE WHERE ATTENDANCEID = :p_attID";

        OracleParameter[] parameters = new OracleParameter[]
        {
            new OracleParameter("p_attID", attendanceID)
        };

        return db.ExecuteNonQuery(sql, parameters);
    }
}