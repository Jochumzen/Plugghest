using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Pluggs
{
   public class PluggHandler
    {
        PluggController plugcntr = new PluggController();
        

        public Plugg AddNewPlugg(Plugg plug)
        {
            return plugcntr.CreatePlug(plug);
        }

        public Boolean AddNewPluggContent(PluggContent plugcontent)
        {
            return plugcntr.CreatePluggContent(plugcontent);
        }

        public List<Plugg> GetPluggList()
        {
            return plugcntr.GetPluggRecords();
        }

        public List<PluggContent> GetPluggincontentList(int PluggId)
        {
            return plugcntr.GetPluggincontents(PluggId);
        }

    }
}
