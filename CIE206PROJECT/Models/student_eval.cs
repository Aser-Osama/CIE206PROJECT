using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class student_eval
{
	[Required]
public int student_id { get; set; }

	[Required]
public int lecture_id { get; set; }

	public bool attendance { get; set; }

	public int criteria_c1 { get; set; }

	public int criteria_c2 { get; set; }

	public int criteria_c3 { get; set; }

	public int criteria_c4 { get; set; }

	public DateTime date { get; set; }

}
}