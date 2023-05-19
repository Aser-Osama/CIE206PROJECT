using System;
using System.ComponentModel.DataAnnotations;

namespace CIE206PROJECT.Models
{
public class lecture_topics
{
	public int lecture_id { get; set; }

	public string topic { get; set; }

	public string topic_description { get; set; }

}
}