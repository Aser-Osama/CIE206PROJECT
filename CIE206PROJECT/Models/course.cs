using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class course
{
	[Required]
public int course_id { get; set; }

	public int tot_sessions { get; set; }

	public string advertisement_text { get; set; }

	public string video_link { get; set; }

}
}