using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class SalariesModel : PageModel
    {
        private readonly SqlConnection _con;

        public SalariesModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable SalariesTable { get; set; }

        private void LoadSalaries()
        {
            SalariesTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblSalaries", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(SalariesTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadSalaries();
        }

        public IActionResult OnPostAdd(int Staff_Id, decimal Amount, int Currency_Id, DateTime Date)
        {
            using (SqlCommand cmd = new SqlCommand("prcSalaries", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@SalaryId", 0);
                cmd.Parameters.AddWithValue("@Staff_Id", Staff_Id);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Currency_Id", Currency_Id);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Salaries");
        }

        public IActionResult OnPostUpdate(int SalaryId, int Staff_Id, decimal Amount, int Currency_Id, DateTime Date)
        {
            using (SqlCommand cmd = new SqlCommand("prcSalaries", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@SalaryId", SalaryId);
                cmd.Parameters.AddWithValue("@Staff_Id", Staff_Id);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Currency_Id", Currency_Id);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadSalaries();
            return Page();
        }

        public IActionResult OnPostDelete(int SalaryId)
        {
            using (SqlCommand cmd = new SqlCommand("prcSalaries", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@SalaryId", SalaryId);

                cmd.Parameters.AddWithValue("@Staff_Id", 0);
                cmd.Parameters.AddWithValue("@Amount", 0);
                cmd.Parameters.AddWithValue("@Currency_Id", 0);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadSalaries();
            return Page();
        }
    }
}