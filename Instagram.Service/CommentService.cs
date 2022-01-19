using Instagram.Data;
using Instagram.Model.Comment_Model;
using Instagram.WebMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Instagram.Service
{
    public class CommentService : Controller
    {
        private readonly Guid _userId;
        private readonly WebMVC.Data.ApplicationDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateComment(CommentCreate model)
        {
            List<string> hashTags = Hashtagger(model.Content);
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            //Tag hashtag = new Tag { Hashtag = string.Join(",", hashTags.Select(s => s.Substring(0)).Distinct()) };
            Tag hashtag = new() { TagId = string.Join(",", hashTags.Select(s => s[0..^0]).Distinct()) };
            ctx.Tags.Add(hashtag);
            var entity =
                new Comment()
                {
                    OwnerId = _userId,
                    Content = model.Content,
                    CreatedUtc = DateTimeOffset.Now,
                };
            ctx.Comments.Add(entity);
            return ctx.SaveChanges() == 1;
        }
        public IEnumerable<CommentListItem> GetComments()
        {
            var query =
                ctx
                .Comments
                .Where(e => e.OwnerId == _userId)
                .Select(
                    e =>
                    new CommentListItem
                    {
                        CommentId = e.CommentId,
                        CreatedUtc = e.CreatedUtc,
                        Content = e.Content,

                    }
                );
            return query.ToArray();
        }
        public CommentDetail GetCommentById(string id)
        {
            var entity =
                ctx
                    .Comments
                    .Single(e => e.CommentId == id && e.OwnerId == _userId);
            return
                new CommentDetail
                {
                    CommentId = entity.CommentId,
                    Content = entity.Content,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc,
                };
        }
        public bool UpdateComment(CommentEdit model)
        {
            var entity =
                ctx
                    .Comments
                    .Single(e => e.CommentId == model.CommentId && e.OwnerId == _userId);
            entity.Content = model.Content;
            entity.ModifiedUtc = DateTimeOffset.UtcNow;
            return ctx.SaveChanges() == 1;
        }
        public static List<string> Hashtagger(string content)
        {
            var rx = new Regex("#+[a-zA-Z0-9(_)]{1,}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(content);
            var list = matches.Cast<Match>().Select(match => match.Value).ToList();
            return list;
        }
    }
}
