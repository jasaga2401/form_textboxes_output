using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace form_textboxes_output
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=Amaze\\SQLEXPRESS;Initial Catalog=stock_control;Integrated Security=True";
        private SqlConnection connection;
        private SqlCommand command;
        private SqlDataReader reader;        

        private SqlDataAdapter adapter;
        private DataTable dataTable;
        private int currentPosition = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Call a method to load data
            LoadData();
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            if (currentPosition < dataTable.Rows.Count - 1)
            {
                currentPosition++;
                DisplayData();
            }
            else
            {
                MessageBox.Show("Already at the last record.");
            }
        }

        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            if (currentPosition > 0)
            {
                currentPosition--;
                DisplayData();
            }
            else
            {
                MessageBox.Show("Already at the first record.");
            }
        }


        private void LoadData()
        {
            try
            {
                // Initialize connection, command, and reader
                connection = new SqlConnection(connectionString);
                connection.Open();

                //string query = "SELECT * FROM dbo.Employees";
                adapter = new SqlDataAdapter("SELECT * FROM Employees", connection);

                //command = new SqlCommand(query, connection);
                //reader = command.ExecuteReader();

                //dataTable = new DataTable();
                //adapter.Fill(dataTable);

                // Initialize the DataTable and fill it with data
                dataTable = new DataTable();
                adapter.Fill(dataTable);

                // Display data from the first record
                DisplayData();

                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void DisplayData()
        {

            if (dataTable.Rows.Count > 0)
            {
                DataRow row = dataTable.Rows[currentPosition];
                // Update your form controls with data from the DataRow
                txtEmployeeID.Text = row["EmployeeID"].ToString();
                txtFirstName.Text = row["FirstName"].ToString();
                txtLastName.Text = row["LastName"].ToString();
            }              
                
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the reader and connection when the form is closing
            if (reader != null && !reader.IsClosed)
            {
                reader.Close();
            }

            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        
    }
}

