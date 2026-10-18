
using System.ComponentModel;
using System.Windows.Forms;

namespace HospitalMDI
{
    public class frmDoctorSchedule : Form
    {
        private DataGridView dgvSchedule;
        private BindingList<DoctorShift> schedules;

        public frmDoctorSchedule()
        {
            Text = "Lịch trực Bác sĩ";
            Size = new System.Drawing.Size(800, 400);
            StartPosition = FormStartPosition.CenterScreen;

            dgvSchedule = new DataGridView();
            dgvSchedule.Dock = DockStyle.Fill;
            dgvSchedule.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvSchedule.ReadOnly = true;
            dgvSchedule.AllowUserToAddRows = false;
            dgvSchedule.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            Controls.Add(dgvSchedule);
            ReloadData();
        }

        public void ReloadData()
        {
            schedules = new BindingList<DoctorShift>
            {
                new DoctorShift
                {
                    DoctorName = "Nguyễn Văn An",
                    Department = "Cấp cứu",
                    Shift = "07:00 - 15:00",
                    Date = "18/10/2026"
                },
                new DoctorShift
                {
                    DoctorName = "Trần Thị Bình",
                    Department = "Nội tổng hợp",
                    Shift = "15:00 - 23:00",
                    Date = "18/10/2026"
                },
                new DoctorShift
                {
                    DoctorName = "Lê Quốc Huy",
                    Department = "Ngoại khoa",
                    Shift = "23:00 - 07:00",
                    Date = "18/10/2026"
                }
            };

            dgvSchedule.DataSource = schedules;
        }
    }

    public class DoctorShift
    {
        public string DoctorName { get; set; }
        public string Department { get; set; }
        public string Shift { get; set; }
        public string Date { get; set; }
    }
}
