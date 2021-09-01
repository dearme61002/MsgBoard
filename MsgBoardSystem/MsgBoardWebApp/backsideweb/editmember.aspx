<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="editmember.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.editmember" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            function getAlldata() {
                $.ajax({
                    type: 'POST',
                    url: 'api/back/GetEditMember',
                    success: function (res) {
                        var rows = [];
                        $.each(res, function (i, item) {
                            var CreateDate = new Date(item.CreateDate);
                            var BirthDay = new Date(item.BirthDay);
                            /*dataID是我自定義的屬性用來查ID用的*/
                            /*  *//*  var mybotton = "<button type = 'button' class='btn btn-primary edit'data-bs-toggle='modal'data-bs-target='#myModal'dataID="+ item.ID +">gg</button>"*/
                            rows.push('<tr><td>' + item.Name + '</td><td>' + CreateDate.getFullYear() + '年' + (CreateDate.getMonth()+1) + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Account + '</td><td>' + item.Password + '</td><td>' + item.Email + '</td><td>' + BirthDay.getFullYear() + '年' + (BirthDay.getMonth()+1) + '月' + BirthDay.getDate() + '日' + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td><td><button type = "button" class="btn btn-primary edit"data-bs-toggle="modal"data-bs-target="#myModal"dataID=' + item.ID + '>編輯</button></td></tr>');
                        })
                        var dd = rows.join('');
                        $('#tb').empty().append(rows.join(''));


                    },
                    error: function () {
                        alert('獲取資料失敗');
                    },
                    beforeSend: function (request) {
                        request.setRequestHeader("key", "<%=dataID%>");
                     }
                }
                )
            }
            $(document).ajaxStart(function () { <%--我的等預覽--%>
                $("#myLoading").show();
            });
            getAlldata();
            /*為刪除綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.del', function () {
                var dataID = $(this).attr('dataID');
                $.ajax({
                    type: 'POST',
                    url: 'api/back/DelMember',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(dataID),
                    success: function (res) {
                        if (res.state !== 200) {
                            return alert('刪除資料失敗');
                        }
                        getAlldata();
                        alert(res.msg);
                    },
                    error: function (res) {
                        return alert('刪除資料失敗');
                    },
                    beforeSend: function (request) {
                        request.setRequestHeader("key", "<%=dataID%>");
                     }

                })
            });
            var dataID = ""; /*選得資料唯一參數*/
            /*為刪除綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.edit', function () {
                dataID = $(this).attr('dataID');

            });


            /* Modal js*/

            $("#yesdo").on("click", function () {

                var name = document.getElementById("inputname").value;
                var account = document.getElementById("inputaccount").value;
                var password = document.getElementById("inputpassword").value;
                var email = document.getElementById("inputemail").value;
                var date = document.getElementById("date").value;
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
                    url: 'api/back/editMember',
       
                    data: "=" + data,
                    success: function (res) {
                        if (res.state !== 200) {
                            return alert(res.msg);
                        }
                        getAlldata();
                        alert(res.msg);
                    },
                    error: function (res) {
                        return alert(res.msg);
                    },
                    beforeSend: function (request) {
                        request.setRequestHeader("key", "<%=dataID%>");
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

              
                $("#myModal").modal('hide');
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
            $(document).ajaxStop(function () {
                $('#myLoading').hide(); <%--我的等預覽--%>
            });
            


        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--圖表--%>
    <table class="table table-bordered table-hover">
        <thead>
            <tr>
                <th>姓名</th>
                <th>創建日期</th>
                <th>帳號</th>
                <th>密碼</th>
                <th>Email</th>
                <th>生日</th>
                <th>刪除</th>
                <th>編輯</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>
    <div class="d-flex justify-content-center"><%--我的等預覽--%>
  <div class="spinner-border text-primary" role="status" id="myLoading" style="width:500px;height:500px;border-width:20px; display:none">  <%--我的等預覽--%>
    <span class="visually-hidden">Loading...</span><%--我的等預覽--%>
  </div>
</div>
    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">編輯會員</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>

       <form id="newform" action="#" class="needs-validation" novalidate>

                <div class="modal-body">
                    <h1 style="text-align: center;">輸入你想編輯的內容</h1>
                    <div class="input-group input-group-lg form-group p-1">
                        <span class="input-group-text">姓名</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputname"required>

                        <div class="invalid-feedback">姓名不能有空白</div>
                    <div class="valid-feedback">OK</div>

                    </div>
                    <div class="input-group input-group-lg form-group p-1">
                        <span class="input-group-text">帳號</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputaccount" required>

                    <div class="invalid-feedback">帳號不能有空白</div>
                    <div class="valid-feedback">OK</div>

                    </div>
                    <div class="input-group input-group-lg form-group p-1">
                        <span class="input-group-text">密碼</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputpassword" required>

                        <div class="invalid-feedback">密碼格式不對要6到15位</div>
                    <div class="valid-feedback">OK</div>



                    </div>
                    <div class="input-group input-group-lg form-group p-1">
                        <span class="input-group-text">Email</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputemail" required>

                      <div id="email-validation" class="invalid-feedback">請再次檢查電子郵件是否正確</div>

                    </div>


                    <div class="input-group input-group-lg form-group p-1">
                        <span class="input-group-text">生日</span>
                        <input type="date" class="form-control" id="date" name="date">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal" id="close" >Close</button>
                    <button type="button" class="btn btn-primary" id="yesdo">確定</button>
                </div>

      </form>
            </div>
        </div>
    </div>
    <!-- Modal -->
    <script src="../jquery/mycheck.js"></script>
</asp:Content>
