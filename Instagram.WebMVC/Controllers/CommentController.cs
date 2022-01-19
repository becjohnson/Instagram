using Instagram.Data;
using Instagram.Model.Comment_Model;
using Instagram.Service;
using Instagram.WebMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Instagram.WebMVC.Controllers
{
    public class CommentController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Authorize]
        private CommentService CreateCommentService()
        {
            var userId = Guid.Parse(User.Identity.Name);
            var service = new CommentService(userId);
            return service;
        }
        // GET: Comment
        public ActionResult Index()
        {
            _ = CreateCommentService();
            var model = Array.Empty<CommentListItem>();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentCreate model)
        {
            if (ModelState.IsValid) return View(model);
            var service = CreateCommentService();
            if (service.CreateComment(model))
            {
                TempData["SaveResult"] = "Your post was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Comment could not be created.");
            return View(model);
        }
        public ActionResult Details(string id)
        {
            var svc = CreateCommentService();
            var model = svc.GetCommentById(id);
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var service = CreateCommentService();
            var detail = service.GetCommentById(id);
            var model =
                new CommentEdit
                {
                    CommentId = detail.CommentId,
                    Content = detail.Content,
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, CommentEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.CommentId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateCommentService();
            if (service.UpdateComment(model))
            {
                TempData["Save Result"] = "Your comment was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your comment could not be updated.");
            return View(model);
        }
    }
}