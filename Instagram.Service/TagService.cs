using Instagram.Model.Post_Model;
using Instagram.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Instagram.Service
{
    public class TagService
    {
        private readonly Guid _userId;
        private readonly ApplicationDbContext dbContext;
        public TagService(Guid userId)
        {
            _userId = userId;
        }
        //List just the picture, use details to see the details of the post
        public IEnumerable<PostListItem> GetPostsByHashtagId(string hashTagId)
        {
            var query =
                    dbContext
                    .Posts
                    .Where(e => e.Tags.TagId == hashTagId)
                    .Select(
                        e =>
            new PostListItem
            {
                OwnerId = _userId,
                UserName = e.UserName,
                Alt = e.Alt,
                GreyScale = e.GreyScale,
                Brightness = e.Brightness,
                Contrast = e.Contrast,
                Saturation = e.Saturation,
                Content = e.Content,
                CreatedUtc = DateTimeOffset.Now,
                Video = e.Video,
                Image = e.Image,
            });
            return query;
        }
        public PostDetail GetSinglePostByHashtagId(string hashTagId, string postId)
        {
            var e =
                    dbContext
                    .Posts
                    .Single(e => e.PostId == postId && e.Tags.TagId == hashTagId);
            return
                new PostDetail
                {
                    OwnerId = _userId,
                    UserName = e.UserName,
                    Alt = e.Alt,
                    GreyScale = e.GreyScale,
                    Brightness = e.Brightness,
                    Contrast = e.Contrast,
                    Saturation = e.Saturation,
                    Content = e.Content,
                    CreatedUtc = DateTimeOffset.Now,
                    VideoLocation = e.Video,
                    ImageLocation = e.Image,
                };
        }
    }
}
