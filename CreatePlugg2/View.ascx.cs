/*
' Copyright (c) 2014  Christoc.com
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using System;
using System.Web.UI.WebControls;
using Christoc.Modules.CreatePlugg2.Components;
using DotNetNuke.Security;
using DotNetNuke.Services.Exceptions;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Services.Localization;
using DotNetNuke.UI.Utilities;
using Plugghest.Base2;
using System.Text;
using Plugghest.Subjects;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace Christoc.Modules.CreatePlugg2
{
    /// -----------------------------------------------------------------------------
    /// <summary>
    /// The View class displays the content
    /// 
    /// Typically your view control would be used to display content or functionality in your module.
    /// 
    /// View may be the only control you have in your project depending on the complexity of your module
    /// 
    /// Because the control inherits from CreatePlugg2ModuleBase you have access to any custom properties
    /// defined there, as well as properties from DNN such as PortalId, ModuleId, TabId, UserId and many more.
    /// 
    /// </summary>
    /// -----------------------------------------------------------------------------
    public partial class View : CreatePlugg2ModuleBase, IActionable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {

                    StringBuilder strStaticListHtml1 = new StringBuilder(@" <ul class='sortable-list'>
                                                                         <ul class='sortable-list ui-sortable'>");
                    foreach (string name in Enum.GetNames(typeof(EComponentType)))
                    {
                        if (name != "NotSet")
                        {
                            strStaticListHtml1.Append("<li style='' class='sortable-item' id='" + name + "'>" + name + "<span class='del-spn' onclick='RemoveCon(this)'>x</span></li>");
                        }
                    }
                    strStaticListHtml1.Append("</ul></ul>");
                    hdStaticListHTML.Value = strStaticListHtml1.ToString();
                    BindTree();


                    string strRichRichTextHTML = CreateRichRichTextHTMLString();



                }

            }
            catch (Exception exc) //Module failed to load
            {
                Exceptions.ProcessModuleLoadException(this, exc);
            }
        }

        private static string CreateRichRichTextHTMLString()
        {
            string strRichRichTextHTML = @"<div class='dnnForm dnnTextEditor dnnClear'>
    
  
    <div style='width:100%;' class='dnnTextPanel' id='dnn_ctr903_View_txtHtmlText_PanelTextEditor'>
		
        <div style='width:100%;' class='dnnTextPanelView' id='dnn_ctr903_View_txtHtmlText_PanelView'>
			
            <span id='dnn_ctr903_View_txtHtmlText_OptView'><input type='radio' onclick='javascript:setTimeout('__doPostBack(\'dnn$ctr903$View$txtHtmlText$OptView$0\',\'\')', 0)' value='BASIC' name='dnn$ctr903$View$txtHtmlText$OptView' id='dnn_ctr903_View_txtHtmlText_OptView_0' style='position: absolute; z-index: -1; opacity: 0;'><span class='dnnRadiobutton'><span class='mark'><img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAMAAAAoyzS7AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAAZQTFRFAAAAAAAApWe5zwAAAAF0Uk5TAEDm2GYAAAAMSURBVHjaYmAACDAAAAIAAU9tWeEAAAAASUVORK5CYII='></span></span><label for='dnn_ctr903_View_txtHtmlText_OptView_0' class='dnnBoxLabel'>Basic Text Box</label><input type='radio' checked='checked' value='RICH' name='dnn$ctr903$View$txtHtmlText$OptView' id='dnn_ctr903_View_txtHtmlText_OptView_1' style='position: absolute; z-index: -1; opacity: 0;'><span class='dnnRadiobutton dnnRadiobutton-checked'><span class='mark'><img src='data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAMAAAAoyzS7AAAAGXRFWHRTb2Z0d2FyZQBBZG9iZSBJbWFnZVJlYWR5ccllPAAAAAZQTFRFAAAAAAAApWe5zwAAAAF0Uk5TAEDm2GYAAAAMSURBVHjaYmAACDAAAAIAAU9tWeEAAAAASUVORK5CYII='></span></span><label for='dnn_ctr903_View_txtHtmlText_OptView_1' class='dnnBoxLabel'>Rich Text Editor</label></span>
            
<div class='dnnLabel' style='position: relative;'>    
    <label>
        <span class='dnnFormLabelNoFloat' id='dnn_ctr903_View_txtHtmlText_plView_lblLabel'>Editor:</span>   
    </label>
    <a href='javascript:__doPostBack('dnn$ctr903$View$txtHtmlText$plView$cmdHelp','')' class='dnnFormHelp' tabindex='-1' id='dnn_ctr903_View_txtHtmlText_plView_cmdHelp'></a>
    <div class='dnnTooltip' id='dnn_ctr903_View_txtHtmlText_plView_pnlHelp' style='position: absolute; right: -29%;'>
				
        <div class='dnnFormHelpContent dnnClear'>
            <span class='dnnHelpText' id='dnn_ctr903_View_txtHtmlText_plView_lblHelp'>Select the editor to use</span>
            <a class='pinHelp' href='#'></a>
       </div>   
    
			</div>
</div>

                
        
		</div>    
        
        <div class='dnnFormItem' id='dnn_ctr903_View_txtHtmlText_DivRichTextBox'>
            <div>
			<div style='border-width: 0px; height: 400px; width: 100%; min-height: 400px; min-width: 940px;' class='RadEditor Black reWrapper' id='dnn_ctr903_View_txtHtmlText_txtHtmlText'>
				<!-- 2013.2.717.40 --><div style='display:none;' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_dialogOpener'>
					<div style='display:none;' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_dialogOpener_Window'>
						<div style='display:none;' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_dialogOpener_Window_C'>

						</div><input type='hidden' name='dnn_ctr903_View_txtHtmlText_txtHtmlText_dialogOpener_Window_ClientState' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_dialogOpener_Window_ClientState' autocomplete='off'>
					</div><input type='hidden' name='dnn_ctr903_View_txtHtmlText_txtHtmlText_dialogOpener_ClientState' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_dialogOpener_ClientState' autocomplete='off'>
				</div><div class='reRibbonBarWrapper'></div><table cellspacing='0' cellpadding='0' style='table-layout:auto;width:100%;height:400px;' id='dnn_ctr903_View_txtHtmlText_txtHtmlTextWrapper'>
					<tbody>
						<tr>
							<td class='reWrapper_corner reCorner_top_left'>&nbsp;</td><td colspan='3' class='reWrapper_center reCenter_top'>&nbsp;</td><td class='reWrapper_corner reCorner_top_right'>&nbsp;</td>
						</tr><tr>
							<td rowspan='4' class='reLeftVerticalSide'>&nbsp;</td><td class='reTlbVertical' id='dnn_ctr903_View_txtHtmlText_txtHtmlTextLeft' rowspan='4'></td><td style='width:100%;' class='reToolCell' id='dnn_ctr903_View_txtHtmlText_txtHtmlTextTop'><div class='Black reToolbarWrapper'>
								<ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='AJAX Spellchecker'><span class='AjaxSpellCheck' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Find And Replace (CTRL+F)'><span class='FindAndReplace' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Paste Options'><span class='PasteStrip' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='reSeparator ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Undo (CTRL+Z)'><span class='Undo' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Redo (CTRL+Y)'><span class='Redo' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Insert Media'><span class='InsertOptions' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Save Template'><span class='TemplateOptions' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='reSeparator ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Hyperlink Manager (CTRL+K)'><span class='LinkManager' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Remove Link (CTRL+SHIFT+K)'><span class='Unlink' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reDropdown' title='' unselectable='on' data-original-title='Custom Links (CTRL+ALT+K)'><span style='width:80px;' class='InsertCustomLink' unselectable='on'>Custom Links</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Insert Symbol'><span class='InsertSymbol' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Insert Table'><span class='InsertTable' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='New Paragraph'><span class='InsertParagraph' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Toggle Full Screen Mode (F11)'><span class='ToggleScreenMode' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Insert Date'><span class='InsertDate' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Insert Time'><span class='InsertTime' unselectable='on'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Bold (CTRL+B)'><span class='Bold' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Italic (CTRL+I)'><span class='Italic' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Underline (CTRL+U)'><span class='Underline' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Strikethrough'><span class='StrikeThrough' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='SuperScript'><span class='Superscript' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Subscript'><span class='Subscript' unselectable='on'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Indent'><span class='Indent' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Outdent'><span class='Outdent' unselectable='on'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Numbered List'><span class='InsertOrderedList' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Bullet List'><span class='InsertUnorderedList' unselectable='on'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Convert to lower case'><span class='ConvertToLower' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Convert to upper case'><span class='ConvertToUpper' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Horizontal Rule'><span class='InsertHorizontalRule' unselectable='on'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Foreground Color'><span class='ForeColor' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Background Color'><span class='BackColor' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reDropdown' title='' unselectable='on' data-original-title='Font Name'><span style='width:80px;' class='FontName' unselectable='on'>Font Name</span></a></li><li class='ui-corner-left'><a href='#' class='reDropdown' title='' unselectable='on' data-original-title='Size'><span style='width:21px;' class='FontSize' unselectable='on'>Size</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Align Left'><span class='JustifyLeft' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Align Center'><span class='JustifyCenter' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Align Right'><span class='JustifyRight' unselectable='on'>&nbsp;</span></a></li><li class='ui-corner-left'><a href='#' class='reTool' title='' unselectable='on' data-original-title='Justify'><span class='JustifyFull' unselectable='on'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul><ul class='reToolbar Black'>
									<li class='reGrip grip_first ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reDropdown' title='' unselectable='on' data-original-title='Paragraph Style'><span style='width:80px;' class='FormatBlock' unselectable='on'>Paragraph Style</span></a></li><li class='reSeparator ui-corner-left'>&nbsp;</li><li class='ui-corner-left'><a href='#' class='reDropdown' title='' unselectable='on' data-original-title='Apply CSS Class'><span style='width:80px;' class='ApplyClass' unselectable='on'>Apply CSS Class</span></a></li><li class='ui-corner-left'><a href='#' class='reTool reSplitButton' title='' unselectable='on' data-original-title='Format Stripper'><span class='FormatStripper' unselectable='on'>&nbsp;</span><span class='split_arrow'>&nbsp;</span></a></li><li class='reGrip grip_last ui-corner-left'>&nbsp;</li>
								</ul>
							</div></td><td class='reTlbVertical' id='dnn_ctr903_View_txtHtmlText_txtHtmlTextRight' rowspan='4'></td><td class='reRightVerticalSide' rowspan='4'>&nbsp;</td>
						</tr><tr>
							<td valign='top' style='height:100%;' class='reContentCell' id='dnn_ctr903_View_txtHtmlText_txtHtmlTextCenter'><label style='display:none;' for='dnn_ctr903_View_txtHtmlText_txtHtmlTextContentHiddenTextarea'>RadEditor hidden textarea</label><textarea style='display:none;' cols='20' rows='4' name='dnn$ctr903$View$txtHtmlText$txtHtmlText' id='dnn_ctr903_View_txtHtmlText_txtHtmlTextContentHiddenTextarea'></textarea><iframe frameborder='0' src='javascript:'&lt;html&gt;&lt;/html&gt;';' style='width: 100%; height: 100%; margin: 0px; padding: 0px;' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_contentIframe' title='Rich text editor with ID dnn_ctr903_View_txtHtmlText_txtHtmlText'>Your browser does not support inline frames or is currently configured not to display inline frames.</iframe></td>
						</tr><tr>
							<td class='reToolZone'><table cellspacing='0' cellpadding='0' style='width: 100%;' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_BottomTable'>
								<tbody>
									<tr>
										<td class='reEditorModesCell'><div id='dnn_ctr903_View_txtHtmlText_txtHtmlText_ModesWrapper' class='reEditorModes'>
											<ul>
												<li class='ui-corner-left'><a class='reMode_design reMode_selected' title='' href='javascript:void(0);' data-original-title='Design'><span>Design</span></a></li><li class='ui-corner-left'><a class='reMode_html' title='' href='javascript:void(0);' data-original-title='HTML'><span>HTML</span></a></li><li class='ui-corner-left'><a class='reMode_preview' title='' href='javascript:void(0);' data-original-title='Preview'><span>Preview</span></a></li>
											</ul>
										</div></td><td id='dnn_ctr903_View_txtHtmlText_txtHtmlTextBottom' class='reBottomZone'><div class='reModule'><span style='line-height:22px'>Words: 0 &nbsp;&nbsp;Characters: 0&nbsp;</span></div></td><td valign='bottom' align='right' style='width:15px;' class='reResizeCell'><div id='dnn_ctr903_View_txtHtmlText_txtHtmlTextBottomResizer' style='cursor: se-resize;'>
											&nbsp;
										</div></td>
									</tr>
								</tbody>
							</table><noscript>
								&lt;p&gt;RadEditor - please enable JavaScript to use the rich text editor.&lt;/p&gt;
							</noscript></td>
						</tr><tr>
							<td class='reToolZone' id='dnn_ctr903_View_txtHtmlText_txtHtmlTextModule' style=''></td>
						</tr><tr>
							<td class='reWrapper_corner reCorner_bottom_left'>&nbsp;</td><td colspan='3' class='reWrapper_center reCenter_bottom'>&nbsp;</td><td class='reWrapper_corner reCorner_bottom_right'>&nbsp;</td>
						</tr>
					</tbody>
				</table><input type='hidden' name='dnn_ctr903_View_txtHtmlText_txtHtmlText_ClientState' id='dnn_ctr903_View_txtHtmlText_txtHtmlText_ClientState' autocomplete='off'>
			</div>
		</div>
        </div>
    
	</div>
</div>";
            return strRichRichTextHTML;
        }

        private static string CreateRichTextHTMLString()
        {
            string strRichTextHTML = @"   <div class='container'>
                <div class='hero-unit'>

                    <div id='alerts'></div>
                    <div class='btn-toolbar' data-role='editor-toolbar' data-target='#editor'>


                        <div class='btn-group'>
                            <a class='btn' data-edit='bold' title='Bold (Ctrl/Cmd+B)'><i class='icon-bold'></i></a>
                            <a class='btn' data-edit='italic' title='Italic (Ctrl/Cmd+I)'><i class='icon-italic'></i></a>

                        </div>
                        <div class='btn-group'>
                            <a class='btn' data-edit='insertunorderedlist' title='Bullet list'><i class='icon-list-ul'></i></a>
                            <a class='btn' data-edit='insertorderedlist' title='Number list'><i class='icon-list-ol'></i></a>

                        </div>

                        <div class='btn-group'>
                            <a class='btn dropdown-toggle' data-toggle='dropdown' title='Hyperlink'><i class='icon-link'></i></a>
                            <div class='dropdown-menu input-append'>
                                <input class='span2' placeholder='URL' type='text' data-edit='createLink' />
                                <button class='btn' type='button'>Add</button>
                            </div>
                            <a class='btn' data-edit='unlink' title='Remove Hyperlink'><i class='icon-cut'></i></a>

                        </div>


                        <div class='btn-group'>
                            <a class='btn' data-edit='undo' title='Undo (Ctrl/Cmd+Z)'><i class='icon-undo'></i></a>
                            <a class='btn' data-edit='redo' title='Redo (Ctrl/Cmd+Y)'><i class='icon-repeat'></i></a>
                        </div>

                    </div>

                    <div id='editor'>
                        Go ahead&hellip;
                    </div>
                    <br />

                </div>



            </div>";
            return strRichTextHTML;
        }
        public void BindTree()
        {
            SubjectHandler objsubhandler = new SubjectHandler();

            var tree = objsubhandler.GetSubjectsAsTree();
            JavaScriptSerializer TheSerializer = new JavaScriptSerializer();
            hdnTreeData.Value = TheSerializer.Serialize(tree);
        }


        public ModuleActionCollection ModuleActions
        {
            get
            {
                var actions = new ModuleActionCollection
                    {
                        {
                            GetNextActionID(), Localization.GetString("EditModule", LocalResourceFile), "", "", "",
                            EditUrl(), false, SecurityAccessLevel.Edit, true, false
                        }
                    };
                return actions;
            }
        }




        protected void btnSaveTitle_Click(object sender, EventArgs e)
        {
            BaseHandler bh = new BaseHandler();
            PluggContainer pc = new PluggContainer(new Localization().CurrentCulture);
            pc.ThePlugg.CreatedByUserId = this.UserId;
            pc.ThePlugg.ModifiedByUserId = this.UserId;
            pc.ThePlugg.PluggId = 0;
            pc.ThePlugg.WhoCanEdit = (EWhoCanEdit)Enum.Parse(typeof(EWhoCanEdit), rdEditPlug.SelectedValue);
            pc.SetTitle(txtTitle.Text);
            pc.SetDescription(txtDescription.Text);

            List<object> cmpData = new List<object>();


            foreach (string StrCmpData in hdcmpData.Value.Split(new string[] { "$#%#$%" }, StringSplitOptions.RemoveEmptyEntries))
            {
                string[] straCmpData = StrCmpData.Split(new string[] { "$$$&$$$" }, StringSplitOptions.RemoveEmptyEntries);

                switch (straCmpData[0])
                {
                    case "RichRichText":
                        PHText RichRichText = new PHText();
                        RichRichText.Text = straCmpData[1];
                        RichRichText.ItemType = ETextItemType.PluggComponentRichRichText;
                        cmpData.Add(RichRichText);
                        break;
                    case "RichText":
                        PHText RichText = new PHText();
                        RichText.Text = straCmpData[1];
                        RichText.ItemType = ETextItemType.PluggComponentRichText;
                        cmpData.Add(RichText);
                        break;
                    case "Label":
                        PHText Label = new PHText();
                        Label.Text = straCmpData[1];
                        Label.ItemType = ETextItemType.PluggComponentLabel;
                        cmpData.Add(Label);
                        break;
                    case "Latex":
                        PHLatex Latex = new PHLatex(straCmpData[1], new Localization().CurrentCulture,ELatexItemType.PluggComponentLatex);
                        cmpData.Add(Latex);
                        break;
                    case "YouTube":
                        Plugghest.Base2.YouTube objYouTube = new Plugghest.Base2.YouTube();
                        string[] strYoutubeval = straCmpData[1].Split(new string[] { "&&&$$&&&" }, StringSplitOptions.RemoveEmptyEntries);
                        objYouTube.YouTubeAuthor = strYoutubeval[3];
                        objYouTube.YouTubeCode = strYoutubeval[2];
                        objYouTube.YouTubeComment = strYoutubeval[5];
                        objYouTube.YouTubeCreatedOn = Convert.ToDateTime(strYoutubeval[4]);
                        objYouTube.YouTubeDuration = Convert.ToInt32(strYoutubeval[1]);
                        objYouTube.YouTubeTitle = strYoutubeval[0];
                        cmpData.Add(objYouTube);
                        break;
                }

            }
            bh.SavePlugg(pc, cmpData);
        }
    }
}