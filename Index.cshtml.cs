using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class IndexModel : PageModel
    {
        private readonly SqlConnection _con;

        public IndexModel(SqlConnection con)
        {
            _con = con;
        }

        public string ErrorMessage { get; set; }   

        public void OnGet()
        {
        }

        public IActionResult OnPost(string Email, string Password)
        {
            DataTable dt = new DataTable();

            using (SqlCommand cmd = new SqlCommand("prcLogin", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Password", Password);

                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                _con.Close();
            }

            if (dt.Rows.Count > 0)
            {
                return RedirectToPage("/Main");
            }
            else
            {
                ErrorMessage = "Invalid email or password";
                return Page();
            }
        }
    }
}