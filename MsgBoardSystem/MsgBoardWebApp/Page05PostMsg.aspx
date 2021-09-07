<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page05PostMsg.aspx.cs" Inherits="MsgBoardWebApp.Page05PostMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title id="pageTitle">Woolong留言版 : 貼文內容</title>
    <style>
        .scroll {
            max-height: 250px;
            overflow-y: auto;
        }
    </style>
    <script>
        // 取得PostID
        const pageUrl = new URL(window.location.href);
        var pid = pageUrl.searchParams.get("PID");
        var test;
        $(document).ready(function () {
            $.ajax({
                url: "/Handler/SystemHandler.ashx?ActionName=GetPostInfo",
                type: "POST",
                data: { "PID": pid },
                success: function (result) {
                    test = result;
                    $("#navText").text(result.Title);
                    $("#headText").text(result.Title);
                    $("#cardBody").text("");
                    $("#cardBody").append(result.Body);
                    $("#cardFooter").text("建立日期 : " + result.CreateDate);
                    $('#pageTitle').text("Woolong留言版 : " + result.Title);
                }
            });

            var msgTable = $('#msgTable').DataTable(
                {
                    "scrollY": "200px",
                    "scrollCollapse": true,
                    "paging": false,
                    "info": false,
                    "searching": false,
                   "order": [[2, "asc"]]
                });

            function AddRow(obj) {
                msgTable.row.add([
                    obj.Body,
                    obj.Name,
                    obj.CreateDate
                ]).draw(false);
            }

            $.ajax({
                url: "/Handler/SystemHandler.ashx?ActionName=GetAllMsg",
                type: "POST",
                data: { "PID": pid },
                success: function (result) {
                    for (var i = 0; i < result.length; i++) {
                        var obj = result[i];
                        AddRow(obj);
                    }
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p class="fs-2 fw-bold" id="headText"></p>
        <nav style="--bs-breadcrumb-divider: '>';" aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Page01Default.aspx">首頁</a></li>
                <li class="breadcrumb-item"><a href="Page04PostingHall.aspx">貼文區</a></li>
                <li class="breadcrumb-item active" aria-current="page" id="navText">神秘頁面</li>
            </ol>
        </nav>
    </div>
    <div class="card">
        <div class="card-body" style="background-color:#fffff1">
            <p class="card-text scroll" id="cardBody">Oops..... 看來你來到了一個神奇的地方，趕快回去正常的頁面吧</p>
        </div>
        <div class="card-footer text-muted" id="cardFooter"></div>
    </div>
    <table id="msgTable" class="table table-striped table-bordered table-sm" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="th-sm" style="width:62%">留言內容</th>
                <th class="th-sm" style="width:15%">留言者</th>
                <th class="th-sm" style="width:23%">時間</th>
            </tr>
        </thead>
    </table>
    <hr class="my-4">

    <form class="row g-3 needs-validation loginFunc" style="display:none" novalidate>
        <div class="mb-3">
            <label for="msgText" class="form-label">留下留言 : </label>
            <textarea class="form-control" id="msgText" rows="3" required placeholder="寫點什麼...."></textarea>
            <div class="invalid-feedback">
                請填入內容!
            </div>
        </div>
        <div class="col-12">
            <button class="btn btn-outline-primary" type="submit">送出</button>
            <button class="btn btn-outline-secondary" type="reset">清除</button>
        </div>
        <hr class="my-4">
    </form>
    
    <script>
        (function () {
            'use strict'
            var forms = document.querySelectorAll('.needs-validation');
            var redirect = function () { window.location.reload(); };
            var noticeModal = new bootstrap.Modal(document.getElementById('noticeModal'));

            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        else {
                            var body = $("#msgText").val();

                            event.preventDefault();
                            $.ajax({
                                url: "/Handler/SystemHandler.ashx?ActionName=NewMsg",
                                type: "POST",
                                data: {
                                    "PID": pid,
                                    "Body": body
                                },
                                success: function (result) {
                                    noticeModal.show();
                                    if ("Success" == result) {
                                        $("#modalText").text("留言成功!");
                                        $(".closeBtn").click(function () { redirect(); });                                      
                                    }
                                    else {
                                        $("#modalText").text(result);
                                        $(".closeBtn").click(function () { noticeModal.hide(); });
                                    }                                  
                                }
                            });
                        }
                        form.classList.add('was-validated')
                    }, false),
                    form.addEventListener('reset', function (resetEvn) {
                        event.preventDefault();
                        $("#msgText").val("");
                    }, false)
                })
        })()
    </script>
</asp:Content>
