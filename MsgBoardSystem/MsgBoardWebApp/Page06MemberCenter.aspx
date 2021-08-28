<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page06MemberCenter.aspx.cs" Inherits="MsgBoardWebApp.Page06MemberCenter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="DataTableFrame/DataTables-1.10.25/css/jquery.dataTables.min.css" />
    <script src="DataTableFrame/DataTables-1.10.25/js/jquery.dataTables.min.js"></script>
    <script>
        $(document).ready(function () {
            // Set Table
            var userPostTable = $('#UserPostTable').DataTable();
            var userMsgTable = $('#UserMsgTable').DataTable();

            // Add Row Function
            function AddPostRow(obj) {
                userPostTable.row.add([
                    obj.PostID,
                    `<a href="http://localhost:49461/Page05PostMsg.aspx?PID=${obj.PostID}">${obj.Title}<a>`,
                    obj.Name,
                    obj.CreateDate
                ]).draw(false);
            }
            function AddMsgRow(obj) {
                userMsgTable.row.add([
                    obj.MsgID,
                    `<a href="http://localhost:49461/Page05PostMsg.aspx?PID=${obj.PostID}">${obj.PostTile}<a>`,
                    obj.Body,
                    obj.Name,
                    obj.CreateDate
                ]).draw(false);
            }

            // Load Post data Ajax
            $.ajax({
                url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=GetUserPost",
                type: "GET",
                data: {},
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        AddPostRow(obj);
                    }
                }
            });

            // Load Msg data Ajax
            $.ajax({
                url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=GetUserMsg",
                type: "GET",
                data: {},
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        AddMsgRow(obj);
                    }
                }
            });

            // Hide First Column
            userPostTable.column(0).visible(false);
            userMsgTable.column(0).visible(false);

            // msg select method
            $('#UserPostTable tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    userPostTable.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });
            $('#UserMsgTable tbody').on('click', 'tr', function () {
                if ($(this).hasClass('selected')) {
                    $(this).removeClass('selected');
                }
                else {
                    userMsgTable.$('tr.selected').removeClass('selected');
                    $(this).addClass('selected');
                }
            });

            // delete msg function
            $('#deletePostBtn').click(function () {
                var rowData = userPostTable.rows('.selected').data().toArray();
                $.ajax({
                    url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=UserDeletePost",
                    type: "POST",
                    data: {
                        "PID": rowData[0][0]
                    },
                    success: function (result) {
                        if ("Success" == result) {
                            alert("刪除成功!");
                        }
                        else {
                            alert(result);
                        }
                    }
                });
                userPostTable.row('.selected').remove().draw(false);
            });
            $('#deleteMsgBtn').click(function () {
                var rowData = userMsgTable.rows('.selected').data().toArray();              
                $.ajax({
                    url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=UserDeleteMsg",
                    type: "POST",
                    data: {
                        "MID": rowData[0][0]
                    },
                    success: function (result) {
                        if ("Success" == result) {
                            alert("刪除成功!");
                        }
                        else {
                            alert(result);
                        }
                    }
                });
                userMsgTable.row('.selected').remove().draw(false);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p class="fs-2 fw-bold">會員中心</p>
        <nav style="--bs-breadcrumb-divider: '>';" aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Page01Default.aspx">首頁</a></li>
                <li class="breadcrumb-item active" aria-current="page">會員中心</li>
            </ol>
        </nav>
    </div>
    <div class="d-grid gap-3">
        <div class="p-2 bg-light border">
            <p class="fs-4">主要功能</p>
            <a class="btn btn-outline-secondary" href="Page061EditInfo.aspx">編輯會員資料</a>
            <a class="btn btn-outline-secondary" href="Page062EditPwd.aspx">修改會員密碼</a>
        </div>
        <div class="p-2 bg-light border">
            <p class="fs-4">
                刪除貼文
                <button class="fs-7 btn btn-outline-danger" id="deletePostBtn">點此刪除所選貼文</button>
            </p>
            <table id="UserPostTable" class="display" style="width: 100%">
                <thead>
                    <tr>
                        <th>PostID</th>
                        <th>標題</th>
                        <th>發文者</th>
                        <th>建立時間</th>
                    </tr>
                </thead>
            </table>
        </div>
        <div class="p-2 bg-light border">
            <p class="fs-4">
                刪除留言
                <button class="fs-7 btn btn-outline-danger" id="deleteMsgBtn">點此刪除所選留言</button>
            </p>
            <table id="UserMsgTable" class="display" style="width: 100%">
                <thead>
                    <tr>
                        <th>MsgID</th>
                        <th>貼文標題</th>
                        <th>留言內容</th>
                        <th>發文者</th>
                        <th>建立時間</th>
                    </tr>
                </thead>
            </table>
        </div>
    </div>
    <hr class="my-4">
</asp:Content>
