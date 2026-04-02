using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class ReviewModel : PageModel
    {
        private readonly SqlConnection _con;

        public ReviewModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable ReviewTable { get; set; }

        private void LoadReviews()
        {
            ReviewTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblReview", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ReviewTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadReviews();
        }

        public IActionResult OnPostAdd(string Name, string Comment, string Rating)
        {
            using (SqlCommand cmd = new SqlCommand("prcReview", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@ReviewId", 0);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@Rating", (object)Rating ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Reviews");
        }

        public IActionResult OnPostUpdate(int ReviewId, string Name, string Comment, string Rating)
        {
            using (SqlCommand cmd = new SqlCommand("prcReview", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@ReviewId", ReviewId);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Comment", Comment);
                cmd.Parameters.AddWithValue("@Rating", (object)Rating ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadReviews();
            return Page();
        }

        public IActionResult OnPostDelete(int ReviewId)
        {
            using (SqlCommand cmd = new SqlCommand("prcReview", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@ReviewId", ReviewId);
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Comment", "");
                cmd.Parameters.AddWithValue("@Rating", DBNull.Value);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadReviews();
            return Page();
        }
    }
}