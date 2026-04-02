using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class ClientsModel : PageModel
    {
        private readonly SqlConnection _con;

        public ClientsModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable ClientsTable { get; set; }

        private void LoadClients()
        {
            ClientsTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblClients", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ClientsTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadClients();
        }

        public IActionResult OnPostAdd(int User_Id, string Name, string Email, DateTime Date, string Gender, string Phone)
        {
            using (SqlCommand cmd = new SqlCommand("prcClients", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@ClientId", 0);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Clients");
        }

        public IActionResult OnPostUpdate(int ClientId, int User_Id, string Name, string Email, DateTime Date, string Gender, string Phone)
        {
            using (SqlCommand cmd = new SqlCommand("prcClients", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@ClientId", ClientId);
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadClients();
            return Page();
        }

        public IActionResult OnPostDelete(int ClientId)
        {
            using (SqlCommand cmd = new SqlCommand("prcClients", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@ClientId", ClientId);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadClients();
            return Page();
        }
    }
}