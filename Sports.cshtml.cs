using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class SportsModel : PageModel
    {
        private readonly SqlConnection _con;

        public SportsModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable SportsTable { get; set; }

        private void LoadSports()
        {
            SportsTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblSports", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(SportsTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadSports();
        }

        public IActionResult OnPostAdd(string Name, string InOutdoor, int TrainerID, decimal Capacity, string Duration)
        {
            using (SqlCommand cmd = new SqlCommand("prcSports", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@SportID", 0);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@InOutdoor", InOutdoor);
                cmd.Parameters.AddWithValue("@TrainerID", TrainerID);
                cmd.Parameters.AddWithValue("@Capacity", Capacity);
                cmd.Parameters.AddWithValue("@Duration", Duration);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Sports");
        }

        public IActionResult OnPostUpdate(int SportID, string Name, string InOutdoor, int TrainerID, decimal Capacity, string Duration)
        {
            using (SqlCommand cmd = new SqlCommand("prcSports", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@SportID", SportID);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@InOutdoor", InOutdoor);
                cmd.Parameters.AddWithValue("@TrainerID", TrainerID);
                cmd.Parameters.AddWithValue("@Capacity", Capacity);
                cmd.Parameters.AddWithValue("@Duration", Duration);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadSports();
            return Page();
        }

        public IActionResult OnPostDelete(int SportID)
        {
            using (SqlCommand cmd = new SqlCommand("prcSports", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@SportID", SportID);

                
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@InOutdoor", "");
                cmd.Parameters.AddWithValue("@TrainerID", 0);
                cmd.Parameters.AddWithValue("@Capacity", 0);
                cmd.Parameters.AddWithValue("@Duration", "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadSports();
            return Page();
        }
    }
}