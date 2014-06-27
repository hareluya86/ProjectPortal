<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administrator.aspx.cs" Inherits="Administrator" MasterPageFile="~/MasterPage.master" %>

<asp:Content ContentPlaceHolderID="head" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" >
    
    <asp:MultiView ID="AdminMultiView" runat="server" >
        <asp:View ID="login" runat="server">
            <div class="row">
                <div class="col-lg-6 col-lg-offset-3">
                    <div class="panel panel-default">
                        <div class="panel-heading">Administrator Authentication</div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="form-group">
                                    <asp:Label Text="Admin ID: " runat="server" CssClass="col-sm-4 col-lg-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="form-group">
                                    <asp:Label Text="Admin Password: " runat="server" CssClass="col-sm-4 col-lg-offset-1 control-label"></asp:Label>
                                    <div class="col-sm-6">
                                        <asp:TextBox runat="server" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px;">
                                <div class="col-lg-6 col-lg-offset-5" style="text-align: right;">
                                    <asp:Button ID="login_button" Text="Login" runat="server" />
                                    <asp:Button ID="clear_button" Text="Clear" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>

