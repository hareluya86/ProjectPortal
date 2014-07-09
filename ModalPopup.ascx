<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalPopup.ascx.cs" Inherits="ModalPopup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<ajaxControl:ModalPopupExtender ID="mpext" runat="server"
    BackgroundCssClass="modalBackground"
    TargetControlID="pnlPopup" PopupControlID="pnlPopup">
</ajaxControl:ModalPopupExtender>

<asp:Panel ID="pnlPopup" runat="server"
    CssClass="modalPopup" Style="display: none;"
    DefaultButton="btnOk">
    <div class="row">
        <div class="col-sm-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
    </div>
    <div class="row">
        <div class="col-sm-12">
            <asp:Button ID="btnOk" runat="server"
                Text="Ok" OnClick="btnOk_Click" />
        </div>
    </div>
</asp:Panel>
<script type="text/javascript">
    function fnClickOK(sender, e) {
        __doPostBack(sender, e);
    }
</script>
