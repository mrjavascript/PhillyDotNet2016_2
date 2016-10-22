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
    public class SecureRecordRetrieveController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IDataProtector _protector;

        public SecureRecordRetrieveController(ApplicationDbContext db, IDataProtectionProvider provider)
        {
            this._db = db;
            this._protector = provider.CreateProtector("web-encrypt-decrypt");
        }

        // GET: /<controller>/
        [HttpGet]
        [Route("record/{recordId}")]
        public IActionResult Index(int recordId)
        {
            // Query for the record
            var result = from record in _db.SecureRecords
                where record.RecordId == recordId
                select record;
            SecureRecord secureRecord = result.First();

            // Decrypt
            secureRecord.FirstName = _protector.Unprotect(secureRecord.FirstName);
            secureRecord.LastName = _protector.Unprotect(secureRecord.LastName);
            secureRecord.CreditCardNo = _protector.Unprotect(secureRecord.CreditCardNo);

            return View(secureRecord);
        }
    }
}
