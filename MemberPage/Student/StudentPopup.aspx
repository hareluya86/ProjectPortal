<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StudentPopup.aspx.cs" Inherits="StudentPopup" MasterPageFile="~/PopupMaster.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-lg-12">
            <h2>Student Contacts</h2>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-2 col-sm-offset-1">
            <img src="../../Images/4286-kill-yourself.jpg" style="max-width: 150px" />
        </div>
        <div class="col-sm-6 col-sm-offset-2">
            <div class="row">
                <div class="form-group">
                    <asp:Label AssociatedControlID="student_id" Text="Student ID: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                    <asp:Label ID="student_id" AssociatedControlID="student_id" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <asp:Label AssociatedControlID="first_name" Text="First Name: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                    <asp:Label ID="first_name" AssociatedControlID="first_name" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <asp:Label AssociatedControlID="last_name" Text="Last Name: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                    <asp:Label ID="last_name" AssociatedControlID="last_name" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="form-group">
                    <asp:Label AssociatedControlID="email" Text="Email: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                    <asp:Label ID="email" AssociatedControlID="email" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-10 col-sm-offset-1">
            <div class="panel panel-success">
                <div class="panel-heading">Student Writeup</div>
                <div class="panel-body" style="overflow: auto; height: 125px;">
                    <asp:Literal ID="student_writeup" runat="server" Mode="Encode"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
