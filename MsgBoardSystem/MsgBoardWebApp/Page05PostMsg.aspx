<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page05PostMsg.aspx.cs" Inherits="MsgBoardWebApp.Page05PostMsg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card">
        <div class="card-body">
            <h5 class="card-title">Post Title</h5>
            <h6 class="card-subtitle mb-2 text-muted">Post subtitle</h6>
            <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
        </div>
    </div>
    <table id="msgTable" class="table table-striped table-bordered table-sm" cellspacing="0" width="100%">
        <thead>
            <tr>
                <th class="th-sm">內容

                </th>
                <th class="th-sm">留言者

                </th>
                <th class="th-sm">時間

                </th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td>a測試留言123123123123123131323123123123123132</td>
                <td>Alan3</td>
                <td>2021-01-01</td>
            </tr>
            <tr>
                <td>b測試留言123</td>
                <td>Alan2</td>
                <td>2021-01-02</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
            <tr>
                <td>c測試留言123</td>
                <td>Alan1</td>
                <td>2021-01-03</td>
            </tr>
        </tbody>
    </table>
    <hr class="my-4">
    <div class="mb-3">
        <label for="exampleFormControlTextarea1" class="form-label">編輯留言 : </label>
        <textarea class="form-control" id="exampleFormControlTextarea1" rows="3"></textarea>
        <div class="invalid-feedback">
            請填入內容!
        </div>
    </div>
    <div class="col-12">
        <button class="btn btn-outline-primary" type="submit">送出</button>
        <button class="btn btn-outline-secondary" type="reset">清除</button>
    </div>
    <hr class="my-4">

        <script>
        (function () {
            'use strict'
            var forms = document.querySelectorAll('.needs-validation')

            Array.prototype.slice.call(forms)
                .forEach(function (form) {
                    form.addEventListener('submit', function (event) {
                        if (!form.checkValidity()) {
                            event.preventDefault();
                            event.stopPropagation();
                        }
                        else {
                            alert("成功");
                            event.preventDefault();
                            location.reload();
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
