using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string key = "settypekey";

        public SetTypeController(RedisService redisService)
        {
            this._redisService = redisService;
            db = _redisService.GetDatabase(2);
        }

        public IActionResult Index()
        {
            HashSet<string> nameList = new HashSet<string>();
            if (db.KeyExists(key))
            {
                db.SetMembers(key).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });
            }
            return View(nameList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            if (db.KeyExists(key))//YOKSA 
            {
                //5 DK EKLE.
                db.KeyExpire(key, DateTime.Now.AddMinutes(5));
            }

            db.SetAdd(key, name);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(string name)
        {
            //Listeden sil
            db.SetRemove(key, name);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveRandom()
        {
            //Listeden sil
            db.SetPop(key);

            return RedirectToAction("Index");
        }

    }
}
