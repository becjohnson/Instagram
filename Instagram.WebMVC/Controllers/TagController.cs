using Instagram.Data;
using Instagram.Model.Hashtag_Model;
using Instagram.Model.TagModel;
using Instagram.Service;
using Instagram.WebMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Instagram.WebMVC.Controllers
{
    public class HashtagController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Authorize]
        private TagService CreateTagService()
        {
            var userId = Guid.Parse(User.Identity.Name);
            var service = new TagService(userId);
            return service;
        }
        // GET: Hashtag
        public ActionResult Index()
        {
            _ = CreateTagService();
            var model = Array.Empty<TagListItem>();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Details(string id)
        {
            var svc = CreateTagService();
            var model = svc.GetPostsByHashtagId(id);
            return View(model);
        }
    }
}
