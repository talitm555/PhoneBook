using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using PhoneBook.Models;
namespace PhoneBook.Controllers
{
    public class PersonController : Controller
    {
        public static PhoneBookDbEntities db = new PhoneBookDbEntities();
        // GET: Person
        public ActionResult Index()
        {
            return View(db.People.ToList());
        }

        // GET: Person/Details/5
        public ActionResult Details(int id)
        {
            var person = db.People.Single(c => c.PersonId == id);
            return View(person);
        }

        // GET: Person/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: Person/Create
        [HttpPost]
        public ActionResult Create(Person p)
        {
            try
            {
                // TODO: Add insert logic here
                p.AddedBy = User.Identity.GetUserId();
                p.UpdateOn = DateTime.Today.Date;
                db.People.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");

                
            }
            catch
            {
                return View();
            }
        }
        // GET: Person/Edit/5
        public ActionResult Edit(int id)
        {
            var person = db.People.Single(c => c.PersonId == id);
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Person p)
        {
            try
            {
                // TODO: Add update logic here
                var u_person = db.People.Single(c => c.PersonId == id);
                TryUpdateModel(u_person);
                u_person.UpdateOn = DateTime.Today.Date;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Person/Delete/5
        public ActionResult Delete(int id)
        {
            var con = db.People.Single(c => c.PersonId == id);
            ViewBag.totalContacts = con.Contacts.Count();
            return View(con);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Person p)
        {
            try
            {
                // TODO: Add delete logic here
                var toDelete = db.People.Single(c => c.PersonId == id);
                foreach (Contact c in db.Contacts)
                {
                    db.Contacts.Remove(c);
                }

                toDelete.Contacts.Clear();
                db.People.Remove(toDelete);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(p);
            }
        }

        // GET: Contact/Create
        public ActionResult CreateContact()
        {
            return View();
        }

        // POST: Contact/Create]\
        [HttpPost]
        public ActionResult CreateContact(int id, Contact c)
        {
            try
            {
                // TODO: Add insert logic here

                var p = db.People.Single(co => co.PersonId == id);
                c.PersonId = p.PersonId;
                p.Contacts.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Details/5
        public ActionResult ViewContacts(int id)
        {
            var person = db.People.Single(c => c.PersonId == id);
            return View(person.Contacts.ToList());
        }

        // GET: Contact/Edit/5
        public ActionResult EditContact(int id)
        {
            var contact = db.Contacts.Single(c => c.ContactId == id);
            return View(contact);
        }

        // POST: Contact/Edit/5
        [HttpPost]
        public ActionResult EditContact(int id, Contact con)
        {
            try
            {
                // TODO: Add update logic here
                var u_contact = db.Contacts.Single(c => c.ContactId == id);
                TryUpdateModel(u_contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Contact/Delete/5
        public ActionResult DeleteContact(int id)
        {
            var con = db.Contacts.Single(c => c.ContactId == id);
            return View(con);
        }

        // POST: Person/Delete/5
        [HttpPost]
        public ActionResult DeleteContact(int id, Contact p)
        {
            try
            {
                // TODO: Add delete logic here
                var con = db.Contacts.Single(c => c.ContactId == id);
                db.Contacts.Remove(con);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View(p);
            }
        }
    }
}