using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ContactManager.Controllers
{
    public class ContactController : Controller
    {
        private readonly ContactContext _context;

        public ContactController(ContactContext context)
        {
            _context = context;
        }

        // GET: Display all contacts
        public IActionResult Index()
        {
            var contacts = _context.Contacts.Include(c => c.Category).ToList();
            return View(contacts);
        }

        // GET: Add Contact Page
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View("Edit", new Contact());
        }

        // GET: Edit Contact Page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            ViewBag.Action = "Edit";
            ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
            return View(contact);
        }

        // POST: Add or Update Contact
        [HttpPost]
        public IActionResult Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
                return View(contact);
            }

            if (contact.ContactId == 0)
                _context.Contacts.Add(contact);
            else
                _context.Contacts.Update(contact);

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Contact Details Page
        [HttpGet]
        public IActionResult Details(int id)
        {
            var contact = _context.Contacts.Include(c => c.Category).FirstOrDefault(c => c.ContactId == id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // GET: Confirm Delete Page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }

        // POST: Delete Contact
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var contact = _context.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contact);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Cancel button action (Redirect appropriately)
        [HttpGet]
        public IActionResult Cancel(int? id)
        {
            return id.HasValue ? RedirectToAction("Details", new { id = id.Value }) : RedirectToAction("Index");
        }
    }
}
