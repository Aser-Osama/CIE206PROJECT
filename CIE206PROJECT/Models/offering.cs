using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class offering
{
	[Required]
public int offering_id { get; set; }

	public int course_id { get; set; }

	public object Start_Date { get; set; }

	public int Price { get; set; }

}
}