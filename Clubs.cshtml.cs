using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class ClubsModel : PageModel
    {
        private readonly SqlConnection _con;

        public ClubsModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable ClubsTable { get; set; }

        private void LoadClubs()
        {
            ClubsTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblClubs", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ClubsTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadClubs();
        }

        public IActionResult OnPostAdd(int Location_Id, int Sport_Id, decimal Price, string Name, string Description)
        {
            using (SqlCommand cmd = new SqlCommand("prcClubs", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@ClubId", 0);
                cmd.Parameters.AddWithValue("@Location_Id", Location_Id);
                cmd.Parameters.AddWithValue("@Sport_Id", Sport_Id);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Clubs");
        }

        public IActionResult OnPostUpdate(int ClubId, int Location_Id, int Sport_Id, decimal Price, string Name, string Description)
        {
            using (SqlCommand cmd = new SqlCommand("prcClubs", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@ClubId", ClubId);
                cmd.Parameters.AddWithValue("@Location_Id", Location_Id);
                cmd.Parameters.AddWithValue("@Sport_Id", Sport_Id);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadClubs();
            return Page();
        }

        public IActionResult OnPostDelete(int ClubId)
        {
            using (SqlCommand cmd = new SqlCommand("prcClubs", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@ClubId", ClubId);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadClubs();
            return Page();
        }
    }
}