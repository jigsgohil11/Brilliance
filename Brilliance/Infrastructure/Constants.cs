using System;


namespace Brilliance.Infrastructure
{
    public class Constants
    {
        //public const string StaticGuid = "a7eb4154-3847-494e-9320-1fb9ac3c93a7";

        public const string IndianCulture = "en-IN";

        public const string AccessDeniedUrl = "login/accessdenied";
        public const string LoginURL = "/login/login";
        public const string DefaultLatLang = "-22.266867;17.048075";

        public const int AllRecordsConstant = -1;
        public const string EncryptedQueryString = "EncryptedQueryString";

        public const string LogoutUrl = "/security/logout";
        public const string AnonymousPermission = "Anonymous";
        public const string NotAuthorized = "NotAuthorized";

        public const string DbDateFormat = "yyyy/MM/dd";
        public const string IndDateFormat = "dd/MM/yyyy";   // Indian Date format
        public const string IndDateFormat2 = "MM/dd/yyyy";
        //public const string DbDateTimeFormat = "yyyy/MM/dd HH:mm";
        public const string DbDateTimeFormat = "yyyy-MM-dd hh:mm:ss";
        public const string DateFormatString = "{0:dd/MM/yyyy}";   // Indian Date format for us

        public const string RegxEmail = @"[A-Za-z0-9\._%+-]+\@[A-Za-z0-9\.-]+\.[A-Za-z]{2,4}";
        //public const string RegxWebSiteURL = @"^(?:(ftp|http|https)?:\/\/)?(?:[\w-]+\.)+([a-z]|[A-Z]|[0-9]){2,6}$";
        public const string RegxWebSiteURL = @"https?:\/\/(www\.)?[-a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)";
        public const string RegxPhone = @"^[789]\d{9}$";

        public const string RegxGuid = @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$";


        #region Role Permission List

        // css class
        public const string Class_Edit_Delete_Permission = "edit_delete_permission";
        public const string Class_Cannot_Access_Create = "cannot_access_create";
        public const string Class_Cannot_Access_Update = "cannot_access_update";
        public const string Class_Cannot_Access_Remove = "cannot_access_remove";
        // css class

        // Permission Types
        public const string Can_View = "Can_View";
        public const string Can_CreateOrUpdate = "Can_Create,Can_Update";
        public const string Can_Delete = "Can_Delete";
        // Permission Types
        public const string Can_Access_Department = "Department";

        public const string Can_Access_Location = "Location";
        public const string Can_Access_BorderPost = "BorderPost";
        public const string Can_Access_Town = "Town";
        public const string Can_Access_Donor = "Donor";
        public const string Can_Access_Constituency = "Constituency";
        public const string Can_Access_Consultant = "Consultant";
        public const string Can_Access_Budget = "Budget";
        public const string Can_Access_Pramoter = "Pramoter";
        public const string Can_Access_ODS = "ODS";
        public const string Can_Access_Sector = "Sector";
        public const string Can_Access_SubSector = "SubSector";
        
