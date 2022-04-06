using System;
using System.Collections.Generic;

namespace hdfc_loan2_app.Models
{
    public partial class Loan2DocumentsTestLog
    {
        public int Id { get; set; }
        public string Appid { get; set; }
        public DateTime? UploadDate { get; set; }
        public string Filepath { get; set; }
        public string DestinationPath { get; set; }
    }
}
