using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly SqlConnection _con;

        public ProductsModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable ProductsTable { get; set; }

        private void LoadProducts()
        {
            ProductsTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblProducts", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ProductsTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadProducts();
        }

        public IActionResult OnPostAdd(string Name, string Description, decimal Price, int CurrencyId)
        {
            using (SqlCommand cmd = new SqlCommand("prcProducts", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@ProductId", 0);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Products");
        }

        public IActionResult OnPostUpdate(int ProductId, string Name, string Description, decimal Price, int CurrencyId)
        {
            using (SqlCommand cmd = new SqlCommand("prcProducts", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@ProductId", ProductId);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Description", (object)Description ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@CurrencyId", CurrencyId);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadProducts();
            return Page();
        }

        public IActionResult OnPostDelete(int ProductId)
        {
            using (SqlCommand cmd = new SqlCommand("prcProducts", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@ProductId", ProductId);

                cmd.Parameters.AddWithValue("@Name", "");
                cmd.Parameters.AddWithValue("@Description", DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", 0);
                cmd.Parameters.AddWithValue("@CurrencyId", 0);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadProducts();
            return Page();
        }
    }
}