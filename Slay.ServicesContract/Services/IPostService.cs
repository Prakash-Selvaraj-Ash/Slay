﻿using JetBrains.Annotations;
using Slay.Models.BusinessObjects.Post;
using Slay.Utilities.ServiceResult;
using System.Threading.Tasks;

namespace Slay.ServicesContracts.Services
{
	public interface IPostService
    {
        Task<ServiceResult<PostItemBo>> GetPostByIdAsync([NotNull]string id);

        Task<ServiceResult<PostItemBo>> CreatePostAsync([NotNull]CreatePostRequestBo createPostRequestBo);

	    Task<ServiceResult<bool>> DeletePostAsync(string id);

	    Task<ServiceResult<PostsResponseBo>> GetPostsAsync(int skip, int limit);

    }
}