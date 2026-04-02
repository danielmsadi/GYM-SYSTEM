using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class StoresModel : PageModel
    {
        private readonly SqlConnection _con;

        public StoresModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable StoresTable { get; set; }

        private void LoadStores()
        {
            StoresTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblStores", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(StoresTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadStores();
        }

        public IActionResult OnPostAdd(string Name, int Club_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcStores", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@Store_Id", 0);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Club_Id", Club_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Stores");
        }

        public IActionResult OnPostUpdate(int Store_Id, string Name, int Club_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcStores", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@Store_Id", Store_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Club_Id", Club_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadStores();
            return Page();
        }

        public IActionResult OnPostDelete(int Store_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcStores", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@Store_Id", Store_Id);
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Club_Id", 0);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadStores();
            return Page();
        }
        
    }
}