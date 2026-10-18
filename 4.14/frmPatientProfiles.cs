
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace HospitalMDI
{
    public class frmPatientProfiles : Form
    {
        private DataGridView dgvPatients;
        private BindingList<Patient> patients;

        public frmPatientProfiles()
        {
            Text = "Hồ sơ Bệnh nhân";
            Size = new System.Drawing.Size(850, 450);
            StartPosition = FormStartPosition.CenterScreen;

            dgvPatients = new DataGridView();
            dgvPatients.Dock = DockStyle.Fill;
            dgvPatients.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;
            dgvPatients.ReadOnly = true;
            dgvPatients.AllowUserToAddRows = false;
            dgvPatients.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;
            dgvPatients.MultiSelect = false;

            Controls.Add(dgvPatients);
            ReloadData();
        }

        public void ReloadData()
        {
            patients = new BindingList<Patient>
            {
                new Patient
                {
                    PatientId = "BN001",
                    FullName = "Nguyễn Văn Minh",
                    Age = 25,
                    Gender = "Nam",
                    Department = "Nội tổng hợp"
                },
                new Patient
                {
                    PatientId = "BN002",
                    FullName = "Trần Thị Lan",
                    Age = 32,
                    Gender = "Nữ",
                    Department = "Tim mạch"
                },
                new Patient
                {
                    PatientId = "BN003",
                    FullName = "Lê Hoàng Nam",
                    Age = 41,
                    Gender = "Nam",
                    Department = "Ngoại khoa"
                }
            };

            dgvPatients.DataSource = patients;
        }
    }

    public class Patient
    {
        public string PatientId { get; set; }
        public string FullName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Department { get; set; }
    }
}
