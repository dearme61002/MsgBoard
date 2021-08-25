<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page021ForgetPW.aspx.cs" Inherits="MsgBoardWebApp.Page021ForgetPW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="row g-3 needs-validation" novalidate>
        <h2>忘記密碼</h2>
        <div class="row mb-3">
            <div class="col-2 text-center">
                <label for="forgetAcc" class="form-label">帳號 : </label>
            </div>
            <div class="col-4">
                <input type="text" class="form-control" id="forgetAcc" value="" required>
                <div class="invalid-feedback">
                    請填入帳號!
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-2 text-center">
                <label for="forgetEmail" class="form-label">Email : </label>
            </div>
            <div class="col-4">
                <input type="email" class="form-control" id="forgetEmail" value="" required>
                <div class="invalid-feedback">
                    請填入Email或正確Email格式!
                </div>
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

            var forms = document.querySelectorAll('.needs-validation')

            Array.prototype.slice.call(forms).forEach(function (form) {
                form.addEventListener('submit', function (login) {
                    if (!form.checkValidity()) {
                        event.preventDefault()
                        event.stopPropagation()
                    }
                    else {
                        alert("送出成功 請檢查Email郵件");
                        event.preventDefault()
                        window.location.href = "http://localhost:49461/Page01Default.aspx";
                    }
                    form.classList.add('was-validated')
                }, false)
                form.addEventListener('reset', function (resetEvn) {
                    $("input:text").val("");
                }, false)
            });
        })()
    </script>
</asp:Content>
