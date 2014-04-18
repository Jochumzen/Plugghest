using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Definitions;
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
        public void AddPage(int portalId, string PageName)
        {
            bool IsPluggPage = true;
            if (PageName[0] == 'C')
                IsPluggPage = false;

            TabInfo newTab = new TabInfo();

            // set new page properties
            newTab.PortalID = portalId;
            newTab.TabName = PageName;
            newTab.Title = PageName;
            newTab.Description = "";
            newTab.KeyWords = "";
            newTab.IsDeleted = false;
            newTab.IsSuperTab = false;
            newTab.IsVisible = false;//for menu...
            newTab.DisableLink = false;
            newTab.IconFile = "";
            newTab.Url = "";

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
