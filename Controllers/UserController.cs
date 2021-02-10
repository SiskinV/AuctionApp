using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionApp.Models.Database;
using AuctionApp.Models.View;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static AuctionApp.Models.Database.IdentityRoleConfiguration;

namespace AuctionApp.Controllers{
    public class UserController : Controller{

        private AuctionAppContext context;
        private UserManager<User> userManager;
        private IMapper mapper;
        private SignInManager<User> signInManager;

        public UserController (AuctionAppContext context, UserManager<User> userManager,IMapper mapper,SignInManager<User> signInManager){
            this.context=context;
            this.userManager=userManager;
            this.mapper=mapper;
            this.signInManager=signInManager;
        }

        public IActionResult isEmailUnique (string email){
            bool exists = this.context.Users.Where(user => user.Email == email).Any();

            if(exists){
                return Json("Email already taken!");
            }else{
                return Json(true);
            }
        }

        public IActionResult isUserNameUnique(string userName)
        {
            bool exists = this.context.Users.Where(user => user.UserName == userName).Any();

            if (exists)
            {
                return Json("Username already taken!");
            }
            else
            {
                return Json(true);
            }
        }

        public IActionResult Register(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model){
            //proverava da li je sve validno u formi ako nije vraca sve kao sto je bilo plus gresku
            if(!ModelState.IsValid){
                return View(model);
            }

            User user = this.mapper.Map<User>(model);

            IdentityResult result = await this.userManager.CreateAsync(user,model.password);

            if(!result.Succeeded){
                foreach(IdentityError error in result.Errors){
                    ModelState.AddModelError("",error.Description);
                }
                return View(model);
            }

            result = await this.userManager.AddToRoleAsync(user,Roles.user.Name);

            if (!result.Succeeded)
            {
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }

            //Ovo treba da promenis da se prebacuje na logIn cim bude postojao
            //return RedirectToAction (nameof(HomeController.Index),"Home");
            return RedirectToAction(nameof(UserController.LogIn),"User");

        }
        public IActionResult LogIn(string returnUrl)
        {
            LogInModel model = new LogInModel()
            {
                returnUrl = returnUrl
            };
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            User user = await this.userManager.FindByNameAsync(model.userName);
            if(user!=null){
                //ovde on ima dodat neki field deleted koji ne znam sta ce mu
                if(user.deleted==true){
                    
                    ModelState.AddModelError("","User was deleted!");
                    return View(model);

                }
            }

            var result = await this.signInManager.PasswordSignInAsync(model.userName, model.password, false, false);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password not valid!");
                return View(model);
            }

            if (model.returnUrl != null)
            {
                return Redirect(model.returnUrl);
            }
            else
            {
                if (await userManager.IsInRoleAsync(user, "Administrator")){
                    return RedirectToAction(nameof(AdminController.Index), "Admin");
                }else{
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut(){
            await this.signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index),"Home");
        }

        public async Task<IActionResult> Details(){
            // Hvatam ulogovanog usera i njega prosledjujem 
            User user = await this.userManager.GetUserAsync(base.User);
            return View(user);
        }

        public async Task<IActionResult> Edit(EditModel model){
            //dohvatam ulogovanog usera i njega prosledjujem cisto zbog imena i prezimena
            User user = await this.userManager.GetUserAsync(base.User);

            model.firstName = user.firstName;
            model.lastName = user.lastName;
            return View(model);
        }

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDetails(EditModel model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction(nameof(UserController.Details));
            }

            User me = await this.userManager.GetUserAsync(base.User);

            me.firstName = model.firstName;
            me.lastName = model.lastName;
            
            if (model.email != null)
                me.Email = model.email;
                
            if (model.password != null)
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(me);
                var result = await userManager.ResetPasswordAsync(me, token, model.password);
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View("Edit", model);
                }

            }
            try
            {
                context.Update(me);
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToAction(nameof(UserController.Details));
        }

        [Authorize(Roles="Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete (string username){

            User user = await this.userManager.FindByNameAsync(username);
            user.deleted=true;
            IList<Auction> auctions = await this.context.auctions.Where(item => item.userId == user.Id && item.state != "SOLD").ToListAsync();
            foreach(var auction in auctions){
                auction.state="DELETED";
                context.Update(auction);
            }
            try{
                context.Update(user);
                await context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){

            }

            return RedirectToAction(nameof(AdminController.UserList),"Admin");
        }

       
    }
}