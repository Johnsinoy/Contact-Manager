using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
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
            try
            {
                var contacts = _context.Contacts.Include(c => c.Category).ToList();
                return View(contacts);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while fetching contacts. Please try again.");
                return View("Error");
            }
        }

        // GET: Add Contact Page
        [HttpGet]
        public IActionResult Add()
        {
            try
            {
                ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
                return View("Edit", new Contact());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while loading the Add Contact page.");
                return View("Error");
            }
        }

        // GET: Edit Contact Page
        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while loading the Edit page.");
                return View("Error");
            }
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

            // **Check if the email already exists in the database**
            bool emailExists = _context.Contacts
                .AsNoTracking() // Prevents tracking issues
                .Any(c => c.Email == contact.Email && c.ContactId != contact.ContactId);

            if (emailExists)
            {
                ModelState.AddModelError("Email", "This email is already registered.");
                ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
                return View(contact);
            }

            // **Check for duplicates in Phone**
            bool duplicateExists = _context.Contacts
                .AsNoTracking()
                .Any(c => c.Phone == contact.Phone); // Ensure it's not the same record being updated

            if (duplicateExists)
            {
                ModelState.AddModelError("", "A contact with the same phone number already exists.");
                ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
                return View(contact);
            }

            try
            {
                if (contact.ContactId == 0)
                    _context.Contacts.Add(contact);
                else
                    _context.Contacts.Update(contact);

                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "An error occurred while saving the contact.");
                ViewBag.Categories = _context.Categories.OrderBy(c => c.Name).ToList();
                return View(contact);
            }
        }


        // GET: Contact Details Page
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var contact = _context.Contacts.Include(c => c.Category).FirstOrDefault(c => c.ContactId == id);
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while loading contact details.");
                return View("Error");
            }
        }

        // GET: Confirm Delete Page
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var contact = _context.Contacts.Find(id);
                if (contact == null)
                {
                    return NotFound();
                }
                return View(contact);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while loading the delete confirmation page.");
                return View("Error");
            }
        }

        // POST: Delete Contact
        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            try
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
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while deleting the contact.");
                return View("Error");
            }
        }

        // GET: Cancel button action (Redirect appropriately)
        [HttpGet]
        public IActionResult Cancel(int? id)
        {
            return id.HasValue ? RedirectToAction("Details", new { id = id.Value }) : RedirectToAction("Index");
        }
    }
}
