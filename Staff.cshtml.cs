using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class StaffModel : PageModel
    {
        private readonly SqlConnection _con;

        public StaffModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable StaffTable { get; set; }

        private void LoadStaff()
        {
            StaffTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblStaffs", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(StaffTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadStaff();
        }

        public IActionResult OnPostAdd(int Role_Id, decimal WorkIngh, int Salary_Id, int User_Id, string Jobtitle, string Shift)
        {
            using (SqlCommand cmd = new SqlCommand("prcStaffs", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@Staff_Id", 0);
                cmd.Parameters.AddWithValue("@Role_Id", Role_Id);
                cmd.Parameters.AddWithValue("@WorkIngh", WorkIngh);
                cmd.Parameters.AddWithValue("@Salary_Id", Salary_Id);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@Jobtitle", Jobtitle);
                cmd.Parameters.AddWithValue("@Shift", Shift);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Staff");
        }

        public IActionResult OnPostUpdate(int Staff_Id, int Role_Id, decimal WorkIngh, int Salary_Id, int User_Id, string Jobtitle, string Shift)
        {
            using (SqlCommand cmd = new SqlCommand("prcStaffs", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@Staff_Id", Staff_Id);
                cmd.Parameters.AddWithValue("@Role_Id", Role_Id);
                cmd.Parameters.AddWithValue("@WorkIngh", WorkIngh);
                cmd.Parameters.AddWithValue("@Salary_Id", Salary_Id);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@Jobtitle", Jobtitle);
                cmd.Parameters.AddWithValue("@Shift", Shift);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadStaff();
            return Page();
        }

        public IActionResult OnPostDelete(int Staff_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcStaffs", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@Staff_Id", Staff_Id);

                cmd.Parameters.AddWithValue("@Role_Id", 0);
                cmd.Parameters.AddWithValue("@WorkIngh", 0);
                cmd.Parameters.AddWithValue("@Salary_Id", 0);
                cmd.Parameters.AddWithValue("@User_Id", 0);
                cmd.Parameters.AddWithValue("@Jobtitle", "");
                cmd.Parameters.AddWithValue("@Shift", "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadStaff();
            return Page();
        }
    }
}