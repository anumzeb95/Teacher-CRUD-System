using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teachercrud.Models;

namespace teachercrud.Controllers
{
    public class CRUDController : Controller
    {
        // GET: CRUD
        //Create
        public ActionResult create()
        {
            return View();
        }

        
        // it will add the record to the database
        [HttpPost]
        public ActionResult create(user_details model)
        {

          
            using (var context = new teacherEntities())
            {
               
                context.user_details.Add(model);
                context.SaveChanges();
            }
            string message = "Created the record successfully";
            ViewBag.Message = message;

           
            return View();
        }


        //Read

        [HttpGet]
        public ActionResult
            Read()
        {
            using (var context = new teacherEntities())
            {

                var data = context.user_details.ToList();
                return View(data);
            }
        }



        //Update
        public ActionResult Update(int id)
        {
            using (var context = new teacherEntities())
            {
                var data = context.user_details.Where(x => x.Id == id).SingleOrDefault();
                return View(data);
            }
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int id, user_details model)
        {
            using (var context = new teacherEntities())
            {

                var data = context.user_details.FirstOrDefault(x => x.Id == id);

                // Check if any record exist
                if (data != null)
                {
                   
                    data.Name = model.Name;
                    data.DOB = model.DOB;
                    data.Gender = model.Gender;
                    data.CNIC = model.CNIC;
                    data.FatherName = model.FatherName;
                    data.Email = model.Email;
                    data.Address = model.Address;
                    data.Contact = model.Contact;

                    context.SaveChanges();

                    // It will redirect to
                    // the Read method
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult
        Delete(int id)
        {
            using (var context = new teacherEntities())
            {
                var data = context.user_details.FirstOrDefault(x => x.Id == id);
                if (data != null)
                {
                    context.user_details.Remove(data);
                    context.SaveChanges();
                    return RedirectToAction("Read");
                }
                else
                    return View();
            }
        }
    }
}