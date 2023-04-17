using ChatAppMVC.Data;
using ChatAppMVC.Hub;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatAppMVC.Controllers
{
    [Authorize(Roles = "Member")]
    public class ChatController : Controller
    {
        
        public IActionResult ChatPage()
        {
            
            return View();
        }
    }
}
