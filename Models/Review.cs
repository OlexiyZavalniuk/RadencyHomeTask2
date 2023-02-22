using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public  class Review
	{
		public int ReviewId { get; set; }

		public string Message { get; set;}

		public int BookId { get; set; }

		public string Reviewer { get; set; }
	}
}
