using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Web;
using Brilliance.Infrastructure.DataProvider;
using Brilliance.Models;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using Brilliance.Models.Entity;
//using Oracle.ManagedDataAccess.Client;
//using HiQPdf;
using System.Threading;
using Brilliance.Models.ViewModel;
using System.Web.Helpers;

namespace Brilliance.Infrastructure
{
    public class Common
    {
        public enum Shapes
        {
            Triangle = 1,
            Square = 2,
            Round
        }

        public enum AppStatus
        {
            Pending,
            Approved,
            Rejected
        }
        public static string SerializeObject<T>(T objectData)
        {
            string defaultJson = JsonConvert.SerializeObject(objectData);
            return defaultJson;
        }

        public string SerializePartialObject<T>(T objectData)
        {
            string defaultJson = JsonConvert.SerializeObject(objectData, (Newtonsoft.Json.Formatting)Formatting.Indented,
                                  new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            return defaultJson;
        }

        public static T DeserializeObject<T>(string json)
        {
            T obj = default(T);
            obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        public static bool PasswordsMatch(string Modelpassword, string Dbpassword)
        {
            if (Modelpassword == Dbpassword)
            {
                return true;
            }
            return false;
        }

        public static DataTable personlist()
        {
            DataTable dtpersonlist = new DataTable();
            dtpersonlist.TableName = "mst_Person";
            dtpersonlist.Columns.Add("PersonID", typeof(Guid));
            dtpersonlist.Columns.Add("PersonName", typeof(string));
            dtpersonlist.Columns.Add("PersonType", typeof(string));
            dtpersonlist.Columns.Add("Telephone", typeof(string));
            dtpersonlist.Columns.Add("Email", typeof(string));
            dtpersonlist.Columns.Add("Fax", typeof(string));
            dtpersonlist.Columns.Add("ClientID", typeof(string));
            dtpersonlist.Columns.Add("IsActive", typeof(bool));
            dtpersonlist.Columns.Add("UpdatedBy", typeof(Guid));
            dtpersonlist.Columns.Add("UpdateOn", typeof(DateTime));

            return dtpersonlist;
        }

        public static String ConvertDataTableTojSonString(DataTable dataTable)
        {
            //System.Web.Script.Serialization.JavaScriptSerializer serializer =
            //       new System.Web.Script.Serialization.JavaScriptSerializer();

            List<Dictionary<String, Object>> tableRows = new List<Dictionary<String, Object>>();

            Dictionary<String, Object> row;

            foreach (DataRow dr in dataTable.Rows)
            {
                row = new Dictionary<String, Object>();
                foreach (DataColumn col in dataTable.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                tableRows.Add(row);
            }
            return SerializeObject(tableRows); //serializer.Serialize(tableRows);
        }

        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());

            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }

        public static Guid CheckIdNullOrEmpty(string guid)
        {
            return !string.IsNullOrEmpty(guid) ? !string.IsNullOrEmpty(Crypto.Decrypt(guid)) ? new Guid(Crypto.Decrypt(guid)) : Guid.Empty : Guid.Empty;
        }
        public static Guid CheckIdNullOrEmptyNonEncrypt(string guid)
        {
            return !string.IsNullOrEmpty(guid) ? new Guid(guid) : Guid.Empty;
        }

        public static string ConvertDateFormate(DateTime date)
        {

            string strdate = "";
            //strdate = date.ToString(@"" + clsSession.strDateFormat.Replace("/", @"\/") + "");
            strdate = date.ToString(Constants.IndDateFormat);
            //strdate = date.ToString("MM/dd/yyyy");

            return strdate;
        }

        public static string ConvertDateFormate2(DateTime date)
        {

            string strdate = "";
            //strdate = date.ToString(@"" + clsSession.strDateFormat.Replace("/", @"\/") + "");
            strdate = date.ToString(Constants.IndDateFormat2);
            //strdate = date.ToString("MM/dd/yyyy");

            return strdate;
        }

        public static string ConvertDateInDbDateFormat(DateTime date)
        {
            //return date.ToString("yyyy-MM-dd hh:mm:ss");
            return date.ToString(Constants.DbDateTimeFormat);
        }

        public static ServiceResponse CheckDuplicateNameOrCode<T>(string code, string name, string codeValue, string nameValue, string titleName, string ClientIDValue, string primaryKey = "", string primaryKeyValue = "") where T : class, new()
        {
            T item = new T();

            codeValue = (!string.IsNullOrEmpty(codeValue) ? codeValue.Replace("'", "''") : codeValue);
            nameValue = (!string.IsNullOrEmpty(nameValue) ? nameValue.Replace("'", "''") : nameValue);

            string customWhereCodeName;
            string customWhereName;
            string customWhereCode;

            var response = new ServiceResponse();
            BaseDataProvider baseDataProvider = new BaseDataProvider();

            if (!string.IsNullOrEmpty(primaryKeyValue))
            {
                //SELECT COUNT(*) FROM mst_Object WHERE ObjectName='Nil' AND IsActive='1' AND ClientID='a7eb4154-3847-494e-9320-1fb9ac3c93a7'
                customWhereCodeName = string.Format("(" + code + "='{0}' OR " + name + "='{1}') AND " + primaryKey + "!='{2}' AND IsActive='1' AND ClientID='{3}' ", codeValue, nameValue, primaryKeyValue, ClientIDValue);

                customWhereName = string.Format("" + name + "='{0}' AND " + primaryKey + "!='{1}' AND IsActive='1' AND ClientID='{2}' ", nameValue, primaryKeyValue, ClientIDValue);


                customWhereCode = string.Format("" + code + "='{0}' AND " + primaryKey + "!='{1}' AND IsActive='1' AND ClientID='{2}' ", codeValue, primaryKeyValue, ClientIDValue);
            }
            else
            {
                customWhereCodeName = string.Format("(" + code + "='{0}' OR " + name + "='{1}') AND IsActive='1' AND ClientID='{2}' ", codeValue, nameValue, ClientIDValue);
                customWhereName = string.Format("" + name + "='{0}' AND IsActive='1' AND ClientID='{1}' ", nameValue, ClientIDValue);
                customWhereCode = string.Format("" + code + "='{0}' AND IsActive='1' AND ClientID='{1}' ", codeValue, ClientIDValue);
            }
            int countCodeName = baseDataProvider.GetEntityCount<T>(null, customWhereCodeName);
            int countName = baseDataProvider.GetEntityCount<T>(null, customWhereName);
            int countCode = baseDataProvider.GetEntityCount<T>(null, customWhereCode);
            if ((countName > 0 && countCode > 0) && (countCodeName > 0 && countName > 0))
            {
                response.IsSuccess = false;
                response.Message = string.Format("NameANDCodeExits", titleName);
                return response;
            }
            else if ((countCodeName > 0) && (countName > 0 && countCodeName > 0))
            {
                response.IsSuccess = false;
                response.Message = string.Format("NameExits", titleName);
                return response;
            }
            else if ((countCodeName > 0) && (countCode > 0 && countCodeName > 0))
            {
                response.IsSuccess = false;
                response.Message = string.Format("CodeExits", titleName);
                return response;
            }

            else
                response.IsSuccess = true;

            return response;
        }

        public static bool SendEmail(string toemail, string message, string subject)
        {
            bool result = false;
            try
            {
                MailMessage NetMail = new MailMessage();
                SmtpClient MailClient = new SmtpClient();
                MailClient.EnableSsl = true;
                using (MailClient = new SmtpClient())
                {
                    string ThisHost = Convert.ToString("smtp.gmail.com");
                    NetworkCredential TheseCredentials = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["Email"]), Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"]));

                    NetMail.To.Add(toemail);
                    NetMail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["Email"]), "DIDESS");
                    NetMail.IsBodyHtml = true;
                    NetMail.Priority = MailPriority.High;
                    NetMail.Subject = subject;
                    NetMail.Body = message;

                    MailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    MailClient.EnableSsl = true;
                    MailClient.Host = ThisHost;
                    MailClient.Port = 587;
                    MailClient.UseDefaultCredentials = false;
                    MailClient.Credentials = TheseCredentials;
                    MailClient.Send(NetMail);
                    NetMail.Dispose();
                    result = true;
                }
            }
            catch (SmtpException Ex)
            {
                try
                {
                    //SmtpClient sc = new SmtpClient(Convert.ToString("smtp.gmail.com"), 587);
                    //sc.EnableSsl = true;
                    //sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                    //sc.UseDefaultCredentials = false;
                    //sc.Credentials = new NetworkCredential(Convert.ToString(ConfigurationManager.AppSettings["Email"]), Convert.ToString(ConfigurationManager.AppSettings["EmailPassword"]));
                    //MailMessage mail = new MailMessage();
                    //using (mail = new MailMessage())
                    //{
                    //    mail.To.Add(new MailAddress(toemail));
                    //    mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["Email"]), "DIDESS");
                    //    mail.Subject = subject;
                    //    mail.IsBodyHtml = true;
                    //    mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                    //    mail.Priority = System.Net.Mail.MailPriority.High;
                    //    mail.Body = message;
                    //    sc.Port = 587;
                    //    sc.EnableSsl = true;
                    //    sc.Send(mail);
                    //    mail.Dispose();
                    //    result = true;
                    //}
                    throw Ex;
                }
                catch (SmtpException ExS)
                {
                    throw Ex;
                    //return false;
                }
            }

