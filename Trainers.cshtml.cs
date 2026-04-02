using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class TrainersModel : PageModel
    {
        private readonly SqlConnection _con;

        public TrainersModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable TrainersTable { get; set; }

        private void LoadTrainers()
        {
            TrainersTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblTrainers", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(TrainersTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadTrainers();
        }

        public IActionResult OnPostAdd(string Name, int Salary_Id, int Trainersports_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcTrainers", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@Trainer_Id", 0);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Salary_Id", Salary_Id);
                cmd.Parameters.AddWithValue("@Trainersports_Id", Trainersports_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Trainers");
        }

        public IActionResult OnPostUpdate(int Trainer_Id, string Name, int Salary_Id, int Trainersports_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcTrainers", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@Trainer_Id", Trainer_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Salary_Id", Salary_Id);
                cmd.Parameters.AddWithValue("@Trainersports_Id", Trainersports_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadTrainers();
            return Page();
        }

        public IActionResult OnPostDelete(int Trainer_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcTrainers", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@Trainer_Id", Trainer_Id);

                
                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Salary_Id", 0);
                cmd.Parameters.AddWithValue("@Trainersports_Id", 0);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadTrainers();
            return Page();
        }
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Index");
        }
    }
}