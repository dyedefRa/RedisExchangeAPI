StackExchange.Redis kütüphanesi ile 
Redis te olan tüm fonksiyonlar kullanabilirsin.

IDistributedCache basit bir şekilde redisi kullandırır.
Fakat StackExchange.Redis  daha işlevseldir.
----------------------------------------

1) StackExchange.Redis indir.
2) Services > redisService oluşturduk.
3) Startupta Injection yaptık ve 
Middleware olarak tanıttık.

-----------Buraya kadar.
Diğer dersler için controller oluşturacagım.

----------------------------------------1 Redis String ----------------------------------------

            db.StringSet("name", "Baki Öztürk");
            db.StringSet("ziyaretci", 100);

            Byte[] imageBytes = default;
            db.StringSet("image:1", imageBytes);


                var value = db.StringGet("name");
            db.StringIncrement("ziyaretci", 2);
            db.StringDecrement("ziyaretci", 1);


----------------------------------------2 Redis List & Linked List (c#) ----------------------------------------

Başa sonra item ekleyip / çıkarabiliyorsun.
Aynı eleman ekleyebiliyorsun.

db.List....  ile başlayan tüm methodlar List ile alakalı.


----------------------------------------3 Redis Set & HASHSET (c#) ----------------------------------------
db.Set....
unique tir.
Sırasız eklenir ?

     HashSet<string> nameList = new HashSet<string>();
            if (db.KeyExists(listKey))
            {
                db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    nameList.Add(x.ToString());
                });
            }
            return View(nameList);


               if (db.KeyExists(listKey))//YOKSA 
            {
                //5 DK EKLE.
                db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));
            }

            db.SetAdd(listKey, name);

            return RedirectToAction("Index");


----------------------------------------4 Redis sorted set ----------------------------------------

Sıralamaya göre set .
 db.Sorted....


 Sıralamaya göre eklersin.
 Eklediğin datayı sırasını da verirsin .
 liste o sıraya göre getirir dataları.

   HashSet<string> list = new HashSet<string>();

            if (db.KeyExists(listKey))
            {
                //sıralamaya göre getir.
                db.SortedSetScan(listKey).ToList().ForEach(x =>
                {
                    list.Add(x.ToString());
                });
            }
            return View(list);
--------------------
             //ilgili keye 1 dk ekle
            db.KeyExpire(listKey, DateTime.Now.AddMinutes(1));

            //keye item ekle, sıraya göre ekliyor..
            db.SortedSetAdd(listKey, name, score);
            return RedirectToAction("Index");

----------------------------------------4 Redis Hash  ----------------------------------------

Dictionary sınıfına karşılık gelir.

SOZLUK gibi düşün.

Kalem Pen
Silgi Eraser.
Dictionary olarak eklenşyor.