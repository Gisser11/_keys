using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Xunit;
using static _keys.Pages.editingBD.IndexModel;

namespace _keys.Pages.CreateForm
{
    public class NewFormModel : PageModel
    {
		public usersDB userdb = new usersDB();
		public void OnGet()
        {
        }
		public void OnPost()
		{
			userdb.name = Request.Form["name"];
			userdb.email = Request.Form["email"];
			userdb.phone = Request.Form["phone"];
			try
			{
				string connectionString = "Server=DESKTOP-HD1U37A\\SQLEXPRESS;Database=WebApplication1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "INSERT INTO _userForm" +
						"(name, email, phone) VALUES " +
						"(@name,@email,@phone);";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", userdb.name);
						command.Parameters.AddWithValue("@email", userdb.email);
						command.Parameters.AddWithValue("@phone", userdb.phone);
						command.ExecuteNonQuery(); // добавлено выполнение команды
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			userdb.name = "";
			userdb.email = "";
			userdb.phone = "";
			Response.Redirect("/editingBD/Index");
		}
	}
}
 