<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page05PostMsg.aspx.cs" Inherits="MsgBoardWebApp.Page05PostMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        // 取得PostID
        const pageUrl = new URL(window.location.href);
        var pid = pageUrl.searchParams.get("PID");
        var getPost;

        $(document).ready(function () {
            $.ajax({
                url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=GetPostInfo",
                type: "POST",
                data: { "PID": pid },
                success: function (result) {
                    getPost = result;
                    $("#navText").text(result.Title);
                    $("#headText").text(result.Title);
                    $("#cardTitle").text(result.Title);
                    $("#cardBody").text(result.Body);
                }
            });

            var msgTable = $('#msgTable').DataTable(
                {
                    "scrollY": "200px",
                    "scrollCollapse": true,
                    "paging": false,
                    "sorting": false,
                    "info": false,
                    "searching": false
                });

            function AddRow(obj) {
                msgTable.row.add([
                    obj.Body,
                    obj.Name,
                    obj.CreateDate
                ]).draw(false);
            }

            $.ajax({
                url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=GetAllMsg",
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
                <li class="breadcrumb-item active" aria-current="page" id="navText"></li>
            </ol>
        </nav>
    </div>
    <div class="card">
        <div class="card-body">
            <h5 class="card-title" id="cardTitle">Post Title</h5>
            <p class="card-text" id="cardBody">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
        </div>
    </div>
    <table id="msgTable" class="table table-striped table-bordered table-sm" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="th-sm">留言內容

                </th>
                <th class="th-sm">留言者

                </th>
                <th class="th-sm">時間

                </th>
            </tr>
        </thead>
    </table>
    <hr class="my-4">

    <form class="row g-3 needs-validation" novalidate>
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
    </form>
    <hr class="my-4">

    <script>
        (function () {
            'use strict'
            var forms = document.querySelectorAll('.needs-validation')

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
                                url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=NewMsg",
                                type: "POST",
                                data: {
                                    "PID": pid,
                                    "Body": body
                                },
                                success: function (result) {
                                    if ("Success" == result) {
                                        alert("成功!");
                                        window.location.reload();
                                    }
                                    else {
                                        alert(result);
                                    }                                  
                                }
                            });
                        }
                        form.classList.add('was-validated')
                    }, false),
                    form.addEventListener('reset', function (resetEvn) {
                        event.preventDefault();
                        $("input:text").val("");
                    }, false)
                })
        })()
    </script>
</asp:Content>
