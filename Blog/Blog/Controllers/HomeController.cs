using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blog.Models;
using PagedList;
using WebMatrix.WebData;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private BlogDBContext db = new BlogDBContext();

        public object Index(int? page, string PostTag)
        {
            // Tags Filter

            var tags = db.Tags.ToList();

            var tagLst = new List<string>();

            foreach(var t in tags)
            {
                tagLst.Add(t.Description);
            }
                        
            ViewBag.PostTag = new SelectList(tagLst);

            var posts = db.Posts.Include(x => x.Author).Include(x => x.Tags).Include(x => x.Replies).ToList(); //returns List<Post>

            if (!String.IsNullOrEmpty(PostTag))
            {
                var tag = db.Tags.Where(x => x.Description == PostTag).First();
                posts = posts.Where(x => x.Tags.Contains(tag)).ToList();
            }

            // Paging
                     
            var pageNumber = page ?? 1;

            var onePageOfPosts = posts.ToPagedList(pageNumber, 5); // will only contain 5 posts

            ViewBag.Page = onePageOfPosts;

            return View();
        }

        // GET: /Details/5

        public ActionResult Details(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // GET: /Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Post post)
        {
            post.Date = System.DateTime.Now;
            post.Author = db.UserProfiles.Find(WebSecurity.CurrentUserId);

            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        //
        // GET: /Edit/5

        public ActionResult Edit(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(post);
        }

        //
        // GET: /Delete/5

        public ActionResult Delete(int id = 0)
        {
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        //
        // POST: /Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //
        // GET: /Reply

        [Authorize]
        public ActionResult Reply(int id = 0)
        {
            PostReply postReply = new PostReply();
            postReply.PostId = id;
            
            if ( db.Posts.Find(id) == null)
            {
                return HttpNotFound();
            }
            return View(postReply);
        }
        

        //
        // POST: /Reply
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply(PostReply postReply)
        {  
            Reply reply = new Reply(); 

            if (ModelState.IsValid)
            {               
                reply.Message = postReply.Comment;
                reply.Date = System.DateTime.Now;
                reply.Replier = db.UserProfiles.Find(WebSecurity.CurrentUserId);
                Post post = db.Posts.Include(x=> x.Replies).Where(x => x.Id == postReply.PostId).First();
                post.Replies.Add(reply); //para que gurde la relacion
                db.Replies.Add(reply);               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(postReply);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}