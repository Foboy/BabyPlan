<%@ Page Title="主页" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="BabyPlan.ManageMent._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
        <asp:Button ID="Button1" runat="server" Text="查询回复列表" onclick="Button1_Click" />
        <asp:Button ID="Button2" runat="server" Text="查询注册用户信息" 
            onclick="Button2_Click" />
    </h2>
    <p>
        <asp:GridView ID="GridView1" runat="server" 
            onrowdatabound="GridView1_RowDataBound">
        </asp:GridView> 
    </p>
</asp:Content>
