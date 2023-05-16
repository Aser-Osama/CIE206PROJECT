using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class Student
{
	[Required]
public int user_id { get; set; }

	public int parent_id { get; set; }

	public string skill_level { get; set; }

}
}