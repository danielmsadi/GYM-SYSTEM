using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class PaymentMethodModel : PageModel
    {
        private readonly SqlConnection _con;

        public PaymentMethodModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable PaymentMethodTable { get; set; }

        private void LoadPM()
        {
            PaymentMethodTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblPaymentMethod", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(PaymentMethodTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadPM();
        }

        public IActionResult OnPostAdd(string Type)
        {
            using (SqlCommand cmd = new SqlCommand("prcPaymentMethod", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@PaymentmethodId", 0);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/PaymentMethod");
        }

        public IActionResult OnPostUpdate(int PaymentmethodId, string Type)
        {
            using (SqlCommand cmd = new SqlCommand("prcPaymentMethod", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@PaymentmethodId", PaymentmethodId);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadPM();
            return Page();
        }

        public IActionResult OnPostDelete(int PaymentmethodId)
        {
            using (SqlCommand cmd = new SqlCommand("prcPaymentMethod", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@PaymentmethodId", PaymentmethodId);
                cmd.Parameters.AddWithValue("@Type", "");
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadPM();
            return Page();
        }
    }
}