﻿using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MovieModel;

namespace MovieMVC.Controllers
{
    public class CinemaController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: Cinema
        public async Task<ActionResult> Index()
        {
            var cinemas = db.Cinemas.Include(c => c.Listing);
            return View(await cinemas.ToListAsync());
        }

        // GET: Cinema/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = await db.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);
        }

        // GET: Cinema/Create
        public ActionResult Create()
        {
            ViewBag.FilmID = new SelectList(db.Listings, "FilmID", "Title");
            return View();
        }

        // POST: Cinema/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CinemaID,Name,Website,PhoneNumber,TicketPrice,FilmID")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                db.Cinemas.Add(cinema);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FilmID = new SelectList(db.Listings, "FilmID", "Title", cinema.FilmID);
            return View(cinema);
        }

        // GET: Cinema/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = await db.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            ViewBag.FilmID = new SelectList(db.Listings, "FilmID", "Title", cinema.FilmID);
            return View(cinema);
        }

        // POST: Cinema/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CinemaID,Name,Website,PhoneNumber,TicketPrice,FilmID")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cinema).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FilmID = new SelectList(db.Listings, "FilmID", "Title", cinema.FilmID);
            return View(cinema);
        }

        // GET: Cinema/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = await db.Cinemas.FindAsync(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);
        }

        // POST: Cinema/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Cinema cinema = await db.Cinemas.FindAsync(id);
            db.Cinemas.Remove(cinema);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
