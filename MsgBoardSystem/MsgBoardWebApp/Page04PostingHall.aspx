<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page04PostingHall.aspx.cs" Inherits="MsgBoardWebApp.Page04PostingHall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>Test Page</h1>
    <form runat="server" id="page04form">
        <asp:Literal runat="server" ID="ltlMsg"></asp:Literal><br />
        <asp:Button runat="server" ID="btnLogout" Text="Logout" OnClick="btnLogout_Click" Visible="false" />
    </form>
</asp:Content>
