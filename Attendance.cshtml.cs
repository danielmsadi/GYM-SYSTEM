using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class AttendanceModel : PageModel
    {
        private readonly SqlConnection _con;

        public AttendanceModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable AttendanceTable { get; set; }

        private void LoadAttendance()
        {
            AttendanceTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblAttendance", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(AttendanceTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadAttendance();
        }

        public IActionResult OnPostAdd(int StaffId, string CheckIn, string CheckOut, DateTime Date)
        {
            using (SqlCommand cmd = new SqlCommand("prcAttendance", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@AttendanceId", 0);
                cmd.Parameters.AddWithValue("@StaffId", StaffId);
                cmd.Parameters.AddWithValue("@CheckIn", CheckIn);
                cmd.Parameters.AddWithValue("@CheckOut", CheckOut);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Attendance");
        }

        public IActionResult OnPostUpdate(int AttendanceId, int StaffId, string CheckIn, string CheckOut, DateTime Date)
        {
            using (SqlCommand cmd = new SqlCommand("prcAttendance", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@AttendanceId", AttendanceId);
                cmd.Parameters.AddWithValue("@StaffId", StaffId);
                cmd.Parameters.AddWithValue("@CheckIn", CheckIn);
                cmd.Parameters.AddWithValue("@CheckOut", CheckOut);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadAttendance();
            return Page();
        }

        public IActionResult OnPostDelete(int AttendanceId)
        {
            using (SqlCommand cmd = new SqlCommand("prcAttendance", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@AttendanceId", AttendanceId);
                cmd.Parameters.AddWithValue("@StaffId", 0);
                cmd.Parameters.AddWithValue("@CheckIn", "");
                cmd.Parameters.AddWithValue("@CheckOut", "");
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadAttendance();
            return Page();
        }
    }
}