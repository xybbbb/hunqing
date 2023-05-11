using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI;
using WeddingProject.Models;
namespace WeddingProject.Controllers
{
    public class HomeController : Controller
    {
        private WeddingDBEntities db = new WeddingDBEntities();
        //套餐方案
        public ActionResult Index(int? page, int? id, string Range)
        {
            ViewBag.tao = db.Taocan.Where(w => w.uid == 1).ToList();
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            if (id == null)
            {
                if (string.IsNullOrEmpty(Range))
                {
                    ViewBag.ta = db.Taocan.Where(t => t.uid != 1 && t.puty == true).OrderBy(o => Guid.NewGuid()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.PageSize = pageSize;
                    ViewBag.TotalItems = db.Taocan.Where(t => t.uid != 1 && t.puty == true).Count();
                    return View();
                }
                else
                {
                    // 获取用户选择的价格区间
                    decimal? minPrice = null;
                    decimal? maxPrice = null;
                    if (Range == "5000以下")
                    {
                        maxPrice = 5000;
                    }
                    else if (Range == "5000-10000")
                    {
                        minPrice = 5000;
                        maxPrice = 10000;
                    }
                    else if (Range == "10000-20000")
                    {
                        minPrice = 10000;
                        maxPrice = 20000;
                    }
                    else if (Range == "20000-30000")
                    {
                        minPrice = 20000;
                        maxPrice = 30000;
                    }
                    else if (Range == "30000-50000")
                    {
                        minPrice = 30000;
                        maxPrice = 50000;
                    }
                    else if (Range == "50000-100000")
                    {
                        minPrice = 50000;
                        maxPrice = 100000;
                    }
                    else if (Range == "100000以上")
                    {
                        minPrice = 100000;
                    }

                    // 根据价格区间筛选商品
                    ViewBag.ta = db.Taocan.Where(p => p.uid != 1 && p.puty == true && (!minPrice.HasValue || p.price >= minPrice)
                    && (!maxPrice.HasValue || p.price < maxPrice)).ToList();
                    ViewBag.PageNumber = pageNumber;
                    ViewBag.PageSize = pageSize;
              
                    ViewBag.TotalItems =db.Taocan.Where(p => p.uid != 1 && p.puty == true && (!minPrice.HasValue || p.price >= minPrice)
                    && (!maxPrice.HasValue || p.price < maxPrice)).Count();
                    return View();
                }

            }
            else
            {

                ViewBag.ta = db.Taocan.Where(t => t.uid != 1 && t.puty == true && t.Changdi.ID == id).OrderBy(o => Guid.NewGuid()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalItems = db.Taocan.Where(t => t.uid != 1 && t.puty == true && t.Changdi.ID == id).Count();
                return View();

            }


        }
        //酒店展示
        public ActionResult Taotype(int? id)
        {

            return View();
        }
        public ActionResult Hote(int? id, int? page)
        {

            int pageNumber = (page ?? 1);
            int pageSize = 4;
            if (id != null)
            {
                var items = db.Hote.Where(d => d.type == id).OrderBy(o => o.id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalItems = db.Hote.Where(d => d.type == id).Count();
                return View("Hote", items);

            }
            else
            {
                var items = db.Hote.OrderBy(o => o.id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                ViewBag.PageNumber = pageNumber;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalItems = db.Hote.Count();
                return View("Hote", items);
            }


        }
        //酒店详情
        public ActionResult Hotelx(int? id)
        {


            ViewBag.Hot = db.Hote.SingleOrDefault(h => h.id == id);
            int idd = 0;
            if (Session["uid"] != null)
            {
                idd = Convert.ToInt32(Session["uid"].ToString());
            }
            ViewBag.tao = db.Taocan.Where(d => d.uid == idd).ToList();
            return View();
        }
        //查询员工
        public ActionResult Caxun(int? id)
        {
            ViewBag.yu = db.Yuan.SingleOrDefault(d => d.ID == id);
            int idd = 0;
            if (Session["uid"] != null)
            {
                idd = Convert.ToInt32(Session["uid"].ToString());
            }
            ViewBag.tao = db.Taocan.Where(s => s.uid == idd).ToList();
            return View();
        }
        //套餐添加
        [SubmitLimit(3)]
        public ActionResult Tao(int? pid, int? tyid, string Name)
        {
            if (Session["uid"] == null)
            {
                return Redirect("/Home/Long");
            }

            int id = int.Parse(Request["ID"]);

            var d = db.Taocan.FirstOrDefault(s => s.ID == id);

            if (d != null)
            {

                Taocan r = d;
                if (r.puty != true)
                {
                    switch (tyid)
                    {
                        case 1:
                            r.sheying = pid;

                            break;
                        case 2:

                            r.huazhuang = pid;

                            break;
                        case 3:

                            r.shexian = pid;
                            break;
                        case 4:

                            r.siyi = pid;
                            break;
                        default:

                            break;
                    }
                    if (!string.IsNullOrEmpty(Request.Form["addr"]))
                    {

                        r.addr = Request.Form["addr"];
                    }
                    if (!string.IsNullOrEmpty(Request.Form["price"]))
                    {

                        r.price = decimal.Parse(Request.Form["price"]);
                    }
                    if (!string.IsNullOrEmpty(Request.Form["puty"]))
                    {
                        if (r.addr != null)
                        {

                            r.puty = true;

                            if (db.SaveChanges() > 0)
                            {
                                Shouyi s = new Shouyi();

                                s.Charge = (decimal)r.price;
                                s.datatime = DateTime.Now;


                                db.Shouyi.Add(s);
                                db.SaveChanges();
                                TempData["Message"] = "支付成功！";
                                return Json(TempData["Message"]);
                            }
                        }
                        else
                        {
                            TempData["Message"] = "请输入地址再支付吧！";
                            return Json(TempData["Message"]);
                        }

                    }
                    if (!string.IsNullOrEmpty(Request.Form["jiHua"]))
                    {
                        DateTime jiHuaTime;
                        if (DateTime.TryParse(Request.Form["jiHua"], out jiHuaTime))
                        {
                            // 时间转换成功，将时间赋值给r.jiHua
                            r.jiHua = jiHuaTime;
                        }
                    }
                    if (!string.IsNullOrEmpty(Request.Form["hid"]))
                    {

                        r.HoID = int.Parse(Request.Form["hid"]);
                    }
                    if (!string.IsNullOrEmpty(Name))
                    {
                        r.Name = Name;
                    }
                    if (!string.IsNullOrEmpty(Request.Form["cid"]))
                    {

                        r.ChangID = int.Parse(Request.Form["cid"]);
                    }

                    if (db.SaveChanges() > 0)
                    {
                        TempData["Message"] = "修改成功！";
                        return Redirect("/Home/UserZhong");
                    }
                    else
                    {
                        TempData["Message"] = "支付失败！";
                        return Redirect("/Home/UserZhong");
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Request.Form["jiHua"]))
                    {
                        DateTime jiHuaTime;
                        if (DateTime.TryParse(Request.Form["jiHua"], out jiHuaTime))
                        {
                            // 时间转换成功，将时间赋值给r.jiHua
                            r.jiHua = jiHuaTime;
                        }
                    }
                    if (!string.IsNullOrEmpty(Name))
                    {
                        r.Name = Name;
                    }
                    if (db.SaveChanges() > 0)
                    {
                        TempData["Message"] = "修改成功！";
                        return Redirect("/Home/UserZhong");
                    }
                    else
                        TempData["Message"] = "您已经完成订单！";
                    return Redirect("/Home/UserZhong");
                }

            }
            else
            {
                int uid = int.Parse(Session["uid"].ToString());
                Taocan t = new Taocan();

                switch (tyid)
                {
                    case 1:
                        t.sheying = pid;
                        break;
                    case 2:
                        t.huazhuang = pid;
                        break;
                    case 3:
                        t.shexian = pid;
                        break;
                    case 4:
                        t.siyi = pid;
                        break;
                    default:
                        break;
                }
                if (!string.IsNullOrEmpty(Request.Form["jiHua"]))
                {
                    DateTime jiHuaTime;
                    if (DateTime.TryParse(Request.Form["jiHua"], out jiHuaTime))
                    {
                        // 时间转换成功，将时间赋值给t.jiHua
                        t.jiHua = jiHuaTime;
                    }
                }
                if (!string.IsNullOrEmpty(Request.Form["hid"]))
                {

                    t.HoID = int.Parse(Request.Form["hid"]);
                }
                if (!string.IsNullOrEmpty(Request.Form["cid"]))
                {

                    t.ChangID = int.Parse(Request.Form["cid"]);
                }
                if (!string.IsNullOrEmpty(Name))
                {
                    t.Name = Name;
                }
                t.datame = DateTime.Now;
                t.uid = uid;
                t.puty = false;

                db.Taocan.Add(t);
                db.SaveChanges();

                TempData["Message"] = "添加成功！";
                return Redirect("/Home/UserZhong");

            }
        }
        //套餐方案添加
        public ActionResult Ta(int? HoID, int? ChangID, int? siyi, int? huazhuang, int? sheying, int? shexian, string Name, decimal? price)
        {
            if (Session["uid"] == null)
            {
                return Redirect("/Home/Long");
            }
            Taocan g = new Taocan();
            g.datame = DateTime.Now;
            g.puty = false;
            g.jiHua = null;
            int uid = int.Parse(Session["uid"].ToString());
            g.uid = uid;
            g.addr = null;
            g.siyi = siyi;
            g.shexian = shexian;
            g.Name = Name;
            g.price = price;
            g.huazhuang = huazhuang;
            g.sheying = sheying;
            g.ChangID = ChangID;
            g.HoID = HoID;
            db.Taocan.Add(g);
            if (db.SaveChanges() > 0)
            {
                TempData["Message"] = "添加成功！";
                return Redirect("/Home/UserZhong");
            }
            return Redirect("/Home/UserZhong");
        }
        //员工展示
        public ActionResult Contact(int? id)
        {

            if (id == null)
            {
                ViewBag.yuan = db.Yuan.ToList();
            }
            else
            {

                ViewBag.yuan = db.Yuan.Where(w => w.typeid == id).ToList();
            }
            return View();
        }
        //婚庆用品展示
        public ActionResult Lifu()
        {
            ViewBag.ite = db.Items.Where(f => f.type == 6).Take(8).OrderBy(o => Guid.NewGuid()).ToList();
            ViewData["hunsha"] = db.Items.Where(f => f.type == 3).Take(8).OrderBy(o => Guid.NewGuid()).ToList();
            return View();
        }
        //物品 展示
        public ActionResult Wupin(int? page, int? ii)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 15;
            List<WeddingProject.Models.Items> items;
            if (ii != null)
            {
                items = db.Items.Where(i => i.type == ii).OrderBy(i => i.type).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                items = db.Items.OrderBy(i => i.id).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            if (ii != null)
            {
                ViewBag.TotalItems = db.Items.Count((i => i.type == ii));
            }
            else
            {
                ViewBag.TotalItems = db.Items.Count();
            }
            return View(items);

        }
        //物品模糊查询
        public ActionResult Wu(int? page, string name)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;
            var items = db.Items.Where(i => i.name.Contains(name)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = db.Items.Where(i => i.name.Contains(name)).Count();
            return View("Wupin", items);

        }
        //物品详情
        public ActionResult Gomai(int? id)
        {

            ViewBag.Id = db.Items.SingleOrDefault(i => i.id == id);
            return View();
        }
        //登录页面
        public ActionResult Long(string Url)
        {
            Session.Remove("ui");
            Session.Remove("uid");
            Session["ReturnUrl"] = Url;
            return View();
        }
        //登录
        public ActionResult Login(string Uid, string pwd)
        {

            UserTB Utb = db.UserTB.FirstOrDefault(d => d.Nubler == Uid || d.Name == Uid);//判断账号是否存在
            if (Utb == null)//不存在返回账号不存在
            {
                ViewBag.tai = "账号不正确";
            }
            else if (pwd != Utb.pwd)
            {
                ViewBag.tai = "密码不正确";//返回文字密码错误
            }
            else
            {

                Session["ui"] = Utb.Name;//验证成功，存入uid和姓名
                Session["uid"] = Utb.UserID;
                string returnUrl = Session["ReturnUrl"] as string;//获取点击到等时的路径
                if (!string.IsNullOrEmpty(returnUrl))//非空返回到点击时正在浏览的网页
                {
                    Session.Remove("ReturnUrl");//清空
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction("Index", "Mo");//登录默认到首页
                }

            }

            return View("Long");
        }
        //注册
        public ActionResult LogUp(UserTB u)
        {
            var Utb = db.UserTB.FirstOrDefault(d => d.Nubler == u.Nubler);
            if (Utb == null)
            {
                u.Age = 18;

                db.UserTB.Add(u);
                if (db.SaveChanges() > 0)
                {

                    return Json(new { success = true, message = "注册成功" });
                }
                else
                {
                    return Json(new { success = false, message = "注册失败" });
                }
            }
            else
            {
                return Json(new { success = false, message = "账号存在" });
            }
        }
        //提交订单购物车
        [SubmitLimit(3)]
        public ActionResult Ding(dingdan k)
        {
            if (Session["uid"] == null)
            {
                return Redirect("/Home/Long");
            }

            k.date = DateTime.Now;
            if (Request.Form["action"] == "addCart")
            {
                // 执行加入购物车操作
                k.state = "未支付";
            }

            k.price = int.Parse(Request["shuliang"]) * k.price;
            k.Userid = Convert.ToInt32(Session["uid"]);
            db.dingdan.Add(k);
            if (db.SaveChanges() > 0)
            {
                TempData["Message"] = "添加成功！";
                return RedirectToAction("Wupin");
            }
            return Redirect("/Home/Wupin");
        }
        //用户主页
        public ActionResult UserZhong()
        {
            if (Session["uid"] == null)
            {
                return View("Long");
            }

            int id = int.Parse(Session["uid"].ToString());

            var data = db.dingdan.Where(d => d.Userid == id).ToList();
            ViewBag.user = db.UserTB.SingleOrDefault(d => d.UserID == id);
            ViewBag.tao = db.Taocan.Where(d => d.uid == id).OrderBy(d => d.puty).ThenByDescending(d => d.ID).ToList();
            return View(data);
        }
        //摄影基地
        public ActionResult Sheying(int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 9;

            var items = db.Changdi.OrderBy(o => Guid.NewGuid()).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = db.Changdi.Count();
            return View(items);
        }
        //订单删除ajax
        public ActionResult Delit(int id)
        {
            dingdan a = db.dingdan.SingleOrDefault(d => d.id == id);
            db.dingdan.Remove(a);
            if (db.SaveChanges() > 0)
                return Content("true");
            else return Content("false");
        }
        //场地模糊查询
        public ActionResult Se(int? page, string name)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 8;
            var items = db.Changdi.Where(i => i.Name.Contains(name)).ToList();
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalItems = db.Changdi.Where(i => i.Name.Contains(name)).Count();
            return View("Sheying", items);
        }
        //订单支付
        public ActionResult Din(int ID, string addr)
        {
            dingdan e = db.dingdan.SingleOrDefault(d => d.id == ID);
            e.addr = addr;
            if (!string.IsNullOrEmpty(Request.Form["put"]))
            {

                e.state = "已支付";
            }
            else
            {
                TempData["Message"] = "支付失败！";
                return Redirect("/Home/UserZhong");
            }
            if (db.SaveChanges() > 0)
            {
                Shouyi s = new Shouyi();
                s.Charge = (decimal)e.price;
                s.datatime = DateTime.Now;
                db.Shouyi.Add(s);
                db.SaveChanges();
                TempData["Message"] = "支付成功！";
                return Redirect("/Home/UserZhong");
            }
            return Redirect("/Home/UserZhong");
        }

    }

}