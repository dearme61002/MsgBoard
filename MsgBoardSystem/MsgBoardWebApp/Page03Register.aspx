﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page03Register.aspx.cs" Inherits="MsgBoardWebApp.Page03Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="row g-3 needs-validation" novalidate>
        <h2>註冊會員</h2>
        <div class="col-9 form-floating mb-3">
            <input type="text" class="form-control" id="floatingAcc" placeholder="123" required>
            <div class="invalid-feedback">
                請填入帳號!
            </div>
            <label for="floatingAcc">請在此填入帳號</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="text" class="form-control" id="floatingPwd" placeholder="123" required>
            <div class="invalid-feedback">
                請填入密碼!
            </div>
            <label for="floatingPwd">請在此填入密碼</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="email" class="form-control" id="floatingEmail" placeholder="name@example.com" required>
            <div class="invalid-feedback">
                請填入正確格式Email!
            </div>
            <label for="floatingEmail">請在此填入 Email</label>
        </div>
        <div class="col-9 form-floating mb-3">
            <input type="date" class="form-control" id="floatingBirthDate" placeholder="2000-1-1">
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

            var forms = document.querySelectorAll('.needs-validation')

            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault()
                            event.stopPropagation()
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
