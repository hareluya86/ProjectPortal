<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoginPage.aspx.cs" Inherits="LoginPage" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-lg-6 col-lg-offset-3">
            <div class="panel panel-default">
                <div class="panel-heading">User Authentication</div>
                <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="AdminLoginPanel">
                    <ProgressTemplate>
                        <div class="overlay">
                            <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <asp:UpdatePanel ID="AdminLoginPanel" runat="server">
                    <ContentTemplate>
                        <div class="panel-body">
                            <div class="row">
                                <asp:PlaceHolder ID="login_message" runat="server"></asp:PlaceHolder>
                            </div>
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label Text="User ID: " runat="server" CssClass="col-sm-4 col-lg-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="input_userid" runat="server" CssClass="form-control"></asp:TextBox>
                                        <ajaxControl:FilteredTextBoxExtender ID="FilterUserid" runat="server"
                                            TargetControlID="input_userid"
                                            FilterType="Numbers" />
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label Text="Password: " runat="server" CssClass="col-sm-4 col-lg-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox ID="input_password" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-lg-6 col-lg-offset-5" style="text-align: right;">
                                    <asp:Button ID="login_button" Text="Login" runat="server" CssClass="btn btn-default"
                                        OnClick="Login" />
                                    <asp:Button ID="clear_button" Text="Clear" runat="server" CssClass="btn btn-default"
                                        OnClick="ClearLogin" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>

                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>

