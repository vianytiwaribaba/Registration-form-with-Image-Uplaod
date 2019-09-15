using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Imagetry.Controllers
{
    public class AuctionController : Controller
    {
        // GET: Auction
        public ActionResult Index()
        {
            tryContext context = new tryContext();

           var list =  context.tbl_vinay.ToList();

            return View(list);
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(tbl_vinay vinay)
        {
            if (vinay.UserImagefile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(vinay.UserImagefile.FileName);

                string extension = Path.GetExtension(vinay.UserImagefile.FileName);

                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;

                vinay.ImagePath = "~/Image/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);

                vinay.UserImagefile.SaveAs(fileName);
            }

            if (vinay.ImagePath == "/Content/Images/No_Image_Available.jpg")
            {
                vinay.ImagePath = null;
            }


            tryContext context = new tryContext();

            context.tbl_vinay.Add(vinay);

            context.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            tryContext context = new tryContext();

            var update = context.tbl_vinay.Find(id);


            if (update.ImagePath == null)
            {
                update.ImagePath = "~/Image/No_Image_Available.jpg";
            }

            return View(update);
        }

        [HttpPost]
        public ActionResult Edit(tbl_vinay vinay)
        {
            if (vinay.UserImagefile != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(vinay.UserImagefile.FileName);

                string extension = Path.GetExtension(vinay.UserImagefile.FileName);

                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;

                vinay.ImagePath = "~/Image/" + fileName;

                fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);

                vinay.UserImagefile.SaveAs(fileName);
            }

            if (vinay.ImagePath == null)
            {
                vinay.ImagePath = "/Content/Images/No_Image_Available.jpg";
            }


            tryContext context = new tryContext();



            context.Entry(vinay).State = System.Data.Entity.EntityState.Modified;

            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult created()
        {
            return View();
        }

        [HttpPost]
        public ActionResult created(tbl_vinay vinay)
        {

            string path = Server.MapPath("~/App_Data/File");
            string filename = Path.GetFileName(vinay.UserImagefile.FileName);

            string fullpath = Path.Combine(path, filename);

           vinay. UserImagefile.SaveAs(fullpath);

            tryContext context = new tryContext();

            context.tbl_vinay.Add(vinay);

            context.SaveChanges();


            return View();
        }
    }
}