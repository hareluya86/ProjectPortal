<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePartners.aspx.cs" Inherits="ManagePartners" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:UpdatePanel ID="EntireManagePartnerPage" runat="server">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-2">
                        <div class="panel panel-primary">
                            <div class="panel-heading">Company Name</div>
                            <div class="panel-body" style="overflow: auto; height: 500px;">
                                <!--dynamically add datasource from codeBehind-->
                                <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="company_list_updatePanel">
                                    <ProgressTemplate>
                                        <div class="overlay">
                                            <img src="../Images/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                                <asp:UpdatePanel ID="company_list_updatePanel" runat="server">
                                    <ContentTemplate>
                                        <asp:Repeater runat="server" ID="company_list">
                                            <ItemTemplate>
                                                <asp:Button CssClass="btn truncate company-button"
                                                    runat="server" Text='<%# Eval("USERNAME") %>'
                                                    CommandArgument='<%# Eval("USERID") %>' OnClick="loadPartner" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-5 myforms">
                        <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="company_contacts_updatePanel">
                            <ProgressTemplate>
                                <div class="overlay">
                                    <img src="../Images/ajax-loader.gif" />
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <asp:UpdatePanel ID="company_contacts_updatePanel" runat="server">
                            <ContentTemplate>
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
                                            <asp:TextBox ID="phone" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group">
                                        <asp:Label AssociatedControlID="fax" Text="Fax: " runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                        <div class="col-sm-8">
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-lg-5">
                        Projects on Portal
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>


</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.truncate').each(function () {
                var button_text = $(this).val().trim();
                if (button_text.length > 12) {
                    $(this).val(button_text.substring(0, 12) + '...');
                }
            })
        })
    </script>
</asp:Content>
