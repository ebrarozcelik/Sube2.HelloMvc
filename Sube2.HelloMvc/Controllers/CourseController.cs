using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sube2.HelloMvc.Models;

namespace Sube2.HelloMvc.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Dersler.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCourse(Ders ders)
        {
            if (ders != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Dersler.Add(ders);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditCourse(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ders = ctx.Dersler.Find(id);
                return View(ders);
            }
        }

        [HttpPost]
        public IActionResult EditCourse(Ders ders)
        {
            if (ders != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ders).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCourse(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                ctx.Dersler.Remove(ctx.Dersler.Find(id));
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
