using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeddingProject.Models;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Newtonsoft.Json;
using System.Data.Entity;


namespace WeddingProject.Controllers
{
    public class MoController : Controller
    {
        private WeddingDBEntities db = new WeddingDBEntities();
        // GET: Mo
        public ActionResult Index()
        {//首页
            var data = db.Images.Where(d => d.uid == 1).OrderBy(d => d.id).Skip(0).Take(5).ToList();
           ViewBag.da = db.Images.Where(d => d.uid == 1).OrderBy(d => d.id).Skip(6).Take(14).ToList();

            return View(data);
        }
        //摄影基地详情
        public ActionResult She(int? id)
        {
            int idd = 0;
            if (Session["uid"] != null)
            {
                idd = Convert.ToInt32(Session["uid"].ToString());
            }
            ViewBag.tao = db.Taocan.Where(d => d.uid == idd).ToList();
            ViewBag.i = db.Changdi.SingleOrDefault(d => d.ID == id);
            return View();
        }
        //套餐删除ajax
        public ActionResult Delete(int? id)
        {

            // 从数据库中删除项目，并返回成功或失败
            var a = db.Taocan.SingleOrDefault(d => d.ID == id);
            db.Taocan.Remove(a);
            if (db.SaveChanges() > 0)
                return Json(new { data = true });
            else return Json(new { data = false });
        }
        //套餐详情
        public ActionResult Ue(int id)
        {
            ViewBag.tao = db.Taocan.SingleOrDefault(d => d.ID == id);

            return View();
        }
        //修改头像
        public ActionResult Updateuser(UserTB u, HttpPostedFileBase File)
        {
            int idd = Convert.ToInt32(Session["uid"].ToString());
            UserTB d = db.UserTB.SingleOrDefault(a => a.UserID == idd);
            if (File != null && File.ContentLength > 0)
            {
                using (Image originalImage = Image.FromStream(File.InputStream, true, true))
                {
                    int newWidth = (int)(originalImage.Width * 0.5);
                    int newHeight = (int)(originalImage.Height * 0.5);
                    Bitmap compressedImage = new Bitmap(newWidth, newHeight);
                    using (Graphics g = Graphics.FromImage(compressedImage))
                    {
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                        string originalFilePath = Server.MapPath("/Content/img/Usertou/" + File.FileName);
                        if (System.IO.File.Exists(originalFilePath))
                        {
                            System.IO.File.Delete(originalFilePath);
                        }
                    }

                    string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(File.FileName);
                    newFileName = newFileName.Substring(0, 28) + ".jpg";
                    var e = Path.Combine(Server.MapPath("/Content/img/Usertou/" + newFileName));

                    // 替换为 compressedImage
                    compressedImage.Save(e, ImageFormat.Jpeg);

                    originalImage.Dispose();
                    compressedImage.Dispose();
                    d.img = newFileName;

                }
            }
            if (u.Name != null)
            {
                d.Name = u.Name;
            }
            if (u.pwd != null) { d.pwd = u.pwd; }
            if (u.Age != null) { d.Age = u.Age; }
            if (u.Sex != null) { d.Sex = u.Sex; }

            if (u.Addr != null) { d.Addr = u.Addr; }
            if (db.SaveChanges() > 0)
            {
                return Redirect("/Home/UserZhong");
            }
            return Redirect("/Home/UserZhong");
        }
        //图库展示
        public ActionResult Tu()
        {
            int idd = Convert.ToInt32(Session["uid"].ToString());
            var f = db.Images.Where(d => d.uid == idd).OrderBy(o => Guid.NewGuid()).ToList();
            return View(f);
        }
        //上传图片
        public ActionResult Tuu(Images d, HttpPostedFileBase File)
        {
            if (File != null)
            {
                using (Image originalImage = Image.FromStream(File.InputStream, true, true))
                {
                    int newWidth = (int)(originalImage.Width * 0.5);
                    int newHeight = (int)(originalImage.Height * 0.5);
                    Bitmap compressedImage = new Bitmap(newWidth, newHeight);
                    using (Graphics g = Graphics.FromImage(compressedImage))
                    {
                        g.CompositingQuality = CompositingQuality.HighQuality;
                        g.SmoothingMode = SmoothingMode.HighQuality;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        g.DrawImage(originalImage, 0, 0, newWidth, newHeight);
                        string originalFilePath = Server.MapPath("/Content/img/photography/" + File.FileName);

                    }
                    string newFileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(File.FileName);
                    newFileName = newFileName.Substring(0, 28) + ".jpg";
                    var e = Path.Combine(Server.MapPath("/Content/img/photography/" + newFileName));

                    // 替换为 compressedImage
                    compressedImage.Save(e, ImageFormat.Jpeg);

                    originalImage.Dispose();
                    compressedImage.Dispose();
                    d.img = newFileName;
                }
                int idd = Convert.ToInt32(Session["uid"].ToString());
                d.uid = idd;
                db.Images.Add(d);
                if (db.SaveChanges() > 0)
                {
                    return Redirect("/Mo/Tu");
                }
                return Redirect("/Mo/Tu");
            }
            return Redirect("/Mo/Tu");
        }
        //删除图片
        public ActionResult Del(int? id)
        {
            Images d = db.Images.SingleOrDefault(x => x.id == id);
            string originalFilePath = Server.MapPath("/Content/img/photography/" + d.img);
            if (System.IO.File.Exists(originalFilePath))
            {
                System.IO.File.Delete(originalFilePath);
            }
            db.Images.Remove(d);
            if (db.SaveChanges() > 0)
            {
                return Content("true");
            }
            return Content("f");
        }
        //修改图片名字
        public ActionResult Tuuu(Images d)
        {
            Images s = db.Images.SingleOrDefault(x => x.id == d.id);
            s.name = d.name;
            if (db.SaveChanges() > 0)
            {
                return Redirect("/Mo/Tu");
            }
            return Redirect("/Mo/Tu");
        }
        //收益页面
        public ActionResult Shou()
        {
            var data = db.Shouyi.Select(s => s.datatime.Year).Distinct().OrderByDescending(y => y).ToList();
            return View(data);
        }
        //返回想 x y
        public ActionResult GetShouyiData(int? id)
        {
            var data = db.Shouyi.Where(s => s.datatime.Year == DateTime.Now.Year)
                       .GroupBy(s => s.datatime.Month)
                       .Select(g => new { x = g.Key, y = g.Sum(s => s.Charge) })
                       .ToList();
            if (id != null)
            {
                data = db.Shouyi.Where(s => s.datatime.Year == id)
                     .GroupBy(s => s.datatime.Month)
                     .Select(g => new { x = g.Key, y = g.Sum(s => s.Charge) })
                     .ToList();
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //主页酒店
        public ActionResult Lo(int? param1)
        {
            if (param1 == null)
            {
                var data = db.Hote.OrderBy(d => d.id).Skip(0).Take(6).Select(h => new { h.id, h.name, h.img, h.Price, h.rongliang }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.Hote.Where(a => a.type == param1).OrderBy(d => d.id).Skip(0).Take(6).Select(h => new { h.id, h.name, h.img, h.Price, h.rongliang }).ToList();
                return Json(data, JsonRequestBehavior.AllowGet);
            }
        
        }



        //主页图片
        //public ActionResult Img()
        //{
        //    var data = db.Images.Where(d=>d.uid==1).OrderBy(d=>d.id).Skip(0).Take(5).Select(h => new { h.id,h.img}).ToList();
        //    var da = db.Images.Where(d => d.uid == 1).OrderBy(d => d.id).Skip(6).Take(14).Select(h => new { h.id, h.img }).ToList();
        //    return Json(new {data, da }, JsonRequestBehavior.AllowGet );
        //}



    }     
}
