<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page01Default.aspx.cs" Inherits="MsgBoardWebApp.Page01Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .AllRangeSetting{
            font-family: 微軟正黑體;
            width:400px;
            background-color:antiquewhite;
        }
    </style>
    <table class="AllRangeSetting">
        <tr>
            <td>
                <h2>歡迎訊息放這裡aaa</h2>
                <a>相關訊息1</a>
                <a>相關訊息2</a>
            </td>
        </tr>
        <tr>
            <td>
                <h2>熱門貼文放這裡</h2>
                <div style="text-align:center">
                    <a href="#">1 貼文標題 發文者 時間</a><br />
                    <a href="#">2 貼文標題 發文者 時間</a><br />
                    <a href="#">3 貼文標題 發文者 時間</a><br />
                    <a href="#">4 貼文標題 發文者 時間</a><br />
                    <a href="#">5 貼文標題 發文者 時間</a><br />
                    <a href="#">6 貼文標題 發文者 時間</a><br />
                    <a href="#">7 貼文標題 發文者 時間</a><br />
                    <a href="#">8 貼文標題 發文者 時間</a><br />
                    <a href="#">9 貼文標題 發文者 時間</a><br />
                    <a href="#">10  貼文標題 發文者 時間</a><br />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
