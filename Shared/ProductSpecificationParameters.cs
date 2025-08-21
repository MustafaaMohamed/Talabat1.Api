
namespace Shared
{
	public class ProductSpecificationParameters
	{
		public int? BrandId { get; set; }
		public int? TypeId { get; set; }
		public string? Sort { get; set; }
		public string? SearchValue { get; set; }
		private int _pageIndex = 1;
		private int _pageSize = 5;
		public int PageSize
		{
			get {return _pageSize;}
			set {_pageSize = value;}
		}
		public int PageIndex
		{
			get { return _pageIndex; }
			set { _pageIndex = value; } 
		}


	}
}
