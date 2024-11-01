using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PRG282_PRJ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void RenameHeaders()
        {
            dgvStudents.Columns[0].HeaderText = "Student ID";
            dgvStudents.Columns[1].HeaderText = "First Name";
            dgvStudents.Columns[2].HeaderText = "Last Name";
        }

        private void LoadData()
        {
            BindingSource bindingSource = new BindingSource();
            DataHandler handler = new DataHandler();

            bindingSource.DataSource = handler.GetStudents();
            dgvStudents.DataSource = bindingSource;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            RenameHeaders();
        }
    }
}