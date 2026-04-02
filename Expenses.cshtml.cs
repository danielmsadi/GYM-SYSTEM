using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class ExpensesModel : PageModel
    {
        private readonly SqlConnection _con;

        public ExpensesModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable ExpensesTable { get; set; }

        private void LoadExpenses()
        {
            ExpensesTable = new DataTable();
            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblExpenses", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ExpensesTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadExpenses();
        }

        public IActionResult OnPostAdd(int Salary_Id, string Type, int Payment_Id, decimal Amount, int CurrencyId, DateTime Date, string Notes)
        {
            using (SqlCommand cmd = new SqlCommand("prcExpenses", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@ExpenseId", 0);
                cmd.Parameters.AddWithValue("@Salary_Id", Salary_Id);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Payment_Id", Payment_Id);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Notes", Notes ?? "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Expenses");
        }

        public IActionResult OnPostUpdate(int ExpenseId, int Salary_Id, string Type, int Payment_Id, decimal Amount, int CurrencyId, DateTime Date, string Notes)
        {
            using (SqlCommand cmd = new SqlCommand("prcExpenses", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@ExpenseId", ExpenseId);
                cmd.Parameters.AddWithValue("@Salary_Id", Salary_Id);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Payment_Id", Payment_Id);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Notes", Notes ?? "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadExpenses();
            return Page();
        }

        public IActionResult OnPostDelete(int ExpenseId)
        {
            using (SqlCommand cmd = new SqlCommand("prcExpenses", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@ExpenseId", ExpenseId);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadExpenses();
            return Page();
        }
    }
}