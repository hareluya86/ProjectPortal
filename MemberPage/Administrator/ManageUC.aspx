<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManageUC.aspx.cs" Inherits="ManageUC" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:UpdatePanel ID="EntireManagePartnerPage" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-sm-2">
                        <!--company list panel-->
                        <div class="panel panel-primary">
                            <div class="panel-heading">UC ID</div>
                            <div class="panel-body entity-left-panel" style="">
                                <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="uc_list_updatePanel">
                                    <ProgressTemplate>
                                        <div class="overlay">
                                            <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <!--dynamically add datasource from codeBehind-->
                                <asp:UpdatePanel ID="uc_list_updatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Repeater runat="server" ID="uc_list">
                                            <ItemTemplate>
                                                <asp:Button CssClass="btn truncate company-button"
                                                    runat="server" Text='<%# Eval("USER_ID") %>'
                                                    CommandArgument='<%# Eval("USER_ID") %>' OnClick="loadUC" />

                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-sm-2">
                        <asp:UpdateProgress runat="server" ID="UpdateProgress5" AssociatedUpdatePanelID="profile_pic_panel">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="profile_pic_panel" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                                <strong>UC ID: <asp:Literal ID="uc_id_holder" runat="server" ></asp:Literal></strong>
                                <asp:Image ID="profile_pic" runat="server" Width="150" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4 myforms">
                        <!--update company contacts panel-->
                        <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="uc_contacts_updatePanel">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="uc_contacts_updatePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:HiddenField ID="uc_id" runat="server" />
                                <div class="row">
                                    <h2>UC Contacts</h2>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="first_name" Text="First Name: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="first_name" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="last_name" Text="Last Name: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="last_name" CssClass="form-control" runat="server"></asp:TextBox>
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
                                    <h2>UC Address</h2>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="address1" Text="Address 1: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="address1" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="address2" Text="Address 2: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
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
                                        <div class="col-sm-2 col-sm-offset-9">
                                            <asp:Button runat="server" ID="UpdateStudentButton" OnClick="UpdateUCContacts"
                                                Text="Update" CssClass="btn btn-default" />
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
                    <div class="col-sm-4">
                        <div class="row">
                            <h2>Courses in Charge</h2>
                        </div>
                        <asp:UpdateProgress runat="server" ID="UpdateProgress2" AssociatedUpdatePanelID="course_list_panel">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel runat="server" ID="course_list_panel" UpdateMode="Conditional" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="well well-sm" style="height: 200px; overflow: auto;">

                                            <asp:Repeater runat="server" ID="course_list">
                                                <ItemTemplate>
                                                    <button class="btn btn-sm btn-default project-button" style="width: 100%; text-align: left; margin-bottom: 5px;">
                                                        <div class="row">
                                                            <div class="col-sm-6">
                                                                <%# Eval("COURSE_NAME") %>
                                                            </div>
                                                        </div>
                                                    </button>
                                                    <input type="hidden" runat="server"
                                                        value='<%# Eval("COURSE_ID") %>' />

                                                </ItemTemplate>
                                            </asp:Repeater>


                                        </div>
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    
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

        $(document).on('click', ".project-button-2", function () {

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
