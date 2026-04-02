using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class UsersModel : PageModel
    {
        private readonly SqlConnection _con;

        public UsersModel(SqlConnection con)
        {

            _con = con;
        }
        public System.Data.DataTable UsersTable { get; set; }

        private void LoadUsers()
        {
            UsersTable = new System.Data.DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblUsers", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(UsersTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadUsers();
        }
        public IActionResult OnPostAddUser(
            string Name,
            string Email,
            string Gender,
            string Phone,
            DateTime Date,
            string Password)
        {
            using (SqlCommand cmd = new SqlCommand("prcUsers", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@User_Id", 0);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Users");
        }



        public IActionResult OnPostDeleteUser(int User_Id)
        {
            using (SqlCommand cmd = new SqlCommand("prcUsers", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@User", "admin");



                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadUsers();
            return Page();
        }

        public IActionResult OnPostUpdateUser(
        int User_Id,
        string Name,
        string Email,
        string Gender,
        string Phone,
        DateTime Date,
        string Password)
        {
            using (SqlCommand cmd = new SqlCommand("prcUsers", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@User_Id", User_Id);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Phone", Phone);
                cmd.Parameters.AddWithValue("@Date", Date);
                cmd.Parameters.AddWithValue("@Password", Password);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadUsers();
            return Page();
        }


    }
}
