<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page062EditPwd.aspx.cs" Inherits="MsgBoardWebApp.Page062EditPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Woolong留言版 : 修改會員密碼</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <p class="fs-2 fw-bold">修改會員密碼</p>
        <nav style="--bs-breadcrumb-divider: '>';" aria-label="breadcrumb">
            <ol class="breadcrumb">
                <li class="breadcrumb-item"><a href="Page01Default.aspx">首頁</a></li>
                <li class="breadcrumb-item"><a href="Page06MemberCenter.aspx">會員中心</a></li>
                <li class="breadcrumb-item active" aria-current="page">修改會員密碼</li>
            </ol>
        </nav>
    </div>
    <form class="row g-3 needs-validation" novalidate>
        <div class="col-9 form-floating mb-3">
            <input type="password" class="form-control" id="oldPwd" placeholder="00000" required>
            <div class="invalid-feedback">
                請填入舊密碼!
            </div>
            <label for="oldPwd">請在此填入舊密碼</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="password" class="form-control" id="newPwd" placeholder="00000" required pattern=".{6,15}" title="">
            <div class="invalid-feedback">
                請填入6~15位新密碼!
            </div>
            <label for="newPwd">請在此填入新密碼</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="password" class="form-control" id="newPwdAgain" placeholder="00000" required pattern=".{6,15}" title="">
            <div class="invalid-feedback">
                請填入6~15位新密碼!
            </div>
            <label for="newPwdAgain">請在此再次填入新密碼</label>
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
            var redirect = function () { window.location.href = "Page01Default.aspx"; };
            var noticeModal = new bootstrap.Modal(document.getElementById('noticeModal'));

            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        else {
                            var oldPwd = $("#oldPwd").val();
                            var newPwd = $("#newPwd").val();
                            var newPwdAgain = $("#newPwdAgain").val();

                            event.preventDefault();
                            $.ajax({
                                url: "/Handler/SystemHandler.ashx?ActionName=UpdatePwd",
                                type: "POST",
                                data: {
                                    "OldPwd": oldPwd,
                                    "NewPwd": newPwd,
                                    "NewPwdAgain": newPwdAgain
                                },
                                success: function (result) {
                                    noticeModal.show();
                                    if ("Success" == result) {
                                        $("#modalText").text("密碼修改成功! 請重新登入會員");
                                        document.cookie = '.ASPXAUTH' + '=; expires=Thu, 01-Jan-70 00:00:01 GMT;';                                       
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
                            $("input:text").val("");
                        }, false)
                })
        })()
    </script>
</asp:Content>
