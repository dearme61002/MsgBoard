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
                            rows.push('<tr><td>' + item.Name + '</td><td>' + CreateDate.getFullYear() + '年' + CreateDate.getMonth() + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Account + '</td><td>' + item.Password + '</td><td>' + item.Email + '</td><td>' + BirthDay.getFullYear() + '年' + BirthDay.getMonth() + '月' + BirthDay.getDate() + '日' + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td><td><button type = "button" class="btn btn-primary edit"data-bs-toggle="modal"data-bs-target="#myModal"dataID=' + item.ID + '>編輯</button></td></tr>');
                        })
                        var dd = rows.join('');
                        $('#tb').empty().append(rows.join(''));


                    },
                    error: function () {
                        alert('獲取資料失敗');
                    }
                }
                )
            }
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
                            return alert('刪除資料失敗');
                        }
                        getAlldata();
                        alert(res.msg);
                    },
                    error: function (res) {
                        return alert('刪除資料失敗');
                    }

                })





                alert(name + account + password + email + date+dataID);
                $("#myModal").modal('hide');
            })
            /*Modal js*/





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

    <!-- Modal -->
    <div class="modal fade" id="myModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">編輯會員</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <h1 style="text-align: center;">輸入你想編輯的內容</h1>
                    <div class="input-group input-group-lg">
                        <span class="input-group-text">姓名</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputname">
                    </div>
                    <div class="input-group input-group-lg">
                        <span class="input-group-text">帳號</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputaccount">
                    </div>
                    <div class="input-group input-group-lg">
                        <span class="input-group-text">密碼</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputpassword">
                    </div>
                    <div class="input-group input-group-lg">
                        <span class="input-group-text">Email</span>
                        <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="inputemail">
                    </div>
                    <div class="input-group input-group-lg">
                        <span class="input-group-text">生日</span>
                        <input type="date" class="form-control" id="date" name="date">
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary" id="yesdo">確定</button>
                </div>
            </div>
        </div>
    </div>
    <!-- Modal -->

</asp:Content>
