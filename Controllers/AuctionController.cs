using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuctionApp.Models.Database;
using AuctionApp.Models.View;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Controllers{
    public class AuctionController:Controller{
        
        private AuctionAppContext context;
        private UserManager<User> userManager;
        private IMapper mapper;

        public AuctionController(AuctionAppContext context,UserManager<User> userManager,IMapper mapper){
            this.context=context;
            this.userManager=userManager;
            this.mapper=mapper;
        }

        public async Task<IActionResult> Create(){
            User loggeInUser = await this.userManager.GetUserAsync(base.User);
            return View();
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuctionModel model){
            if(!ModelState.IsValid){
                return View(model);
            }
            
            if(model.dateOpen < DateTime.Now){
                ModelState.AddModelError("","Date of opening must be in the future!");
                return View(model);
            }

            if(model.dateExpire < model.dateOpen){
                ModelState.AddModelError("","Date of expiry must be after date of opening!");
                return View(model);
            }

            User loggedInUser = await this.userManager.GetUserAsync(base.User);

            using(BinaryReader reader = new BinaryReader(model.image.OpenReadStream())){
                Auction newAuction = new Auction(){
                    userId=loggedInUser.Id,
                    user=loggedInUser,
                    name=model.name,
                    startPrice=model.startPrice,
                    description=model.description,
                    dateCreate=DateTime.Now,
                    dateExpire=model.dateExpire,
                    dateOpen=model.dateOpen,
                    image = reader.ReadBytes(Convert.ToInt32(reader.BaseStream.Length))
                };

                try{

                    await this.context.auctions.AddAsync(newAuction);
                    await this.context.SaveChangesAsync();

                }catch(DbUpdateConcurrencyException){
                    throw;
                }
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
            //return RedirectToAction(nameof(AuctionController.MyAuctions),"Auction");
        }

        public async Task<IActionResult> updateIndex(string words,int min,int max,string state,int numOfAuctions){

            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            string id = "X";
            int num = 0;
            if (loggedInUser != null) id = loggedInUser.Id;

            // trazim sve aukcije koje nisam ja napravio i koje nisu obrisane jer ne mogu sam da bidujem na svojoj aukciji
            IQueryable<Auction> query = this.context.auctions.Where(trans => trans.userId != id && trans.state != "DELETED");

            if (words != null && words != "") query = query.Where(item => item.name.Contains(words));
            if (min > 0) query = query.Where(item => (item.startPrice + item.bidAmount > min));
            if (max > 0) query = query.Where(item => (item.startPrice + item.bidAmount < max));
            if (state != "ANY") query = query.Where(item => item.state == state);

            num = query.Count();
            return PartialView("AllAuctionsPartial", num);
        }

        // ovo sled treba da uradis 
        public async Task<IActionResult> getAllAuctions(int num, string words, int min, int max, string state,int numOfAuctions)//ovo ne menjati napraviti novu updatIndex sa search komponentama
        {   
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            string id = "X";
            if (loggedInUser != null) id = loggedInUser.Id;
            //Uzmi sve one ciji vlasnik nisam ja i koje nisu obrisane do tada!!!!
            IQueryable<Auction> query = this.context.auctions.Where(trans => trans.userId != id && trans.state != "DELETED");
            if (words != null && words != "") query = query.Where(item => item.name.Contains(words));
            if (min > 0) query = query.Where(item => (item.startPrice + item.bidAmount > min));
            if (max > 0) query = query.Where(item => (item.startPrice + item.bidAmount < max));
            if (state != "ANY") query = query.Where(item => item.state == state);
            IList<Auction> auctionList = null;
            if (query.Any())
                auctionList = await query.OrderByDescending(act => act.dateCreate).Skip(num * numOfAuctions).Take(numOfAuctions).ToListAsync();//sve ove queryje podeliti na dva i posle proveriti da li je model null

            return PartialView("AuctionList", auctionList);//OVDE TREBA NEKI DRUGI
        }

        public async Task<IActionResult> getAuctions(int num,int numOfAuctions){
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            IQueryable<Auction> a = this.context.auctions.Where(a => a.userId == loggedInUser.Id && a.state!="DELETED");
            IList<Auction> al = null;
            if(a.Any()){
                al = await a.OrderByDescending(a => a.dateCreate).Skip(num*numOfAuctions).Take(numOfAuctions).ToListAsync();
            }
            return PartialView("MyAuctionPartial",al);
        }

        public async Task<IActionResult> MyAuctions(){
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            int auctions  = this.context.auctions.Where(t => t.userId==loggedInUser.Id).Count();
            return View(auctions);
        }


        public IActionResult Details(int? id){
            return View(id);
        }



        public async Task<IActionResult> getDetails(int? id){
            if(id==null){
                return NotFound();
            }

            Auction auction = await this.context.auctions.Where(a => a.id==id).FirstAsync();

            if(auction==null){
                return NotFound();
            }

            return PartialView("DetailsPartial",auction);
        }

        public async Task<IActionResult> getDraftAuction(int num)
        {
            IQueryable<Auction> autionListQ = this.context.auctions.Where(a => a.state == "DRAFT");
            IList<Auction> auctionList = null;
            if (autionListQ.Any())
            {
                auctionList = await autionListQ.OrderByDescending(act => act.dateCreate).Skip(num * 12).Take(12).ToListAsync();
            }
            return PartialView("~/Views/Admin/AuctionPartial.cshtml", auctionList);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Auction auction = await this.context.auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }

            if (auction.state != "DRAFT") return View("MyAuctions");
            auction.state = "READY";

            try
            {
                context.Update(auction);
                var res = await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return RedirectToAction(nameof(AdminController.Index), "Admin");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Auction auction = await this.context.auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            if (auction.state != "DRAFT") return View("MyAuctions");
            auction.state = "DELETED";

            try
            {
                context.Update(auction);
                var res = await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return RedirectToAction(nameof(AdminController.Index), "Admin");
            //return View("MyAuctions");//i ovo mozda nije dobro e
        }

        [HttpPost]
        public async Task<IActionResult> updateAuctions(){
            DateTime now = DateTime.Now;
            IList<Auction> allAuctions = await this.context.auctions.Where (a => a.state!="DELETED" && a.state!="EXPIRED").Include(item => item.user).ToListAsync();

            if(allAuctions.Any()){
                foreach(var auction in allAuctions){
                    if(auction.state=="READY" && auction.dateOpen<=now){
                        auction.state="OPEN";
                        context.Update(auction);
                    }else if(auction.state=="DRAFT" && auction.dateOpen<=now){
                        auction.state="EXPIRED";
                        context.Update(auction);
                    }else if(auction.state=="OPEN" && auction.dateExpire<=now){
                        var bidded = auction.lastBidder;
                        if(bidded==null) bidded=auction.lastBidder="-";
                        if(bidded!="-"){
                            auction.state="SOLD";
                            //ovde bi valjda trebalo da ide - jer se oduzima od ukupnog broja njegovih tokena
                            //proeri to
                            auction.user.tokens -= auction.startPrice+auction.bidAmount;
                            context.Update(auction.user);
                        }else{
                            auction.state="EXPIRED";
                            context.Update(auction);
                        }
                    }
                }
                try{
                    var res = await this.context.SaveChangesAsync();
                }catch(DbUpdateConcurrencyException){
                    return Json(false);
                }
            }
            return Json(true);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Auction auction = await this.context.auctions.FindAsync(id);
            if (auction == null)
            {
                return NotFound();
            }
            if (auction.state != "DRAFT") return RedirectToAction(nameof(AuctionController.MyAuctions), "Auction");
            return View(auction);

        }


        //Ovo govance ne radi :(
        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAuction(int id, [Bind("id,userId,name,description,startPrice,dateCreate,dateOpen,dateExpire,state,image,bidAmount")] Auction model)
        {
            if (model.dateOpen < DateTime.Now)
            {
                ModelState.AddModelError("", "Date of opening must be in the future!");
                return View("Edit", model);
            }

            if (model.dateExpire < model.dateOpen)
            {
                ModelState.AddModelError("", "Date of expiry must be after date of opening!");
                return View("Edit", model);
            }
            if (ModelState.IsValid)
            {
                if (model.state != "DRAFT") return RedirectToAction(nameof(AuctionController.MyAuctions), "Auction");
                try
                {
                    context.Update(model);
                    var res = await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }

            }

            return RedirectToAction(nameof(AuctionController.MyAuctions));// ovo nije dobro probs
        }

    }
}

