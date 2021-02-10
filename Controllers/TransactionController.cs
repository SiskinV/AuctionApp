using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuctionApp.Models.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionApp.Controllers{
    [Authorize(Roles = "User")]

    public class TransactionController:Controller{

        private AuctionAppContext context;
        private UserManager<User> userManager;

        public TransactionController(AuctionAppContext context,UserManager<User> userManager){
            this.context=context;
            this.userManager=userManager;
        }

        public IActionResult BuyTokens(){
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int tokens){
            
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            loggedInUser.tokens += tokens;

            Transaction thisTrans = new Transaction(){
                tokens = tokens,
                userId = loggedInUser.Id,
                date = DateTime.Now,
                cost = tokens
            };

            try{
                this.context.Update(loggedInUser);
                await this.context.transactions.AddAsync(thisTrans);
                await this.context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }


            return Json(true);
        }

        public async Task<IActionResult> TransactionList(){
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            int transactions = this.context.transactions.Where(trans => trans.userId == loggedInUser.Id).Count();
            return View(transactions);
        }

        public async Task<IActionResult> getTransactions(int num){
            User loggedInUser = await this.userManager.GetUserAsync(base.User);
            IQueryable<Transaction> transactionListQ = this.context.transactions.Where(trans => trans.userId == loggedInUser.Id);
            IList<Transaction> transactionList = null;
            if (transactionListQ.Any()) // bira odredjeni broj transakcija u odnosu na koji next,prev sam kliknuo 
                transactionList = await transactionListQ.OrderByDescending(trans => trans.date).Skip(num * 10).Take(10).ToListAsync();
            return PartialView("Transaction", transactionList);
        }
    }
}