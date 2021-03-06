﻿using System.Collections.Generic;

namespace Slay.Models.BusinessObjects.Comment
{
	public sealed class CommentsResponseBo
	{
		public IEnumerable<CommentItemBo> Comments { get; set; }

		public int? Skip { get; set; }

		public int? Limit { get; set; }
	}
}