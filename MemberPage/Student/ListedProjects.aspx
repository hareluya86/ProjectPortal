<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListedProjects.aspx.cs" Inherits="ListedProjects" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:UpdatePanel ID="EntireListedProjectsPage" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-3">
                        <!--company list panel-->
                        <div class="panel panel-primary">
                            <div class="panel-heading">Project Titles</div>
                            <div class="panel-body entity-left-panel" style="">
                                <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="project_titles_updatepanel">
                                    <ProgressTemplate>
                                        <div class="overlay">
                                            <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <!--dynamically add datasource from codeBehind-->
                                <asp:UpdatePanel ID="project_titles_updatepanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Repeater runat="server" ID="project_titles">
                                            <ItemTemplate>
                                                <asp:Button CssClass="btn truncate project-button"
                                                    runat="server" Text='<%# Eval("PROJECT_TITLE") %>'
                                                    ToolTip='<%# Eval("PROJECT_TITLE") %>' OnClick="loadProject"
                                                    CommandArgument='<%# Eval("PROJECT_ID") %>'  />

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-4 myforms">
                        <!--update company contacts panel-->
                        <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="project_updatePanel">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="project_updatePanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:HiddenField ID="company_id" runat="server" />
                                <div class="row">
                                    <h2><asp:Literal ID="project_title" runat="server" /></h2>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="company_name" Text="Company Name: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                        <asp:Label ID="company_name" AssociatedControlID="company_name" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="contact_name" Text="Contact Name: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                        <asp:Label ID="contact_name" AssociatedControlID="contact_name" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="contact_number" Text="Contact Name: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                        <asp:Label ID="contact_number" AssociatedControlID="contact_number" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="contact_email" Text="Contact Email: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                        <asp:Label ID="contact_email" AssociatedControlID="contact_email" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">Project Requirements</div>
                                        <div class="panel-body" style="overflow: auto; height:125px;">
                                            <asp:Literal ID="project_requirements" runat="server" Mode="Encode" ></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="panel panel-success">
                                        <div class="panel-heading">UC comments</div>
                                        <div class="panel-body" style="overflow: auto; height:125px;">
                                            <asp:Literal ID="uc_comments" runat="server" Mode="Encode" ></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                                <ajaxControl:ModalPopupExtender ID="error_modal_control" runat="server"
                                    PopupControlID="delete_projects_error" TargetControlID="hiddenModalTarget"
                                    OkControlID="okButton" BackgroundCssClass="overlay">
                                </ajaxControl:ModalPopupExtender>
                                <asp:HiddenField runat="server" ID="hiddenModalTarget" />
                                <asp:Panel runat="server" ID="delete_projects_error">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">Message</div>
                                        <div class="panel-body" style="overflow: auto;">
                                            <div class="row">
                                                <div class="col-sm-12">
                                                    <asp:PlaceHolder runat="server" ID="error_message"></asp:PlaceHolder>
                                                    <asp:Label runat="server" ID="error_label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-sm-2 col-sm-offset-9">
                                                    <asp:Button runat="server" ID="okButton" CssClass="btn btn-default" />
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-5 ">

                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <!--for truncating longer partner names-->
    <script id="truncate-button-script" type="text/javascript">
        $(document).ready(function () {
            $('.truncate').each(function () {
                var button_text = $(this).val().trim();
                if (button_text.length > 30) {
                    $(this).val(button_text.substring(0, 30) + '...');
                }
            })
        })
    </script>
    <!--for changing the selected button-->
    <script id="change-button-script" type="text/javascript">
        $(document).ready(function () {
            $('.project-button').click(function () {
                $('.project-button').each(function () {
                    $(this).removeClass('btn-primary');
                })
                $(this).addClass('btn-primary');
                //$('#company_list_updatePanel').block('<div class="overlay"><img src="~/Images/ajax-loader.gif" /></div>');
            })
            $('.project-button').ajaxComplete(function () {
                //$('#company_list_updatePanel').unblockUI();
            })
            return false;
        })
    </script>
    <!--for toggling the project button-->
    <script id="toggle-project-button-script" type="text/javascript">

        $(document).on('click', ".", function () {

            if ($(this).hasClass('btn-danger')) {
                $(this).removeClass('btn-danger');
            } else {

                $(this).addClass('btn-danger');
                $(this).next().attr('name', 'selected')
            }
            return false; //very important! if not the container will refresh!
        });
    </script>
    <script id="test-blocking" type="text/javascript">
        var uiId = '';

        function PageRequestManager_beginRequest(sender, args) {
            var postbackElem = args.get_postBackElement();
            uiId = postbackElem.id;
            postbackElem.disabled = true;
        }


        function PageRequestManager_endRequest(sender, args) {
            $get(uiId).disabled = false;
        }
    </script>
    
</asp:Content>
