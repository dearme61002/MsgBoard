<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page02Login.aspx.cs" Inherits="MsgBoardWebApp.Page02Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="row g-3 needs-validation" novalidate>
        <hr class="my-4">
        <div class="row">
            <div class="col-3">
                <label for="txtAcc" class="form-label">帳號 : </label>
            </div>
            <div class="col-6">
                <input type="text" class="form-control" id="txtAcc" value="" required>
                <div class="invalid-feedback">
                    Looks bad!
                </div>
            </div>
            <div class="col-3"></div>
        </div>
        <div class="row">
            <div class="col-3">
                <label for="txtPwd" class="form-label">密碼 : </label>
            </div>
            <div class="col-6">
                <input type="text" class="form-control" id="txtPwd" value="" required>
                <div class="invalid-feedback">
                    Looks bad!
                </div>
            </div>
            <div class="col-3"></div>
        </div>
        <div class="col-12">
            <button class="btn btn-primary" type="submit">送出</button>
        </div>
        <hr class="my-4">
    </form>

    <script>
        // Example starter JavaScript for disabling form submissions if there are invalid fields
        (function () {
            'use strict'

            // Fetch all the forms we want to apply custom Bootstrap validation styles to
            var forms = document.querySelectorAll('.needs-validation')

            // Loop over them and prevent submission
            Array.prototype.slice.call(forms).forEach(function (form) {
                form.addEventListener('submit', function (event) {
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
                                alert("Success");
                                window.location.href = "http://localhost:49461/Page04PostingHall.aspx";
                            }
                        });
                    }
                    form.classList.add('was-validated')
                }, false)
            });
        })()
    </script>
</asp:Content>
