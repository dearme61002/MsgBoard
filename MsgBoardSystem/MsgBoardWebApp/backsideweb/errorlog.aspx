<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="errorlog.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.errorlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
         $(function () {
        $.ajax({
            type: 'POST',
            url: 'api/back/GeterrorLogs',
            success: function (res) {
                var  rows=[];
                $.each(res, function (i, item) {
                    var CreateDate = new Date(item.CreateDate);
                    rows.push('<tr><td>' + item.ID + '</td><td>' + CreateDate.getFullYear() + '年' + CreateDate.getMonth() + '月' + + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + + CreateDate.getMinutes() + '分' + + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Body + '</td><td><a href="javascript:;">刪除</a></td></tr>');
                })
                var dd = rows.join('');
                    $('#tb').empty().append(rows.join(''));
                
            },
            error: function () {
                alert('獲取資料失敗');
            }
        }
        )
    }) 
     </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--圖表--%>
    <table class="table table-bordered table-hover">
        <thead>
            <tr>
                <th>ID</th>
                <th>產生時間</th>
                <th>內文</th>
                <th>操作</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
        </table>
</asp:Content>
