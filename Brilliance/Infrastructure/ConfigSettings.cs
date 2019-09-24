using System;
using System.Configuration;

namespace Brilliance.Infrastructure
{
    public static class ConfigSettings
    {
        public static readonly string BasePath = ConfigurationManager.AppSettings["BasePath"];
        public static readonly string HiqPdfSerialKey = Convert.ToString(ConfigurationManager.AppSettings["HiqPdfSerialKey"]);
        public static readonly int DBCommandTimeOut = Convert.ToInt32(ConfigurationManager.AppSettings["DBCommandTimeOut"]);
        public static readonly string Appreview = ConfigurationManager.AppSettings["AppReviewLink"];
        public static readonly string AppreviewForEAS = ConfigurationManager.AppSettings["AppReviewLinkForEAS"];
        public static readonly string AppReviewForImport = ConfigurationManager.AppSettings["AppReviewLinkForImport"];
        public static readonly string AppReviewForIUMP = ConfigurationManager.AppSettings["AppReviewLinkForIUMP"];
        public static readonly string AppReviewForSME = ConfigurationManager.AppSettings["AppReviewLinkForSME"];
        public static readonly string AppReviewForBSSP = ConfigurationManager.AppSettings["AppReviewLinkForBSSP"];
        public static readonly string AppReviewForRentalSubsidy = ConfigurationManager.AppSettings["AppReviewForRentalSubsidy"];
        public static readonly string AppReviewLinkForSpecialIncentives = ConfigurationManager.AppSettings["AppReviewLinkForSpecialIncentives"];
        public static readonly string AppReviewForIndustry = ConfigurationManager.AppSettings["AppReviewLinkForIndustry"];

        public static readonly string PageSize = ConfigurationManager.AppSettings["PageSize"];
        public static readonly bool EnableBundlingMinification = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableBundlingMinification"]);

        public static readonly string DidessLogo = ConfigurationManager.AppSettings["DidessLogo"];
    }
}