    <%@ Page Language="C#" AutoEventWireup="true" CodeFile="TestHibernate.aspx.cs" Inherits="Tests_Default" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>

<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <ajaxControl:ToolkitScriptManager ID="ScriptManager1" runat="server" />

            <ajaxControl:TabContainer runat="server">
                <ajaxControl:TabPanel runat="server" HeaderText="Hibernate setup">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-8">
                                <div class="panel panel-default">
                                    <div class="panel-heading">For testing DB connection and build schema</div>
                                    <div class="panel-body">
                                        <!--leave this for the moment-->
                                        <asp:UpdateProgress runat="server" ID="PageUpdateProgress" AssociatedUpdatePanelID="GenerateSchemaPanel">
                                            <ProgressTemplate>
                                                <h>Loading...</h>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="GenerateSchemaPanel" runat="server">
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="GenerateSchemaButton" EventName="Click" />
                                            </Triggers>
                                            <ContentTemplate>
                                                <asp:Button ID="GenerateSchemaButton" runat="server" Text="Generate Schema" OnClick="Generate_Schema" />
                                                <asp:Label ID="GenerateSchemaResult" runat="server"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </ContentTemplate>
                </ajaxControl:TabPanel>
                <ajaxControl:TabPanel runat="server" HeaderText="Create Entities">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-sm-6">
                                <div class="panel panel-default">
                                    <div class="panel-heading">Test insertion of UserAccounts</div>
                                    <div class="panel-body">
                                        <asp:UpdatePanel ID="InsertUserAccountPanel" runat="server">
                                            <ContentTemplate>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                            ControlToValidate="TestInsertUserAccount_NumberOfUsers_Textbox" runat="server"
                                                            ErrorMessage="Only Numbers allowed" Display="Dynamic"
                                                            ValidationExpression="\d+">
                                                            </asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">

                                                        <asp:Label ID="TestInsertUserAccount_NumberOfUsers" runat="server" Text="Num of Users:" />&nbsp;
                                                        <asp:TextBox ID="TestInsertUserAccount_NumberOfUsers_Textbox" runat="server" Width="50"/>
                                                        <ajaxControl:FilteredTextBoxExtender ID="numUsersFilter" runat="server" 
                                                            TargetControlID="TestInsertUserAccount_NumberOfUsers_Textbox"
                                                            FilterType="Numbers" /> 
                                                        <asp:Button ID="TestInsertUserAccount_NumberOfUsers_Submit" runat="server" Text="Insert"
                                                             OnClick="Test_Insert_UserAccount" />
                                                    </div>

                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="panel panel-default">
                                                            <asp:Datagrid ID="InsertedUserTable" runat="server" CssClass="table"
                                                                 AllowPaging="true" PageSize="10" 
                                                                 OnPageIndexChanged="InsertedUserTable_PageIndexChanging"
                                                                 DataKeyField="USERID">
                                                                <PagerStyle Position="Bottom" CssClass="pagination" />
                                                            </asp:Datagrid>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </ajaxControl:TabPanel>
            </ajaxControl:TabContainer>
        </div>
    </div>


</asp:Content>
