using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace MongoService
{
    public class MongoHelper
    {
        private readonly MongoClient _mongoClient;

        public MongoHelper(string connectionString)
        {
            _mongoClient = new MongoClient(connectionString);
        }

        public IMongoCollection<T> GetCollection<T>(string database, string collection)
        {
            var db = _mongoClient.GetDatabase(database);
            return db.GetCollection<T>(collection);
        }

        /// <summary>
        /// 新增数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="database">库</param>
        /// <param name="collection">集合（表）</param>
        /// <param name="entity">实体(文档)</param>
        public void Add<T>(string database, string collection)
        {
            var coll = GetCollection<BsonDocument>(database, collection);

            //DateTime startTime = DateTime.Now;

            //var writeConcern = WriteConcern.Unacknowledged;

            //for (int i = 0; i < 100000; i++)
            //{
            //    //var entity = new MongoService.Log_Info();
            //    //entity.AddTime = DateTime.Now;
            //    //entity.adddate = DateTime.Now;
            //    //entity.IsStar = false;
            //    //entity.cityid = 0;
            //    //entity.LogClassID = 1;
            //    //entity.LogTypeId = 0;
            //    //entity.LogIDOld = 0;
            //    //entity.userid = i;
            //    //entity.isdel = 0;
            //    //entity.orgid = 22;
            //    //entity.mapid = 884444;
            //    //entity.LogMemo = "测试Log_Info插入";
            //    //entity.isadmin = 0;
            //    //entity.mapid2 = 884444;
            //    //int dbSource = 5;
            //    //entity.DBSource = dbSource;
            //    //entity.AddFromMethod = "zhonghai.mongodbtest.testinsert";

            //    var document = new BsonDocument
            //    {
            //        { "AddTime", DateTime.Now.ToString() },
            //        { "adddate", DateTime.Now.ToString()},
            //        { "IsStar", false },
            //        { "cityid",0 },
            //        { "LogClassID",1 },
            //        { "LogTypeId",0 },
            //        { "LogIDOld",0 },
            //        { "userid",i },
            //        { "isdel",0 },
            //        { "orgid",22 },
            //        { "mapid",884444 },
            //        { "LogMemo","测试Log_Info插入" },
            //        { "isadmin",0 },
            //        { "mapid2",884444 },
            //        { "DBSource",5 },
            //        { "AddFromMethod","zhonghai.mongodbtest.testinsert" },
            //    };

            //    //mongoHelper.Add("test", "test", entity);
            //    //await coll.InsertOneAsync(entity);
            //    coll.InsertOne(document);
            //}

            for (int a = 0; a < 100; a++)
            {
                var list = new List<BsonDocument>();
                for (int i = 0; i < 1000; i++)
                {
                    var document = new BsonDocument
                    {
                        { "AddTime", DateTime.Now.ToString() },
                        { "adddate", DateTime.Now.ToString()},
                        { "IsStar", false },
                        { "cityid",0 },
                        { "LogClassID",1 },
                        { "LogTypeId",0 },
                        { "LogIDOld",0 },
                        { "userid",i },
                        { "isdel",0 },
                        { "orgid",22 },
                        { "mapid",884444 },
                        { "LogMemo","测试Log_Info插入" },
                        { "isadmin",0 },
                        { "mapid2",884444 },
                        { "DBSource",5 },
                        { "AddFromMethod","zhonghai.mongodbtest.testinsert" },
                    };
                    list.Add(document);
                }
                coll.InsertMany(list);
            }

            //await coll.InsertOneAsync(entity);
        }


        /// <summary>
        /// 列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="database">库</param>
        /// <param name="collection">集合</param>
        /// <param name="predicate">过滤条件</param>
        /// <param name="selector">查询字段</param>
        /// <param name="sort">排序</param>
        /// <param name="top">取X</param>
        /// <returns></returns>
        public List<TResult> ToList<T, TResult>(string database, string collection, Expression<Func<T, bool>> predicate, Expression<Func<T, TResult>> selector, Func<Sort<T>, Sort<T>> sort, int? top)
        {
            var coll = GetCollection<T>(database, collection);

            var find = coll.Find(predicate);

            if (sort != null)
            {
                List<SortItem> sortList = sort(new Sort<T>());

                var mySort = Builders<T>.Sort.Combine(sortList.Select(sortItem => sortItem.SortType == ESort.Asc
                    ? Builders<T>.Sort.Ascending(sortItem.FieldName)
                    : Builders<T>.Sort.Descending(sortItem.FieldName)).ToList());

                find = find.Sort(mySort);
            }

            if (top != null)
                find = find.Limit(top);

            return find.Project(selector).ToList();
        }

        /// <summary>
        /// 按条件查询条数
        /// </summary>
        /// <param name="database">库</param>
        /// <param name="collection">集合</param>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public long Count<T>(string database, string collection, Expression<Func<T, bool>> predicate)
        {
            var coll = GetCollection<T>(database, collection);

            return coll.CountDocuments(predicate);
        }
    }

    /// <summary>
    /// 排序容器类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Sort<T>
    {
        private readonly List<SortItem> _list = new List<SortItem>();

        /// <summary>
        /// 倒叙
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Sort<T> Desc<TField>(Expression<Func<T, TField>> expression)
        {
            _list.Add(new SortItem(expression.Body, ESort.Desc));
            return this;
        }

        /// <summary>
        /// 顺序
        /// </summary>
        /// <typeparam name="TField"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Sort<T> Asc<TField>(Expression<Func<T, TField>> expression)
        {
            _list.Add(new SortItem(expression.Body, ESort.Asc));
            return this;
        }

        /// <summary>
        /// 重载自定义类
        /// </summary>
        /// <param name="value"></param>
        public static implicit operator List<SortItem>(Sort<T> value)
        {
            return value._list;
        }
    }

    /// <summary>
    /// 排序项
    /// </summary>
    public class SortItem
    {
        public SortItem(Expression expressionBody, ESort sortType)
        {
            ExpressionBody = expressionBody;
            SortType = sortType;
        }
        public Expression ExpressionBody { get; }

        public string FieldName => (ExpressionBody as MemberExpression)?.Member.Name;

        public ESort SortType { get; }
    }

    public enum ESort
    {
        Asc = 1,
        Desc = -1
    }
}
