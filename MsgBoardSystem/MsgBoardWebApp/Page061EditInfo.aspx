<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page061EditInfo.aspx.cs" Inherits="MsgBoardWebApp.Page061EditInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Woolong留言版 : 編輯會員資料</title>
    <script>
        var testVal;
        $(document).ready(function () {
            $.ajax({
                url: "/Handler/SystemHandler.ashx?ActionName=GetEditInfo",
                type: "GET",
                data: {},
                success: function (result) {
                    $("#staticAcc").val(result.Account);
                    $("#editName").val(result.Name);
                    $("#staticLevel").val(result.Level);
                    $("#editEmail").val(result.Email);
                    $("#editBirthday").val(result.Birthday);
                    $("#staticCreateDate").val(result.CreateDate);
                }
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p class="fs-2 fw-bold">編輯會員資料</p>
        <nav style="--bs-breadcrumb-divider: '>';" aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Page01Default.aspx">首頁</a></li>
                <li class="breadcrumb-item"><a href="Page06MemberCenter.aspx">會員中心</a></li>
                <li class="breadcrumb-item active" aria-current="page">編輯會員資料</li>
            </ol>
        </nav>
    </div>
    <hr class="my-4">
    <form class="row g-3 needs-validation" novalidate>
         <div class="mb-3 row">
            <label for="staticAcc" class="col-sm-2 col-form-label">帳號</label>
            <div class="col-sm-10">
                <input type="text" readonly class="form-control-plaintext" id="staticAcc" value="-">
            </div>
        </div>
         <div class="mb-3 row">
            <label for="editName" class="col-sm-2 col-form-label">使用者名稱</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" id="editName" placeholder="-" required>
                    <div class="invalid-feedback">
                        請填入使用者名稱!
                    </div>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="staticLevel" class="col-sm-2 col-form-label">帳號權限</label>
            <div class="col-sm-10">
                <input type="text" readonly class="form-control-plaintext" id="staticLevel" value="-">
            </div>
        </div>
        <div class="mb-3 row">
            <label for="editEmail" class="col-sm-2 col-form-label">Email</label>
            <div class="col-sm-10">
                <input type="text" class="form-control" id="editEmail" placeholder="-" required>
                    <div class="invalid-feedback">
                        請填入Email或格式錯誤!
                    </div>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="editBirthday" class="col-sm-2 col-form-label">生日</label>
            <div class="col-sm-10">
                <input type="date" class="form-control" id="editBirthday" placeholder="" required>
                    <div class="invalid-feedback">
                        生日日期錯誤!
                    </div>
            </div>
        </div>
        <div class="mb-3 row">
            <label for="staticCreateDate" class="col-sm-2 col-form-label">建立日期</label>
            <div class="col-sm-10">
                <input type="text" readonly class="form-control-plaintext" id="staticCreateDate" value="-">
            </div>
        </div>

        <div class="col-12">
            <button class="btn btn-outline-primary" type="submit">送出</button>
            <button class="btn btn-outline-danger" type="reset">清除</button>
        </div>
        <hr class="my-4">
    </form>

    <script>
        (function () {
            'use strict'
            var forms = document.querySelectorAll('.needs-validation');
            var redirect = function () { window.location.href = "Page06MemberCenter.aspx"; }
            var noticeModal = new bootstrap.Modal(document.getElementById('noticeModal'));

            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        else {
                            var name = $("#editName").val();
                            var email = $("#editEmail").val();
                            var birthday = $("#editBirthday").val();
                            var account = $("#staticAcc").val();

                            event.preventDefault();
                            $.ajax({
                                url: "/Handler/SystemHandler.ashx?ActionName=UpdateInfo",
                                type: "POST",
                                data: {
                                    "Name": name,
                                    "Email": email,
                                    "Birthday": birthday,
                                    "Account": account
                                },
                                success: function (result) {
                                    noticeModal.show();
                                    if ("Success" == result) {
                                        $("#modalText").text("更改成功! 轉跳至會員中心!");
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
                        $('[id^="edit"]').val("");
                        event.preventDefault();
                    }, false)
                })
        })()
    </script>
</asp:Content>
