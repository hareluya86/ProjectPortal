<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePartners.aspx.cs" Inherits="ManagePartners" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:UpdatePanel ID="EntireManagePartnerPage" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-3"><!--company list panel-->
                        <div class="panel panel-primary">
                            <div class="panel-heading">Company Name</div>
                            <div class="panel-body" style="overflow: auto; height: 550px;">
                                <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="company_list_updatePanel"
                                    >
                                    <ProgressTemplate>
                                        <div class="overlay">
                                            <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <!--dynamically add datasource from codeBehind-->
                                <asp:UpdatePanel ID="company_list_updatePanel" runat="server" UpdateMode="Conditional" >
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="delete_project_button" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:Repeater runat="server" ID="company_list">
                                            <ItemTemplate>
                                                <asp:Button CssClass="btn truncate company-button"
                                                    runat="server" Text='<%# Eval("USERNAME") %>'
                                                    CommandArgument='<%# Eval("USER_ID") %>' OnClick="loadPartner" />

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5 myforms"><!--update company contacts panel-->
                        <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="company_contacts_updatePanel">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="company_contacts_updatePanel" runat="server" 
                            >
                            <ContentTemplate>
                                <asp:HiddenField ID="company_id" runat="server" />
                                <div class="row">
                                    <h2>Company Contacts</h2>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="company_name" Text="Company Name: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="company_name" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="company_reg_no" Text="Company Reg No: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <ajaxControl:FilteredTextBoxExtender ID="companyRegNoFilter" runat="server"
                                                            TargetControlID="company_reg_no" 
                                                            FilterType="Numbers" />
                                            <asp:TextBox ID="company_reg_no" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="email" Text="Email: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            
                                            <asp:TextBox ID="email" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="phone" Text="Phone: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <ajaxControl:FilteredTextBoxExtender ID="phoneFilter" runat="server"
                                                            TargetControlID="phone" 
                                                            FilterType="Numbers" />
                                            <asp:TextBox ID="phone" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="fax" Text="Fax: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <ajaxControl:FilteredTextBoxExtender ID="faxFilter" runat="server"
                                                            TargetControlID="fax" 
                                                            FilterType="Numbers" />
                                            <asp:TextBox ID="fax" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <h2>Company Address</h2>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="address1" Text="Address Line 1: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="address1" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="address2" Text="Address Line 2: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="address2" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="city_town" Text="City/Town: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="city_town" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="state" Text="State: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="state" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <ajaxControl:FilteredTextBoxExtender ID="zipcodeFilter" runat="server"
                                                            TargetControlID="zipcode" 
                                                            FilterType="Numbers" />
                                        <asp:Label AssociatedControlID="zipcode" Text="Zip Code: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="zipcode" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="country" Text="Country: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="country" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <div class="col-sm-2 col-sm-offset-10">
                                            <asp:Button runat="server" ID="UpdateCompanyButton" OnClick="UpdateCompanyContacts"
                                                Text="Update" CssClass="btn btn-default"  />
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-4">
                        <div class="row">
                            <h2>Projects on Portal</h2>
                        </div>
                        <asp:UpdateProgress runat="server" ID="UpdateProgress2" AssociatedUpdatePanelID="project_list_panel">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel runat="server" ID="project_list_panel" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="well well-sm" style="height: 300px; overflow:auto;">

                                            <asp:Repeater runat="server" ID="project_list">
                                                <ItemTemplate>
                                                    <button class="btn btn-sm btn-default project-button" style="width: 100%; text-align: left; margin-bottom: 5px;"
                                                        value="">
                                                        <%# Eval("PROJECT_TITLE") %>
                                                    </button>
                                                    <input type="hidden" runat="server"
                                                        value='<%# Eval("PROJECT_ID") %>' />
                                                </ItemTemplate>
                                            </asp:Repeater>


                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-lg-offset-8">
                                        <asp:Button ID="delete_project_button" runat="server" CssClass="btn btn-default" Text="Delete" OnClick="Delete_Projects" />
                                        <ajaxControl:ModalPopupExtender ID="error_modal_control" runat="server"
                                            PopupControlID="delete_projects_error" TargetControlID="hiddenModalTarget"
                                            CancelControlID="cancelButton" BackgroundCssClass="overlay">
                                        </ajaxControl:ModalPopupExtender>
                                        <asp:HiddenField runat="server" ID="hiddenModalTarget" />
                                        <asp:Panel runat="server" ID="delete_projects_error">
                                            <div class="panel panel-primary">
                                                <div class="panel-heading">Message</div>
                                                <div class="panel-body" style="overflow: auto;">
                                                    <asp:PlaceHolder runat="server" ID="error_message"></asp:PlaceHolder>
                                                    <asp:Button runat="server" ID="cancelButton" />
                                                </div>
                                            </div>

                                        </asp:Panel>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
                if (button_text.length > 12) {
                    $(this).val(button_text.substring(0, 12) + '...');
                }
            })
        })
    </script>
    <!--for changing the selected button-->
    <script id="change-button-script" type="text/javascript">
        $(document).ready(function () {
            $('.company-button').click(function () {
                $('.company-button').each(function () {
                    $(this).removeClass('btn-primary');
                })
                $(this).addClass('btn-primary');
                //$('#company_list_updatePanel').block('<div class="overlay"><img src="~/Images/ajax-loader.gif" /></div>');
            })
            $('.company-button').ajaxComplete(function () {
                //$('#company_list_updatePanel').unblockUI();
            })
            return false;
        })
    </script>
    <!--for toggling the project button-->
    <script id="toggle-project-button-script" type="text/javascript">

        $(document).on('click', ".project-button", function () {

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
