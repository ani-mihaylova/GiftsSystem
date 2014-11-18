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
using System.IO;

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

            return View("Details", user);
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

            return View("Edit", user);
        }

        // POST: RegularUsers/Edit/5
        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = this.data.Users.GetById(model.ID);

                userToUpdate.UserName = model.UserName;
                userToUpdate.GiftsCollections = model.GiftsCollections;

                if (model.UploadedImage != null)
                {
                    using (var memory = new MemoryStream())
                    {
                        model.UploadedImage.InputStream.CopyTo(memory);
                        var content = memory.GetBuffer();

                        userToUpdate.Image = new Image
                        {
                            Content = content,
                            FileExtension = model.UploadedImage.FileName.Split(new[] { '.' }).Last()
                        };
                    }
                }

                this.data.Users.Update(userToUpdate);
                this.data.SaveChanges();

                return RedirectToAction("Details", new { id = model.ID });
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
        public ActionResult Image(int id)
        {
            var image = this.data.Images.GetById(id);
            if (image == null)
            {
                throw new HttpException(404, "Image not found");
            }

            return File(image.Content, "image/" + image.FileExtension);
        }
    }
}
