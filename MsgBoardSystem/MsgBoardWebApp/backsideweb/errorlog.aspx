<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="errorlog.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.errorlog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            function getAlldata() {
                $.ajax({
                    type: 'POST',
                    url: 'api/back/GeterrorLogs',
                    success: function (res) {
                        var rows = [];
                        $.each(res, function (i, item) {
                            var CreateDate = new Date(item.CreateDate);
                            /*dataID是我自定義的屬性用來查ID用的*/
                            rows.push('<tr><td>' + (i + 1) + '</td><td>' + CreateDate.getFullYear() + '年' + (CreateDate.getMonth()+1) + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Body + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td></tr>');
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
            getAlldata();
            /*為刪除綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.del', function () {
                var dataID = $(this).attr('dataID');
                /*var data = { "dataID": dataID };*/
                $.ajax({
                    type: 'POST',
                    url: 'api/back/DelErrorLogs',
                    //      data: '=' + dataID,
                    // contentType: 'application/x-www-form-urlencoded',
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
         
            $(document).ajaxStart(function () {<%--我的等預覽--%>
                $('#myLoading').show(); <%--我的等預覽--%>
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
                <th>#</th>
                <th>產生時間</th>
                <th>內文</th>
                <th>操作</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>
<div class="d-flex justify-content-center"><%--我的等預覽--%>
  <div class="spinner-border" role="status" id="myLoading">  <%--我的等預覽--%>
    <span class="visually-hidden">Loading...</span><%--我的等預覽--%>
  </div>
</div>
</asp:Content>
