<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page04PostingHall.aspx.cs" Inherits="MsgBoardWebApp.Page04PostingHall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(document).ready(function () {
            $('#dtBasicExample').DataTable();

            /*$.ajax({
                url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=GetSession",
                type: "GET",
                data: {},
                success: function (result) {
                    alert(result);
                }
            });*/
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h2>貼文區</h2>
    <form runat="server" id="page04form">
        <asp:Literal runat="server" ID="ltlMsg"></asp:Literal><br />
    </form>
    <div class="list-group">
        <a href="#" class="list-group-item list-group-item-action list-group-item-info" aria-current="true">
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">貼文標題1</h5>
                <small class="text-muted">3 days ago(時間點)</small>
            </div>
            <p class="mb-1">一些內容放這裡....</p>
            <small>第二段放這裡...</small>
        </a>
        <a href="#" class="list-group-item list-group-item-action list-group-item-info">
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">貼文標題2</h5>
                <small class="text-muted">3 days ago(時間點)</small>
            </div>
            <p class="mb-1">一些內容放這裡....</p>
            <small class="text-muted">第二段放這裡...</small>
        </a>
        <a href="#" class="list-group-item list-group-item-action list-group-item-info">
            <div class="d-flex w-100 justify-content-between">
                <h5 class="mb-1">貼文標題3</h5>
                <small class="text-muted">3 days ago(時間點)</small>
            </div>
            <p class="mb-1">一些內容放這裡....</p>
            <small class="text-muted">第二段放這裡...</small>
        </a>
    </div>
    <hr class="my-4">
    <table id="dtBasicExample" class="table table-striped table-bordered table-sm table-hover" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="th-sm">標題

                </th>
                <th class="th-sm">發文者

                </th>
                <th class="th-sm">時間

                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>
                    <a href="../Page05PostMsg.aspx?PID=abc123">測試項目1</a>
                </td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目2</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目3</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目4</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目5</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目6</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目7</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目8</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目9</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目10</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目11</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目12</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目13</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>測試項目14</td>
                <td>Tiger Nixon</td>
                <td>2021-01-01</td>
            </tr>
        </tbody>
        <tfoot>
            <tr>
                <th>標題
                </th>
                <th>發文者
                </th>
                <th>時間
                </th>
            </tr>
        </tfoot>
    </table>
    <hr class="my-4">
    <script src="Bootstrap/js/bootstrap.bundle.min.js"></script>
</asp:Content>
