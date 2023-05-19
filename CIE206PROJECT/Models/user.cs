using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class user
{
	[Required]
public int user_id { get; set; }

	public object date_of_birth { get; set; }

	public object join_date { get; set; }

	public string address { get; set; }

	public string name { get; set; }

	public string password { get; set; }

	public string email { get; set; }

	public string user_type { get; set; }

}
}