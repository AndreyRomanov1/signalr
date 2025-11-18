using System;
using System.Collections.Generic;
using System.Linq;
using BadNews.Models.Comments;
using BadNews.Repositories.Comments;
using Microsoft.AspNetCore.Mvc;

namespace BadNews.Controllers;

[ApiController]
public class CommentsController : ControllerBase
{
    private readonly CommentsRepository commentsRepository;

    public CommentsController(CommentsRepository commentsRepository)
    {
        this.commentsRepository = commentsRepository;
    }

    // GET
    [HttpGet("api/news/{id}/comments")]
    public ActionResult<CommentsDto> GetCommentsForNews(Guid newsId)
    {
        var comments = commentsRepository.GetComments(newsId);
        return comments.ToDto(newsId);
    }
}

public static class Mapper
{
    public static CommentsDto ToDto(this IReadOnlyCollection<Comment> model, Guid newsId)
    {
        return new CommentsDto
        {
            NewsId = newsId,
            Comments = model.Select(t => t.ToDto()).ToArray(),
        };
    }

    public static CommentDto ToDto(this Comment model)
    {
        return new CommentDto
        {
            User = model.User,
            Value = model.Value,
        };
    }
}