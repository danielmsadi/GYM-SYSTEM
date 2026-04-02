using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class LocationModel : PageModel
    {
        private readonly SqlConnection _con;

        public LocationModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable LocationTable { get; set; }

        private void LoadLocations()
        {
            LocationTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblLocation", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(LocationTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadLocations();
        }

        public IActionResult OnPostAdd(string Area, string Address, string Streetname)
        {
            using (SqlCommand cmd = new SqlCommand("prcLocation", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@LocationId", 0);
                cmd.Parameters.AddWithValue("@Area", Area);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Streetname", Streetname);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Location");
        }

        public IActionResult OnPostUpdate(int LocationId, string Area, string Address, string Streetname)
        {
            using (SqlCommand cmd = new SqlCommand("prcLocation", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@LocationId", LocationId);
                cmd.Parameters.AddWithValue("@Area", Area);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Streetname", Streetname);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadLocations();
            return Page();
        }

        public IActionResult OnPostDelete(int LocationId)
        {
            using (SqlCommand cmd = new SqlCommand("prcLocation", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@LocationId", LocationId);
                cmd.Parameters.AddWithValue("@Area", "");
                cmd.Parameters.AddWithValue("@Address", "");
                cmd.Parameters.AddWithValue("@Streetname", "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadLocations();
            return Page();
        }
    }
}