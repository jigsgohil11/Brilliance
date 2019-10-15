using System.Data;
using System.Linq;
using System.Reflection;
using Brilliance.Models;
using PetaPoco;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;

namespace Brilliance.Infrastructure.DataProvider
{
    public class BaseDataProvider
    {
        private readonly Database _db;
        public BaseDataProvider()
        {
            _db = new Database("DefaultConnection");
            _db.CommandTimeout = ConfigSettings.DBCommandTimeOut;
        }

        public T SaveEntity<T>(T item) where T : class
        {
            if (_db.IsNew(item))
            {
                _db.Insert(item);
            }
            else
            {
                _db.Update(item);
            }
            return item;
        }

        public void DeleteEntity<T>(Guid id) where T : class
        {
            string primaryKeyName = GetPrimaryKeyName<T>();
            _db.Delete<T>("WHERE " + primaryKeyName + "=@0", id);
        }

        public object ExecQuery(string query)
        {
            return _db.Execute(query);
        }

        public object GetScalar(string spname, List<SearchValueData> searchParam = null)
        {
            return _db.ExecuteScalar<object>(GetSPString(spname, searchParam));
        }

        public object GetScalar(string sqlquery)
        {
            return _db.ExecuteScalar<object>(sqlquery);
        }

        public int GetEntityCount<T>(List<SearchValueData> searchParam = null, string customWhere = "") where T : class, new()
        {
            return _db.ExecuteScalar<int>(GetFilterString<T>(searchParam, "", "", customWhere, true));
        }

        private string GetFilterString<T>(List<SearchValueData> searchParam, string sortIndex = "", string sortDirection = "", string customWhere = "", bool getCount = false) where T : class, new()
        {
            string select = (getCount ? "SELECT COUNT(*) FROM " : "SELECT * FROM ");
            return GetFilterSQLString<T>(searchParam, sortIndex, sortDirection, customWhere, !getCount, select);
        }

        private string GetFilterSQLString<T>(List<SearchValueData> searchParam, string sortIndex, string sortDirection, string customWhere, bool allowSort, string select) where T : class, new()
        {
            T item = new T();
            string where = "";
            string tableName = GetTableName<T>();
            select += tableName;

            if (searchParam != null && searchParam.Count > 0)
            {
                where += " WHERE 1=1";
                foreach (SearchValueData val in searchParam)
                {
                    string value = val.Value.Replace("'", "''");
                    PropertyInfo propertyInfo = item.GetType().GetProperty(val.Name);
                    if (propertyInfo.PropertyType == typeof(string))
                    {
                        if (val.IsEqual)
                            where += " AND " + val.Name + " = '" + value + "'";
                        else if (val.IsNotEqual)
                            where += " AND " + val.Name + " != '" + value + "'";
                        else
                            where += " AND " + val.Name + " LIKE '%" + value + "%'";
                    }
                    else if (propertyInfo.PropertyType == typeof(bool) || propertyInfo.PropertyType == typeof(bool?))
                    {
                        where += " AND " + val.Name + "=" + value;
                    }
                    else if (propertyInfo.PropertyType == typeof(int) || propertyInfo.PropertyType == typeof(int?) ||
                             propertyInfo.PropertyType == typeof(long?) || propertyInfo.PropertyType == typeof(long))
                    {
                        if (val.IsNotEqual)
                            where += " AND " + val.Name + "!=" + value;
                        else
                            where += " AND " + val.Name + "=" + value;
                    }
                    else if (propertyInfo.PropertyType == typeof(DateTime))
                    {
                        if (searchParam.Count(a => a.Name == val.Name) > 1)
                        {
                            where += " AND ((" + val.Name + " BETWEEN " +
                                     searchParam.FirstOrDefault(a => a.Name == val.Name).Value + " AND " +
                                     searchParam.LastOrDefault(a => a.Name == val.Name).Value + ") OR (" + val.Name +
                                     " BETWEEN " + searchParam.LastOrDefault(a => a.Name == val.Name).Value +
                                     " AND " + searchParam.FirstOrDefault(a => a.Name == val.Name).Value + "))";
                        }
                        else
                        {
                            where += " AND " + val.Name + "='" + value + "'";
                        }
                    }
                }
            }

            where += customWhere != ""
                         ? string.IsNullOrEmpty(where) ? " WHERE (" + customWhere + ")" : " AND (" + customWhere + ")"
                         : "";

            if (sortIndex != "")
                where += " ORDER BY " + sortIndex + " " + sortDirection;
            else if (!string.IsNullOrEmpty(GetSortKeyName<T>()) && !string.IsNullOrEmpty(GetSortDirection<T>()) && allowSort)
            {
                where += " ORDER BY " + GetSortKeyName<T>() + " " + GetSortDirection<T>();
            }

            return select + where.Replace("1=1 AND", "").Replace("1=1", "").Replace("@", "@@");
        }

