using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace CreatorAPI
{
    public class Constants
    {
        // Email Credentials
        public const string IverityEmail = "noreply@valinfo.co.za";
        public const string EmailPass = "ARd2WK4DJveKvzYB";

        //public const string IverityEmail = "postmaster@mail.valinfo.co.za";
        //public const string EmailPass = "73a6ca6f400dafc9d7dbdb730c4b91a7";

        // MIE Credentials
        public static string MieUsername = Convert.ToBoolean(ConfigurationManager.AppSettings["OnProduction"]) ? "valinfoint" : "valinfoint";
        public static string MiePassword = Convert.ToBoolean(ConfigurationManager.AppSettings["OnProduction"]) ? "valinfoint229" : "valinfoint127";
        public const string MieSource = "SMARTWEB";
        public const string MieRequestSource = "VALINFOI";
        public static string MieWcfUrl = Convert.ToBoolean(ConfigurationManager.AppSettings["OnProduction"]) ? "https://www.mie.co.za/secure/Services/epcvRequestWCF/epcvRequestWCF.svc" :
            "https://www.mie2.co.za/external/services/epcvRequestWCF/epcvRequestWCF.svc";

        // Defaults
        public const string DefaultPassword = "Prints123#"; // "P@ssWord123";
        public const string DefaultProfileImage = "/images/profile_default.png";
        public const string ExcelContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        // Settings
        public const string IPrintsPathSettingName = "SAPS69";
        public const string ImagePathSettingName = "IprintsImages";
        public const string SecretSettingName = "NotificationsSecret";
        public const string ManualBgScreeningSetting = "ManualBGScreeningOn";
        public const string CrimCheckSetting = "CrimCheckCost";
        public const string AppUrlSettingName = "AppUrl";
        public const string ManualBsEmailSetting = "ManualBSEmail";

        // Account lockout
        public const int AppLockoutTime = 5;
        public const int AppLockoutAttempts = 3;

        // App roles
        public const string AppRoleSuperAdmin = "Super-Admin";
        public const string AppRoleCoordinator = "Coordinator";
        public const string AppRoleClientAdmin = "Client-Admin";
        public const string AppRoleClerk = "Clerk";
    }
}