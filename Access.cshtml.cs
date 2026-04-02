using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class AccessModel : PageModel
    {
        private readonly SqlConnection _con;

        public AccessModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable AccessTable { get; set; }

        private void LoadAccess()
        {
            AccessTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblAccess", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(AccessTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadAccess();
        }

        public IActionResult OnPostAdd(string Type, string Name, int User_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcAccess", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@Access_Id", 0);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Access");
        }

        public IActionResult OnPostUpdate(int Access_Id, string Type, string Name, int User_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcAccess", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@Access_Id", Access_Id);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadAccess();
            return Page();
        }

        public IActionResult OnPostDelete(int Access_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcAccess", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@Access_Id", Access_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadAccess();
            return Page();
        }
    }
}