using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using System.Collections.Generic;
using System.Linq;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : BaseController
    {
        public string hashkey { get; set; } = "hashtypekey";

        public HashTypeController(RedisService redisService) : base(redisService, 4)
        {

        }
        public IActionResult Index()
        {
            Dictionary<string, string> hashdictionarykey = new Dictionary<string, string>();

            if (db.KeyExists(hashkey))
            {
                db.HashGetAll(hashkey).ToList().ForEach(x =>
                {
                    hashdictionarykey.Add(x.Name, x.Value);
                });
            }
            return View(hashdictionarykey);
        }

        [HttpPost]
        public IActionResult Add(string name, string value)
        {
            db.HashSet(hashkey, name, value);
            return RedirectToAction("Index");
        }


        public IActionResult Remove(string name)
        {
            //Hashten sil
            db.HashDelete(hashkey, name);

            return RedirectToAction("Index");
        }
    }
}
