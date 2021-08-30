<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page01Default.aspx.cs" Inherits="MsgBoardWebApp.Page01Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        div {
            //border: 1px solid #000000;
        }
   
    </style>
    <script>
        $(document).ready(function () {
            $.ajax({
                url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=GetAllPost",
                type: "GET",
                data: {},
                success: function (result) {
                    var table = '<table class="table table-striped">';
                    table += '<tr> <th>Title</th> <th>Name</th>  <th>CreateDate</th>  </tr>';

                    for (var i = 0; i < 10; i++) {
                        var obj = result[i];
                        var htmlText =
                            `<tr> 
                                <td><a href="http://localhost:49461/Page05PostMsg.aspx?PID=${obj.PostID}">${obj.Title}</a></td>
                                <td>${obj.Name}</td>
                                <td>${obj.CreateDate}</td>
                            </tr>`;
                        table += htmlText;
                    }

                    table += "</table>";
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
