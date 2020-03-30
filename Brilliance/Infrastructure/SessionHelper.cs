using Brilliance.Models;
using System;
using System.Data;
using System.Web;


namespace Brilliance.Infrastructure
{
    public class SessionHelper
    {
        public static Guid UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] != null)
                    return new Guid(HttpContext.Current.Session["UserID"].ToString());
                else
                    return Guid.Empty;
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }

        public static Guid LoginLogID
        {
            get
            {
                if (HttpContext.Current.Session["LoginLogID"] != null)
                    return new Guid(HttpContext.Current.Session["LoginLogID"].ToString());
                else
                    return Guid.Empty;
            }
            set
            {
                HttpContext.Current.Session["LoginLogID"] = value;
            }
        }

        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["UserName"] != null)
                    return HttpContext.Current.Session["UserName"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }
        public static string FullName
        {
            get
            {
                if (HttpContext.Current.Session["FullName"] != null)
                    return HttpContext.Current.Session["FullName"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["FullName"] = value;
            }
        }

        public static string Photo
        {
            get
            {
                if (HttpContext.Current.Session["Photo"] != null)
                    return HttpContext.Current.Session["Photo"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["Photo"] = value;
            }
        }

        public static string Password
        {
            get
            {
                if (HttpContext.Current.Session["Password"] != null)
                    return HttpContext.Current.Session["Password"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["Password"] = value;
            }
        }

        public static Guid ClientID
        {
            get
            {
                if (HttpContext.Current.Session["ClientID"] != null)
                    return new Guid(HttpContext.Current.Session["ClientID"].ToString());
                else
                    return Guid.Empty;
            }
            set
            {
                HttpContext.Current.Session["ClientID"] = value;
            }
        }

        public static Guid RoleID
        {
            get
            {
                if (HttpContext.Current.Session["RoleID"] != null)
                    return new Guid(HttpContext.Current.Session["RoleID"].ToString());
                else
                    return Guid.Empty;
            }
            set
            {
                HttpContext.Current.Session["RoleID"] = value;
            }
        }

        public static string RoleName
        {
            get
            {
                if (HttpContext.Current.Session["RoleName"] != null)
                    return HttpContext.Current.Session["RoleName"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["RoleName"] = value;
            }
        }

        public static string RegistrationStatus
        {
            get
            {
                if (HttpContext.Current.Session["RegistrationStatus"] != null)
                    return HttpContext.Current.Session["RegistrationStatus"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["RegistrationStatus"] = value;
            }
        }

        public static ServiceResponse SessionResponse
        {
            get { return (ServiceResponse)HttpContext.Current.Session["SessionResponse"]; }
            set { HttpContext.Current.Session["SessionResponse"] = value; }
        }

        public static string Permissions
        {
            get { return HttpContext.Current.Session["Permissions"].ToString(); }
            set { HttpContext.Current.Session["Permissions"] = value; }
        }

        public static string LoginUserRoleRightList
        {
            get { return HttpContext.Current.Session["LoginUserRoleRightList"].ToString(); }
            set { HttpContext.Current.Session["LoginUserRoleRightList"] = value; }
        }

        public static string UserType
        {
            get
            {
                if (HttpContext.Current.Session["UserType"] != null)
                    return HttpContext.Current.Session["UserType"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["UserType"] = value;
            }
        }

        public static string RegionIDS
        {
            get
            {
                if (HttpContext.Current.Session["RegionIDS"] != null)
                    return HttpContext.Current.Session["RegionIDS"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["RegionIDS"] = value;
            }
        }

        public static string ProgrammeID
        {
            get
            {
                if (HttpContext.Current.Session["ProgrammeID"] != null)
                    return HttpContext.Current.Session["ProgrammeID"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["ProgrammeID"] = value;
            }
        }

        //public static List<Models.Entity.Notification> NotificationList
        //{
        //    get { return (List<Models.Entity.Notification>)HttpContext.Current.Session["NotificationList"]; }
        //    set { HttpContext.Current.Session["NotificationList"] = value; }
        //}

        public static DataTable NotificationList
        {
            get { return (DataTable)HttpContext.Current.Session["NotificationList"]; }
            set { HttpContext.Current.Session["NotificationList"] = value; }
        }

        public static DataSet DashboardPercentList
        {
            get { return (DataSet)HttpContext.Current.Session["DashboardPercentList"]; }
            set { HttpContext.Current.Session["DashboardPercentList"] = value; }
        }

        //public static DataTable tbldivision
        //{
        //    get
        //    {
        //        return HttpContext.Current.Session["tbldivision"] != null ? (HttpContext.Current.Session["tbldivision"] as DataTable) : null;
        //    }
        //    set
        //    {
        //        HttpContext.Current.Session["tbldivision"] = value;
        //    }
        //}
        public static string ConstituencyIDs
        {
            get
            {
                if (HttpContext.Current.Session["ConstituencyIDs"] != null)
                    return HttpContext.Current.Session["ConstituencyIDs"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["ConstituencyIDs"] = value;
            }
        }

        public static string LocationIDS
        {
            get
            {
                if (HttpContext.Current.Session["LocationIDS"] != null)
                    return HttpContext.Current.Session["LocationIDS"].ToString();
                else
                    return string.Empty;
            }
            set
            {
                HttpContext.Current.Session["LocationIDS"] = value;
            }
        }

        public static bool FirstTimeLogin
        {
            get
            {
                if (!string.IsNullOrEmpty(Convert.ToString(HttpContext.Current.Session["FirstTimeLogin"])))
                    return Convert.ToBoolean(HttpContext.Current.Session["FirstTimeLogin"].ToString());
                else
                    return false;
            }
            set { HttpContext.Current.Session["FirstTimeLogin"] = value; }
        }

        public static long TotalDocumentCount
        {
            get
            {
                if (HttpContext.Current.Session["TotalDocumentCount"] != null)
                    return Convert.ToInt64(Convert.ToString(HttpContext.Current.Session["TotalDocumentCount"]));
                else
                    return 0;
            }
            set
            {
                HttpContext.Current.Session["TotalDocumentCount"] = value;
            }
        }

        public static DataSet QuotaApplicationData
        {
            get { return (DataSet)HttpContext.Current.Session["QuotaApplicationData"]; }
            set { HttpContext.Current.Session["QuotaApplicationData"] = value; }
        }

        public static DataSet Applicationdata
        {
            get { return (DataSet)HttpContext.Current.Session["Applicationdata"]; }
            set { HttpContext.Current.Session["Applicationdata"] = value; }
        }
    }
}