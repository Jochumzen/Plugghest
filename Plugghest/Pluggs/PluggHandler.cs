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

        //P.J. I added this method. Remove this comment if you accept it.
        public Plugg GetPlugg(int pluggId)
        {
            return pluggcntr.GetPlugg(pluggId);
        }

        public Plugg AddNewPlugg(Plugg plug)
        {
            return pluggcntr.CreatePlug(plug);
        }

        public Boolean AddNewPluggContent(PluggContent plugcontent)
        {
            return pluggcntr.CreatePluggContent(plugcontent);
        }

        public List<Plugg> GetPluggList()
        {
            return pluggcntr.GetPluggRecords();
        }

        public List<PluggContent> GetPluggincontentList(int pluggId)
        {
            return pluggcntr.GetPluggincontents(pluggId);
        }

    }
}
