using Instagram.Data;
using Instagram.Model.Reply_Model;
using Instagram.WebMVC.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Instagram.Services
{
    public class ReplyService : Controller
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext ctx;
        private readonly UserManager<ApplicationUser> _userManager;
        public ReplyService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateReply(ReplyCreate model)
        {
            List<string> hashTags = Hashtagger(model.Content);
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Tag hashtag = new() { TagId = string.Join(",", hashTags.Select(s => s[0..^0]).Distinct()) };
            ctx.
                Tags.Add(hashtag);
            var entity =
                new Reply()
                {
                    OwnerId = _userId,
                    Content = model.Content,
                    ProfilePicture = user.ProfilePicture,
                    UserName = user.UserName,
                    CreatedUtc = DateTimeOffset.Now,
                };
            ctx.Replies.Add(entity);
            return ctx.SaveChanges() == 1;
        }
        public IEnumerable<ReplyListItem> GetReplys()
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            var query =
                ctx
                .Replies
                .Where(e => e.OwnerId == _userId)
                .Select(
                    e =>
                    new ReplyListItem
                    {
                        ReplyId = e.ReplyId,
                        CreatedUtc = e.CreatedUtc,
                        ProfilePicture = user.ProfilePicture,
                        Content = e.Content,
                    }
                );
            return query.ToArray();
        }
        //USE PARTIAL PAGES
        public ReplyDetail GetReplyById(string id)
        {
            var entity =
                ctx
                    .Replies
                    .Single(e => e.ReplyId == id && e.OwnerId == _userId);
            return
                new ReplyDetail
                {
                    ReplyId = entity.ReplyId,
                    Content = entity.Content,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc,
                };
        }

        public bool UpdateReply(ReplyEdit model)
        {
            var entity =
                ctx
                    .Replies
                    .Single(e => e.ReplyId == model.ReplyId && e.OwnerId == _userId);
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
