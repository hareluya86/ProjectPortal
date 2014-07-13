<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubmitNewProject.aspx.cs" Inherits="MemberPage_Partner_NewProject" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
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
                    <div class="col-lg-5">
                        <div class="row">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="project_title" Text="Project Title: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="project_title" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="contact_name" Text="Contact Name: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="contact_name" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="contact_num" Text="Contact Number: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="contact_num" CssClass="form-control" runat="server"></asp:TextBox>
                                    <ajaxControl:FilteredTextBoxExtender ID="projectContactNumber" runat="server"
                                        TargetControlID="contact_num"
                                        FilterType="Numbers" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="contact_email" Text="Contact Email: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                <div class="col-sm-8">
                                    <asp:TextBox ID="contact_email" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!--categories-->
                    <div class="col-lg-7">
                        <div class="row">
                            <div class="form-group">
                                <asp:Label Text="Categories: " CssClass="col-lg-2 control-label" runat="server" AssociatedControlID="category_list"></asp:Label>
                                <div class="col-lg-8">

                                    <div class="well well-sm" style="height: 200px; overflow: auto;">

                                        <asp:Repeater runat="server" ID="category_list">
                                            <ItemTemplate>
                                                <button class="btn btn-sm btn-default category-button col-sm-6 truncate" style="text-align: left; margin-bottom: 5px;"
                                                    title='<%# Eval("CATEGORY_NAME") %>'>
                                                    <%# Eval("CATEGORY_NAME") %>
                                                </button>
                                                <input type="hidden" runat="server"
                                                    value='<%# Eval("CATEGORY_ID") %>' />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-10">
                        <div class="row">
                            <div class="form-group">
                                <asp:Label AssociatedControlID="project_requirements" Text="Requirements:" runat="server" CssClass="col-sm-2 control-label"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="project_requirements" CssClass="form-control" runat="server" TextMode="MultiLine" Height="200"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-lg-2 col-lg-offset-8" style="margin-top: 5px; text-align: right;">
                        <asp:Button runat="server" ID="SubmitProjectButton" CssClass="btn btn-primary"
                            OnClick="SubmitProjectButton_Click" Text="Submit" />
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
