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
    public class SessionFiles
    {
        //private attributes
        private string _sessionId;
        private string _environment;

        // begin public attributes
        public string UserDirectory;

        public SessionFiles(string environment, string sessionId)
        {
            _sessionId = sessionId;
            _environment = environment;
            this.init();
        }

        // privately initialized the folder options for reference elsewhere.
        private void init()
        {
            if (!Directory.Exists(Path.Combine(_environment, "UserFiles", _sessionId)))
            {
                Directory.CreateDirectory(Path.Combine(_environment, "UserFiles", _sessionId));
            }
            this.UserDirectory = Path.Combine(_environment, "UserFiles", _sessionId).ToString();
        }

        public async Task UploadFile(IFormFile aspFile)
        {
            var file = Path.Combine(_environment, "UserFiles", _sessionId, aspFile.FileName);
            using (var fileStream = new FileStream(file, FileMode.Create))
            {
                await aspFile.CopyToAsync(fileStream);
            }
        }

        public IEnumerable<string> UploadedList()
        {
            return Directory.EnumerateFiles(this.UserDirectory);
        }

    }

}

