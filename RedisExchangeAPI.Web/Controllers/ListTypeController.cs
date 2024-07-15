using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        private string key = "listtypekey";
        public ListTypeController(RedisService redisService)
        {
            this._redisService = redisService;
            db = _redisService.GetDatabase(1);
        }

        public IActionResult Index()
        {
            List<string> namesList = new List<string>();

            //İlgili key varmı ?
            if (db.KeyExists(key))
            {
                //Hepsini getir.
                db.ListRange(key).ToList().ForEach(x =>
                {
                    namesList.Add(x.ToString());
                });
            }
            return View(namesList);
        }

        [HttpPost]
        public IActionResult Add(string name)
        {
            //Sağına ekle
            db.ListRightPush(key, name);

            return RedirectToAction("Index");
        }

        public IActionResult Remove(string name)
        {
            //Listeden sil
            db.ListRemove(key, name);

            return RedirectToAction("Index");
        }

        public IActionResult RemoveFirst()
        {
            //Listeden SOLDAN SİL   = LİST LEFT POP
            db.ListLeftPop(key);

            return RedirectToAction("Index");
        }
    }
}
