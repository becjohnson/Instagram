using Instagram.Data;
using Instagram.Model.Reply_Model;
using Instagram.Service;
using Instagram.Services;
using Instagram.WebMVC.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Instagram.WebMVC.Controllers
{
    public class ReplyController : Microsoft.AspNetCore.Mvc.Controller
    {
        [Authorize]
        private ReplyService CreateReplyService()
        {
            var userId = Guid.Parse(User.Identity.Name);
            var service = new ReplyService(userId);
            return service;
        }
        // GET: Reply
        public ActionResult Index()
        {
            _ = CreateReplyService();
            var model = Array.Empty<ReplyListItem>();
            return View(model);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReplyCreate model)
        {
            if (ModelState.IsValid) return View(model);
            var service = CreateReplyService();
            if (service.CreateReply(model))
            {
                TempData["SaveResult"] = "Your post was created.";
                return RedirectToAction("Index");
            };
            ModelState.AddModelError("", "Reply could not be created.");
            return View(model);
        }
        public ActionResult Details(string id)
        {
            var svc = CreateReplyService();
            var model = svc.GetReplyById(id);
            return View(model);
        }
        public ActionResult Edit(string id)
        {
            var service = CreateReplyService();
            var detail = service.GetReplyById(id);
            var model =
                new ReplyEdit
                {
                    ReplyId = detail.ReplyId,
                    Content = detail.Content,
                };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, ReplyEdit model)
        {
            if (!ModelState.IsValid) return View(model);
            if (model.ReplyId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }
            var service = CreateReplyService();
            if (service.UpdateReply(model))
            {
                TempData["Save Result"] = "Your reply was updated.";
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Your reply could not be updated.");
            return View(model);
        }
    }
}