        public T GetEntity<T>(Guid id) where T : class, new()
        {
            T item = new T();
            string tableName = GetTableName<T>();
            string primaryKeyName = GetPrimaryKeyName<T>();
            return _db.SingleOrDefault<T>("SELECT * FROM " + tableName + " WHERE " + primaryKeyName + "='" + id + "'") ?? item;
        }

        public T GetEntity<T>(string spname, List<SearchValueData> searchParam = null) where T : class, new()
        {
            return GetEntityList<T>(spname, searchParam).SingleOrDefault();
        }

        public T GetEntity<T>(List<SearchValueData> searchParam = null, string customWhere = "", string sortIndex = "", string sortDirection = "") where T : class, new()
        {
            return _db.SingleOrDefault<T>(GetFilterString<T>(searchParam, sortIndex, sortDirection, customWhere));
        }

        public List<T> GetEntityList<T>(string spname, List<SearchValueData> searchParam = null) where T : class, new()
        {
            return _db.Fetch<T>(";" + GetSPString(spname, searchParam));
        }

        public List<dynamic> GetDynamicList(string spname, List<SearchValueData> searchParam = null)
        {
            return _db.Fetch<dynamic>(";" + GetSPString(spname, searchParam));
        }

        public List<T> GetEntityList<T>(List<SearchValueData> searchParam = null, string customWhere = "", string sortIndex = "", string sortDirection = "") where T : class, new()
        {
            return _db.Query<T>(GetFilterString<T>(searchParam, sortIndex, sortDirection, customWhere)).ToList<T>();
        }

        public List<T> GetEntityList<T>(string sqlQuery) where T : class, new()
        {
            return _db.Query<T>(sqlQuery).ToList<T>();
        }

        public Page<T> GetEntityPageList<T>(List<SearchValueData> searchParam, int? pageSizeCount, int pageIndex, string sortIndex, string sortDirection, string customWhere = "") where T : class, new()
        {
            int pageSize = Convert.ToInt32(ConfigSettings.PageSize);

            if (pageSizeCount != null)
                pageSize = pageSizeCount.Value;

            if (sortIndex == "")
                sortIndex = GetSortKeyName<T>();

            if (sortDirection == "")
                sortDirection = GetSortDirection<T>();

            Page<T> pageList = _db.Page<T>(pageIndex, pageSize, GetFilterString<T>(searchParam, sortIndex, sortDirection, customWhere));

            //To get last page data if current page index is greater than last page
            if (pageList.Items.Count == 0 && pageList.TotalPages > 0 && pageList.TotalItems > 0)
                pageList = _db.Page<T>(pageList.TotalPages, pageSize, GetFilterString<T>(searchParam, sortIndex, sortDirection, customWhere));

            return pageList;
        }

        public Page<T> GetEntityPageList<T>(string spname, List<SearchValueData> searchParam, int? pageSizeCount, int pageIndex, string sortIndex, string sortDirection, string customWhere = "") where T : class, new()
        {
            int pageSize = Convert.ToInt32(ConfigSettings.PageSize);

            if (pageSizeCount != null)
                pageSize = pageSizeCount.Value;

            if (sortIndex == "")
                sortIndex = GetSortKeyName<T>();

            if (sortDirection == "")
                sortDirection = GetSortDirection<T>();

            return _db.Page<T>(pageIndex, pageSize, GetSPString(spname, searchParam));
        }

        private string GetFilterString<T>(List<SearchValueData> searchParam, string sortIndex = "", string sortDirection = "", string customWhere = "") where T : class, new()
        {
            T item = new T();
            string tableName = GetTableName<T>();
            string select = "SELECT * FROM " + tableName;
            string where = "";

            if (searchParam != null && searchParam.Count > 0)
            {
                where += " WHERE 1=1";
                foreach (SearchValueData val in searchParam)
                {
                    string value = val.Value.Replace("'", "''");
                    PropertyInfo propertyInfo = item.GetType().GetProperty(val.Name);
                    var propertyType = propertyInfo.PropertyType;
                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                    {
                        propertyType = propertyType.GetGenericArguments()[0];
                    }

                    if (propertyType == typeof(string))
                    {
                        if (val.IsEqual)
                            where += " AND " + val.Name + " = '" + value + "'";
                        else if (val.IsNotEqual)
                            where += " AND " + val.Name + " != '" + value + "'";
                        else
                            where += " AND " + val.Name + " LIKE '%" + value + "%'";
                    }
                    else if (propertyType == typeof(bool))
                    {
                        where += " AND " + val.Name + "=" + value;
                    }
                    else if (propertyType == typeof(int) || propertyType == typeof(long) || propertyType == typeof(decimal) || propertyType == typeof(float))
                    {
                        if (val.IsNotEqual)
                            where += " AND " + val.Name + "!=" + value;
                        else
                            where += " AND " + val.Name + "=" + value;
                    }
                    else if (propertyType == typeof(DateTime))
                    {
                        if (searchParam.Count(a => a.Name == val.Name) > 1)
                        {
                            where += " AND ((" + val.Name + " BETWEEN " + searchParam.First(a => a.Name == val.Name).Value + " AND " + searchParam.Last(a => a.Name == val.Name).Value + ") OR (" + val.Name + " BETWEEN " + searchParam.Last(a => a.Name == val.Name).Value + " AND " + searchParam.First(a => a.Name == val.Name).Value + "))";
                        }
                        else
                        {
                            where += " AND " + val.Name + "='" + value + "'";
                        }
                    }
                }
            }

            where += customWhere != "" ? string.IsNullOrEmpty(where) ? " WHERE (" + customWhere + ")" : " AND (" + customWhere + ")" : "";

            if (sortIndex != "")
                where += " ORDER BY " + sortIndex + " " + sortDirection;

            return select + where.Replace("1=1 AND", "").Replace("1=1", "").Replace("@", "@@");
        }

