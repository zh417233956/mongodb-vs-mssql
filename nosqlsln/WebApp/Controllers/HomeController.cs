using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MongoService;
using SQLService;
using WebApp.Models;
using Log_Info = MongoService.Log_Info;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //ViewBag.MongoData = Newtonsoft.Json.JsonConvert.SerializeObject(GetData());
            ViewBag.SQLData = Newtonsoft.Json.JsonConvert.SerializeObject(GetSqlData());
            return View();
        }

        private object GetSqlData()
        {
            SQLService.LogInfoService logService = new LogInfoService();

            //插入
            DateTime startTime = DateTime.Now;
            //for (int i = 0; i < 100000; i++)
            //{
            //    var entity = new SQLService.Log_Info();
            //    entity.AddTime = DateTime.Now;
            //    entity.adddate = DateTime.Now;
            //    entity.IsStar = false;
            //    entity.cityid = 0;
            //    entity.LogClassID = 1;
            //    entity.LogTypeId = 0;
            //    entity.LogIDOld = 0;
            //    entity.userid = i;
            //    entity.isdel = 0;
            //    entity.orgid = 22;
            //    entity.mapid = 884444;
            //    entity.LogMemo = "测试Log_Info插入";
            //    entity.isadmin = 0;
            //    entity.mapid2 = 884444;
            //    int dbSource = 5;
            //    entity.DBSource = dbSource;
            //    entity.AddFromMethod = "zhonghai.mongodbtest.testinsert";
            //    entity.LogID = logService.Add(entity);
            //}

            var entity = new SQLService.Log_Info();
            entity.AddTime = DateTime.Now;
            entity.adddate = DateTime.Now;
            entity.IsStar = false;
            entity.cityid = 0;
            entity.LogClassID = 1;
            entity.LogTypeId = 0;
            entity.LogIDOld = 0;
            entity.userid = 0;
            entity.isdel = 0;
            entity.orgid = 22;
            entity.mapid = 884444;
            entity.LogMemo = "测试Log_Info插入";
            entity.isadmin = 0;
            entity.mapid2 = 884444;
            int dbSource = 5;
            entity.DBSource = dbSource;
            entity.AddFromMethod = "zhonghai.mongodbtest.testinsert";
            entity.LogID = logService.Add(entity);

            DateTime endTime = DateTime.Now;

            //查询
            var count = 0;// logService.GetRecordCount("1=1 and LogClassID=1");
            var difftime = (endTime - startTime).TotalMilliseconds;
            var result = new
            {
                count,
                difftime
            };
            return result;
        }

        public ActionResult GetJson()
        {
            return Content(Newtonsoft.Json.JsonConvert.SerializeObject(GetData()));
        }

        private object GetData()
        {
            var mongodbUrl = System.Configuration.ConfigurationManager.AppSettings.Get("MongoDBUrl");
            MongoHelper mongoHelper = new MongoHelper(mongodbUrl);
            //var result = mongoHelper.ToList<Log_Info, Log_Info>("test", "testloginfo", a => a.LogClassID == 1 && a.mapid == 884445, a => a, a => a.Asc(b => b.AddTime), 10);

            DateTime startTime = DateTime.Now;
            //for (int i = 0; i < 100000; i++)
            //{
            //    var entity = new MongoService.Log_Info();
            //    entity.AddTime = DateTime.Now;
            //    entity.adddate = DateTime.Now;
            //    entity.IsStar = false;
            //    entity.cityid = 0;
            //    entity.LogClassID = 1;
            //    entity.LogTypeId = 0;
            //    entity.LogIDOld = 0;
            //    entity.userid = i;
            //    entity.isdel = 0;
            //    entity.orgid = 22;
            //    entity.mapid = 884444;
            //    entity.LogMemo = "测试Log_Info插入";
            //    entity.isadmin = 0;
            //    entity.mapid2 = 884444;
            //    int dbSource = 5;
            //    entity.DBSource = dbSource;
            //    entity.AddFromMethod = "zhonghai.mongodbtest.testinsert";
            //    //mongoHelper.Add("test", "test", entity);
            //    await mongoHelper.AddAsync("test", "test", entity);
            //}

            //mongoHelper.Add<object>("test", "test");
            mongoHelper.Add<object>("unittest", "test_test");

            DateTime endTime = DateTime.Now;
            //查询
            var count = 0;// mongoHelper.Count<Log_Info>("unittest", "test", a => a.LogClassID == 1);
            var difftime = (endTime - startTime).TotalMilliseconds;
            var result = new
            {
                count,
                difftime
            };
            return result;
        }


    }
}