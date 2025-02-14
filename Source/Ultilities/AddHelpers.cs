﻿using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ultilities
{
    public static class AddHelpers
    {
        public static MvcHtmlString LessString(this HtmlHelper html,string str, int maxLength){
            if (str.Length < maxLength)
            {
                return new MvcHtmlString(str);
            }
            return new MvcHtmlString(
                string.Format("{0}...",str.Substring(0,maxLength-3))
                );

        }
        public static string Price2String(this HtmlHelper html, decimal price)
        {
            return string.Format("{0:N0} đ",price);
        }
        //kiểm tra xem đã đăng nhập chưa và lấy cookie cho người dùng
        public static bool IsLogged(this HtmlHelper html)
        {
            if (HttpContext.Current.Session["Logged"] == null)
            {
                if (HttpContext.Current.Request.Cookies["UserId"] != null)
                {
                    int id = int.Parse(HttpContext.Current.Request.Cookies["UserId"].Value);
                    using (var dc = new QLBH_WebEntities())
                    {
                        var user = dc.Users.Where(u => u.f_ID == id).FirstOrDefault();
                        if (user != null)
                        {
                            HttpContext.Current.Session["Logged"] = new UserInfo { Username = user.f_Username, Permission = Ulti.PermissionMapTo(user.f_Permission)};
                            return true;
                        }
                    }
                }
                return false;
            }
            return true;

        }
        //Lấy UserName
        public static UserInfo GetUserInfo(this HtmlHelper html)
            {
                var ui = HttpContext.Current.Session["Logged"] as UserInfo;

                return ui;
            }

            //lấy UserName
            public static string GetUsername(this HtmlHelper html)
            {
                var ui = HttpContext.Current.Session["Logged"] as UserInfo;

                if (ui != null)
                {
                    return ui.Username;
                }
                return "";
            }
            //số lượng sản phẩm hiện lên ở trên
            public static Cart GetCart(this HtmlHelper html)
            {
                if (HttpContext.Current.Session["cart"] == null)
                {
                    HttpContext.Current.Session["cart"] = new Cart();
                }
                return (Cart)HttpContext.Current.Session["cart"];
            }

            public static IList<SelectListItem> GetSLICat(this HtmlHelper html)
            {
                var l = new List<SelectListItem>();
                using (var dc = new QLBH_WebEntities())
                {
                    foreach (var c in dc.Categories.ToList())
                    {
                        l.Add(new SelectListItem
                        {
                            Value = c.CatID.ToString(),
                            Text = c.CatName
                        });
                    }
                }
                return l;
            }

            public static IList<SelectListItem> GetSLIProducer(this HtmlHelper html)
            {
                var l = new List<SelectListItem>();
                using (var dc = new QLBH_WebEntities())
                {
                    foreach (var p in dc.Producers.ToList())
                    {
                        l.Add(new SelectListItem
                        {
                            Value = p.ProducerID.ToString(),
                            Text = p.ProducerName
                        });
                    }
                }
                return l;
            }

            public static IList<SelectListItem> GetSLIStt(this HtmlHelper html)
            {
                var l = new List<SelectListItem>();
                using (var dc = new QLBH_WebEntities())
                {
                    foreach (var c in dc.Statuses.ToList())
                    {
                        l.Add(new SelectListItem
                        {
                            Value = c.SttID.ToString(),
                            Text = c.SttName
                        });
                    }
                }
                return l;
            }
        }
}