        public const string Can_Access_SMEApplication = "SMEApplication";
        public const string Can_Access_EASApplication = "EASApplication";
        public const string Can_Access_GeneralOption = "GeneralOption";
        public const string Can_Access_ReasonCode = "ReasonCode";
        public const string Can_Access_Equipment = "Equipment";
        public const string Can_Access_ProductType = "ProductType";
        public const string Can_Access_SupportingAgency = "SupportingAgency";
        public const string Can_Access_SubDivision = "SubDivision";
        public const string Can_Access_Message = "Message";
        public const string Can_Access_Objective = "Objective";
        public const string Can_Access_Program = "Program";
        public const string Can_Access_Registration = "NewApplication";
        public const string Can_Access_RegistrationDetails = "RegistrationDetails";
        public const string Can_Access_Projects = "Projects";
        public const string Can_Access_ProjectsReview = "ProjectsReview";
        //public const string Can_Access_EquipmentAidScheme = "EquipmentAidScheme";
        //public const string Can_Access_BusinessSupportServicesProgramme = "BusinessSupportServicesProgramme";
        //public const string Can_Access_IndustrialUpgradingandModernizationProgram = "IndustrialUpgradingandModernizationProgram";
        //public const string Can_Access_RentalSpaceforMSMEs = "RentalSpaceforMSMEs";
        //public const string Can_Access_SiteandPremises = "SiteandPremises";
        //public const string Can_Access_ManufacturingIncentives = "ManufacturingIncentives";
        //public const string Can_Access_OzoneProgramme = "OzoneProgramme";
        //public const string Can_Access_SMECertificate = "SMECertificate";
        public const string Can_Access_Region = "Region";
        public const string Can_Access_UserRole = "UserRole";
        public const string Can_Access_UserGroup = "UserGroup";
        public const string Can_Access_UserAlert = "UserAlert";
        public const string Can_Access_UserAction = "ActionLog";
        public const string Can_Access_UserLogin = "LoginLog";
        public const string Can_Access_Organizational = "Organizational";
        public const string Can_Access_UploadByUser = "UploadByUser";
        public const string Can_Access_FiscalYear = "FiscalYear";
        public const string Can_Access_FiscalYearEnd = "FiscalYearEnd";
        public const string Can_Access_ApproveReject = "Approve/Reject";



        public const string Can_Access_ClientSetup = "Client Setup";
        public const string Can_Access_Orgadmin = "Organisation admin";
        public const string Can_Access_Company = "Company";
        public const string Can_Access_Division = "Division";
        public const string Can_Access_Crt = "Complaints Reporting Tool";
        public const string Can_Access_CRTList = "CRT List";
        public const string Can_Access_User = "User";
        public const string Can_Access_Crtadmin = "CRT Admin";
        public const string Can_Access_FieldLabels = "Field Label configuration";
        public const string Can_Access_Dropselect = "Drop-select configuration";
        public const string Can_Access_CrtReports = "CRT Portal reports";
        #endregion Role Permission List

        public const string StatusRejected = "Rejected";
        public const string StatusPending = "Pending";
        public const string AssoType_Registration = "Registration";

        public const string Create = "Create";
        public const string Update = "Update";
        public const string Delete = "Delete";

        public const string Web = "Web";


        public const string Ozone = "7eab4a1d-bac5-4640-b540-ba0ef406008e";
        public const string SME = "53b094a8-c6f8-4aae-ba6b-6a917b63b9aa";
        public const string IUMP = "f325c7b2-a8bf-4077-86d7-027e6f4730c4";
        public const string SitesPremises = "a7f16897-bd1a-4d72-99ee-5849c34c9eea";
        public const string RentalSupport = "9dd94c07-3f1a-4467-88ea-5e34f942b48c";
        public const string BSSP = "f8420029-c2ea-4730-bc6d-924a701b46c6";
        public const string SpecialIncentives = "41f9a7f6-6f9a-4356-8095-e4704ab11caa";
        public const string EquipmentAid = "50e7e37b-c434-4637-8455-8d53ae27d641";
        public const string ApplicationSubmitNotification = "New Ozone Quota Application({0}) is created. You can start reviewing it.";
        public const string QuotareviewNotification = "Application Review for Ozone Quota Application ({0}) is completed. You can start Technical Review.";
        public const string ImportSubmitNotification = "New Ozone Quota Import Certificate Application ({0}) is created. You can start reviewing it.";
        public const string IUMPSubmitNotification = "New IUMP Certificate Application ({0}) is created. You can start reviewing it.";
        public const string IndustrySubmitNotification = "New Ozone {0} Application ({1}) is created. You can start reviewing it.";

        public const string SMEApplicationSubmitNotification = "New SME Certificate({0}) is created. You can start reviewing it.";

        public const string DATACAPTURE = "DATACAPTURE";
        public const string APPLICATIONREVIEW = "APPLICATIONREVIEW";
        public const string TECHNICALREVIEW = "TECHNICALREVIEW";
        public const string APPROVED = "APPROVED";
        public const string EandM = "EandM";

        #region profileimage
       
        public const string ComplaintFilenotePath = "~/ComplaintFiles/";
        public const string OrganisationlogoPath = "~/Organisationlogos/";

        #endregion
    }
}