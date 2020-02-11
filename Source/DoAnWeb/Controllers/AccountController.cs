using DoAnWeb.Models;
using DoAnWeb.Ultilities;
using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace DoAnWeb.Controllers
{
    public class AccountController : Controller
    {
         //[AuthActionFilter]

        // GET: Account/Login
        public ActionResult Login()
        {
             
            return View();
        }

        //post: Account/Login
        [HttpPost]
        public ActionResult Login(UserInfo ui)
        {
            var pass = Ulti.Md5Hash(ui.Password);
            using (var dc = new QLBH_WebEntities())
            {
                var test = dc.Users.ToList();
                var user = dc.Users.Where(u => u.f_Username == ui.Username
                    && u.f_Password == pass).FirstOrDefault();
            
                 if (user != null){
                    if(user.f_Permission == 0)
                    {
                        Session["Logged"] = ui;
                        Session["Updated"] = null;
                        Response.Cookies["UserId"].Value = user.f_ID.ToString();
                        Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(7);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //them chuc nang admin
                        ui.Permission = user.f_Permission;
                        //cookie
                        Session["Logged"] = ui;
                        Session["Updated"] = null;
                        Response.Cookies["UserId"].Value = user.f_ID.ToString();
                        Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(7);

                        return RedirectToAction("Index", "ManageProduct");// trả về trang Index nếu đã nhập đúng thông tin
                    }
                   
                }
              
              
                else {
                    ViewBag.ErrorMsg = "Thông tin đăng nhập chưa đúng";
                }
            }
            return View();
        }
        //post: Account/Logout
        [HttpPost]
        public ActionResult Logout()
        {
            Session["Logged"] = null;
            Session["cart"] = null;//khi chúng ta thanh toán và thoát ra đăng nhập lại giỏ hàng sẽ trở về 0
            Response.Cookies["UserId"].Expires=DateTime.Now.AddDays(-1);//-1 là ngày hôm qua
            return RedirectToAction("Index", "Home");
 
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            var u = new UserRegisting
            {
                Username="user001",
                Password="1234",
                PasswordRetype="1234",
                Name="user 0009",
                DOB="12/6/1999",
            };
            return View(u);
        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]

        //kiểm tra captch
        //Lưu thông tin đăng kí vào CSDL
        public ActionResult Register(UserRegisting user)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message
                ViewBag.ErrorMsg = "Incorrect CAPTCHA code!";
            }
            else
            {
                // TODO: Captcha validation passed, proceed with protected action
                var u = new User
                {
                    f_Username = user.Username,
                    f_Password = Ulti.Md5Hash(user.Password),
                    f_Name = user.Name,
                    f_Email = user.Email,
                    f_DOB = DateTime.ParseExact(user.DOB, "d/m/yyyy", null)
                };
                using (var dc = new QLBH_WebEntities())
                {
                    dc.Users.Add(u);
                    dc.SaveChanges();
                }
                Session["Registered"] = 1;
                return RedirectToAction("Login", "Account");
            }      
            return View();
        }
        //kiểm tra đăng nhập nếu chưa đăng nhập thì sẽ chuyển về file profile còn đăng nhập rồi sẽ chuyển về gile profile
      
        public ActionResult Profile()
        {
            //if (Session["Logged"]==null)
            //{
            //    return RedirectToAction("Login", "Account");
            //}
            var ui = Session["Logged"] as UserInfo;
            return View(ui);
        }

        public ActionResult UpdateProfile()
        {
            var ui = Session["Logged"] as UserInfo;
            using (var qlbh = new QLBH_WebEntities())
            {
                var u = new User();
                u = qlbh.Users.Where(uo => uo.f_Username == ui.Username).FirstOrDefault();
                ui.FullInfo = u;
                return View(ui);
            }

        }

        [HttpPost]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "ExampleCaptcha", "Incorrect CAPTCHA code!")]

        public ActionResult UpdateProfile(UserInfo ui)
        {
            if (!ModelState.IsValid)
            {
                // TODO: Captcha validation failed, show error message
                ViewBag.ErrorMsg = "Incorrect CAPTCHA code!";
            }
            else
            {
                User u = ui.FullInfo;
                u.f_Password = Ulti.Md5Hash(ui.Password);
                using (var qlbh = new QLBH_WebEntities())
                {
                    var oldUser = qlbh.Users.Where(ou => ou.f_Username == ui.Username).FirstOrDefault();
                    oldUser.f_Name = u.f_Name;
                    oldUser.f_Password = u.f_Password;
                    oldUser.f_Email = u.f_Email;
                    oldUser.f_DOB = u.f_DOB;
                    qlbh.Entry(oldUser).State = EntityState.Modified;
                    qlbh.SaveChanges();
                }
                Session["Updated"] = 1;
                Session["Logged"] = null;
                Session["cart"] = null;
                Response.Cookies["UserId"].Expires = DateTime.Now.AddDays(-1);
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        public ActionResult Dashboard()
        {
            return View("Dashboard");
        }
    }
}