<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page061EditInfo.aspx.cs" Inherits="MsgBoardWebApp.Page061EditInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <form class="row g-3 needs-validation" novalidate>
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
