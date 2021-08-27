<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page01Default.aspx.cs" Inherits="MsgBoardWebApp.Page01Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div {
            //border: 1px solid #000000;
        }
   
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-7">
                <h1 class="display-4 text-center alert-danger">熱門焦點123</h1>

                <table class="table table-striped">
                    <tr>
                        <td><a href="#">1. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">2. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">3. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">4. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">5. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">6. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">7. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">8. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">9. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                    <tr>
                        <td><a href="#">10. (主題)貼文標題 發文者 時間</a></td>
                    </tr>
                </table>
            </div>
            <div class="col-5">
                <img class="img-fluid" src="Img/udon.png" />
            </div>
        </div>
    </div>
</asp:Content>
