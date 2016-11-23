using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using MovieModel;

namespace MovieMVC.Controllers
{
    public class MovieListingController : Controller
    {
        private MovieContext db = new MovieContext();

        // GET: MovieListing
        public async Task<ActionResult> Index()
        {
            return View(await db.Listings.ToListAsync());
        }

        // GET: MovieListing/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieListing movieListing = await db.Listings.FindAsync(id);
            if (movieListing == null)
            {
                return HttpNotFound();
            }
            return View(movieListing);
        }

        // GET: MovieListing/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MovieListing/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FilmID,Title,Certification,Genre,Description,RunTime")] MovieListing movieListing)
        {
            if (ModelState.IsValid)
            {
                db.Listings.Add(movieListing);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(movieListing);
        }

        // GET: MovieListing/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieListing movieListing = await db.Listings.FindAsync(id);
            if (movieListing == null)
            {
                return HttpNotFound();
            }
            return View(movieListing);
        }

        // POST: MovieListing/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FilmID,Title,Certification,Genre,Description,RunTime")] MovieListing movieListing)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movieListing).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(movieListing);
        }

        // GET: MovieListing/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovieListing movieListing = await db.Listings.FindAsync(id);
            if (movieListing == null)
            {
                return HttpNotFound();
            }
            return View(movieListing);
        }

        // POST: MovieListing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            MovieListing movieListing = await db.Listings.FindAsync(id);
            db.Listings.Remove(movieListing);
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
