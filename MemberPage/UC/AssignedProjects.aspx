<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AssignedProjects.aspx.cs" Inherits="ProjectStatus" MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControl" %>
<asp:Content ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:DataGrid ID="project_list" runat="server" AllowPaging="true" PageSize="10" GridLines="None"
                OnPageIndexChanged="project_list_PageIndexChanged" DataKeyField="PROJECT_ID" BorderStyle="None"
                AllowSorting="true"
                AutoGenerateColumns="False" CssClass="table">
                <HeaderStyle CssClass="" Font-Bold="true" />

                <Columns>
                    <asp:BoundColumn DataField="PROJECT_ID" HeaderText="Project ID" />
                    <asp:BoundColumn DataField="PROJECT_TITLE" HeaderText="Project Title" />
                    <asp:TemplateColumn HeaderText="Project Company">
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem,"PROJECT_OWNER.USERNAME") %>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="CONTACT_NAME" HeaderText="Contact Person" />
                    <asp:BoundColumn DataField="CONTACT_NUMBER" HeaderText="Contact Number" />
                    <asp:BoundColumn DataField="CONTACT_EMAIL" HeaderText="Contact Email" />
                    <asp:BoundColumn DataField="PROJECT_STATUS" HeaderText="Status" />
                    <asp:TemplateColumn>
                        <HeaderTemplate>
                            Select
                        </HeaderTemplate>
                        <ItemTemplate>
                            <input type="checkbox" id="appId" onclick="" runat="server" name="appId"
                                class="checkbox" value='<%# Eval("PROJECT_ID") %>' />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                
            </asp:DataGrid>
            <asp:HiddenField ClientIDMode="Static" ID="selected_projects" runat="server" />
        </div>
    </div>
    <div class="row">
        <div class="col-sm-6 col-sm-offset-6" style="text-align: right;">
            <asp:Button ID="assigned_button" runat="server" CssClass="btn btn-success" Text="Mark as Assigned" OnClick="assigned_button_Click" />
            <asp:Button ID="complete_button" runat="server" CssClass="btn btn-primary" Text="Mark as Completed" OnClick="complete_button_Click" />
            <asp:Button ID="terminate_button" runat="server" CssClass="btn btn-danger" Text="Mark as Terminated" OnClick="terminate_button_Click"/>
        </div>
    </div>

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
    <!--for passing the selected application IDs-->
    <script id="select-application-script" type="text/javascript">
        $(document).on('click', ".checkbox", function () {
            var selected = $('#selected_projects').val();
            if (this.checked) {
                if (selected.length <= 0)
                    $('#selected_projects').val(this.value);
                else
                    $('#selected_projects').val(selected + "," + this.value);
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
                $('#selected_projects').val(removed_array);
            }
            //return false; //very important! if not the container will refresh!
        });
    </script>
</asp:Content>