        private string GetSPString(string spname, List<SearchValueData> searchParam)
        {
            string sp = string.Format("EXEC {0}", spname);

            if (searchParam != null)
            {
                sp = searchParam.Aggregate(sp, (current, searchValueData) => current + string.Format(" @@{0} = '{1}',", searchValueData.Name, searchValueData.Value == null ? searchValueData.Value : searchValueData.Value.Replace("@", "@@").Replace("'", "''")));
                if (searchParam.Any())
                    sp = sp.TrimEnd(',');
            }
            return sp;
        }

        protected string GetSortKeyName<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(SortAttribute), true)[0] as SortAttribute).KeyValue;
        }

        protected string GetStoreProcedureName<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(StoreProcedureAttribute), true)[0] as StoreProcedureAttribute).StoreProcedureName;
        }

        protected string GetSortDirection<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(SortAttribute), true)[0] as SortAttribute).DirectionValue;
        }

        protected string GetTableName<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(TableNameAttribute), true)[0] as TableNameAttribute).Value;
        }

        protected string GetPrimaryKeyName<T>()
        {
            return (typeof(T).GetCustomAttributes(typeof(PrimaryKeyAttribute), true)[0] as PrimaryKeyAttribute).Value;
        }

        public Page<T> GetPageInStoredProcResultSet<T>(int pageIndex, int pageSize, int count, List<T> itemsList) where T : class, new()
        {
            var result = new Page<T>
            {
                CurrentPage = pageIndex,
                ItemsPerPage = pageSize,
                TotalItems = count
            };
            result.TotalPages = result.TotalItems / pageSize;

            if ((result.TotalItems % pageSize) != 0)
                result.TotalPages++;

            result.Items = itemsList;
            return result;
        }

        public List<DropDownItem> GetAutoCompleteList(string tableName, string textField, string valueField, string searchText, int pageSize, string searchCriteria1 = "", string searchCriteria2 = "")
        {
            string query = "select distinct top {0}  {1} as [Key],{2} as Value from {3} where {4} like '%{5}%' {6} {7}";
            return GetEntityList<DropDownItem>(string.Format(query, pageSize, textField, valueField, tableName, textField, searchText, searchCriteria1, searchCriteria2));
        }

        #region Add For Generic List and Search

        public ServiceResponse DeleteRecord<T>(long id, string successMessage = "Record deleted successfully") where T : class, new()
        {
            ServiceResponse response = new ServiceResponse();
            List<SearchValueData> searchList = new List<SearchValueData>();

            searchList.Add(new SearchValueData { Name = "PrimaryKeyID", Value = Convert.ToString(id) });
            searchList.Add(new SearchValueData { Name = "PrimaryKeyName", Value = GetPrimaryKeyName<T>() });
            searchList.Add(new SearchValueData { Name = "TableName", Value = GetTableName<T>() });

            int status = Convert.ToInt32(GetScalar("DeleteRecord", searchList));
            if (status == 1)
            {
                response.Message = successMessage;
                response.IsSuccess = true;
            }

            if (status == 0)
            {
                response.Message = "Sorry, record is used in the system. You can't delete the record.";
                response.IsSuccess = false;
            }
            return response;
        }
        public ServiceResponse DeleteRecord<T>(string customWhere, string successMessage = "Record deleted successfully") where T : class, new()
        {
            ServiceResponse response = new ServiceResponse();
            List<SearchValueData> searchList = new List<SearchValueData>();
            //searchList.Add(new SearchValueData { Name = "PrimaryKeyID", Value = Convert.ToString(id) });
            //searchList.Add(new SearchValueData { Name = "PrimaryKeyName", Value = GetPrimaryKeyName<T>() });
            searchList.Add(new SearchValueData { Name = "CustomWhere", Value = customWhere });
            searchList.Add(new SearchValueData { Name = "TableName", Value = GetTableName<T>() });

            int status = Convert.ToInt32(GetScalar("DeleteRecord", searchList));
            if (status == 1)
            {
                response.Message = successMessage;
                response.IsSuccess = true;
            }

            if (status == 0)
            {
                response.Message = "Sorry, record is used in the system. You can't delete the record.";
                response.IsSuccess = false;
            }
            return response;
        }

        #endregion Add For Generic List and Search

        #region For Multiple ResutlSet


        /// <summary>
        /// Perform a multi-results set query
        /// </summary>
        /// <param name="query">The SQL query to be executed</param>
        /// <param name="args">Arguments to any embedded parameters in the SQL</param>
        /// <typeparam name="T">The Type representing Model in which Result Set Data will be Bind</typeparam>
        /// <returns>Return Object of Generic Type T</returns>
        public T GetMultipleEntity<T>(string query, params object[] args) where T : new()
        {
            GridReader grd = _db.QueryMultiple(query, args);
            T obj = new T();
            Type returnType = typeof(T);
            PropertyInfo[] propertyInfo = returnType.GetProperties();
            foreach (var info in propertyInfo)
            {
                bool isFirstorDefault = false;
                Type t;
                if (info.PropertyType.GetGenericArguments().Any())
                {
                    t = info.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    isFirstorDefault = true;
                    t = info.PropertyType;
                }
                var value =
                (typeof(BaseDataProvider).GetMethod("GetValueOf")
                .MakeGenericMethod(t)
                .Invoke(null, new object[] { grd, isFirstorDefault }));
                info.SetValue(obj, value, null);
            }
            _db.CloseSharedConnection();
            return obj;
        }

        public T GetMultipleEntity<T>(string spname, List<SearchValueData> searchParam) where T : new()
        {
            GridReader grd = _db.QueryMultiple(GetSPString(spname, searchParam));
            T obj = new T();
            Type returnType = typeof(T);
            PropertyInfo[] propertyInfo = returnType.GetProperties();
            foreach (var info in propertyInfo)
            {

                if (!info.CanWrite)
                {
                    continue;
                }
                bool isFirstorDefault = false;
                Type t;
                if (info.PropertyType.GetGenericArguments().Any())
                {
                    t = info.PropertyType.GetGenericArguments()[0];
                }
                else
                {
                    isFirstorDefault = true;
                    t = info.PropertyType;
                }
                var value =
                (typeof(BaseDataProvider).GetMethod("GetValueOf")
                .MakeGenericMethod(t)
                .Invoke(null, new object[] { grd, isFirstorDefault }));
                info.SetValue(obj, value, null);
            }
            _db.CloseSharedConnection();
            return obj;
        }

        /// <summary>
        /// Reads from a GridReader, Either List of Type T or Object if Type T
        /// </summary>
        /// <param name="reader">GridReader from which result set will be read.</param>
        /// <param name="isFirstorDefault">If true function returns first row or default value as object of type T else return List of T"></typeparam></param>
        /// <typeparam name="T">The Type representing a row in the result set</typeparam>
        /// <returns>An enumerable collection of result records</returns>
        public static object GetValueOf<T>(GridReader reader, bool isFirstorDefault)
        {
            IEnumerable<T> lst = reader.Read<T>();
            if (isFirstorDefault)
                return lst.FirstOrDefault();
            return lst.ToList();
        }

        /// <summary>
        /// Execute Query And Return DataSet (This Support Only for single Table ResultSet)
        /// </summary>
        /// <param name="sql">The SQL query to be executed</param>
        /// <param name="args">Arguments to any embedded parameters in the SQL</param>
        /// <returns>Dataset With Single Table Only</returns>
        public DataSet GetDataSet(string query, params object[] args)
        {
            return _db.QueryForDataSet(query, args);
        }

        public DataSet GetDataSet(string spname, List<SearchValueData> searchParam)
        {
            string qry = GetSPString(spname, searchParam);
            DataSet ds = _db.GetDataSet(qry);

            return ds;
        }

        #endregion For Multiple ResutlSet

        public bool BulkInsert(string tableName, DataTable dt)
        {
            if (!string.IsNullOrEmpty(tableName) && dt != null && dt.Rows.Count != 0)
                return _db.BulkInsert(tableName, dt);
            else
                return false;
        }
        public DataSet BulkInsert(string spname, SqlCommand cmd)
        {
            return _db.BulkInsert(spname, cmd);
        }
    }
}