using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
/* 
(if System.Data.OleDb is not functioning then do the following)
Navigate to the Tools Tab
Click NuGet Packet Manager
Click Nuget Packet Manager Console // A command-line console should pop up at the bottom of Visual Studio
Insert the following command
Install-Package System.Data.OleDb
*/


/* 
 * Connection string:
 * Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ethan\OneDrive\Documents\ExampleDatabase.accdb
 */
namespace Chapter13_DatabaseGUI
{
    public partial class Form1 : Form
    {
        private string sConnection = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\Ethan\OneDrive\Documents\ExampleDatabase.accdb";
        private OleDbConnection dbConn;
        private OleDbCommand dbCmd;
        private string sql = "SELECT * FROM memberTable ORDER BY LastName ASC, FirstName ASC;";
        private OleDbDataReader dbReader; // This provides Read-Only access. It cannot update the database.
        Member aMember;

        public Form1()
        {
            InitializeComponent();
        }

        public void AccessDatabaseQuery() // Example 13-3, pg 764
        {
            try
            {
                this.listBox1.Items.Clear();
                /*
                 * Construct an object to store the connection string.
                 */
                dbConn = new OleDbConnection(sConnection);
                dbConn.Open();
                /*
                 * Construct an object to hold the SQL query. 
                 * Link the dbConn object to the dbCmd object. 
                 */
                dbCmd = new OleDbCommand();
                dbCmd.CommandText = sql;
                dbCmd.Connection = dbConn;
                /*
                 * Construct a OleDbDataReader object.
                 * Links dbReader to the dbCmd object. 
                 */
                dbReader = dbCmd.ExecuteReader();
                while (dbReader.Read())
                {
                    aMember = new Member(dbReader["StudentID"].ToString(), dbReader["FirstName"].ToString(), dbReader["LastName"].ToString());
                    this.listBox1.Items.Add(aMember.Id + " " + aMember.FirstName + " " + aMember.LastName);
                }
                dbReader.Close();
                dbConn.Close();
            }
            catch (Exception e)
            {
                this.listBox1.Items.Add(e.Message);
            }
        }


        private void btnGetData_Click(object sender, EventArgs e)
        {
            AccessDatabaseQuery();
        }
    }
    public class Member
    {
        private string id;
        private string firstName;
        private string lastName;
        private string phoneNumber;

        public Member(string id, string firstName, string lastName) // Constructor
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public string Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
    }
}
