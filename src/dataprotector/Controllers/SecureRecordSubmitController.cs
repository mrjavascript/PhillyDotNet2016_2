using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dataprotector.Data;
using dataprotector.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace dataprotector.Controllers
{
    public class SecureRecordSubmitController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IDataProtector _protector;

        public SecureRecordSubmitController(ApplicationDbContext db, IDataProtectionProvider provider)
        {
            this._db = db;
            this._protector = provider.CreateProtector("web-encrypt-decrypt");
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            SecureRecord secureRecord = new SecureRecord();
            return View(secureRecord);
        }

        [HttpPost]
        public IActionResult Index(SecureRecord secureRecord)
        {
            // Encrypt
            secureRecord.FirstName = _protector.Protect(secureRecord.FirstName);
            secureRecord.LastName = _protector.Protect(secureRecord.LastName);
            secureRecord.CreditCardNo = _protector.Protect(secureRecord.CreditCardNo);

            // Save to the database
            this._db.SecureRecords.Add(secureRecord);
            this._db.SaveChanges();

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}
