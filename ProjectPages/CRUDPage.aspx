<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CRUDPage.aspx.cs" Inherits="CPSC1517Project.ProjectPages.CRUDPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Program Maintenance Page</h1>

    <%--Program ID field--%>
     <div class="row">
        <div class="col-md-4 text-right">
                <asp:Label ID="Label1" runat="server" Text="Program ID"
                     AssociatedControlID="ProgramID">
                </asp:Label>
        </div>
        <div class="col-md-4 text-left">
                <asp:TextBox ID="ProgramID" runat="server" ReadOnly="true">
                </asp:TextBox>
        </div>
    </div>
   <%--Program Name field--%>
    <div class="row">
        <div class="col-md-4 text-right">
                  <asp:Label ID="Label2" runat="server" Text="Program Name"
                     AssociatedControlID="ProgramName"></asp:Label>
        </div>
        <div class="col-md-4 text-left">
                <asp:TextBox ID="ProgramName" runat="server"></asp:TextBox>
        </div>
    </div>
    <%--Diploma Name field--%>
     <div class="row">
        <div class="col-md-4 text-right">
                  <asp:Label ID="Label3" runat="server" Text="Diploma Name"
                     AssociatedControlID="DiplomaName"></asp:Label>
        </div>
        <div class="col-md-4 text-left">
                <asp:TextBox ID="DiplomaName" runat="server"></asp:TextBox>
        </div>
    </div>
    <%--School code dropdown list--%>
    <div class="row">
        <div class="col-md-4 text-right">
                <asp:Label ID="Label6" runat="server" Text="School"
                     AssociatedControlID="SchoolList">
                </asp:Label>
        </div>
        <div class="col-md-4 text-left">
                <asp:DropDownList ID="SchoolList" runat="server" Width="300px">
                </asp:DropDownList> 
        </div>
    </div>
    <%--Tuition field--%>
    <div class="row">
        <div class="col-md-4 text-right">
                  <asp:Label ID="Label4" runat="server" Text="Tuition"
                     AssociatedControlID="Tuition"></asp:Label>
        </div>
        <div class="col-md-4 text-left">
                <asp:TextBox ID="Tuition" runat="server"></asp:TextBox>
        </div>
    </div>
    <%--International Tuition field--%>
    <div class="row">
        <div class="col-md-4 text-right">
                  <asp:Label ID="Label5" runat="server" Text="International Tuition"
                     AssociatedControlID="InternationalTuition"></asp:Label>
        </div>
        <div class="col-md-4 text-left">
                <asp:TextBox ID="InternationalTuition" runat="server"></asp:TextBox>
        </div>
    </div>
    <div class="row">
        <div class="col-md-4">
        </div>
        <div class="col-md-6 text-left">
            <asp:Button ID="BackButton" runat="server" Text="Back" CausesValidation="false" OnClick="Back_Click" />&nbsp;&nbsp;
            <asp:Button ID="AddButton" runat="server" OnClick="Add_Click" Text="Add"/>&nbsp;&nbsp;
            <asp:Button ID="UpdateButton" runat="server" OnClick="Update_Click" Text="Update"/>&nbsp;&nbsp;
            <asp:Button ID="DeleteButton" runat="server" OnClick="Delete_Click" Text="Delete"
              OnClientClick="return CallFunction();"/>
        </div>
    </div>
    <br /><br />
    <div class="row">
        <div class="offset-2"> 
            <asp:DataList ID="Message" runat="server">
            <ItemTemplate>
                <%# Container.DataItem %>
            </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    <div class="row">
        <div class="offset-2"> 
            <label ID="LabelMessage1" name="LabelMessage1" runat="server" />
        </div>
    </div>
    <script type="text/javascript">
        function CallFunction() {
            return confirm("Are you sure you wish to delete this record?");
       }
   </script>
</asp:Content>
