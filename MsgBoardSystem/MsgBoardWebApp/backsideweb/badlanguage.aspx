<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="badlanguage.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.badlanguage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {



            /*驗證表單增加按鈕*/
            /*檢驗*/
            document.querySelector('#topTitle').addEventListener('blur', validatetopTitle);
            function validatetopTitle() {
                const Titletop = document.querySelector('#topTitle');
                const re = /^\S+$/;
                if (re.test(Titletop.value)) {
                    Titletop.classList.remove('is-invalid');
                    Titletop.classList.add('is-valid');

                    return true;
                } else {
                    Titletop.classList.add('is-invalid');
                    Titletop.classList.remove('is-valid');

                    return false;
                }
            }

            /*增加功能按鈕*/
            /*  Topadd Modal js*/
            $("#topAdd").on("click", function () {

               
                var addData = document.getElementById("topTitle").value;

                const obj = {
                   
                    addData: addData
            
                };
                data = JSON.stringify(obj);
                $.ajax({

                    type: 'POST',
                    url: 'api/back/addswearing',

                    data: "=" + data,
                    success: function (res) {
                        if (res.state !== 200) {
                            return alert(res.msg);
                        }
                       /* getAlldata();*/
                        alert(res.msg);
                    },
                    error: function (res) {
                        if (res === undefined) {
                            return alert("增加失敗");
                        }
                        return alert(res.msg);
                    }
                })
                document.getElementById("topTitle").value = "";
           

            })

            /*  Topadd Modal js*/
            /*驗證表單增加按鈕*/

        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--開始製作增加標題按鈕--%>
    <div class="container mb-4">
        <form id="topform" action="#" class="needs-validation" novalidate>
            <div class="row">
                <div class="col-2">

                    <div type="text" class="btn alert-dark" style="width: 100%; cursor: default">禁言關鍵字</div>
                </div>
                <div class="col-8">

                    <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="topTitle" required>
                    <div class="invalid-feedback">不有空白</div>
                </div>
                <div class="col-2">
                    <button type="button" class="btn btn-primary"  id="topAdd">增加</button>

                </div>
            </div>
        </form>
    </div>
</asp:Content>
