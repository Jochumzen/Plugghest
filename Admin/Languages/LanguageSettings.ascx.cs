#region Copyright
// 
// DotNetNuke® - http://www.dotnetnuke.com
// Copyright (c) 2002-2014
// by DotNetNuke Corporation
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
#region Usings

using System;

using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;


#endregion

namespace DotNetNuke.Modules.Admin.Languages
{
    /// -----------------------------------------------------------------------------
    /// Project	 : DotNetNuke
    /// Class	 : LanguageSettings
    /// -----------------------------------------------------------------------------
    /// <summary>
    ///   Supplies LanguageSettings functionality for the Extensions module
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <history>
    ///   [cnurse]   04/03/2008    Created
    /// </history>
    /// -----------------------------------------------------------------------------
    public partial class LanguageSettings : ModuleSettingsBase
    {
        public override void UpdateSettings()
        {
            base.UpdateSettings();

            var modController = new ModuleController();
            modController.UpdateModuleSetting(ModuleContext.ModuleId, "UsePaging", chkUsePaging.Checked.ToString());
            modController.UpdateModuleSetting(ModuleContext.ModuleId, "PageSize", txtPageSize.Text);
            modController.UpdateModuleSetting(ModuleContext.ModuleId, "ShowLanguages", chkShowLanguages.Checked.ToString());
            modController.UpdateModuleSetting(ModuleContext.ModuleId, "ShowSettings", chkShowSettings.Checked.ToString());
        }

        public override void LoadSettings()
        {
            base.LoadSettings();

            valPageSize.MinimumValue = "1";
            valPageSize.MaximumValue = Int32.MaxValue.ToString();

            chkUsePaging.Checked = Convert.ToBoolean(ModuleContext.Settings["UsePaging"]);

            int _PageSize = 1000;
            //default page size
            if (Convert.ToInt32(ModuleContext.Settings["PageSize"]) == 0)
            {
                txtPageSize.Text = _PageSize.ToString();
            }
            else
            {
                txtPageSize.Text = Convert.ToString(ModuleContext.Settings["PageSize"]);
            }

            chkShowLanguages.Checked = Convert.ToBoolean(ModuleContext.Settings["ShowLanguages"]);
            chkShowSettings.Checked = Convert.ToBoolean(ModuleContext.Settings["ShowSettings"]);


        }
    }
}