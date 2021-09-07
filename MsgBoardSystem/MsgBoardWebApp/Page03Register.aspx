<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page03Register.aspx.cs" Inherits="MsgBoardWebApp.Page03Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="row g-3 needs-validation" novalidate>
        <p class="fs-2 fw-bold">註冊會員</p>
        <div class="col-9 form-floating mb-3">
            <input type="text" class="form-control" id="floatingName" placeholder="123" required>
            <div class="invalid-feedback">請填入用戶名稱!</div>
            <label for="floatingAcc">請在此填入用戶名稱</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="text" class="form-control" id="floatingAcc" placeholder="123" required pattern=".{5,15}" title="">
            <div class="invalid-feedback">請填入正確格式帳號!</div>
            <label for="floatingAcc">請在此填入5~15位帳號</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="password" class="form-control" id="floatingPwd" placeholder="" pattern=".{6,15}" required title="">
            <div class="invalid-feedback">請填入正確格式密碼!</div>
            <label for="floatingPwd">請在此填入6~15位密碼</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="email" class="form-control" id="floatingEmail" required>
            <div class="invalid-feedback">請填入正確格式Email!</div>
            <label for="floatingEmail">請在此填入 Email</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="date" class="form-control" id="floatingBirthDate">
            <div class="invalid-feedback">請選擇正確日期!</div>
            <label for="floatingBirthDate">請在此填入生日</label>
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
                            var name = $("#floatingName").val();
                            var account = $("#floatingAcc").val();
                            var password = $("#floatingPwd").val();
                            var email = $("#floatingEmail").val();
                            var birthday = $("#floatingBirthDate").val();

                            event.preventDefault();
                            $.ajax({
                                url: "/Handler/SystemHandler.ashx?ActionName=Register",
                                type: "POST",
                                data: {
                                    "Name" : name,
                                    "Account": account,
                                    "Password": password,
                                    "Email": email,
                                    "BirthDay": birthday
                                },
                                success: function (result) {
                                    noticeModal.show();
                                    if ("Success" == result) {
                                        $("#modalText").text("註冊成功!! 轉跳至首頁");
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
