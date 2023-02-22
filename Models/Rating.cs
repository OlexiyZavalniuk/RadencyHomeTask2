using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
	public class Rating
	{
		public int RatingId { get; set; }

		public int BookId { get; set; }

		public Score Score { get; set; }
	}

	public enum Score
	{
		terrible = 1,
		bad,
		norm,
		good,
		excellent
	}
}
