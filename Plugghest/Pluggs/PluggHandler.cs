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

       //Plugg

       public Plugg GetPlugg(int pluggId)
       {
           return pluggcntr.GetPlugg(pluggId);
       }

       public IEnumerable<Plugg> GetAllPluggs()
       {
           return pluggcntr.GetAllPluggs();
       }

       public void CreatePlugg(Plugg p)
       {
           pluggcntr.CreatePlugg(p);
       }

       public void UpdatePlugg(Plugg p)
       {
           pluggcntr.UpdatePlugg(p);
       }

       public void DeleteAllPluggs()
       {
           pluggcntr.DeleteAllPluggs();
       }

       //PluggContent

       public void CreatePluggContent(PluggContent plugcontent)
       {
           pluggcntr.CreatePluggContent(plugcontent);
       }

       public PluggContent GetPluggContent(int pluggId, string cultureCode)
       {
           return pluggcntr.GetPluggContent(pluggId, cultureCode);
       }

       public void UpdatePluggContent(PluggContent pc)
       {
           pluggcntr.UpdatePluggContent(pc);
       }
       
       public List<PluggContent> GetPluggContentList(int pluggId)
       {
           return pluggcntr.GetPluggContent(pluggId);
       }

       public void DeleteAllPluggContent()
       {
           pluggcntr.DeleteAllPluggContent();
       }

       //PluggForDNN

       public List<PluggInfoForDNNGrid> GetPluggListForGrid()
       {
           return pluggcntr.GetPluggRecords();
       }
       



    }
}
