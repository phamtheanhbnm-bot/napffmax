
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalMDI
{
    public class frmHospitalMain : Form
    {
        private MenuStrip menuStrip;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel lblDoctor;
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar progressBar;

        public frmHospitalMain()
        {
            InitializeHospitalForm();
            CreateMenu();
            CreateStatusBar();
        }

        private void InitializeHospitalForm()
        {
            Text = "HỆ THỐNG QUẢN LÝ BỆNH VIỆN";
            IsMdiContainer = true;
            WindowState = FormWindowState.Maximized;
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.WhiteSmoke;
            MainMenuStrip = menuStrip;
        }

        private void CreateMenu()
        {
            menuStrip = new MenuStrip();

            ToolStripMenuItem menuSystem = new ToolStripMenuItem("Hệ thống");
            ToolStripMenuItem menuLogout = new ToolStripMenuItem("Đăng xuất");
            ToolStripMenuItem menuReload = new ToolStripMenuItem("Nạp lại dữ liệu");
            ToolStripMenuItem menuExit = new ToolStripMenuItem("Thoát");

            menuLogout.Click += (s, e) => Logout();
            menuReload.Click += (s, e) => ReloadData();
            menuExit.Click += (s, e) => Close();

            menuSystem.DropDownItems.Add(menuLogout);
            menuSystem.DropDownItems.Add(menuReload);
            menuSystem.DropDownItems.Add(new ToolStripSeparator());
            menuSystem.DropDownItems.Add(menuExit);

            ToolStripMenuItem menuManage = new ToolStripMenuItem("Quản lý");
            ToolStripMenuItem menuPatients = new ToolStripMenuItem("Hồ sơ Bệnh nhân");
            ToolStripMenuItem menuDoctors = new ToolStripMenuItem("Danh mục Bác sĩ");
            ToolStripMenuItem menuDepartments = new ToolStripMenuItem("Danh mục Khoa/Phòng");

            menuPatients.Click += (s, e) => OpenChild<frmPatientProfiles>();
            menuDoctors.Click += (s, e) => OpenChild<frmDoctorSchedule>();
            menuDepartments.Click += (s, e) => OpenDepartments();

            menuManage.DropDownItems.Add(menuPatients);
            menuManage.DropDownItems.Add(menuDoctors);
            menuManage.DropDownItems.Add(menuDepartments);

            ToolStripMenuItem menuWindow = new ToolStripMenuItem("Cửa sổ");
            ToolStripMenuItem menuCascade = new ToolStripMenuItem("Xếp tầng");
            ToolStripMenuItem menuHorizontal = new ToolStripMenuItem("Ngang");
            ToolStripMenuItem menuVertical = new ToolStripMenuItem("Dọc");
            ToolStripMenuItem menuCloseAll = new ToolStripMenuItem("Đóng tất cả");

            menuCascade.Click += (s, e) =>
                LayoutMdi(MdiLayout.Cascade);

            menuHorizontal.Click += (s, e) =>
                LayoutMdi(MdiLayout.TileHorizontal);

            menuVertical.Click += (s, e) =>
                LayoutMdi(MdiLayout.TileVertical);

            menuCloseAll.Click += (s, e) => CloseAllChildren();

            menuWindow.DropDownItems.Add(menuCascade);
            menuWindow.DropDownItems.Add(menuHorizontal);
            menuWindow.DropDownItems.Add(menuVertical);
            menuWindow.DropDownItems.Add(new ToolStripSeparator());
            menuWindow.DropDownItems.Add(menuCloseAll);

            menuStrip.Items.Add(menuSystem);
            menuStrip.Items.Add(menuManage);
            menuStrip.Items.Add(menuWindow);

            Controls.Add(menuStrip);
            MainMenuStrip = menuStrip;
        }

        private void CreateStatusBar()
        {
            statusStrip = new StatusStrip();

            lblDoctor = new ToolStripStatusLabel(
                "Bác sĩ trực: Nguyễn Văn An");

            lblStatus = new ToolStripStatusLabel(
                "Sẵn sàng");

            progressBar = new ToolStripProgressBar();
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            progressBar.Size = new Size(150, 16);

            statusStrip.Items.Add(lblDoctor);
            statusStrip.Items.Add(new ToolStripStatusLabel(" | "));
            statusStrip.Items.Add(lblStatus);
            statusStrip.Items.Add(new ToolStripStatusLabel("Tiến trình:"));
            statusStrip.Items.Add(progressBar);

            Controls.Add(statusStrip);
        }

        private void OpenChild<T>() where T : Form, new()
        {
            foreach (Form child in MdiChildren)
            {
                if (child is T)
                {
                    if (child.WindowState == FormWindowState.Minimized)
                        child.WindowState = FormWindowState.Normal;

                    child.Activate();
                    lblStatus.Text = "Đã kích hoạt cửa sổ đang mở";
                    return;
                }
            }

            T form = new T();
            form.MdiParent = this;
            form.Show();

            lblStatus.Text = "Đã mở " + form.Text;
        }

        private void OpenDepartments()
        {
            MessageBox.Show(
                "Chức năng Danh mục Khoa/Phòng đang được xây dựng.",
                "Quản lý bệnh viện",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void CloseAllChildren()
        {
            foreach (Form child in MdiChildren)
                child.Close();

            lblStatus.Text = "Đã đóng tất cả cửa sổ con";
        }

        private void ReloadData()
        {
            progressBar.Value = 0;
            lblStatus.Text = "Đang nạp lại dữ liệu...";

            foreach (Form child in MdiChildren)
            {
                if (child is frmPatientProfiles patients)
                    patients.ReloadData();

                if (child is frmDoctorSchedule doctors)
                    doctors.ReloadData();
            }

            progressBar.Value = 100;
            lblStatus.Text = "Nạp dữ liệu hoàn tất";

            MessageBox.Show(
                "Đã nạp lại dữ liệu hiển thị.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            progressBar.Value = 0;
        }

        private void Logout()
        {
            DialogResult result = MessageBox.Show(
                "Bạn có muốn đăng xuất và đóng các cửa sổ đang mở không?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                CloseAllChildren();
                lblDoctor.Text = "Bác sĩ trực: Chưa đăng nhập";
                lblStatus.Text = "Đã đăng xuất";
            }
        }
    }
}
