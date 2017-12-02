﻿using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Slay.Models.BusinessObjects.Post;
using Slay.Models.DataTransferObjects.Post;
using Slay.ServicesContracts.Services;
using System.Threading.Tasks;

namespace Slay.Host.Controllers.ClientControllers
{
	[Produces("application/json")]
	[Route("api/Post")]
	public class PostController : ControllerBase
	{
		private readonly IPostService _postService;

		private readonly IMapper _mapper;

		public PostController(IMapper mapper, IPostService postService)
		{
			this._mapper = mapper;

			this._postService = postService;
		}

		[HttpGet("{id}", Name = Routes.GetPost)]
		public async Task<IActionResult> GetPostByIdAsync(string id)
		{
			try
			{
				var serviceResult = await this._postService.GetPostByIdAsync(id);

				if (serviceResult.HasErrors)
				{
					return new BadRequestObjectResult(serviceResult.Errors);
				}

				if (serviceResult.Value == null)
				{
					return new NotFoundResult();
				}

				return new OkObjectResult(this._mapper.Map<PostResponseDto>(serviceResult.Value));
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);

				return new NotFoundResult();
			}
		}

		[HttpPost(Name = Routes.CreatePost)]
		public async Task<IActionResult> CreatePostAsync([FromBody] CreatePostRequestDto createPostDto)
		{
			try
			{
				var createPostBo = this._mapper.Map<CreatePostRequestBo>(createPostDto);

				var serviceResult = await this._postService.CreatePostAsync(createPostBo);

				if (serviceResult.HasErrors)
				{
					return new BadRequestObjectResult(serviceResult.Errors);
				}

				var mappedResult = this._mapper.Map<PostResponseDto>(serviceResult.Value);

				return CreatedAtRoute(Routes.GetPost, new { id = mappedResult.Id }, mappedResult);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				
				return new BadRequestResult();
			}
		}

		[HttpDelete("{id}", Name = Routes.DeletePost)]
		public async Task<IActionResult> DeletePostAsync(string id)
		{
			try
			{
				var serviceResult = await this._postService.DeletePostAsync(id);

				if (serviceResult.HasErrors)
				{
					return new BadRequestObjectResult(serviceResult.Errors);
				}

				return new EmptyResult();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				
				return new BadRequestResult();
			}
		}
	}
}