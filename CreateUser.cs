using Oracle.ManagedDataAccess.Client;
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
    public partial class CreateUser : Form
    {
        OracleConnection conn;
        Create_User u;

        public CreateUser()
        {
            InitializeComponent();
            CenterToScreen();

            // Lấy kết nối SYSUSER
            conn = Database.Get_ConnectSys();
            if (conn == null)
            {
                MessageBox.Show("Không thể kết nối đến database!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            u = new Create_User(conn);

            // Gán sự kiện FormClosing
            this.FormClosing += CreateUser_FormClosing;
        }

        private void CreateUser_FormClosing(object sender, FormClosingEventArgs e)
        {
            Database.Close_ConnectSYS();
        }

        private void btn_cr_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txt_user.Text))
            {
                MessageBox.Show("Chưa điền User Name!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_user.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txt_pass.Text))
            {
                MessageBox.Show("Chưa điền Password!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_pass.Focus();
                return;
            }

            try
            {
                int kq = u.Pro_CheckUser(txt_user.Text);

                if (kq == 0) // User không tồn tại
                {
                    if (u.Pro_CreateUser(txt_user.Text, txt_pass.Text))
                    {
                        MessageBox.Show($"Tạo tài khoản '{txt_user.Text}' thành công!",
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"Tạo tài khoản '{txt_user.Text}' thất bại!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (kq == 1) // User tồn tại và đang mở
                {
                    DialogResult res = MessageBox.Show(
                        $"Bạn có muốn thay đổi mật khẩu cho user '{txt_user.Text}'?",
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (res == DialogResult.Yes)
                    {
                        if (u.Pro_CreateUser(txt_user.Text, txt_pass.Text))
                        {
                            MessageBox.Show($"Đổi mật khẩu tài khoản '{txt_user.Text}' thành công!",
                                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show($"Đổi mật khẩu tài khoản '{txt_user.Text}' thất bại!",
                                "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else if (kq == 2) // User bị khóa
                {
                    MessageBox.Show($"User '{txt_user.Text}' đang bị khóa!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else // Lỗi khác
                {
                    MessageBox.Show("Có lỗi khi kiểm tra user!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
