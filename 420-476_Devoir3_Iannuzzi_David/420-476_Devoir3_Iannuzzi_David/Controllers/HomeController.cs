using _420_476_Devoir3_Iannuzzi_David.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _420_476_Devoir3_Iannuzzi_David.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(String id, String psw, String remember)
        {
            HttpCookie uCookie = Request.Cookies.Get("userName");
            Session["signedIn"] = "false";

            using (NORTHWND_Entities context = new NORTHWND_Entities())
            {


                //Affichage en fonction du cookie
                if (uCookie != null)
                {
                    if (uCookie.Value != null)
                    {
                        //Si le username est sauvegardé (Client)
                        if (uCookie.Value.Length > 0)
                        {

                            ViewBag.id = uCookie.Value;
                            ViewBag.check = "yes";
                        }
                        //En cas d'exception..
                        else
                        {
                            ViewBag.id = "";
                            ViewBag.check = "";
                        }
                    }
                }
                //Si pas de cookie
                else
                {
                    ViewBag.id = "";
                    ViewBag.check = "";
                }

                //Sign in
                if (id != null && psw != null)
                {
                    if (id != "" && psw != "")
                    {
                        //Cherche dans les Users prédéfinis
                        foreach (var p in context.Users)
                        {
                            if (p != null)
                            {
                                //User trouvé
                                if (id.Equals(p.Login) && psw.Equals(p.Password))
                                {
                                    //Se souvenir de l'identifiant
                                    if (remember == "yes")
                                    {
                                        uCookie = new HttpCookie("userName");
                                        uCookie.Value = id;
                                        uCookie.Expires = DateTime.Now.AddDays(7);
                                        Response.Cookies.Add(uCookie);
                                    }
                                    //Oublier l'identifiant
                                    else
                                    {
                                        uCookie = new HttpCookie("userName");
                                        uCookie.Expires = DateTime.Now.AddDays(-1);
                                        Response.Cookies.Add(uCookie);
                                    }
                                    Session["userName"] = p.Firstname + " " + p.Lastname; //Nom du user connecté
                                    Session["signedIn"] = "true"; //Confirmation de status de connection
                                    return RedirectToAction("Index", "Products");
                                }
                            }
                        }
                    }
                }
            }
            return View();
        }
    }
}