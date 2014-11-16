using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GiftsSystem.Data;
using GiftsSystem.Models;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using GiftsSystem.Web.ViewModels.User;

namespace GiftsSystem.Web.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IGiftsSystemData data)
            : base(data)
        {

        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
       {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = this.data.Users.All().Where(u => u.Id == id).Project().To<DetailsUserViewModel>().FirstOrDefault();
                 
            if (user == null)
            {
                return Redirect("Account/Login");
            }
                      
            return View("Details",user);
        }


        // GET: RegularUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Details", new { id = id });
            }

            var user = this.data.Users.All().Where(u => u.Id == id).Project().To<EditUserViewModel>().FirstOrDefault();
            if (user == null)
            {
                return RedirectToAction("Details", new { id = id });
            }

            return View("Edit",user);
        }

        // POST: RegularUsers/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "Image")]EditUserViewModel model, HttpPostedFileBase Image)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(regularUser).State = EntityState.Modified;
                //db.SaveChanges();
                //this.data.Users.UpdateValues(c => new 
                //{
                //    Id=model.ID,
                //    UserName = model.UserName,
                //    ModifiedOn = DateTime.Now
                //});

                var userToUpdate = this.data.Users.GetById(model.ID);

                userToUpdate.UserName = model.UserName;
                userToUpdate.WishLists = model.GiftsCollections;


                //Add image
                if (Image != null && Image.ContentLength > 0)
                {
                    byte[] imgBinaryData = new byte[Image.ContentLength];
                    int readResult = Image.InputStream.Read(imgBinaryData, 0, Image.ContentLength);
                    model.Image = imgBinaryData;
                }

                this.data.SaveChanges();

                return RedirectToAction("Details", new { id=model.ID });
            }

            return RedirectToAction("Details", new { id = model.ID });
        }

        //// GET: RegularUsers/Delete/5
        //public ActionResult Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    RegularUser regularUser = db.ApplicationUsers.Find(id);
        //    if (regularUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(regularUser);
        //}

        //// POST: RegularUsers/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(string id)
        //{
        //    RegularUser regularUser = db.ApplicationUsers.Find(id);
        //    db.ApplicationUsers.Remove(regularUser);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
