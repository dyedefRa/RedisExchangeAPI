using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SortedSetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string key = "sortedsettypekey";

        public SortedSetTypeController(RedisService redisService)
        {
            this._redisService = redisService;
            db = _redisService.GetDatabase(3);
        }
        public IActionResult Index()
        {
            HashSet<string> list = new HashSet<string>();

            if (db.KeyExists(key))
            {
                //küçükten büyüğe doğru getir.
                //db.SortedSetScan(listKey).ToList().ForEach(x =>
                //{
                //    list.Add(x.ToString());
                //});

                //büyükten küçüğe doğru getir.
                db.SortedSetRangeByRank(key, order:Order.Descending).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });

            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Add(string name, int score)
        {
            //ilgili keye 1 dk ekle
            db.KeyExpire(key, DateTime.Now.AddMinutes(1));

            //keye item ekle, sıraya göre ekliyor..
            db.SortedSetAdd(key, name, score);
            return RedirectToAction("Index");
        }
    }
}
