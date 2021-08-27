<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="passwordchange.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.passwordchange" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <style>
        #form {
            background-color: white;
            border-radius: 5px;
            padding: 20px;
            margin-top: 5%;
        }
    </style>
    <div class="row">
        <div id='form' class="col-8  mx-auto">

            <form id="newform" action="#" class="needs-validation" novalidate>


                <div class="form-group p-1">
                    <label for="username">姓名</label>

                    <input type="text" class="form-control" id="inputname" name="username" required />

                    <div class="invalid-feedback">姓名不能有空白</div>
                    <div class="valid-feedback">OK</div>

                </div>

                <div class="form-group p-1">
                    <label for="username">帳戶</label>

                    <input type="text" class="form-control" id="inputaccount" name="inputaccount" required />

                    <div class="invalid-feedback">帳號不能有空白</div>
                    <div class="valid-feedback">OK</div>

                </div>
                <div class="form-group p-1">
                    <label for="username">密碼</label>

                    <input type="password" class="form-control" id="inputpassword" name="inputpassword" required />

                    <div class="invalid-feedback">密碼格式不對</div>
                    <div class="valid-feedback">OK</div>

                </div>
                <div class="form-group p-1">
                    <label for="username">Email</label>

                    <input type="email" class="form-control" id="inputemail" name="inputemail" required />

                    <div id="email-validation" class="invalid-feedback">請再次檢查電子郵件是否正確</div>

                </div>
                <div class="form-group p-1">
                    <label for="date">生日</label>
                    <input type="date" class="form-control" id="date" name="date">
                </div>
                <%-- <button type="button" class="btn btn-primary" id="yesdo">確定</button>--%>
                <button type="button" class=" mt-4 d-block w-100 btn btn-primary" id="yesdo">Submit</button>
            </form>
        </div>
    </div>

    <script src="../jquery/mycheck.js"></script>

    <%--表單按鈕--%>
    <script>
        $(function () {

            $("#yesdo").on("click", function () {

                var name = document.getElementById("inputname").value;
                var account = document.getElementById("inputaccount").value;
                var password = document.getElementById("inputpassword").value;
                var email = document.getElementById("inputemail").value;
                var date = document.getElementById("date").value;
                var dataID = '<%=dataID %>'
                const obj = {
                    name: name,
                    account: account,
                    password: password,
                    email: email,
                    date: date,
                    dataID: dataID
                };
                data = JSON.stringify(obj);
                $.ajax({

                    type: 'POST',
                    url: 'api/back/editmydata',

                    data: "=" + data,
                    success: function (res) {
                        if (res.state !== 200) {
                            return alert(res.msg);
                           
                        }
                        alert(res.msg);

                   

                        function getCookie(name) {
                            let arr = document.cookie.match(new RegExp("(^| )" + name + "=([^;]*)(;|$)"));
                            if (arr != null) return unescape(arr[2]);
                            return null;
                        }
                        //刪除cookie
                        function delCookie(name) {
                            var exp = new Date();
                            exp.setTime(exp.getTime() - 1);
                            var cval = getCookie(name);
                            if (cval != null)
                                document.cookie = name + "=" + cval + ";expires=" + exp.toGMTString() + ";path=/";
                        }
                        delCookie('.ASPXAUTH');
                        document.cookie;
                        
                      /*  document.cookie = '.ASPXAUTH' + '=; expires=Thu, 01-Jan-70 00:00:01 GMT;';*/
                      
                        alert("必須登出");
                           // window.location.href = "http://localhost:49461/Page01Default.aspx";
                        parent.window.location.assign("../Page01Default.aspx");
                    },
                    error: function (res) {
                        return alert(res.msg);
                    }

                })
                /*變回空值*/
                document.getElementById("inputname").value = "";
                document.getElementById("inputaccount").value = "";
                document.getElementById("inputpassword").value = "";
                document.getElementById("inputemail").value = "";
                document.getElementById("date").value = "";

                validatePassword()
                validateEmail()
                validateUsername()
                validateaccount()


            })
            /*Modal js*/

            $('#close').on('click', function () {
                document.getElementById("inputname").value = "";
                document.getElementById("inputaccount").value = "";
                document.getElementById("inputpassword").value = "";
                document.getElementById("inputemail").value = "";
                document.getElementById("date").value = "";
                validatePassword()
                validateEmail()
                validateUsername()
                validateaccount()
            });



        })



       
       
    </script>





</asp:Content>
