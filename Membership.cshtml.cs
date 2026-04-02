using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using System.Data;

namespace gymmm.Pages
{
    public class MembershipModel : PageModel
    {
        private readonly SqlConnection _con;

        public MembershipModel(SqlConnection con)
        {
            _con = con;
        }

        public DataTable MembershipTable { get; set; }

        private void LoadMemberships()
        {
            MembershipTable = new DataTable();

            using (SqlCommand cmd = new SqlCommand("SELECT * FROM tblMembership", _con))
            {
                _con.Open();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(MembershipTable);
                _con.Close();
            }
        }

        public void OnGet()
        {
            LoadMemberships();
        }

        public IActionResult OnPostAdd(
            int Client_Id,
            int Plan_Id,
            string Planname,
            decimal Price,
            string Currency,
            DateTime StartDate,
            DateTime EndDate,
            decimal Discount)
        {
            using (SqlCommand cmd = new SqlCommand("prcMembership", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "INSERT");
                cmd.Parameters.AddWithValue("@MembershipId", 0);
                cmd.Parameters.AddWithValue("@Client_Id", Client_Id);
                cmd.Parameters.AddWithValue("@Plan_Id", Plan_Id);
                cmd.Parameters.AddWithValue("@Planname", Planname);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Currency", Currency);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@Discount", Discount);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            return RedirectToPage("/Membership");
        }

        public IActionResult OnPostUpdate(
            int MembershipId,
            int Client_Id,
            int Plan_Id,
            string Planname,
            decimal Price,
            string Currency,
            DateTime StartDate,
            DateTime EndDate,
            decimal Discount)
        {
            using (SqlCommand cmd = new SqlCommand("prcMembership", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "UPDATE");
                cmd.Parameters.AddWithValue("@MembershipId", MembershipId);
                cmd.Parameters.AddWithValue("@Client_Id", Client_Id);
                cmd.Parameters.AddWithValue("@Plan_Id", Plan_Id);
                cmd.Parameters.AddWithValue("@Planname", Planname);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Currency", Currency);
                cmd.Parameters.AddWithValue("@StartDate", StartDate);
                cmd.Parameters.AddWithValue("@EndDate", EndDate);
                cmd.Parameters.AddWithValue("@Discount", Discount);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadMemberships();
            return Page();
        }

        public IActionResult OnPostDelete(int MembershipId)
        {
            using (SqlCommand cmd = new SqlCommand("prcMembership", _con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@Operation", "DELETE");
                cmd.Parameters.AddWithValue("@MembershipId", MembershipId);

                cmd.Parameters.AddWithValue("@Client_Id", 0);
                cmd.Parameters.AddWithValue("@Plan_Id", 0);
                cmd.Parameters.AddWithValue("@Planname", "");
                cmd.Parameters.AddWithValue("@Price", 0);
                cmd.Parameters.AddWithValue("@Currency", "");
                cmd.Parameters.AddWithValue("@StartDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@EndDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Discount", 0);
                cmd.Parameters.AddWithValue("@User", "admin");

                _con.Open();
                cmd.ExecuteNonQuery();
                _con.Close();
            }

            LoadMemberships();
            return Page();
        }
    }
}