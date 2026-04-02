using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class ScreenModel : PageModel
    {
        private readonly SqlConnection _con;

        public ScreenModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable ScreenTable { get; set; }

        private void LoadScreen()
        {
            ScreenTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblScreen", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ScreenTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadScreen();
        }

        public IActionResult OnPostAdd(string Name, int Access_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcScreen", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@Screen_Id", 0);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Access_Id", Access_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Screen");
        }

        public IActionResult OnPostUpdate(int Screen_Id, string Name, int Access_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcScreen", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@Screen_Id", Screen_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Access_Id", Access_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadScreen();
            return Page();
        }

        public IActionResult OnPostDelete(int Screen_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcScreen", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@Screen_Id", Screen_Id);

                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Access_Id", 0);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadScreen();
            return Page();
        }
    }
}