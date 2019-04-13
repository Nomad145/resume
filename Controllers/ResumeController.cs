using System;
using System.Linq;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using michaelphillips.dev.Models;
using static System.Net.Mime.MediaTypeNames;
using static Microsoft.Net.Http.Headers.HeaderNames;

namespace michaelphillips.dev.Controllers
{
    public class ResumeController : Controller
    {
        private const string VimMimeType = "text/vim-help";
        private const string HTMLResumePath = "wwwroot/assets/resume.html";
        private const string VimHelpResumePath = "wwwroot/assets/resume.txt";

        /// <summary>
        /// Serve the Resume as either a HTML or a Vim Helpfile based on the
        /// request Accept Header.
        /// </summary>
        public IActionResult Index()
        {
            if (ClientRequestedVimHelpfile())
            {
                return VimHelpfileResume();
            }

            return HTMLResume();
        }

        /// <summary>
        /// Determine if the client has requested the resume as a Vim Helpfile.
        /// </summary>
        ///
        /// <returns>
        /// Returns true if the request 'Accept' header is present and equals
        /// 'text/plain'.
        /// </returns>
        private bool ClientRequestedVimHelpfile()
        {
            string acceptHeader = this
                .HttpContext
                .Request
                ?.Headers[Accept];

            return acceptHeader == VimMimeType;
        }

        /// <summary>
        /// Prepare the Vim Helpfile version of the Resume.
        /// </summary>
        ///
        /// <returns>
        /// The Vim Helpfile version of a Resume
        /// </returns>
        private FileContentResult VimHelpfileResume()
        {
            return PrepareResumeResult(VimHelpResumePath, Text.Plain);
        }

        /// <summary>
        /// Prepare the HTML version of the Resume.
        /// </summary>
        ///
        /// <returns>
        /// The HTML version of a Resume
        /// </returns>
        private FileContentResult HTMLResume()
        {
            return PrepareResumeResult(HTMLResumePath, Text.Html);
        }

        /// <summary>
        /// Prepare the `FileContentResult` with the given `Path` and `MimeType`.
        /// </summary>
        ///
        /// <remarks>
        /// The `MimeType` parameter must match the type of the file at `Path`.
        /// </remarks>
        ///
        /// <param name="Path">The path the resume on the server</param>
        /// <param name="MimeType">The MimeType of the file at the given Path</param>
        private FileContentResult PrepareResumeResult(string Path, string MimeType)
        {
            byte[] resumeContents = System.IO.File.ReadAllBytes(Path);

            FileContentResult result = new FileContentResult(resumeContents, MimeType);

            return result;
        }
    }
}
