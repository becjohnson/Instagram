using Instagram.Data;
using Instagram.Model.Post_Model;
using Instagram.WebMVC.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Instagram.Service
{
    public class PostService : Controller
    {
        private readonly Guid _userId;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext dbContext;
        public PostService() { }
        public PostService(Guid userId, IWebHostEnvironment hostEnvironment)
        {
            _userId = userId;
            webHostEnvironment = hostEnvironment;
        }
        public bool CreatePost(PostCreate model)
        {
            string uniqueFileName = UploadedVideo(model);
            string uniqueFileName2 = UploadedImage(model);
            List<string> hashTags = Hashtagger(model.Content);
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Tag hashTag = new() { TagId = string.Join(",", hashTags.Select(s => s[0..^0]).Distinct()) };
            dbContext.Tags.Add(hashTag);
            var entity =
            new Post
            {
                OwnerId = _userId,
                UserName = user.UserName,
                ProfilePicture = user.ProfilePicture,
                Alt = model.Alt,
                GreyScale = model.GreyScale,
                Brightness = model.Brightness,
                Contrast = model.Contrast,
                Saturation = model.Saturation,
                Content = model.Content,
                CreatedUtc = DateTimeOffset.Now,
                Video = uniqueFileName,
                Image = uniqueFileName2,
                Tags = hashTag
            };
            dbContext.Posts.Add(entity);
            return dbContext.SaveChanges() == 2;
        }
        public PostDetail GetPostById(string id)
        {
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            Post post =
            dbContext
            .Posts
            .Single(e => e.PostId == id);
            var entity =
                        dbContext
                        .Posts
                        .Single(e => e.PostId == id);
            return
            new PostDetail
            {
                OwnerId = _userId,
                UserName = entity.UserName,
                Alt = entity.Alt,
                ProfilePictureLocation = user.ProfilePicture,
                GreyScale = entity.GreyScale,
                Brightness = entity.Brightness,
                Contrast = entity.Contrast,
                Saturation = entity.Saturation,
                Content = entity.Content,
                CreatedUtc = DateTimeOffset.Now,
                VideoLocation = post.Video,
                ImageLocation = post.Image,
            };
        }
        public bool UpdatePost(PostEdit model)
        {
            var entity =
                    dbContext
                    .Posts
                    .Single(e => e.PostId == model.PostId && e.OwnerId == _userId);
            entity.Content = model.Content;
            entity.ModifiedUtc = DateTimeOffset.UtcNow;
            return dbContext.SaveChanges() == 1;
        }
        public bool DeletePost(string id)
        {
            var entity =
                    dbContext
                    .Posts
                    .Single(e => e.PostId == id && e.OwnerId == _userId);
            dbContext.Posts.Remove(entity);
            return dbContext.SaveChanges() == 1;
        }
        private string UploadedVideo(PostCreate model)
        {
            string uniqueFileName = null;
            if (model.Video != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "audios");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Video.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Video.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
        private string UploadedImage(PostCreate model)
        {
            string uniqueFileName2 = null;
            if (model.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName2 = Guid.NewGuid().ToString() + "_" + model.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName2);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Image.CopyTo(fileStream);
            }
            return uniqueFileName2;
        }
        public static List<string> Hashtagger(string content)
        {
            var rx = new Regex("#+[a-zA-Z0-9(_)]{1,}", RegexOptions.Compiled);
            MatchCollection matches = rx.Matches(content);
            var list = matches.Cast<Match>().Select(match => match.Value).ToList();
            return list;
        }
        public void HashtagFinder(string content, PostListItem post)
        {
            var hashtags = Hashtagger(content);
            var hashtag = string.Join(",", hashtags.Select(s => s[0..^0]).Distinct());
            var query = dbContext.Tags.Where(e => e.TagId == hashtag);
        }
    }
}
