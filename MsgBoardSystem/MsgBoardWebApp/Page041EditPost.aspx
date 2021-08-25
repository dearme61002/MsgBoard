<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page041EditPost.aspx.cs" Inherits="MsgBoardWebApp.Page041EditPost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form class="row g-3 needs-validation" novalidate>
        <h2>撰寫新貼文</h2>
        <div class="mb-3">
            <label for="exampleFormControlInput1" class="form-label">貼文標題 : </label>
            <input type="text" class="form-control" id="exampleFormControlInput1" placeholder="">
            <div class="invalid-feedback">
                請填入貼文標題!
            </div>
        </div>
        <div class="mb-3">
            <label for="exampleFormControlTextarea1" class="form-label">貼文內容 : </label>
            <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
            <div class="invalid-feedback">
                請填入貼文內容!
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

            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault()
                            event.stopPropagation()
                        }
                        else {
                            alert("建立成功");
                            event.preventDefault();
                            window.location.href = "http://localhost:49461/Page04PostingHall.aspx";
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
