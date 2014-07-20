<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PromoteProject.aspx.cs" Inherits="PromoteProject" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    
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
