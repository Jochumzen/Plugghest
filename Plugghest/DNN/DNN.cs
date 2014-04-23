using System.Runtime.InteropServices;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Definitions;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Tabs;
using DotNetNuke.Security.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.DNN
{

    public class DNNHelper
    {
        protected struct ModuleSettings
        {
            public string SettingName;
            public string SettingValue;

            public ModuleSettings(string name, string value)
            {
                SettingName = name;
                SettingValue = value;
            }
        }

        public void AddPage(string PageName, string PageUrl)
        {
            PortalSettings portalSettings = new PortalSettings();
            int portalId = portalSettings.PortalId;

            bool IsPluggPage = true;
            if (PageUrl[0] == 'C')
                IsPluggPage = false;


            // set skin
            string myPortalSkin;
            string myPortalContainer;
            if (IsPluggPage)
            {
                myPortalSkin = "[G]Skins/20047-UnlimitedColorPack-033/PluggPage.ascx";
                myPortalContainer = portalSettings.DefaultPortalContainer;
            }
            else
            {
                myPortalSkin = "[G]Skins/20047-UnlimitedColorPack-033/CoursePage.ascx";
                myPortalContainer = portalSettings.DefaultPortalContainer;
            }

            TabInfo newTab = new TabInfo();

            // set new page properties
            newTab.PortalID = portalId;
            newTab.TabName = PageName;
            newTab.Title = PageUrl;
            newTab.Description = "";
            newTab.KeyWords = "";
            newTab.IsDeleted = false;
            newTab.IsSuperTab = false;
            newTab.IsVisible = false;//for menu...
            newTab.DisableLink = false;
            newTab.IconFile = "";
            newTab.Url = "";
            newTab.SkinSrc = myPortalSkin;
            newTab.ContainerSrc = myPortalContainer;

            //Add permission to the page so that all users can view it
            foreach (PermissionInfo p in PermissionController.GetPermissionsByTab())
            {
                if (p.PermissionKey == "VIEW")
                {
                    TabPermissionInfo tpi = new TabPermissionInfo();
                    tpi.PermissionID = p.PermissionID;
                    tpi.PermissionKey = p.PermissionKey;
                    tpi.PermissionName = p.PermissionName;
                    tpi.AllowAccess = true;
                    tpi.RoleID = -1; //ID of all users
                    newTab.TabPermissions.Add(tpi);
                }
            }

            // create new page
            TabController controller = new TabController();
            int tabId = controller.AddTab(newTab, true);
            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(tabId);

            // temporary solution: Change Page URL to ID of Plugg/Course Page directly in DB
            TabUrl tu = new TabUrl();
            TabUrlController tc = new TabUrlController();
            tu.TabId = tabId;
            tu.SeqNum = 0;
            tu.Url = "/" + PageUrl;
            tu.QueryString = "";
            tu.HttpStatus = "200";
            tu.IsSystem = true;
            tu.PortalAliasUsage = 0;
            tu.CreatedByUserID = 1;
            tu.CreatedOnDate = DateTime.Now;
            tu.LastModifiedByUserID = 1;
            tu.LastModifiedOnDate = DateTime.Now;
            tc.CreateTabUrl(tu);

            // add modules to new page
            //int RnCRatingModuleId = 0;
            int RnCCommentModuleId = 0;
            if (IsPluggPage)
            {
                AddModuleToPage(portalId, tabId, "DisplayPlugg", "DisplayPlugg", "RowTwo_Grid8_Pane");

                AddModuleToPage(portalId, tabId, "CourseMenu", "CourseMenu", "RowTwo_Grid4_Pane");

                //RnCRatingModuleId = AddModuleToPage(portalId, tabId, "DNNCentric RnC", "DNNCentric.RatingAndComments", "RowTwo_Grid4_Pane");

                RnCCommentModuleId = AddModuleToPage(portalId, tabId, "DNNCentric RnC", "DNNCentric.RatingAndComments", "RowTwo_Grid8_Pane");
            }
            else
            {
                AddModuleToPage(portalId, tabId, "DisplayCourse", "DisplayCourse", "TopPane");
            }

            //// do settings for DNNCentric Rating
            //if (IsPluggPage & RnCRatingModuleId != 0)
            //{
            //    ModuleController m = new ModuleController();
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingCommentLength", "1000");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingCommentsPerPage", "10");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingCaptchaForAnonyComments", "TRUE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingDisplayedName", "DN");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingPersistComObjInSession", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingEnabledNotifications", "1,2,3,4,5,6,7");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingNewCommentNotificationForReplies", "TRUE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingCommentModRoleID", "1");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingOwnerRoleID", "1");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingCommentModeration", "ModNone");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingModUserID", "1");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingDisplayTemplate", "Classic");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingOwnerUserID", "1");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingAnonyComments", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingAnonyRatings", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingShowHideCommentPoint", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingShowHideNameEmailWebsite", "TRUE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingShowHideRatingPoints", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingShowHideViews", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingCommentObject", "tabid:174");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingTheme", "Smart");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingShow", "OnlyRatings");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingRatingChangeAllowed", "TRUE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingHTMLAllowed", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingHideCommentOnReport", "TRUE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingRoleAllowedPostingComment", "all");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingRoleAllowedToRate", "all");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingProfileLink", "/Activity-Feed/userId/[UserID]");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingProfileLinkAnonymous", "[WebSite]");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingLinkTarget", "_blank");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingProfileLinkNoFollow", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingShowProfileImage", "TRUE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingImageSourceType", "DNNProfileImage");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingDNNProfileImageWidth", "80");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingImageWidth", "80");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingImageMaxRated", "pg");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingCustomImageUrl", "");
            //    //Ugly way of building http://mywebsite/DesktopModules/DNNCentric-RatingAndComments/images/noProfile.jpg
            //    string s = DotNetNuke.Common.Globals.NavigateURL();
            //    s = s.Replace("http://", "");
            //    s = "http://" + s.Substring(0, s.IndexOf('/')) + DotNetNuke.Common.Globals.DesktopModulePath + "DNNCentric-RatingAndComments/images/noProfile.jpg";
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingDefaultImage", s);
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingRncWidth", "357");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingRncAlignment", "left");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingPermaLinkEnabled", "FALSE");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingSortingOrderValue", "5");
            //    m.UpdateModuleSetting(RnCRatingModuleId, "PRC_settingPostCommentsAnonymously", "FALSE");                   
            //}

            // do settings for DNNCentric Comments
            if (IsPluggPage & RnCCommentModuleId != 0)
            {
                ModuleController m = new ModuleController();
                m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingCommentLength", "1000");
                m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingCommentsPerPage", "10");
                m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingCaptchaForAnonyComments", "true");
                m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingDisplayedName", "U");
                m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingPersistComObjInSession", "false");
                m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingEnabledNotifications", "1,2,3,4,5,6,7");
                m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingNewCommentNotificationForReplies", "true");

                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingCommentModRoleID", "1");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingOwnerRoleID", "1");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingCommentModeration", "ModNone");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingModUserID", "1");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingDisplayTemplate", "Classic");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingOwnerUserID", "1");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingAnonyComments", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingAnonyRatings", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingShowHideCommentPoint", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingShowHideNameEmailWebsite", "TRUE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingShowHideRatingPoints", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingShowHideViews", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingCommentObject", "tabid:174");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingTheme", "Smart");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingShow", "OnlyComments");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingRatingChangeAllowed", "TRUE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingHTMLAllowed", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingHideCommentOnReport", "TRUE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingRoleAllowedPostingComment", "all");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingRoleAllowedToRate", "all");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingProfileLink", "/Activity-Feed/userId/[UserID]");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingProfileLinkAnonymous", "[WebSite]");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingLinkTarget", "_blank");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingProfileLinkNoFollow", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingShowProfileImage", "TRUE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingImageSourceType", "DNNProfileImage");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingDNNProfileImageWidth", "80");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingImageWidth", "80");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingImageMaxRated", "pg");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingCustomImageUrl", "");
                ////Ugly way of building http://mywebsite/DesktopModules/DNNCentric-RatingAndComments/images/noProfile.jpg
                //string s = DotNetNuke.Common.Globals.NavigateURL();
                //s = s.Replace("http://", "");
                //s = "http://" + s.Substring(0, s.IndexOf('/')) + DotNetNuke.Common.Globals.DesktopModulePath + "DNNCentric-RatingAndComments/images/noProfile.jpg";
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingDefaultImage", s);
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingRncWidth", "600");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingRncAlignment", "left");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingPermaLinkEnabled", "FALSE");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingSortingOrderValue", "5");
                //m.UpdateModuleSetting(RnCCommentModuleId, "PRC_settingPostCommentsAnonymously", "FALSE");
            }
        }

        public int AddModuleToPage(int portalId, int tabId, string DesktopModuleFriendlyName, string ModuleDefFriendlyName, string paneName)
        {
            int moduleId = 0;
            ModuleDefinitionInfo moduleDefinitionInfo = new ModuleDefinitionInfo();
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.PortalID = portalId;
            moduleInfo.TabID = tabId;
            moduleInfo.ModuleOrder = 1;
            moduleInfo.ModuleTitle = "";
            moduleInfo.PaneName = paneName;
            moduleInfo.DisplayPrint = false;
            moduleInfo.IsShareable = true;
            moduleInfo.IsShareableViewOnly = true;
            if (DesktopModuleFriendlyName == "DNNCentric.RatingAndComments")
                moduleInfo.Content = "DNNCentric.RatingAndComments";

            DesktopModuleInfo myModule = null;
            foreach (KeyValuePair<int, DesktopModuleInfo> kvp in DesktopModuleController.GetDesktopModules(portalId))
            {
                DesktopModuleInfo mod = kvp.Value;
                if (mod != null)
                    if (mod.FriendlyName == DesktopModuleFriendlyName)
                    {
                        myModule = mod;
                        break;
                    }
            }

            if (myModule != null)
            {
                var mc = new ModuleDefinitionController();
                var mInfo = new ModuleDefinitionInfo();
                mInfo = ModuleDefinitionController.GetModuleDefinitionByFriendlyName(ModuleDefFriendlyName,
                    myModule.DesktopModuleID);
                moduleInfo.ModuleDefID = mInfo.ModuleDefID;
                moduleInfo.CacheTime = moduleDefinitionInfo.DefaultCacheTime;//Default Cache Time is 0
                moduleInfo.InheritViewPermissions = true;  //Inherit View Permissions from Tab
                moduleInfo.AllTabs = false;
                moduleInfo.Alignment = "Top";

                ModuleController moduleController = new ModuleController();
                moduleId = moduleController.AddModule(moduleInfo);
            }

            //Clear Cache
            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(tabId);
            DotNetNuke.Common.Utilities.DataCache.ClearTabsCache(portalId);
            DotNetNuke.Common.Utilities.DataCache.ClearPortalCache(portalId, false);
            return moduleId;
        }
    }
}
