<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePartners.aspx.cs" Inherits="ManagePartners" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-3">
            <div class="panel panel-primary">
                <div class="panel-heading">Companies</div>
                <div class="panel-body">
                    <asp:ObjectDataSource ID="companies" runat="server" />
                    <asp:Repeater runat="server" ID="company_list" DataSourceId="companies" >
                        <ItemTemplate>
                            <button type="button" class="btn btn-default">
                                <%# Eval("USERNAME") %>
                            </button>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-lg-4">
                This is the partner details
            </div>
            <div class="col-lg-5">
                This are their projects
            </div>
        </div>
</asp:Content>
