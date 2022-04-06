using hdfc_loan2_app.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace hdfc_loan2_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly hdfc_applicationsContext _context;

        public DocumentsController(hdfc_applicationsContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("getbyid/{appid}")]
        public IActionResult GetbyID(string appid)
        {
            var doc = _context.Loan2DocumentsTest.Where(x => x.Appid == appid).FirstOrDefault();
            string applicationid = doc.Appid;

            if(doc.Appid == appid)
            {
                DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"G:\hdfc_loan2_testapplications");
                FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles("A123*", SearchOption.AllDirectories);

                foreach (FileInfo foundFile in filesInDir)
                {
                    string fullName = foundFile.FullName;
                    string Name = foundFile.Name;


                    string fileName = Name;
                    string sourcePath = fullName;
                    string targetPath = @"G:\hdfc_copy";


                    string sourceFile = System.IO.Path.Combine(sourcePath);
                    string destFile = System.IO.Path.Combine(targetPath, fileName);


                    System.IO.File.Copy(sourceFile, destFile, true);


                    var logs = new Loan2DocumentsTestLog
                    {
                        Appid = applicationid,
                        Filepath = sourcePath,
                        DestinationPath = targetPath,
                        UploadDate = DateTime.Now,
                    };

                    _context.Loan2DocumentsTestLog.Add(logs);
                    _context.SaveChanges();



                }
                return Ok("Success");
            }
            else
            {
                return Ok("Wrong AppID");
            }
           
            
        }
    }
}
