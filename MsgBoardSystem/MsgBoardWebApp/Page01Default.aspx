<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page01Default.aspx.cs" Inherits="MsgBoardWebApp.Page01Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div {
            /*border: 1px solid #000000;*/
        }
   
    </style>
    <script>
        $(document).ready(function () {
            $.ajax({ url: "/Handler/SystemHandler.ashx?ActionName=DefaultPageLoad", type: "POST"});

            $.ajax({
                url: "/Handler/SystemHandler.ashx?ActionName=GetAllPost",
                type: "GET",
                data: {},
                success: function (result) {
                    var table = '<div class="list-group">';

                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        if ("Member" == obj.Level && true == obj.ismaincontent) {
                            var htmlText =
                                `<a href="Page05PostMsg.aspx?PID=${obj.PostID}" class="list-group-item list-group-item-action">
                                    <table>
                                        <tr>
                                            <td class="fw-bold" style="width:45%">${obj.Title}</td>
                                            <td style="width:30%">${obj.Name}</td>
                                            <td style="width:25%">${obj.CreateDate}</td>
                                        </tr>
                                    </table>
                                </a>`;
                            table += htmlText;
                        }
                    }

                    table += "</div>";
                    $("#divPostList").append(table);
                }
            });

        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="row">
            <div class="col-7">
                <h1 class="display-4 text-center alert-danger">熱門焦點</h1>
                <div id="divPostList"></div>
            </div>
            <div class="col-5">
                <img class="img-fluid" src="Img/udon.png" />
            </div>
        </div>
    </div>
</asp:Content>
