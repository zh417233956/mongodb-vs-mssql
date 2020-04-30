using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Log_Info
    {
        public Log_Info()
        {
            AddTime = DateTime.Now;
            adddate = AddTime.Date;
        }
        public int LogClassID { get; set; }
        public int LogTypeId { get; set; }
        public int mapid { get; set; }
        public int mapid2 { get; set; }
        public string LogMemo { get; set; }
        public bool IsStar { get; set; }
        public DateTime AddTime { get; set; }
        public int userid { get; set; }
        public int orgid { get; set; }
        public int cityid { get; set; }
        public int isdel { get; set; }
        public int LogIDOld { get; set; }
        public int isadmin { get; set; }
        public int DBSource { get; set; }
        public string AddFromMethod { get; set; }
        public DateTime adddate { get; set; }
    }
}