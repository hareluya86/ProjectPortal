<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdateDetails.aspx.cs" Inherits="UpdateDetails" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="NewProjectUpdatePanel">
                <ProgressTemplate>
                    <div class="overlay">
                        <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <asp:UpdatePanel runat="server" ID="NewProjectUpdatePanel" UpdateMode="Conditional">
                <ContentTemplate>
                    <!--main info-->
                    <div class="row">
                        <!--Company Contacts-->
                        <div class="col-lg-6">
                            <div class="row">
                                <h2>Company Contacts</h2>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="company_name" Text="Company name: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="company_name" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="email" Text="Email: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="email" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="password" Text="Password: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="phone" Text="Phone: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="phone" CssClass="form-control" runat="server"></asp:TextBox>
                                        <ajaxControl:FilteredTextBoxExtender ID="projectContactNumber" runat="server"
                                            TargetControlID="phone"
                                            FilterType="Numbers" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="fax" Text="Fax: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="fax" CssClass="form-control" runat="server"></asp:TextBox>
                                        <ajaxControl:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                            TargetControlID="fax"
                                            FilterType="Numbers" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!--Company Address-->
                        <div class="col-lg-6">
                            <div class="row">
                                <h2>Company Address</h2>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="address1" Text="Address Line 1: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="address1" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="address2" Text="Address Line 2: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="address2" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="city_town" Text="City/Town: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="city_town" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="state" Text="State: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="state" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="zipcode" Text="Zip Code: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="zipcode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label AssociatedControlID="country" Text="Country: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                    <div class="col-sm-8">
                                        <asp:TextBox ID="country" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-2 col-lg-offset-9" style="margin-top: 5px; text-align: right;">
                            <asp:Button runat="server" ID="UpdateDetailsButton" CssClass="btn btn-primary"
                                Text="Update" OnClick="updateDetails" />
                            <asp:Button runat="server" ID="ResetButton" CssClass="btn btn-default"
                                Text="Reset" />
                        </div>
                    </div>
                    <ajaxControl:ModalPopupExtender ID="error_modal_control" runat="server"
                        PopupControlID="submit_new_project_error" TargetControlID="hiddenModalTarget"
                        OkControlID="okButton" BackgroundCssClass="overlay">
                    </ajaxControl:ModalPopupExtender>
                    <asp:HiddenField runat="server" ID="hiddenModalTarget" />
                    <asp:Panel runat="server" ID="submit_new_project_error">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Message</div>
                            <div class="panel-body" style="overflow: auto;">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:PlaceHolder runat="server" ID="error_message"></asp:PlaceHolder>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-2 col-sm-offset-9">
                                        <asp:Button runat="server" ID="okButton" CssClass="btn btn-default" Text="Ok" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                    <ajaxControl:ModalPopupExtender ID="password_popup" runat="server"
                        PopupControlID="PasswordPopup" TargetControlID="HiddenField1"
                        OkControlID="okButton" BackgroundCssClass="overlay">
                    </ajaxControl:ModalPopupExtender>
                    <asp:HiddenField runat="server" ID="HiddenField1" />
                    <asp:HiddenField runat="server" ID="hiddenPassword" />
                    <asp:Panel runat="server" ID="PasswordPopup">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Change Password</div>
                            <div class="panel-body" style="overflow: auto;">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:PlaceHolder runat="server" ID="ChangePasswordMessage"></asp:PlaceHolder>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="old_password" Text="Enter old password: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="old_password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="new_password" Text="Enter new password again: " runat="server" CssClass="col-sm-3 control-label"></asp:Label>
                                        <div class="col-sm-8">
                                            <asp:TextBox ID="new_password" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-4 col-sm-offset-8">
                                        <asp:Button runat="server" ID="Button1" CssClass="btn btn-default" Text="Confirm"
                                             OnClick="ConfirmPasswordChange" />
                                        <asp:Button runat="server" ID="Button2" CssClass="btn btn-default" Text="Cancel"
                                             OnClick="ClearFields" />
                                    </div>
                                </div>

                            </div>
                        </div>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <!--for toggling the project button-->
    <script id="toggle-project-button-script" type="text/javascript">

        $(document).on('click', ".category-button", function () {

            if ($(this).hasClass('btn-danger')) {
                $(this).removeClass('btn-danger');
            } else {

                $(this).addClass('btn-danger');
                $(this).next().attr('name', 'selected')
            }
            return false; //very important! if not the container will refresh!
        });
    </script>
    <!--for truncating longer category names-->
    <script id="truncate-button-script" type="text/javascript">
        $(document).ready(function () {
            $('.truncate').each(function () {
                var button_text = $(this).text().trim();

                if (button_text.length > 33) {
                    $(this).text(button_text.substring(0, 30) + '...');
                }
            })
        })
    </script>
</asp:Content>
