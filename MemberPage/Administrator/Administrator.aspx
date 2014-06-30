<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Administrator.aspx.cs" Inherits="Administrator" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <ajaxControl:TabContainer runat="server">
        <ajaxControl:TabPanel HeaderText="Partner Details" runat="server">
            <ContentTemplate>
                <h2>This is where you manage the partners</h2>
            </ContentTemplate>
        </ajaxControl:TabPanel>
    </ajaxControl:TabContainer>
</asp:Content>
