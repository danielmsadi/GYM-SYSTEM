using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class TrainersSportsModel : PageModel
    {
        private readonly SqlConnection _con;

        public TrainersSportsModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable TrainersSportsTable { get; set; }

        private void LoadTS()
        {
            TrainersSportsTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblTrainersSports", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(TrainersSportsTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadTS();
        }

        public IActionResult OnPostAdd(int Sport_Id, int Trainers_Id, string Name)
        {
            using (SqlCommand cmd = new SqlCommand("prcTrainersSports", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@Trainersports_Id", 0);
                cmd.Parameters.AddWithValue("@Sport_Id", Sport_Id);
                cmd.Parameters.AddWithValue("@Trainers_Id", Trainers_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Trainersports");
        }

        public IActionResult OnPostUpdate(int Trainersports_Id, int Sport_Id, int Trainers_Id, string Name)
        {
            using (SqlCommand cmd = new SqlCommand("prcTrainersSports", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@Trainersports_Id", Trainersports_Id);
                cmd.Parameters.AddWithValue("@Sport_Id", Sport_Id);
                cmd.Parameters.AddWithValue("@Trainers_Id", Trainers_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadTS();
            return Page();
        }

        public IActionResult OnPostDelete(int Trainersports_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcTrainersSports", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@Trainersports_Id", Trainersports_Id);
                cmd.Parameters.AddWithValue("@Sport_Id", 0);
                cmd.Parameters.AddWithValue("@Trainers_Id", 0);
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadTS();
            return Page();
        }
    }
}