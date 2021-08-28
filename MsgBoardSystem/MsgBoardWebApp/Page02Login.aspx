<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page02Login.aspx.cs" Inherits="MsgBoardWebApp.Page02Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="row g-3 needs-validation" novalidate>
        <h2>會員登入</h2>
        <div class="row mb-3">
            <div class="col-2 text-center">
                <label for="txtAcc" class="form-label">帳號 : </label>
            </div>
            <div class="col-4">
                <input type="text" class="form-control" id="txtAcc" value="" required>
                <div class="invalid-feedback">
                    請填入帳號!
                </div>
            </div>
        </div>
        <div class="row mb-3">
            <div class="col-2 text-center">
                <label for="txtPwd" class="form-label">密碼 : </label>
            </div>
            <div class="col-4">
                <input type="password" class="form-control" id="txtPwd" value="" required>
                <div class="invalid-feedback">
                    請填入密碼!
                </div>
            </div>
        </div>
        <div class="col-12">
            <button class="btn btn-primary" type="submit">送出</button>
            <a class="btn btn btn-outline-info" type="button" href="Page021ForgetPW.aspx">忘記密碼</a>
            <input class="btn btn-warning" type="reset" value="登出" id="logoutBtn">
        </div>
        <hr class="my-4">
    </form>

    <script>
        (function () {
            'use strict'

            var forms = document.querySelectorAll('.needs-validation')
            var redirect = function () { window.location.href = "http://localhost:49461/Page04PostingHall.aspx"; }
            var noticeModal = new bootstrap.Modal(document.getElementById('noticeModal'));

            Array.prototype.slice.call(forms).forEach(function (form) {
                form.addEventListener('submit', function (login) {
                    if (!form.checkValidity()) {
                        event.preventDefault()
                        event.stopPropagation()
                    }
                    else {
                        var acc = $("#txtAcc").val();
                        var pwd = $("#txtPwd").val();
                        event.preventDefault()
                        $.ajax({
                            url: "http://localhost:49461/Handler/SystemHandler.ashx?ActionName=Login",
                            type: "POST",
                            data: {
                                "Account": acc,
                                "Password": pwd
                            },
                            success: function (result) {                                
                                var authx = document.cookie.indexOf(".ASPXAUTH");
                                noticeModal.show();

                                if ("Success" == result) {
                                    if (authx == 0) {
                                        $("#modalText").text("登入成功!");
                                        $(".closeBtn").click(function () {
                                            $('#funcList').show();
                                            redirect();
                                        });
                                    }
                                    else {
                                        $("#modalText").text("Auth cookie fail");
                                    }
                                }
                                else {
                                    $('#funcList').hide();
                                    $("#modalText").text(result);
                                }
                            }
                        });
                    }
                    form.classList.add('was-validated')
                }, false)
                form.addEventListener('reset', function (resetEvn) {
                    document.cookie = '.ASPXAUTH' + '=; expires=Thu, 01-Jan-70 00:00:01 GMT;';
                    alert("登出成功");
                    window.location.href = "http://localhost:49461/Page01Default.aspx";
                }, false)
            });
        })()
    </script>
</asp:Content>
