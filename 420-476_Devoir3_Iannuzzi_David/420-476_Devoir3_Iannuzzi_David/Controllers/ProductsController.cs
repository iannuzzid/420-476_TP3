using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using _420_476_Devoir3_Iannuzzi_David.Models;
using System.IO;

namespace _420_476_Devoir3_Iannuzzi_David.Controllers
{
    public class ProductsController : Controller
    {
        private NORTHWND_Entities db = new NORTHWND_Entities();
        string imgPath = "~/App_Data/";

        [HttpGet]
        public ActionResult Index()
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.imgPath = imgPath;
            var products = db.Products.Include(p => p.Category).Include(p => p.Supplier);

            ViewBag.Cats = new SelectList(db.Categories, "CategoryName", "CategoryName");
            return View(products.ToList());
        }

        
        public ActionResult Index(String selection)
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.imgPath = imgPath;
            ViewBag.Cats = new SelectList(db.Categories, "CategoryName", "CategoryName");
            var products = db.Products.Where(db => db.Category.CategoryName.Contains(selection)).Include(p => p.Category).Include(p => p.Supplier);
            
            return View(products.ToList());
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.img = product.Image;
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HttpPostedFileBase fileIn, [Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued,Image")] Product product)
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            if (ModelState.IsValid)
            {
                var fileName = Path.GetFileName(fileIn.FileName);
                var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                fileIn.SaveAs(path);
                product.Image = fileName;

                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", product.SupplierID);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", product.SupplierID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase fileIn, [Bind(Include = "ProductID,ProductName,SupplierID,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued,Image")] Product product)
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            if (ModelState.IsValid)
            {
                if (fileIn != null)
                {
                    var fileName = Path.GetFileName(fileIn.FileName);
                    var path = Path.Combine(Server.MapPath("~/App_Data/"), fileName);
                    fileIn.SaveAs(path);
                    product.Image = fileName;

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", product.SupplierID);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["signedIn"] != "true")
            {
                return RedirectToAction("Index", "Home");

            }
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
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
