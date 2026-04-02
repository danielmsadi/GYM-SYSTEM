using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class IncomeModel : PageModel
    {
        private readonly SqlConnection _con;

        public IncomeModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable IncomeTable { get; set; }

        private void LoadIncome()
        {
            IncomeTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblIncome", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(IncomeTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadIncome();
        }

        public IActionResult OnPostAdd(
            int Registration_Id,
            int Sale_Id,
            string Source,
            DateTime Date,
            int Membership_Id,
            int Plan_Id,
            string Notes)
        {
            using (SqlCommand cmd = new SqlCommand("prcIncome", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@IncomeId", 0);
                cmd.Parameters.AddWithValue("@Registration_Id", Registration_Id);
                cmd.Parameters.AddWithValue("@Sale_Id", Sale_Id);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Membership_Id", Membership_Id);
                cmd.Parameters.AddWithValue("@Plan_Id", Plan_Id);
                cmd.Parameters.AddWithValue("@Notes", Notes??"");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Income");
        }

        public IActionResult OnPostUpdate(
            int IncomeId,
            int Registration_Id,
            int Sale_Id,
            string Source,
            DateTime Date,
            int Membership_Id,
            int Plan_Id,
            string Notes)
        {
            using (SqlCommand cmd = new SqlCommand("prcIncome", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@IncomeId", IncomeId);
                cmd.Parameters.AddWithValue("@Registration_Id", Registration_Id);
                cmd.Parameters.AddWithValue("@Sale_Id", Sale_Id);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Membership_Id", Membership_Id);
                cmd.Parameters.AddWithValue("@Plan_Id", Plan_Id);
                cmd.Parameters.AddWithValue("@Notes", Notes??"");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadIncome();
            return Page();
        }

        public IActionResult OnPostDelete(int IncomeId)
        {
            using (SqlCommand cmd = new SqlCommand("prcIncome", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@IncomeId", IncomeId);

                cmd.Parameters.AddWithValue("@Registration_Id", 0);
                cmd.Parameters.AddWithValue("@Sale_Id", 0);
                cmd.Parameters.AddWithValue("@Source", "");
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Membership_Id", 0);
                cmd.Parameters.AddWithValue("@Plan_Id", 0);
                cmd.Parameters.AddWithValue("@Notes", "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadIncome();
            return Page();
        }
    }
}