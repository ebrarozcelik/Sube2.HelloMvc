using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sube2.HelloMvc.Models;

namespace Sube2.HelloMvc.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            using (var ctx = new OkulDbContext())
            {
                var lst = ctx.Ogrenciler.ToList();
                return View(lst);
            }
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Ogrenci ogr)
        {
            if (ogr != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Ogrenciler.Add(ogr);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult EditStudent(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                var ogr = ctx.Ogrenciler.Find(id);
                return View(ogr);
            }
        }

        [HttpPost]
        public IActionResult EditStudent(Ogrenci ogr)
        {
            if (ogr != null)
            {
                using (var ctx = new OkulDbContext())
                {
                    ctx.Entry(ogr).State = EntityState.Modified;
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteStudent(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                ctx.Ogrenciler.Remove(ctx.Ogrenciler.Find(id));
                ctx.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public IActionResult AssignCourse(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                Ogrenci student = ctx.Ogrenciler.Include(item => item.Dersler).FirstOrDefault(item => item.Ogrenciid == id);
                var courseList = ctx.Dersler.ToList();
                ViewBag.CourseId = new SelectList(courseList, "Dersid", "Dersad");
                return View(student);
            }

        }

        public ActionResult AssignCourseStudent(int Ogrenciid, int courseId)
        {
            using (var ctx = new OkulDbContext())
            {
                Ogrenci student = ctx.Ogrenciler.Include(item => item.Dersler).FirstOrDefault(item => item.Ogrenciid == Ogrenciid);
                Ders course = ctx.Dersler.Find(courseId);
                student.Dersler.Add(course);
                ctx.SaveChanges();
                return RedirectToAction("Courses", new { id = student.Ogrenciid });
            }
        }


        public ActionResult Courses(int id)
        {
            using (var ctx = new OkulDbContext())
            {
                Ogrenci student = ctx.Ogrenciler.Include(item => item.Dersler).FirstOrDefault(item => item.Ogrenciid == id);
                return View(student);
            }
        }

    }
}
