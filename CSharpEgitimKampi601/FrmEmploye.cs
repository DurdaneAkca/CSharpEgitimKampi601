using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace CSharpEgitimKampi601
{
    public partial class FrmEmploye : Form
    {
        public FrmEmploye()
        {
            InitializeComponent();
        }

        string connectionString = "Server=localhost;port=5432;Database=CustomerDb;user Id=postgres;Password=1234";

        void EmployeList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Employes";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            connection.Close();
        }
        
        void DepartmentList()
        {
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Select * From Departments";
            var command = new NpgsqlCommand(query, connection);
            var adapter = new NpgsqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            cmbEmployeDepartment.DisplayMember = "DepartmentName";
            cmbEmployeDepartment.ValueMember = "DepartmentId";
            cmbEmployeDepartment.DataSource = dataTable;
            connection.Close();
        }
        private void btnList_Click(object sender, EventArgs e)
        {
            EmployeList();
        }

        private void FrmEmploye_Load(object sender, EventArgs e)
        {
            EmployeList();
            DepartmentList();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string employeName = txtEmployeName.Text;
            string employeSurname = txtEmployeSurname.Text;
            decimal employeSalary = decimal.Parse(txtEmployeSalary.Text);
            int departmentId = int.Parse(cmbEmployeDepartment.SelectedValue.ToString());

            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "insert into Employes (EmployeName,EmployeSurname,EmployeSalary,Departmentid) values (@employeName,@employeSurname,@employeSalary,@departmentid)";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeName", employeName);
            command.Parameters.AddWithValue("@employeSurname", employeSurname);
            command.Parameters.AddWithValue("@employeSalary", employeSalary);
            command.Parameters.AddWithValue("@departmentid", departmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Ekleme işlemi başarılı");
            connection.Close();
            EmployeList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtEmployeId.Text);
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Delete From Employes where EmployeId=@employeId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeId", id);
            command.ExecuteNonQuery();
            MessageBox.Show("Silme işlemi başarılı");
            connection.Close();
            EmployeList();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string employeName = txtEmployeName.Text;
            string employeSurname = txtEmployeSurname.Text;
            decimal employeSalary = decimal.Parse(txtEmployeSalary.Text);
            int departmentId = int.Parse(cmbEmployeDepartment.SelectedValue.ToString());
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "Update Employes Set EmployeName=@employeName,EmployeSurname=@employeSurname,EmployeSalary=@employeSalary where EmployeId=@employeId";
            var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@employeName", employeName);
            command.Parameters.AddWithValue("@employeSurname", employeSurname);
            command.Parameters.AddWithValue("@employeSalary", employeSalary);
            command.Parameters.AddWithValue("@employeId", departmentId);
            command.ExecuteNonQuery();
            MessageBox.Show("Güncelleme işlemi başarılı");
            connection.Close();
            EmployeList();
        }
    }
}
