using hdfc_loan2_app.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Web.Http;

namespace hdfc_loan2_app.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly hdfc_applicationsContext _context;

        public DocumentsController(hdfc_applicationsContext context)
        {
            _context = context;
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Microsoft.AspNetCore.Mvc.Route("getbyid")]
        public IActionResult GetbyID([FromUri] string[] appid)
        {
            foreach (string id in appid)
            {


                var doc = _context.Loan2DocumentsTest.Where(x => x.Appid == id).FirstOrDefault();
                string applicationid = doc.Appid;

                if (doc.Appid == id) 
                { 
                    DirectoryInfo hdDirectoryInWhichToSearch = new DirectoryInfo(@"G:\hdfc_loan2_testapplications");
                    FileInfo[] filesInDir = hdDirectoryInWhichToSearch.GetFiles(id + "*", SearchOption.AllDirectories);

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
                    
                }
                else
                {
                    return Ok("Wrong Application ID");
                }
            }

            return Ok("Success");

        }
    }
}
