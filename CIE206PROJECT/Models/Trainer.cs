using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class Trainer
{
	[Required]
public int user_id { get; set; }

	public string level { get; set; }

	public string field { get; set; }

}
}