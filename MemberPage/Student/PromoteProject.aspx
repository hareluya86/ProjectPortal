<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromoteProject.aspx.cs" Inherits="PromoteProject" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-6 col-lg-offset-2">
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
                    <asp:Label AssociatedControlID="project_writeup" Text="Project Writeup: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="project_writeup" CssClass="form-control" runat="server" TextMode="MultiLine" Rows="10"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="form-group" style="margin-top: 5px;">
                    <asp:Label AssociatedControlID="website" Text="Website: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="website" CssClass="form-control" runat="server" TextMode="Url"></asp:TextBox>
                    </div>
                </div>
            </div>

        </div>
    </div>
    <div class="row">
        <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="UploadFilePanel">
            <ProgressTemplate>
                <div class="overlay">
                    <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UploadFilePanel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
            <ContentTemplate>
                <div class="col-lg-6 col-lg-offset-2">
                    <div class="row">
                        <div class="form-group" style="margin-top: 5px;">
                            <asp:Label AssociatedControlID="FileUploader" Text="Files to upload: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                            <div class="col-sm-8">
                                <div class="row">
                                    <div class="col-sm-8">
                                        <asp:FileUpload ID="FileUploader" runat="server" ToolTip="Files must be zipped and less than 50mb." />
                                    </div>
                                    <div class="col-sm-4">
                                        <asp:Button ID="UploadButton"
                                            Text="Upload file" OnClick="UploadButton_Click"
                                            runat="server"></asp:Button>
                                    </div>
                                </div>
                                <div class="row">
                                    <asp:RadioButton
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-4">
                    <asp:PlaceHolder ID="upload_message" runat="server"></asp:PlaceHolder>
                </div>
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
