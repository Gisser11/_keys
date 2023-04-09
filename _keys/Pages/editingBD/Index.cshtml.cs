using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using static _keys.Pages.editingBD.IndexModel;

namespace _keys.Pages.editingBD
{
    [Authorize(Roles="Admin, user")]
    public class IndexModel : PageModel
    {
        public List<usersDB> users_list = new List<usersDB>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Server=DESKTOP-HD1U37A\\SQLEXPRESS;Database=WebApplication1;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM _userForm";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                usersDB userdb = new usersDB();

                                userdb.id = "" + reader.GetInt32(0);
								userdb.name = reader.GetString(1);
                                userdb.email = reader.GetString(2);
                                userdb.phone = reader.GetString(3);
                                 
								users_list.Add(userdb);
                            
							}
							
						}
					}
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.ToString());
            }

        }
        public class usersDB
        {
            public string? id;
            public string? name;
            public string? email;
			public string? phone;

		}
    }
}
