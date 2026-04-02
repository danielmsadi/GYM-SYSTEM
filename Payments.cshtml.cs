using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class PaymentsModel : PageModel
    {
        private readonly SqlConnection _con;

        public PaymentsModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable PaymentsTable { get; set; }

        private void LoadPayments()
        {
            PaymentsTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblPayments", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PaymentsTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadPayments();
        }

        public IActionResult OnPostAdd(int Paymentmethod_Id, string Type, decimal Tax, decimal Amount, int Currency_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcPayments", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                
                cmd.Parameters.AddWithValue("@Paymentmethod_Id", Paymentmethod_Id);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Tax", Tax);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Currency_Id", Currency_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Payments");
        }

        public IActionResult OnPostUpdate(int PaymentId, int Paymentmethod_Id, string Type, decimal Tax, decimal Amount, int Currency_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcPayments", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@PaymentId", PaymentId);
                cmd.Parameters.AddWithValue("@Paymentmethod_Id", Paymentmethod_Id);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Tax", Tax);
                cmd.Parameters.AddWithValue("@Amount", Amount);
                cmd.Parameters.AddWithValue("@Currency_Id", Currency_Id);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadPayments();
            return Page();
        }

        public IActionResult OnPostDelete(int PaymentId)
        {
            using (SqlCommand cmd = new SqlCommand("prcPayments", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@PaymentId", PaymentId);

                cmd.Parameters.AddWithValue("@Paymentmethod_Id", 0);
                cmd.Parameters.AddWithValue("@Type", "");
                cmd.Parameters.AddWithValue("@Tax", 0);
                cmd.Parameters.AddWithValue("@Amount", 0);
                cmd.Parameters.AddWithValue("@Currency_Id", 0);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadPayments();
            return Page();
        }
    }
}