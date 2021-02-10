using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionApp.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Controllers{

    [Authorize(Roles="Administrator")]
    public class AdminController : Controller{

        private AuctionAppContext context;

        public AdminController(AuctionAppContext context){
            this.context=context;
        }

        public IActionResult Index(){
            int drafts = this.context.auctions.Where(a => a.state=="DRAFT").Count();
            return View(drafts);
        }

        public async Task<IActionResult> UserList(){

            IList<User> userList = await context.Users.ToListAsync();
            return View(userList);
        }

    }
}