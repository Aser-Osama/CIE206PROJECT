using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using CIE206PROJECT.Controllers;
using CIE206PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CIE206PROJECT.Pages.Admin_Pages
{
    public class Send_Request : PageModel
    {
        private readonly DB_Container _DBC;
        private readonly LoginController _LC;
        public RequestsPage _DB { get; set; }

        [BindProperty(SupportsGet =true)]
        public Request req { get; set; }

        [BindProperty(SupportsGet = true)]
        public string errorstring  { get; set; }
        
        [BindProperty]
        public int SendTo { get; set; }
        [BindProperty]
        public string MsgSubject { get; set; }
        [BindProperty]
        public string MsgContent { get; set; }
        private readonly ILogger<Send_Request> _logger;

        public Send_Request(ILogger<Send_Request> logger, DB_Container container, LoginController _loginController)
        {
            _logger = logger;
            _DBC = container;
            _LC = _loginController;
        }

        public void OnGet()
        {
            _DB = _DBC.requestsPage_DB;
        }

        public bool isadmin()
        {
            return _LC.IsAdmin();
        }
        public IActionResult OnPostSendRequest()
        {
                _DB = _DBC.requestsPage_DB;
                req.subject = MsgSubject;
                req.content = MsgContent;
                req.request_id = _DB.getMaxReqID() + 1;
                req.sent_by = _LC.GetLoggedInUserId();
                req.datetime = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss");
                if (SendTo == 1) { 
                    req.sent_to = _DB.getSSID() ;
                }
                else {
                    req.sent_to =  _DB.getCEOID();
                }
                if (!_DB.addRequest(req))
                {
                    string err = "YOUR REQUEST WAS NOT ACCEPTED";
                    return RedirectToPage("/Admin_Pages/Send_Request", new { req = req, errorstring =  err});
                }
                else
                {
                    string err = "YOUR REQUEST WAS SENT, SEND ANOTHER?";
                    return RedirectToPage("/Admin_Pages/Send_Request", new {errorstring = err, req = new Request() });
                }
        }
    }
}