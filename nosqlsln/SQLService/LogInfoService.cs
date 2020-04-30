using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLService
{
    public class LogInfoService
    {
        public DataTable GetData(int id)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append(" select *  ");
            strSql.Append(" FROM Log_Info a ");
            strSql.Append(" where a.LogID=@Id");
            SqlParameter[] parameters = {
                new SqlParameter("@Id", SqlDbType.Int,4)
            };
            parameters[0].Value = id;
            var result = SQLDBHelper.Query(strSql.ToString(), parameters);

            return result.Tables[0];
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add2(Log_Info model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into Log_Info(");
            strSql.Append("[LogClassID], [LogTypeId], [mapid], [mapid2], [LogMemo], [IsStar], [AddTime], [userid], [orgid], [cityid], [isdel], [isadmin], [DBSource], [AddFromMethod], [adddate]");
            strSql.Append(") values (");
            strSql.Append("@LogClassID, @LogTypeId,@mapid,@mapid2,@LogMemo,@IsStar,@AddTime,@userid,@orgid,@cityid,@isdel,@isadmin,@DBSource,@AddFromMethod,@adddate");
            strSql.Append(") ");
            //strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@LogClassID", SqlDbType.Int,4) ,
                        new SqlParameter("@LogTypeId", SqlDbType.Int,4) ,
                        new SqlParameter("@mapid", SqlDbType.Int,4) ,
                        new SqlParameter("@mapid2", SqlDbType.Int,4) ,
                        new SqlParameter("@LogMemo", SqlDbType.VarChar,200) ,
                        new SqlParameter("@IsStar", SqlDbType.Bit,2) ,
                        new SqlParameter("@AddTime", SqlDbType.DateTime) ,
                        new SqlParameter("@userid", SqlDbType.Int,4) ,
                        new SqlParameter("@orgid", SqlDbType.Int,4),
                        new SqlParameter("@cityid", SqlDbType.Int,4),
                        new SqlParameter("@isdel", SqlDbType.Int,4),
                        new SqlParameter("@isadmin", SqlDbType.Int,4),
                        new SqlParameter("@DBSource", SqlDbType.Int,4),
                        new SqlParameter("@AddFromMethod", SqlDbType.VarChar,200),
                        new SqlParameter("@adddate", SqlDbType.DateTime) ,

            };

            parameters[0].Value = model.LogClassID;
            parameters[1].Value = model.LogTypeId;
            parameters[2].Value = model.mapid;
            parameters[3].Value = model.mapid2;
            parameters[4].Value = model.LogMemo;
            parameters[5].Value = model.IsStar;
            parameters[6].Value = model.AddTime;
            parameters[7].Value = model.userid;
            parameters[8].Value = model.orgid;
            parameters[9].Value = model.cityid;
            parameters[10].Value = model.isdel;
            parameters[11].Value = model.isadmin;
            parameters[12].Value = model.DBSource;
            parameters[13].Value = model.AddFromMethod;
            parameters[14].Value = model.adddate;


            object obj = SQLDBHelper.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {

                return Convert.ToInt32(obj);

            }

        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Log_Info loginfo)
        {
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append("insert into Log_Info(");
            //strSql.Append("[LogClassID], [LogTypeId], [mapid], [mapid2], [LogMemo], [IsStar], [AddTime], [userid], [orgid], [cityid], [isdel], [isadmin], [DBSource], [AddFromMethod], [adddate]");
            //strSql.Append(") values ");
            //strSql.Append(") values (");
            //strSql.Append("1, 0,884444,884444,\'{0}\',0,\'{1}\',0,0,0,0,0,0,\'{2}\',\'{3}\'");
            //strSql.Append(") ");

            //for (int a = 0; a < 100; a++)
            //{
            //    StringBuilder sql_list = new StringBuilder();
            //    for (int i = 0; i < 1000; i++)
            //    {
            //        sql_list.Append(string.Format(strSql.ToString(), loginfo.LogMemo, loginfo.AddTime,
            //            loginfo.AddFromMethod, loginfo.adddate));
            //        sql_list.Append(";");
            //    }

            //    object obj = SQLDBHelper.GetSingle(sql_list.ToString(), null);
            //}
            string val = "(1, 0,884444,884444,\'{0}\',0,\'{1}\',0,0,0,0,0,0,\'{2}\',\'{3}\'),";
            for (int a = 0; a < 100; a++)
            {
                StringBuilder strSql = new StringBuilder();
                strSql.Append("insert into Log_Info(");
                strSql.Append("[LogClassID], [LogTypeId], [mapid], [mapid2], [LogMemo], [IsStar], [AddTime], [userid], [orgid], [cityid], [isdel], [isadmin], [DBSource], [AddFromMethod], [adddate]");
                strSql.Append(") values ");

                StringBuilder sql_list = new StringBuilder();
                for (int i = 0; i < 1000; i++)
                {
                    strSql.Append(string.Format(val.ToString(), loginfo.LogMemo, loginfo.AddTime,loginfo.AddFromMethod, loginfo.adddate));
                }

                object obj = SQLDBHelper.GetSingle(strSql.ToString().TrimEnd((',')), null);
            }

            return 0;

        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM Log_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return SQLDBHelper.Query(strSql.ToString());
        }


        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM Log_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return SQLDBHelper.Query(strSql.ToString());
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) FROM Log_Info ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            object obj = SQLDBHelper.GetSingle(strSql.ToString());
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
    }
}
