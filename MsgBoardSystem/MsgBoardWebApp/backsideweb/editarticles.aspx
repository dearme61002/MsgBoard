<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="editarticles.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.editarticles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--圖表--%>
    <table class="table table-bordered table-hover">
        <thead>
            <tr>
                <th>發文者姓名</th>
                <th>創建日期</th>
                <th>發文者帳號</th>
                <th>發文者UserID</th>
                <th>發文者回應的主題</th>
                <th>發文者的發表留言內容</th>
                <th>從資料庫刪除</th>
                <th>刪除資料但資料庫還保有發文者訊息</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>
</asp:Content>
