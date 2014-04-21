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
                myPortalSkin = "[G]Skins/20047-UnlimitedColorPack-033/InsidePage-leftmenu.ascx";
                myPortalContainer = portalSettings.DefaultPortalContainer;
            }
            else
            {
                myPortalSkin = portalSettings.DefaultPortalSkin;
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
            if (IsPluggPage)
            {
                AddModuleToPage(portalId, tabId, "DisplayPlugg");

                AddModuleToPage(portalId, tabId, "CourseMenu");
            }
            else
            {
                AddModuleToPage(portalId, tabId, "DisplayCourse");
            }
        
        }

        public void AddModuleToPage(int portalId, int tabId, string ModuleFriendlyName)
        {
            ModuleDefinitionInfo moduleDefinitionInfo = new ModuleDefinitionInfo();
            ModuleInfo moduleInfo = new ModuleInfo();
            moduleInfo.PortalID = portalId;
            moduleInfo.TabID = tabId;
            moduleInfo.ModuleOrder = 1;
            moduleInfo.ModuleTitle = "";
            moduleInfo.PaneName = "";
            moduleInfo.DisplayPrint = false;

            DesktopModuleInfo myModule = null;
            foreach (KeyValuePair<int, DesktopModuleInfo> kvp in DesktopModuleController.GetDesktopModules(portalId))
            {
                DesktopModuleInfo mod = kvp.Value;
                if (mod != null)
                    if (mod.FriendlyName == ModuleFriendlyName)
                    {
                        myModule = mod;
                        break;
                    }
            }

            if (myModule != null)
            {
                var mc = new ModuleDefinitionController();
                var mInfo = new ModuleDefinitionInfo();
                mInfo = ModuleDefinitionController.GetModuleDefinitionByFriendlyName(myModule.FriendlyName,
                    myModule.DesktopModuleID);
                moduleInfo.ModuleDefID = mInfo.ModuleDefID;
                moduleInfo.CacheTime = moduleDefinitionInfo.DefaultCacheTime;//Default Cache Time is 0
                moduleInfo.InheritViewPermissions = true;  //Inherit View Permissions from Tab
                moduleInfo.AllTabs = false;
                moduleInfo.Alignment = "Top";

                ModuleController moduleController = new ModuleController();
                int moduleId = moduleController.AddModule(moduleInfo);
            }

            //Clear Cache
            DotNetNuke.Common.Utilities.DataCache.ClearModuleCache(tabId);
            DotNetNuke.Common.Utilities.DataCache.ClearTabsCache(portalId);
            DotNetNuke.Common.Utilities.DataCache.ClearPortalCache(portalId, false);
        }
    }
}
