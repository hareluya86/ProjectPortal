<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ManagePartners.aspx.cs" Inherits="ManagePartners" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div style="">
        <div class="row">
            <div class="col-md-3">
                <div class="panel panel-primary">
                    <div class="panel-heading">Company Name</div>
                    <div class="panel-body" style="overflow: auto">
                        <!--dynamically add datasource from codeBehind-->
                        <asp:Repeater runat="server" ID="company_list">
                            <ItemTemplate>
                                <button type="button" class="btn btn-default truncate" style="width: 100%; text-align: left; overflow: hidden;"
                                    value="">
                                    <%# Eval("USERNAME") %>
                                </button>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <div class="col-lg-4">
                <div class="panel panel-primary">
                    <div class="panel-heading">Company Contacts</div>
                    <div class="panel-body">
                        <div class="form-group">
                            <label for="company_name">Company Name: </label>
                            <asp:TextBox ID="company_name" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-5">
                This are their projects
            </div>
        </div>
    </div>
</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.truncate').each(function () {
                var button_text = $(this).text().trim();
                if (button_text.length > 20) {
                    $(this).text(button_text.substring(0, 20)+'...');
                }
            })
        })
    </script>
</asp:Content>
