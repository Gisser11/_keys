using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

using Microsoft.Data.SqlClient;
using static _keys.Pages.editingBD.IndexModel;

namespace _keys.Pages.EditAndDelete
{
    public class EditModel : PageModel
    {
		public usersDB userdb = new usersDB();
		public void OnGet()
        {
			String id = Request.Query["id"];
			Console.WriteLine(id);
			try
			{
				string connectionString = "Server=DESKTOP-HD1U37A\\SQLEXPRESS;Database=WebApplication1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "SELECT * FROM _userForm WHERE id = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@id", id);
						using (SqlDataReader reader = command.ExecuteReader()) 
						{
							if (reader.Read())
							{
								userdb.id = "" + reader.GetInt32(0);
								userdb.name = "" + reader.GetString(1);
								userdb.email = "" + reader.GetString(2);
								userdb.phone = "" + reader.GetString(3);


							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
		}
		public void OnPost()
		{
			userdb.id = Request.Form["id"];
			userdb.name = Request.Form["name"];
			userdb.email = Request.Form["email"];
			userdb.phone = Request.Form["phone"];

			Console.WriteLine(userdb.id);
			Console.WriteLine(userdb.name);

			try
			{
				string connectionString = "Server=DESKTOP-HD1U37A\\SQLEXPRESS;Database=WebApplication1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";
				using (SqlConnection connection = new SqlConnection(connectionString))
				{
					connection.Open();
					string sql = "UPDATE _userForm " +
						"set name = @name, email = @email, phone = @phone " +
						"WHERE id = @id";
					using (SqlCommand command = new SqlCommand(sql, connection))
					{
						command.Parameters.AddWithValue("@name", userdb.name);
						command.Parameters.AddWithValue("@email", userdb.email);
						command.Parameters.AddWithValue("@phone", userdb.phone);
						command.Parameters.AddWithValue("@id", userdb.id);

						command.ExecuteNonQuery(); // добавлено выполнение команды
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.ToString());
			}
			Response.Redirect("/editingBD/Index");

		}

	}
}
