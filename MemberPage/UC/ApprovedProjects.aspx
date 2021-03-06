﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApprovedProjects.aspx.cs" Inherits="ApprovedProjects" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:UpdatePanel ID="EntireListedProjectsPage" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="row">
                    <div class="col-lg-3">
                        <!--company list panel-->
                        <div class="panel panel-primary">
                            <div class="panel-heading">Project Titles</div>
                            <div class="panel-body entity-left-panel" style="">
                                <!--dynamically add datasource from codeBehind-->
                                <asp:UpdateProgress runat="server" ID="UpdateProgress1" AssociatedUpdatePanelID="project_list_panel">
                                    <ProgressTemplate>
                                        <div class="overlay">
                                            <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                        </div>
                                    </ProgressTemplate>
                                </asp:UpdateProgress>

                                <asp:UpdatePanel ID="project_list_panel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Repeater runat="server" ID="project_titles">
                                            <ItemTemplate>
                                                <asp:Button CssClass="btn truncate project-button"
                                                    runat="server" Text='<%# Eval("PROJECT_TITLE") %>'
                                                    ToolTip='<%# Eval("PROJECT_TITLE") %>' OnClick="loadProject"
                                                    CommandArgument='<%# Eval("PROJECT_ID") %>' />

                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-9">
                        <div class="row">
                            <asp:UpdateProgress runat="server" ID="UpdateProgress4" AssociatedUpdatePanelID="project_title_panel">
                                <ProgressTemplate>
                                    <div class="overlay">
                                        <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="project_title_panel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <h2>
                                        <asp:Literal ID="project_title" runat="server" /></h2>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="row">
                            <asp:UpdateProgress runat="server" ID="UpdateProgress2" AssociatedUpdatePanelID="project_details_panel">
                                <ProgressTemplate>
                                    <div class="overlay">
                                        <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:UpdatePanel ID="project_details_panel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                <ContentTemplate>
                                    <div class="col-lg-6 myforms">
                                        <!--project detail panel-->
                                        <asp:HiddenField ID="project_id" runat="server" />

                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label AssociatedControlID="company_name" Text="Company Name: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                                <asp:Label ID="company_name" AssociatedControlID="company_name" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label AssociatedControlID="contact_name" Text="Contact Name: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                                <asp:Label ID="contact_name" AssociatedControlID="contact_name" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label AssociatedControlID="contact_number" Text="Contact Number: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                                <asp:Label ID="contact_number" AssociatedControlID="contact_number" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label AssociatedControlID="contact_email" Text="Contact Email: " runat="server" CssClass="col-sm-5 control-label"></asp:Label>
                                                <asp:Label ID="contact_email" AssociatedControlID="contact_email" runat="server" CssClass="col-sm-7 control-label"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="panel panel-success">
                                                <div class="panel-heading">Project Requirements</div>
                                                <div class="panel-body" style="overflow: auto; height: 125px;">
                                                    <asp:Literal ID="project_requirements" runat="server" Mode="Encode"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="panel panel-success">
                                                <div class="panel-heading">UC comments</div>
                                                <div class="panel-body" style="overflow: auto; height: 125px;">
                                                    <asp:Literal ID="uc_comments" runat="server" Mode="Encode"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-6 ">
                                        <!--panel to show categories-->
                                        <div class="row">
                                            <div class="form-group">
                                                <asp:Label Text="Categories: " CssClass="col-lg-2 control-label" runat="server" AssociatedControlID="category_list"></asp:Label>
                                                <div class="col-lg-12">
                                                    <div class="well well-sm" style="height: 120px; overflow: auto;">

                                                        <asp:Repeater runat="server" ID="category_list">
                                                            <ItemTemplate>
                                                                <button class="btn btn-sm btn-default category-button col-sm-6 truncate" style="text-align: left; margin-bottom: 5px;"
                                                                    title='<%# Eval("CATEGORY_NAME") %>'>
                                                                    <%# Eval("CATEGORY_NAME") %>
                                                                </button>
                                                                <input type="hidden" runat="server"
                                                                    value='<%# Eval("CATEGORY_ID") %>' />
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <!--panel to show numbers-->
                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <asp:Label AssociatedControlID="allocated_slots" Text="Allocated: " runat="server" CssClass="col-sm-8 control-label"></asp:Label>
                                                    <asp:Label ID="allocated_slots" AssociatedControlID="allocated_slots" runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <asp:Label AssociatedControlID="num_applications" Text="Applications: " runat="server" CssClass="col-sm-8 control-label"></asp:Label>
                                                    <asp:Label ID="num_applications" AssociatedControlID="num_applications" runat="server" CssClass="col-sm-4 control-label"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12" style="height: 250px;">
                                                <!--Applications panel-->
                                                <asp:UpdateProgress runat="server" ID="UpdateProgress3" AssociatedUpdatePanelID="project_appplications_panel">
                                                    <ProgressTemplate>
                                                        <div class="overlay">
                                                            <asp:Image runat="server" ImageUrl="~/Images/ajax-loader.gif" />
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <div class="overlay" runat="server" id="assigned_project_panel" visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-10 col-lg-offset-1">
                                                            <h2>This project is already assigned to: </h2>
                                                        </div>
                                                    </div>

                                                    <asp:Repeater runat="server" ID="assigned_project_members">
                                                        <ItemTemplate>
                                                            <div class="row">
                                                                <div class="col-lg-10 col-lg-offset-1">
                                                                    <strong>
                                                                        <%# Eval("FIRSTNAME")+" "+Eval("LASTNAME")+" (Student ID: "+Eval("USER_ID")+" Email: "+Eval("EMAIL")+")"  %>
                                                                    </strong>
                                                                </div>
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>

                                                </div>
                                                <asp:UpdatePanel ID="project_appplications_panel" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="project_application_list" runat="server" AllowPaging="true" PageSize="5" GridLines="None"
                                                            OnPageIndexChanged="project_application_list_PageIndexChanged" DataKeyField="APPLICATION_ID" BorderStyle="None"
                                                            AllowSorting="true"
                                                            AutoGenerateColumns="False" CssClass="table">
                                                            <HeaderStyle CssClass="" Font-Bold="true" />

                                                            <Columns>
                                                                <asp:BoundColumn DataField="APPLICATION_ID" HeaderText="Application ID" />
                                                                <asp:TemplateColumn HeaderText="Student ID">
                                                                    <ItemTemplate>
                                                                        <%#DataBinder.Eval(Container.DataItem,"APPLICANT.USER_ID") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Firstname">
                                                                    <ItemTemplate>
                                                                        <asp:HyperLink runat="server" NavigateUrl="#"
                                                                            onclick='<%# "window.open(\"StudentPopup.aspx?studentid="+DataBinder.Eval(Container.DataItem,"APPLICANT.USER_ID")
                                                                                            +"\",\"_blank\",\"menubar=no,height=600,width=800\");"%>'>
                                                                            <%#DataBinder.Eval(Container.DataItem,"APPLICANT.FIRSTNAME") %>
                                                                        </asp:HyperLink>
                                                                        
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Contact">
                                                                    <ItemTemplate>
                                                                        <%#DataBinder.Eval(Container.DataItem,"APPLICANT.PHONE") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Select
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <input type="checkbox" id="appId" onclick="" runat="server" name="appId"
                                                                            class="checkbox" value='<%# Eval("APPLICATION_ID") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                        <ajaxControl:ModalPopupExtender ID="student_info_modal_popup" runat="server"
                                                            PopupControlID="student_info_panel" TargetControlID="HiddenField2"
                                                            BackgroundCssClass="overlay">
                                                        </ajaxControl:ModalPopupExtender>
                                                        <asp:HiddenField runat="server" ID="HiddenField2" />
                                                        <asp:Panel runat="server" ID="student_info_panel" Width="400px">
                                                            <div class="panel panel-primary">
                                                                <div class="panel-heading">Message</div>
                                                                <div class="panel-body" style="overflow: auto;">
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <asp:PlaceHolder runat="server" ID="student_info_message"></asp:PlaceHolder>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-2 col-sm-offset-10">
                                                                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-default" Text="Ok"
                                                                                OnClick="okButton_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <asp:HiddenField ClientIDMode="Static" ID="selected_applications" runat="server" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <!--Apply button panel-->

                                            <div class="col-lg-3 col-lg-offset-9">
                                                <asp:Button ID="assign_button" OnClick="assign_project" runat="server"
                                                    CssClass="btn btn-primary" />
                                            </div>
                                            <ajaxControl:ModalPopupExtender ID="apply_project_popup" runat="server"
                                                PopupControlID="apply_project_panel" TargetControlID="HiddenField1"
                                                BackgroundCssClass="overlay">
                                            </ajaxControl:ModalPopupExtender>
                                            <asp:HiddenField runat="server" ID="HiddenField1" />
                                            <asp:Panel runat="server" ID="apply_project_panel" Width="400px">
                                                <div class="panel panel-primary">
                                                    <div class="panel-heading">Message</div>
                                                    <div class="panel-body" style="overflow: auto;">
                                                        <div class="row">
                                                            <div class="col-sm-12">
                                                                <asp:PlaceHolder runat="server" ID="apply_project_message"></asp:PlaceHolder>
                                                            </div>
                                                        </div>
                                                        <div class="row">
                                                            <div class="col-sm-2 col-sm-offset-10">
                                                                <!--<asp:Button ID="okButton" runat="server" CssClass="btn btn-default" Text="Ok"
                                                                    OnClick="okButton_Click" />-->
                                                                <asp:Button ID="anotherButton" runat="server" CssClass="btn btn-default" Text="Ok"
                                                                    OnClick="okButton_Click" />
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </asp:Panel>



                                        </div>
                                    </div>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>


