using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class trainer_eval
{
	[Required]
public int lecture_id { get; set; }

	public int criteria_c1 { get; set; }

	public int criteria_c2 { get; set; }

	public int criteria_c3 { get; set; }

	public int criteria_c4 { get; set; }

	public DateTime date { get; set; }

	public int attended { get; set; }

}
}