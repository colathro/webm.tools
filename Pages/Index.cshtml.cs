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
        public string _cookie;
        private IHostingEnvironment _environment;

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
        }

        public async Task OnPostAsync()
        {
            var file = Path.Combine(_environment.ContentRootPath, "uploads", Upload.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await Upload.CopyToAsync(fileStream);
            }
        }
        
    }
}
