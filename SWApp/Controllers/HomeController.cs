using SWApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SWApp.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase Upload)
        {
            string pathName = Path.GetFileName(Upload.FileName);
            var path = Path.Combine(Server.MapPath("~/File"), pathName);
            Upload.SaveAs(path);
            StreamReader reader = new StreamReader(path, Encoding.Default);            
            var getAllStream = reader.ReadToEnd();
            getAllStream = getAllStream.ToLower();
            var wordListSplitSpace = getAllStream.Split(' ', '.', ' ', ':', ';');
            List<string> wordList = new List<string>();
            foreach (var item in wordListSplitSpace)
            {
                if (!wordList.Any(r=>r.ToString() == item))
                {
                    wordList.Add(item);
                    var word = new Word
                    {
                        Content = item,
                        Count = 0,
                        CreatedDate = DateTime.Now
                    };
                    db.Words.Add(word);
                    db.SaveChanges();
                }
                
            }
            return View();
        }
        
        public JsonResult GetWords(string word)
        {
            var words = db.Words.Where(r => r.Content.Contains(word));
            words = words.OrderBy(r => r.Count);
            return Json(words, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult WordCounter(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                var getWord = db.Words.FirstOrDefault(r => r.Content == word);
                getWord.Count = getWord.Count + 1;
                db.SaveChanges();                
            }
            return null;
        }
    }
}