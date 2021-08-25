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
                            rows.push('<tr><td>' + item.Name + '</td><td>' + CreateDate.getFullYear() + '年' + CreateDate.getMonth() + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Account + '</td><td>' + item.Password + '</td><td>' + item.Email + '</td><td>' + BirthDay.getFullYear() + '年' + BirthDay.getMonth() + '月' + BirthDay.getDate() + '日' + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td><td><a href="javascript:;"class="edit" dataID="' + item.ID + '">送出</a></td></tr>');
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
                var data = { "dataID": dataID };
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
</asp:Content>
