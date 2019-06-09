using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;


namespace webm.tools.Pages
{
    public class IndexModel : PageModel
    {
        // Class Properties
        private IHostingEnvironment _environment;
        private string _cookie;

        public SessionFiles _sessionFiles;

        [BindProperty]
        public IFormFile Upload { get; set; }

        public IndexModel(IHostingEnvironment environment)
        {
            _environment = environment;
        }

        public void OnGet()
        {
            this._cookie = HttpContext.Session.GetString("Test String");
            if (String.IsNullOrEmpty(this._cookie)){
                HttpContext.Session.SetString("Test String", HttpContext.Session.Id);    
            }
            this.SetupSessionFile();
            this.SetupViewData();
        }

        public async Task OnPostAsync()
        {
            this.SetupSessionFile();
            await _sessionFiles.UploadFile(Upload);
            Response.Redirect(Request.Path);
            this.SetupViewData();
        }

        public ActionResult OnPostDownloadFile(string file)
        {
            return File(file, "application/octet-stream", "Testfile.txt");
        }

        private void SetupSessionFile()
        {
            _sessionFiles = new SessionFiles(_environment.ContentRootPath, HttpContext.Session.Id);
        }

        private void SetupViewData()
        {
            ViewData["SessionFiles"] = _sessionFiles;
        }
        
    }
}