            return result;
        }

        public static string UploadFile(HttpPostedFileBase File, bool isImg)
        {
            string strReturn = "";
            try
            {
                if (File != null)
                {
                    if (isImg)
                    {
                        var ProfPicName = Guid.NewGuid() + Path.GetExtension(File.FileName);

                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"])))
                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"]));

                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID)))
                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID));

                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID + "/" + "Image")))
                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID + "/" + "Image"));

                        var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID + "/" + "Image" + "/"), ProfPicName);

                        File.SaveAs(path);

                        strReturn = SessionHelper.ClientID + "/" + "Image" + "/" + ProfPicName;
                    }
                    else
                    {
                        var ProfPicName = Guid.NewGuid() + Path.GetExtension(File.FileName);

                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"])))
                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"]));

                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID)))
                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID));

                        if (!Directory.Exists(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID + "/" + "Document")))
                            Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID + "/" + "Document"));

                        var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath(ConfigurationManager.AppSettings["Files"] + SessionHelper.ClientID + "/" + "Document" + "/"), ProfPicName);

                        File.SaveAs(path);

                        strReturn = SessionHelper.ClientID + "/" + "Document" + "/" + ProfPicName;
                    }
                }
                return strReturn;
            }
            catch
            {
                return strReturn;
            }
        }

        public static void ActionLog(string actionName, string operationType, string oldvalue = null, string newValue = null)
        {
            BaseDataProvider bdp = new BaseDataProvider();

            ActionLog actionLog = new ActionLog();
            actionLog.UserID = SessionHelper.UserId;
            actionLog.ClientID = SessionHelper.ClientID;
            actionLog.LoginLogID = SessionHelper.LoginLogID;
            actionLog.ActionDate = DateTime.Now;
            actionLog.ActionObject = actionName;
            actionLog.ActionFrom = Constants.Web;
            actionLog.OperationType = operationType;
            actionLog.OldValue = oldvalue;
            actionLog.NewValue = newValue;

            bdp.SaveEntity<ActionLog>(actionLog);
        }

        public static string ConvertModelToJsonString<T>(T model) where T : class, new()
        {
            return JsonConvert.SerializeObject(model);
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }

        public static DataTable ProgramDocument()
        {
            DataTable dtprogramdocument = new DataTable();
            dtprogramdocument.TableName = "mst_ProgrammeDocumentType";
            dtprogramdocument.Columns.Add("ProgrammeID", typeof(Guid));
            dtprogramdocument.Columns.Add("DocumentTypeID", typeof(Guid));
            dtprogramdocument.Columns.Add("IsActive", typeof(bool));
            dtprogramdocument.Columns.Add("UpdateOn", typeof(DateTime));
            dtprogramdocument.Columns.Add("UpdatedBy", typeof(Guid));
            dtprogramdocument.Columns.Add("ClientID", typeof(Guid));
            dtprogramdocument.Columns.Add("IsRequired", typeof(bool));
            return dtprogramdocument;
        }

        public static DataTable Companyownerinfo()
        {
            DataTable dtownerinfo = new DataTable();
            dtownerinfo.TableName = "tblowner";
            dtownerinfo.Columns.Add("CompanyID", typeof(Guid));
            dtownerinfo.Columns.Add("OwnerSalutation", typeof(string));
            dtownerinfo.Columns.Add("OwnerFirstName", typeof(string));
            dtownerinfo.Columns.Add("OwnerLastName", typeof(string));
            dtownerinfo.Columns.Add("OwnerPercentageOwned", typeof(int));
            dtownerinfo.Columns.Add("GenderTerm", typeof(string));
            dtownerinfo.Columns.Add("NamibianID", typeof(string));
            dtownerinfo.Columns.Add("PassportID", typeof(string));
            dtownerinfo.Columns.Add("Citizenship", typeof(string));
            dtownerinfo.Columns.Add("ClientID", typeof(Guid));
            dtownerinfo.Columns.Add("IsActive", typeof(bool));
            dtownerinfo.Columns.Add("UpdateOn", typeof(DateTime));
            dtownerinfo.Columns.Add("UpdatedBy", typeof(Guid));

            return dtownerinfo;
        }

        public static DataTable DocumentInfo()
        {
            DataTable dtdocumentinfo = new DataTable();
            dtdocumentinfo.TableName = "tbldocument";
            dtdocumentinfo.Columns.Add("DocCetagory", typeof(Guid));
            dtdocumentinfo.Columns.Add("AssociationType", typeof(string));
            dtdocumentinfo.Columns.Add("AssociationID", typeof(Guid));
            dtdocumentinfo.Columns.Add("ApplicationID", typeof(Guid));
            dtdocumentinfo.Columns.Add("ClientID", typeof(Guid));
            dtdocumentinfo.Columns.Add("UserID", typeof(Guid));
            dtdocumentinfo.Columns.Add("IsActive", typeof(bool));
            return dtdocumentinfo;
        }

        public static DataTable EligibilitycriteriaInfo()
        {
            DataTable EligibilitycriteriaInfo = new DataTable();
            EligibilitycriteriaInfo.TableName = "Eligibilitycriteria";
            EligibilitycriteriaInfo.Columns.Add("ApplicationID", typeof(Guid));
            EligibilitycriteriaInfo.Columns.Add("ProgrammeCriteriaID", typeof(Guid));
            EligibilitycriteriaInfo.Columns.Add("IsVerify", typeof(bool));
            EligibilitycriteriaInfo.Columns.Add("IsActive", typeof(bool));
            EligibilitycriteriaInfo.Columns.Add("UpdatedBy", typeof(Guid));
            EligibilitycriteriaInfo.Columns.Add("UpdateOn", typeof(DateTime));
            EligibilitycriteriaInfo.Columns.Add("ClientID", typeof(Guid));

            return EligibilitycriteriaInfo;
        }

        public static List<T> DataReaderMapToList<T>(IDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    if (!object.Equals(dr[prop.Name], DBNull.Value))
                    {
                        prop.SetValue(obj, dr[prop.Name], null);
                    }
                }
                list.Add(obj);
            }
            return list;
        }

        //public static ServiceResponse CreatePdf(StringWriter stringWriter, string fileFolderpath, Guid? guidUniqueFileName)
        //{
        //    var response = new ServiceResponse();
        //    var refReceiptHtml = stringWriter != null ? stringWriter.ToString() : "";
        //    string filename = (guidUniqueFileName == null || guidUniqueFileName == Guid.Empty ? (Guid.NewGuid() + ".pdf") : (guidUniqueFileName + ".pdf"));
        //    HtmlToPdf htmlToPdfConverter = new HtmlToPdf();
        //    try
        //    {
        //        htmlToPdfConverter.SerialNumber = ConfigSettings.HiqPdfSerialKey;
        //        htmlToPdfConverter.BrowserWidth = 1200;
        //        htmlToPdfConverter.HtmlLoadedTimeout = 120;
        //        htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
        //        htmlToPdfConverter.Document.PdfStandard = PdfStandard.Pdf;
        //        htmlToPdfConverter.Document.Margins = new PdfMargins(10);
        //        htmlToPdfConverter.TriggerMode = ConversionTriggerMode.Auto;
        //        htmlToPdfConverter.Document.Header.Enabled = true;

        //        byte[] pdfBuffer = null;
        //        pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(refReceiptHtml, "");
        //        string servermap = System.Web.HttpContext.Current.Server.MapPath("~" + fileFolderpath + filename);   // "~/Documents/Application/"
        //        using (Stream output = new FileStream(servermap, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None))
        //        {
        //            output.Write(pdfBuffer, 0, pdfBuffer.Length);
        //        }
        //        response.IsSuccess = true;
        //        response.Data = (fileFolderpath + filename);    // "/Documents/Application/"
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Internal server error: " + ex.Message;
        //    }
        //    return response;
        //}

        public static LoginUserRoleRightModel IsPermissionAllowed(string permissionName)
        {
            List<LoginUserRoleRightModel> LoginUserRoleRightList = Json.Decode<List<LoginUserRoleRightModel>>(SessionHelper.LoginUserRoleRightList).ToList();
            return (LoginUserRoleRightModel)LoginUserRoleRightList.Where(x => x.DisplayName.Trim().ToLower() == permissionName.Trim().ToLower()).FirstOrDefault();
        }
        public static ServiceResponse DeletepdffileFromFolder(Guid applicationId, string pdfFilePath)
        {
            var response = new ServiceResponse();
            if (applicationId != Guid.Empty && !string.IsNullOrEmpty(pdfFilePath))
            {
                try
                {
                    Thread.Sleep(5000);
                    string path = HttpContext.Current.Server.MapPath(pdfFilePath);
                    FileInfo file = new FileInfo(path);
                    if (file.Exists)
                    {
                        file.Delete();
                        response.IsSuccess = true;
                    }
                    else
                        response.Message = "Pdf file not found";
                }
                catch (Exception ex)
                {
                    response.Message = "Internal server error: " + ex.Message;
                }
            }
            else
                response.Message = "Oops, something went wrong";
            return response;
        }
    }
}