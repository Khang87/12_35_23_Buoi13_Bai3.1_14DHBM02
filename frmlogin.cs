using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12_35_5_14DHBM02
{
    public partial class frmlogin : Form
    {

        public frmlogin()
        {
            InitializeComponent();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ textbox
            string host = txt_host.Text.Trim();
            string port = txt_port.Text.Trim();
            string sid = txt_sid.Text.Trim();  // orclpdb (chữ thường)

            // Kiểm tra thông tin nhập
            if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(port) || string.IsNullOrWhiteSpace(sid))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin Host, Port và SID!",
                    "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Hiển thị thông tin debug
            Console.WriteLine($"Trying to connect: {host}:{port}/{sid}");

            // Lưu thông tin kết nối - KHÔNG dùng SYSDBA cho SYSUSER
            Database.Set_Database(host, port, sid, "SYSUSER", "SYSUSER", "");

            // Thử kết nối
            if (Database.ConnectSys())
            {
                MessageBox.Show($"Kết nối SYSUSER thành công!\n\n" +
                               $"Host: {host}\nPort: {port}\nSID: {sid}",
                               "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Hide(); // Ẩn form login
                CreateUser createForm = new CreateUser();
                createForm.Show(); // Mở form CreateUser
            }
            else
            {
                // Gợi ý thử với SID khác
                DialogResult result = MessageBox.Show(
                    "Không thể kết nối với SERVICE_NAME '" + sid + "'.\n\n" +
                    "Bạn có muốn thử với:\n" +
                    "1. SID 'orcl2' (CDB)?\n" +
                    "2. Hoặc kiểm tra lại thông tin?",
                    "Lỗi kết nối",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Thử với SID orcl2
                    txt_sid.Text = "orcl2";
                    btn_create_Click(sender, e); // Gọi lại chính nó
                }
            }
        }


        public string host, port, sid, user, password;

        private void btn_login_Click_1(object sender, EventArgs e)
        {
            host = txt_host.Text;
            port = txt_port.Text;
            sid = txt_sid.Text;
            user = txt_user.Text;
            password = txt_password.Text;

            if (!Check_Textbox(host, port, sid, user, password))
                return;

            // Kiểm tra nếu là SYS thì dùng SYSDBA, còn lại không dùng privilege
            string privilege = "";
            if (user.ToUpper() == "SYS" || user.ToUpper() == "SYSUSER")
            {
                privilege = "SYSDBA";
            }

            // Lưu thông tin kết nối
            Database.Set_Database(host, port, sid, user, password, privilege);

            if (Database.Connect())
            {
                MessageBox.Show("Đăng nhập thành công!");

                // Đóng form login
                this.Hide();

                // Ở đây bạn có thể mở form chính tùy theo user
                // Ví dụ: new MainForm().Show();
            }
            else
            {
                // Kiểm tra trạng thái user
                Check_Status(user);
            }
        }

        private void Check_Status(string user)
        {
            string status = Database.Get_StaTus(user);

            if (status == "LOCKED" || status == "LOCKED(TIMED)")
            {
                MessageBox.Show("Tài khoản bị khóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (status == "EXPIRED(GRACE)")
            {
                MessageBox.Show("Tài khoản sắp hết hạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (status == "EXPIRED & LOCKED(TIMED)")
            {
                MessageBox.Show("Tài khoản bị khóa do hết hạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (status == "EXPIRED")
            {
                MessageBox.Show("Tài khoản đã hết hạn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (status == "")
            {
                MessageBox.Show("Tài khoản không tồn tại hoặc không thể kiểm tra trạng thái", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại! Xem lại thông tin đăng nhập: UserName, Password", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Check_Textbox(string host, string port, string sid, string user, string pass)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ Host.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(port))
            {
                MessageBox.Show("Vui lòng nhập Port.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(sid))
            {
                MessageBox.Show("Vui lòng nhập SID.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(user))
            {
                MessageBox.Show("Vui lòng nhập User.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Vui lòng nhập Password.", "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            host = txt_host.Text;
            port = txt_port.Text;
            sid = txt_sid.Text;
            user = txt_user.Text;
            password = txt_password.Text;

            if (!Check_Textbox(host, port, sid, user, password))
                return;

            string privilege = (user.ToUpper() == "SYS") ? "SYSDBA" : "";

            // Lưu thông tin kết nối user thường với SID
            Database.Set_Database(host, port, sid, user, password, privilege);

            if (Database.Connect())
            {
                MessageBox.Show("Đăng nhập thành công");               
                this.Hide();
            }
            else
            {
                Check_Status(user);
            }

        }

    }
}
