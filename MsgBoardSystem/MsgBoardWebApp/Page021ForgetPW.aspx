<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page021ForgetPW.aspx.cs" Inherits="MsgBoardWebApp.Page021ForgetPW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Woolong留言版 : 忘記密碼</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="row g-3 needs-validation" novalidate>
        <p class="fs-2 fw-bold">忘記密碼</p>
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
        <div class="row mb-3">
            <div class="col-2 text-center">
                <label for="forgetBirthday" class="form-label">生日 : </label>
            </div>
            <div class="col-4">
                <input type="date" class="form-control" id="forgetBirthday" value="" required>
                <div class="invalid-feedback">
                    生日日期格式錯誤!
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
            var forms = document.querySelectorAll('.needs-validation');
            var noticeModal = new bootstrap.Modal(document.getElementById('noticeModal'));
            var redirect = function () { window.location.href = "Page01Default.aspx"; };

            Array.prototype.slice.call(forms).forEach(function (form) {
                form.addEventListener('submit', function (login) {
                    if (!form.checkValidity()) {
                        event.preventDefault()
                        event.stopPropagation()
                    }
                    else {
                        var account = $("#forgetAcc").val();
                        var email = $("#forgetEmail").val();
                        var birthday = $("#forgetBirthday").val();

                        event.preventDefault();
                        $.ajax({
                            url: "/Handler/SystemHandler.ashx?ActionName=ForgetPW",
                            type: "POST",
                            data: {
                                "Account": account,
                                "Email": email,
                                "Birthday": birthday
                            },
                            success: function (result) {
                                noticeModal.show();
                                if ("Success" == result[0]) {
                                    $(".modal-body").append(result[1]);
                                    $(".closeBtn").click(function () { redirect(); });
                                }
                                else {
                                    $("#modalText").text(result[0]);
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
            });
        })()
    </script>
</asp:Content>
