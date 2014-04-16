using DotNetNuke.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Plugghest.Pluggs
{
   public class PluggHandler
    {
        PluggController pluggcntr = new PluggController();


        public Plugg GetPlugg(int pluggId)
        {
            return pluggcntr.GetPlugg(pluggId);
        }

        public int AddNewPlugg(Plugg plug)
        {
            return pluggcntr.CreatePlug(plug);
        }

        public Boolean AddNewPluggContent(PluggContent plugcontent)
        {
            return pluggcntr.CreatePluggContent(plugcontent);
        }

        public List<PluggInfoForDNNGrid> GetPluggList()
        {
            return pluggcntr.GetPluggRecords();
        }

        public List<PluggContent> GetPluggincontentList(int pluggId)
        {
            return pluggcntr.GetPluggincontents(pluggId);
        }

    }
}
