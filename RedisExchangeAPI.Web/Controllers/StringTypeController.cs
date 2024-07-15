using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            this._redisService = redisService;
            db = _redisService.GetDatabase(0);
        }

        public IActionResult Index()
        {
            db.StringSet("name", "Baki Öztürk");
            db.StringSet("ziyaretci", 100);

            Byte[] imageBytes = default;
            db.StringSet("image:1", imageBytes);

            return View();
        }

        public IActionResult Show()
        {
            var value = db.StringGet("name");
            db.StringIncrement("ziyaretci", 2);
            db.StringDecrement("ziyaretci", 1);

            if (value.HasValue)
            {
                ViewBag.name = value.ToString();
            }

            return View();
        }
    }
}