<asp:Content ContentPlaceHolderID="Scripts" runat="server">
    <!--for truncating longer partner names-->
    <script id="truncate-button-script" type="text/javascript">
        $(document).ready(function () {
            $('.truncate').each(function () {
                var button_text = $(this).val().trim();
                if (button_text.length > 30) {
                    $(this).val(button_text.substring(0, 30) + '...');
                }
            })
        })
    </script>
    <!--for changing the selected button-->
    <script id="change-button-script" type="text/javascript">
        $(document).ready(function () {
            $('.project-button').click(function () {
                $('.project-button').each(function () {
                    $(this).removeClass('btn-primary');
                })
                $(this).addClass('btn-primary');
                //$('#company_list_updatePanel').block('<div class="overlay"><img src="~/Images/ajax-loader.gif" /></div>');
            })
            $('.project-button').ajaxComplete(function () {
                //$('#company_list_updatePanel').unblockUI();
            })
            return false;
        })
    </script>
    <!--for selecting all applications-->
    <script id="select-all-checkbox-script" type="text/javascript">
        $(document).on('click', ".checkAll", function () {

            if (this.checked) {
                $('.checkbox').each(function () {
                    this.checked = true;
                })
            }
            else {
                $('.checkbox').each(function () {
                    this.checked = false;
                })
            }
        });
    </script>
    <!--for passing the selected application IDs-->
    <script id="select-application-script" type="text/javascript">
        $(document).on('click', ".checkbox", function () {
            var selected = $('#selected_applications').val();
            if (this.checked) {
                if (selected.length <= 0)
                    $('#selected_applications').val(this.value);
                else
                    $('#selected_applications').val(selected + "," + this.value);
            }
            else {
                var selected_array = selected.split(',');
                var removed_array = "";
                for (var i = 0; i < selected_array.length; i++) {
                    if (selected_array[i] != this.value) {
                        if (removed_array.length <= 0)
                            removed_array += selected_array[i];
                        else
                            removed_array += "," + selected_array[i];
                    }
                }
                $('#selected_applications').val(removed_array);
            }
            //return false; //very important! if not the container will refresh!
        });
    </script>

</asp:Content>
