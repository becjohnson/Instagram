using AutoMapper;
using Instagram.Data;
using Instagram.Model.Post_Model;
using Instagram.Service;
using Instagram.WebMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace Instagram.WebMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IWebHostEnvironment webHost;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly PostService _svc;
        public PostController(PostService service)
        {
            _svc = service;
        }
        private PostService CreatePostService()
        {
            ApplicationUser user = new ApplicationUser();
            IWebHostEnvironment webhost = webHost;
            ApplicationDbContext ctx = _dbContext;
            IMapper mapper = _mapper;
            Guid userId = user.UserId;
            var service = new PostService(userId, webhost, ctx, mapper);
            return service;
        }
        // GET: Post
        public ActionResult Index()
        {
            _ = CreatePostService();
            var model = new PostListItem[0];
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreate model)
        {
            if (ModelState.IsValid)
            {
                var service = CreatePostService();
                if (service.Create(model))
                {
                    TempData["SaveResult"] = "Your post was created.";
                    return RedirectToAction("Index");
                };
                ModelState.AddModelError("", "Post could not be created.");
            }
            return View(model);
        }
        public ActionResult Details(string id)
        {
            var svc = CreatePostService();
            var model = svc.GetPostById(id);
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var service = CreatePostService();
            var detail = service.GetPostById(id);
            var model =
                new PostEdit
                {
                    PostId = detail.PostId,
                    Content = detail.Content,
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, PostEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.PostId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreatePostService();
            if (service.Update(model))
            {
                TempData["Save Result"] = "Your post was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your post could not be updated.");
            return View(model);
        }
        private string UploadedVideo(PostCreate model)
        {
            string uniqueFileName = null;
            if (model.Video != null)
            {
                string uploadsFolder = Path.Combine(webHost.WebRootPath, "audios");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Video.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using var fileStream = new FileStream(filePath, FileMode.Create);
                model.Video.CopyTo(fileStream);
            }
            return uniqueFileName;
        }
    }
}
