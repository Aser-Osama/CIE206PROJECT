using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class Student_groups
{
	[Required]
public int group_no { get; set; }

	[Required]
public int Student_id { get; set; }

}
}