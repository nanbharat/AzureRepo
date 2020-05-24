using StorageCrudApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StorageCrudApp.Controllers
{
    public class StorageController : Controller
    {
        // GET: Storage
        public ActionResult Index(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                // Get particular student info  
                TableManager TableManagerObj = new TableManager("person"); // 'person' is the name of the table  
                                                                           // pass query where RowKey eq value of id
                List<Student> SutdentListObj = TableManagerObj.RetrieveEntity<Student>("RowKey eq '" + id + "'");
                Student StudentObj = SutdentListObj.FirstOrDefault();
                return View(StudentObj);
            }
            return View(new Student());
        }


        [HttpPost]
        public ActionResult Index(string id, FormCollection formData)
        {
            Student StudentObj = new Student();
            StudentObj.Name = formData["Name"] == "" ? null : formData["Name"];
            StudentObj.Department = formData["Department"] == "" ? null : formData["Department"];
            StudentObj.Email = formData["Email"] == "" ? null : formData["Email"];

            // Insert  
            if (string.IsNullOrEmpty(id))
            {
                StudentObj.PartitionKey = "Student";
                StudentObj.RowKey = Guid.NewGuid().ToString();

                TableManager TableManagerObj = new TableManager("person");
                TableManagerObj.InsertEntity<Student>(StudentObj, true);
            }
            // Update  
            else
            {
                StudentObj.PartitionKey = "Student";
                StudentObj.RowKey = id;

                TableManager TableManagerObj = new TableManager("person");
                TableManagerObj.InsertEntity<Student>(StudentObj, false);
            }
            return RedirectToAction("Get");
        }


        public ActionResult Get()
        {
            TableManager TableManagerObj = new TableManager("person");
            List<Student> SutdentListObj = TableManagerObj.RetrieveEntity<Student>();
            return View(SutdentListObj);
        }


        public ActionResult Delete(string id)
        {
            TableManager TableManagerObj = new TableManager("person");
            List<Student> SutdentListObj = TableManagerObj.RetrieveEntity<Student>("RowKey eq '" + id + "'");
            Student StudentObj = SutdentListObj.FirstOrDefault();
            TableManagerObj.DeleteEntity<Student>(StudentObj);
            return RedirectToAction("Get");
        }




    }
}