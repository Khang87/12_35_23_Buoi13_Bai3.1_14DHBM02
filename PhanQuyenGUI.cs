using Oracle.ManagedDataAccess.Client;
using System.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace _12_35_5_14DHBM02// Thay thế bằng namespace của project bạn
{
    public partial class PhanQuyenGUI : Form
    {
        // =================================================================
        // KHAI BÁO CÁC BIẾN CẦN THIẾT (Hình 65)
        // =================================================================
        private PhanQuyen pq = new PhanQuyen();
        private DataTable dtUser;
        private DataTable dtRole;
        private DataTable dtTable;
        private DataTable dtPFP; // Procedure, Function, Package

        private string selectedUser = string.Empty;
        private string selectedRole = string.Empty;
        private string selectedTable = string.Empty;
        private string selectedProcedure = string.Empty;
        private string selectedFunction = string.Empty;
        private string selectedPackage = string.Empty;
        private string defaultSchema = "DEFAULT_SCHEMA_HERE"; // Thay thế bằng Schema mặc định

        public PhanQuyenGUI()
        {
            InitializeComponent();
            PhanQuyenGUI_Load(null, null);
        }

        private void PhanQuyenGUI_Load(object sender, EventArgs e)
        {
            // Cài đặt màu sắc ban đầu
            SetTableCheckBoxColor_User(); // Hình 66
            SetTableCheckBoxColor_Role(); // Hình 67
            SetPFPCheckBoxColor_User();   // Hình 68
            SetPFPCheckBoxColor_Role();   // Hình 69

            // Giả định ComboBox Schema có sẵn
            cbbUserSchema.Items.Add(defaultSchema);
            cbbUserSchema.SelectedIndex = 0;

            // Load dữ liệu
            LoadUserIntoComboBox(); // Hình 72
            LoadRoleIntoComboBox(); // Hình 73
            LoadPFPIntoComboBox(defaultSchema); // Hình 75

            // Hàm Load tất cả các quyền vào DatagridVew của User và Role (Hình 76)
            // (Không có mã code cụ thể cho hàm này, giả định việc này xảy ra trong LoadUser/LoadRole)
            // Cập nhật dgvRole và dgvUser rỗng khi form load lần đầu
            dgvRole.DataSource = null;
            dgvUser.DataSource = null;
        }

        // =================================================================
        // CÁC HÀM THIẾT LẬP MÀU (Hình 66 - 69)
        // =================================================================
        private void SetTableCheckBoxColor_User() // Hình 66
        {
            cbSelect_User.BackColor = Color.LightGreen;
            cbInsert_User.BackColor = Color.LightGreen;
            cbUpdate_User.BackColor = Color.LightGreen;
            cbDelete_User.BackColor = Color.LightGreen;
        }

        private void SetTableCheckBoxColor_Role() // Hình 67
        {
            cbSelect_Role.BackColor = Color.LightBlue;
            cbInsert_Role.BackColor = Color.LightBlue;
            cbUpdate_Role.BackColor = Color.LightBlue;
            cbDelete_Role.BackColor = Color.LightBlue;
        }

        private void SetPFPCheckBoxColor_User() // Hình 68
        {
            cbExecute_User.BackColor = Color.LightGreen;
        }

        private void SetPFPCheckBoxColor_Role() // Hình 69
        {
            cbExecute_Role.BackColor = Color.LightBlue;
        }

        // =================================================================
        // HÀM GÁN TÊN BẢNG VÀO LABEL (Hình 70)
        // =================================================================
        private void SetTableNameLabel(string tableName) // Hình 70
        {
            lblTableName.Text = "Tên bảng đang chọn: " + tableName;
        }

        // =================================================================
        // HÀM THIẾT LẬP TEXT TRÊN NÚT GÁN/HỦY ROLE CHO USER (Hình 71)
        // =================================================================
        private void SetGrantRevokeRoleButtonText(string userName, string roleName) // Hình 71
        {
            if (pq.CheckRoleOfUser(userName, roleName))
            {
                btnGrantRevokeRoleUser.Text = "Hủy gán Role cho User";
            }
            else
            {
                btnGrantRevokeRoleUser.Text = "Gán Role cho User";
            }
        }

        // =================================================================
        // HÀM LOAD USER VÀO COMBOBOX (Hình 72)
        // =================================================================
        private void LoadUserIntoComboBox() // Hình 72
        {
            dtUser = pq.SelectAllUsers();
            cbbUser.DataSource = dtUser;
            cbbUser.DisplayMember = "USERNAME";
            cbbUser.ValueMember = "USERNAME";
            ResetComboBox(cbbUser);
        }

        // =================================================================
        // HÀM LOAD ROLE VÀO COMBOBOX (Hình 73)
        // =================================================================
        private void LoadRoleIntoComboBox() // Hình 73
        {
            dtRole = pq.SelectAllRoles();
            cbbRole.DataSource = dtRole;
            cbbRole.DisplayMember = "ROLENAME";
            cbbRole.ValueMember = "ROLENAME";
            ResetComboBox(cbbRole);
        }

        // =================================================================
        // HÀM XÓA VÀ CHỌN COMBOBOX (Hình 74)
        // =================================================================
        private void ResetComboBox(ComboBox cbb) // Hình 74
        {
            cbb.SelectedIndex = -1;
            cbb.Text = string.Empty;

            if (cbb.Items.Count > 0)
            {
                cbb.SelectedIndex = 0;
            }
        }

        // =================================================================
        // HÀM LOAD PFP VÀO COMBOBOX (Hình 75)
        // =================================================================
        private void LoadPFPIntoComboBox(string schemaName) // Hình 75
        {
            dtPFP = pq.SelectObjectBySchema(schemaName);

            // Lọc Procedure
            DataRow[] procRows = dtPFP.Select("OBJECT_TYPE = 'PROCEDURE'");
            DataTable dtProc = (procRows.Length > 0) ? procRows.CopyToDataTable() : dtPFP.Clone();
            cbbProcedure.DataSource = dtProc;
            cbbProcedure.DisplayMember = "OBJECT_NAME";
            cbbProcedure.ValueMember = "OBJECT_NAME";
            ResetComboBox(cbbProcedure);

            // Lọc Function
            DataRow[] funcRows = dtPFP.Select("OBJECT_TYPE = 'FUNCTION'");
            DataTable dtFunc = (funcRows.Length > 0) ? funcRows.CopyToDataTable() : dtPFP.Clone();
            cbbFunction.DataSource = dtFunc;
            cbbFunction.DisplayMember = "OBJECT_NAME";
            cbbFunction.ValueMember = "OBJECT_NAME";
            ResetComboBox(cbbFunction);

            // Lọc Package
            DataRow[] packRows = dtPFP.Select("OBJECT_TYPE = 'PACKAGE'");
            DataTable dtPack = (packRows.Length > 0) ? packRows.CopyToDataTable() : dtPFP.Clone();
            cbbPackage.DataSource = dtPack;
            cbbPackage.DisplayMember = "OBJECT_NAME";
            cbbPackage.ValueMember = "OBJECT_NAME";
            ResetComboBox(cbbPackage);
        }

        // =================================================================
        // HÀM LOAD QUYỀN TABLE CỦA USER (Hình 77)
        // =================================================================
        private void LoadTablePrivsOfUser() // Hình 77
        {
            // Bỏ qua sự kiện CheckedChanged khi set giá trị
            cbSelect_User.CheckedChanged -= cbTablePrivs_User_CheckedChanged;
            cbInsert_User.CheckedChanged -= cbTablePrivs_User_CheckedChanged;
            cbUpdate_User.CheckedChanged -= cbTablePrivs_User_CheckedChanged;
            cbDelete_User.CheckedChanged -= cbTablePrivs_User_CheckedChanged;

            cbSelect_User.Checked = false;
            cbInsert_User.Checked = false;
            cbUpdate_User.Checked = false;
            cbDelete_User.Checked = false;

            if (!string.IsNullOrEmpty(selectedUser) && !string.IsNullOrEmpty(selectedTable))
            {
                DataTable dtPrivs = pq.SelectTablePrivsOfUser(selectedUser, selectedTable);

                foreach (DataRow row in dtPrivs.Rows)
                {
                    string priv = row["PRIVILEGE"].ToString().ToUpper();
                    if (priv == "SELECT") cbSelect_User.Checked = true;
                    if (priv == "INSERT") cbInsert_User.Checked = true;
                    if (priv == "UPDATE") cbUpdate_User.Checked = true;
                    if (priv == "DELETE") cbDelete_User.Checked = true;
                }
            }

            cbSelect_User.CheckedChanged += cbTablePrivs_User_CheckedChanged;
            cbInsert_User.CheckedChanged += cbTablePrivs_User_CheckedChanged;
            cbUpdate_User.CheckedChanged += cbTablePrivs_User_CheckedChanged;
            cbDelete_User.CheckedChanged += cbTablePrivs_User_CheckedChanged;
        }

        // =================================================================
        // HÀM LOAD QUYỀN TABLE CỦA ROLE (Hình 78)
        // =================================================================
        private void LoadTablePrivsOfRole() // Hình 78
        {
            // Bỏ qua sự kiện CheckedChanged khi set giá trị
            cbSelect_Role.CheckedChanged -= cbTablePrivs_Role_CheckedChanged;
            cbInsert_Role.CheckedChanged -= cbTablePrivs_Role_CheckedChanged;
            cbUpdate_Role.CheckedChanged -= cbTablePrivs_Role_CheckedChanged;
            cbDelete_Role.CheckedChanged -= cbTablePrivs_Role_CheckedChanged;

            cbSelect_Role.Checked = false;
            cbInsert_Role.Checked = false;
            cbUpdate_Role.Checked = false;
            cbDelete_Role.Checked = false;

            if (!string.IsNullOrEmpty(selectedRole) && !string.IsNullOrEmpty(selectedTable))
            {
                // Sử dụng hàm truy vấn quyền của User (vì Oracle lưu trữ quyền Role và User trong cùng một tập hợp view)
                DataTable dtPrivs = pq.SelectTablePrivsOfUser(selectedRole, selectedTable);

                foreach (DataRow row in dtPrivs.Rows)
                {
                    string priv = row["PRIVILEGE"].ToString().ToUpper();
                    if (priv == "SELECT") cbSelect_Role.Checked = true;
                    if (priv == "INSERT") cbInsert_Role.Checked = true;
                    if (priv == "UPDATE") cbUpdate_Role.Checked = true;
                    if (priv == "DELETE") cbDelete_Role.Checked = true;
                }
            }

            cbSelect_Role.CheckedChanged += cbTablePrivs_Role_CheckedChanged;
            cbInsert_Role.CheckedChanged += cbTablePrivs_Role_CheckedChanged;
            cbUpdate_Role.CheckedChanged += cbTablePrivs_Role_CheckedChanged;
            cbDelete_Role.CheckedChanged += cbTablePrivs_Role_CheckedChanged;
        }

        // =================================================================
        // HÀM LOAD QUYỀN EXECUTE CỦA USER/ROLE (Hình 79)
        // =================================================================
        private void LoadExecutePrivs(string entityName, string objectName, string objectType, CheckBox cb) // Hình 79
        {
            // Tạm thời loại bỏ sự kiện CheckedChanged
            if (cb.Name.Contains("User")) cb.CheckedChanged -= cbExecute_User_CheckedChanged;
            else cb.CheckedChanged -= cbExecute_Role_CheckedChanged;

            cb.Checked = false;

            if (string.IsNullOrEmpty(entityName) || string.IsNullOrEmpty(objectName))
            {
                if (cb.Name.Contains("User")) cb.CheckedChanged += cbExecute_User_CheckedChanged;
                else cb.CheckedChanged += cbExecute_Role_CheckedChanged;
                return;
            }

            DataTable dtPrivs = pq.SelectObjectPrivsOfUser(entityName); // Giả định hàm này trả về tất cả quyền execute cho P/F/P

            DataRow[] rows = dtPrivs.Select(string.Format("OBJECT_NAME = '{0}' AND OBJECT_TYPE = '{1}' AND PRIVILEGE = 'EXECUTE'", objectName, objectType));

            if (rows.Length > 0)
            {
                cb.Checked = true;
            }

            // Gán lại sự kiện CheckedChanged
            if (cb.Name.Contains("User")) cb.CheckedChanged += cbExecute_User_CheckedChanged;
            else cb.CheckedChanged += cbExecute_Role_CheckedChanged;
        }

        // =================================================================
        // SỰ KIỆN CHỌN COMBOBOX SCHEMA (Hình 80)
        // =================================================================
        private void cbbSchema_SelectedIndexChanged(object sender, EventArgs e) // Hình 80
        {
            if (cbbUserSchema.SelectedValue != null)
            {
                string schemaName = cbbUserSchema.SelectedValue.ToString();

                // Load Table
                dtTable = pq.SelectAllTables(schemaName);
                cbbTable.DataSource = dtTable;
                cbbTable.DisplayMember = "TABLE_NAME";
                cbbTable.ValueMember = "TABLE_NAME";
                ResetComboBox(cbbTable);

                // Load P/F/P
                LoadPFPIntoComboBox(schemaName);
            }
        }

        // =================================================================
        // SỰ KIỆN CHỌN COMBOBOX USER NAME (Hình 81-82)
        // =================================================================
        private void cbbUser_SelectedIndexChanged(object sender, EventArgs e) // Hình 81-82
        {
            if (cbbUser.SelectedValue != null)
            {
                selectedUser = cbbUser.SelectedValue.ToString();

                // Tải lại quyền Role của User (Hình 82)
                dgvUser.DataSource = pq.SelectRoleOfUser(selectedUser);

                // Tải lại quyền Table và PFP của User nếu đang chọn
                LoadTablePrivsOfUser();
                // Cần xác định loại PFP đang được chọn để tải quyền Execute
                if (!string.IsNullOrEmpty(selectedProcedure)) LoadExecutePrivs(selectedUser, selectedProcedure, "PROCEDURE", cbExecute_User);
                else if (!string.IsNullOrEmpty(selectedFunction)) LoadExecutePrivs(selectedUser, selectedFunction, "FUNCTION", cbExecute_User);
                else if (!string.IsNullOrEmpty(selectedPackage)) LoadExecutePrivs(selectedUser, selectedPackage, "PACKAGE", cbExecute_User);

                // Cập nhật nút gán/hủy Role (Hình 71)
                if (!string.IsNullOrEmpty(selectedRole))
                {
                    SetGrantRevokeRoleButtonText(selectedUser, selectedRole);
                }
            }
            else
            {
                selectedUser = string.Empty;
                dgvUser.DataSource = null;
                // Vô hiệu hóa/Xóa tick các checkbox User
                LoadTablePrivsOfUser();
                cbExecute_User.Checked = false;
            }
        }

        // =================================================================
        // SỰ KIỆN CHỌN COMBOBOX TABLE (Hình 83)
        // =================================================================
        private void cbbTable_SelectedIndexChanged(object sender, EventArgs e) // Hình 83
        {
            if (cbbTable.SelectedValue != null)
            {
                selectedTable = cbbTable.SelectedValue.ToString();
                SetTableNameLabel(selectedTable); // Hình 70

                // Tải lại quyền Table
                LoadTablePrivsOfUser();
                LoadTablePrivsOfRole();
            }
            else
            {
                selectedTable = string.Empty;
                lblTableName.Text = "Tên bảng đang chọn: (Chưa chọn)";
            }
        }

        // =================================================================
        // SỰ KIỆN CHỌN COMBOBOX ROLE (Hình 84)
        // =================================================================
        private void cbbRole_SelectedIndexChanged(object sender, EventArgs e) // Hình 84
        {
            if (cbbRole.SelectedValue != null)
            {
                selectedRole = cbbRole.SelectedValue.ToString();

                // Tải lại quyền Table
                LoadTablePrivsOfRole();

                // Tải lại quyền PFP
                if (!string.IsNullOrEmpty(selectedProcedure)) LoadExecutePrivs(selectedRole, selectedProcedure, "PROCEDURE", cbExecute_Role);
                else if (!string.IsNullOrEmpty(selectedFunction)) LoadExecutePrivs(selectedRole, selectedFunction, "FUNCTION", cbExecute_Role);
                else if (!string.IsNullOrEmpty(selectedPackage)) LoadExecutePrivs(selectedRole, selectedPackage, "PACKAGE", cbExecute_Role);

                // Cập nhật nút gán/hủy Role (Hình 71)
                if (!string.IsNullOrEmpty(selectedUser))
                {
                    SetGrantRevokeRoleButtonText(selectedUser, selectedRole);
                }
            }
            else
            {
                selectedRole = string.Empty;
                btnGrantRevokeRoleUser.Text = "Gán/Hủy Role cho User";
                // Vô hiệu hóa/Xóa tick các checkbox Role
                LoadTablePrivsOfRole();
                cbExecute_Role.Checked = false;
            }
        }

        // =================================================================
        // SỰ KIỆN CHỌN COMBOBOX PROCEDURE (Hình 85)
        // =================================================================
        private void cbbProcedure_SelectedIndexChanged(object sender, EventArgs e) // Hình 85
        {
            if (cbbProcedure.SelectedValue != null)
            {
                selectedProcedure = cbbProcedure.SelectedValue.ToString();
                selectedFunction = string.Empty;
                selectedPackage = string.Empty;
                LoadExecutePrivs(selectedUser, selectedProcedure, "PROCEDURE", cbExecute_User);
                LoadExecutePrivs(selectedRole, selectedProcedure, "PROCEDURE", cbExecute_Role);
            }
            else
            {
                selectedProcedure = string.Empty;
            }
        }

        // =================================================================
        // SỰ KIỆN CHỌN COMBOBOX FUNCTION (Hình 86)
        // =================================================================
        private void cbbFunction_SelectedIndexChanged(object sender, EventArgs e) // Hình 86
        {
            if (cbbFunction.SelectedValue != null)
            {
                selectedFunction = cbbFunction.SelectedValue.ToString();
                selectedProcedure = string.Empty;
                selectedPackage = string.Empty;
                LoadExecutePrivs(selectedUser, selectedFunction, "FUNCTION", cbExecute_User);
                LoadExecutePrivs(selectedRole, selectedFunction, "FUNCTION", cbExecute_Role);
            }
            else
            {
                selectedFunction = string.Empty;
            }
        }

        // =================================================================
        // SỰ KIỆN CHỌN COMBOBOX PACKAGE (Hình 87)
        // =================================================================
        private void cbbPackage_SelectedIndexChanged(object sender, EventArgs e) // Hình 87
        {
            if (cbbPackage.SelectedValue != null)
            {
                selectedPackage = cbbPackage.SelectedValue.ToString();
                selectedProcedure = string.Empty;
                selectedFunction = string.Empty;
                LoadExecutePrivs(selectedUser, selectedPackage, "PACKAGE", cbExecute_User);
                LoadExecutePrivs(selectedRole, selectedPackage, "PACKAGE", cbExecute_Role);
            }
            else
            {
                selectedPackage = string.Empty;
            }
        }

        // =================================================================
        // HÀM GÁN/HỦY QUYỀN CHÍNH (Hình 88-89)
        // =================================================================
        private void ExecuteGrantRevokePrivs(string grantee, string objectName, string objectType, string privilege, string action) // Hình 88-89
        {
            string result = pq.GrantRevokeObjectPrivs(grantee, objectName, objectType, privilege, action);
            MessageBox.Show(result, "Thông báo");
        }

        // =================================================================
        // HÀM LẤY DỮ LIỆU VÀ GỌI HÀM GÁN/HỦY QUYỀN P/F/P (Hình 92-93)
        // =================================================================
        private void HandlePFPUserPrivilege(string objectName, string objectType, bool isChecked) // Hình 92
        {
            if (string.IsNullOrEmpty(selectedUser) || string.IsNullOrEmpty(objectName)) return;

            string action = isChecked ? "GRANT" : "REVOKE";
            ExecuteGrantRevokePrivs(selectedUser, objectName, objectType, "EXECUTE", action);
        }

        private void HandlePFPRolePrivilege(string objectName, string objectType, bool isChecked) // Hình 93
        {
            if (string.IsNullOrEmpty(selectedRole) || string.IsNullOrEmpty(objectName)) return;

            string action = isChecked ? "GRANT" : "REVOKE";
            ExecuteGrantRevokePrivs(selectedRole, objectName, objectType, "EXECUTE", action);
        }

        // =================================================================
        // HÀM LẤY DỮ LIỆU VÀ GỌI HÀM GÁN/HỦY QUYỀN TABLE (Hình 94-95)
        // =================================================================
        private void HandleTableUserPrivilege(string privilege, bool isChecked) // Hình 94
        {
            if (string.IsNullOrEmpty(selectedUser) || string.IsNullOrEmpty(selectedTable)) return;

            string action = isChecked ? "GRANT" : "REVOKE";
            ExecuteGrantRevokePrivs(selectedUser, selectedTable, "TABLE", privilege, action);
        }

        private void HandleTableRolePrivilege(string privilege, bool isChecked) // Hình 95
        {
            if (string.IsNullOrEmpty(selectedRole) || string.IsNullOrEmpty(selectedTable)) return;

            string action = isChecked ? "GRANT" : "REVOKE";
            ExecuteGrantRevokePrivs(selectedRole, selectedTable, "TABLE", privilege, action);
        }

        // =================================================================
        // SỰ KIỆN TICK CHECKBOX GÁN QUYỀN P/F/P (Hình 96-98)
        // =================================================================
        private void cbExecute_User_CheckedChanged(object sender, EventArgs e) // Hình 96-98
        {
            if (!((CheckBox)sender).Focused) return;

            bool isChecked = ((CheckBox)sender).Checked;

            // Xử lý Procedure (Hình 96)
            if (!string.IsNullOrEmpty(selectedProcedure))
            {
                HandlePFPUserPrivilege(selectedProcedure, "PROCEDURE", isChecked);
            }
            // Xử lý Function (Hình 97)
            else if (!string.IsNullOrEmpty(selectedFunction))
            {
                HandlePFPUserPrivilege(selectedFunction, "FUNCTION", isChecked);
            }
            // Xử lý Package (Hình 98)
            else if (!string.IsNullOrEmpty(selectedPackage))
            {
                HandlePFPUserPrivilege(selectedPackage, "PACKAGE", isChecked);
            }
        }

        private void cbExecute_Role_CheckedChanged(object sender, EventArgs e) // Tương tự User
        {
            if (!((CheckBox)sender).Focused) return;

            bool isChecked = ((CheckBox)sender).Checked;

            if (!string.IsNullOrEmpty(selectedProcedure))
            {
                HandlePFPRolePrivilege(selectedProcedure, "PROCEDURE", isChecked);
            }
            else if (!string.IsNullOrEmpty(selectedFunction))
            {
                HandlePFPRolePrivilege(selectedFunction, "FUNCTION", isChecked);
            }
            else if (!string.IsNullOrEmpty(selectedPackage))
            {
                HandlePFPRolePrivilege(selectedPackage, "PACKAGE", isChecked);
            }
        }

        // =================================================================
        // SỰ KIỆN TICK CHECKBOX GÁN QUYỀN TABLE (Hình 99-100)
        // =================================================================
        private void cbTablePrivs_User_CheckedChanged(object sender, EventArgs e) // Hình 99
        {
            if (!((CheckBox)sender).Focused) return;

            CheckBox cb = (CheckBox)sender;
            string priv = cb.Tag.ToString();
            HandleTableUserPrivilege(priv, cb.Checked);

            // Sau khi gán/hủy quyền cho User, cần cập nhật lại quyền Role nếu có thay đổi
            // (Thường không cần thiết trừ khi có WITH GRANT OPTION)
            // LoadTablePrivsOfRole(); 
        }

        private void cbTablePrivs_Role_CheckedChanged(object sender, EventArgs e) // Hình 100
        {
            if (!((CheckBox)sender).Focused) return;

            CheckBox cb = (CheckBox)sender;
            string priv = cb.Tag.ToString();
            HandleTableRolePrivilege(priv, cb.Checked);
        }

        // =================================================================
        // SỰ KIỆN CLICK NÚT GÁN/HỦY ROLE CHO USER (Hình 101)
        // =================================================================
        private void btnGrantRevokeRoleUser_Click(object sender, EventArgs e) // Hình 101
        {
            if (string.IsNullOrEmpty(selectedUser) || string.IsNullOrEmpty(selectedRole))
            {
                MessageBox.Show("Vui lòng chọn User và Role.", "Lỗi");
                return;
            }

            // Hàm gán/hủy gán Role cho User (Hình 90-91)
            string action = (btnGrantRevokeRoleUser.Text.Contains("Gán")) ? "GRANT" : "REVOKE";
            string result = pq.GrantRevokeRoleToUser(selectedUser, selectedRole, action);
            MessageBox.Show(result, "Thông báo");

            // Cập nhật lại trạng thái nút và DataGridView
            SetGrantRevokeRoleButtonText(selectedUser, selectedRole); // Hình 71
            dgvUser.DataSource = pq.SelectRoleOfUser(selectedUser); // Hình 82
        }
    }
}