namespace CIE206PROJECT.Controllers
{
	public class DB_Container
	{
		public DataPage dataPage_DB { get; set; }
		public CoursePage coursePage_DB { get; set; }
		public DB_Container()
		{
			dataPage_DB= new DataPage();
			coursePage_DB= new CoursePage();
		}
	}
}
