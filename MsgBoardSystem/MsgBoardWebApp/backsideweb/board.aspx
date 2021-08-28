<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="board.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.board" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {
            /*取得資料*/
            function getAlldata() {
                $.ajax({
                    type: 'POST',
                    url: 'api/back/',
                    success: function (res) {
                        var rows = [];
                        $.each(res, function (i, item) {
                            var CreateDate = new Date(item.CreateDate);
                            var BirthDay = new Date(item.BirthDay);
                            /*dataID是我自定義的屬性用來查ID用的*/
                            /*  *//*  var mybotton = "<button type = 'button' class='btn btn-primary edit'data-bs-toggle='modal'data-bs-target='#myModal'dataID="+ item.ID +">gg</button>"*/
                            rows.push('<tr><td>' + item.Name + '</td><td>' + CreateDate.getFullYear() + '年' + (CreateDate.getMonth() + 1) + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Account + '</td><td>' + item.Password + '</td><td>' + item.Email + '</td><td>' + BirthDay.getFullYear() + '年' + BirthDay.getMonth() + '月' + BirthDay.getDate() + '日' + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td><td><button type = "button" class="btn btn-primary edit"data-bs-toggle="modal"data-bs-target="#myModal"dataID=' + item.ID + '>編輯</button></td></tr>');
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
        })
        /*取得資料*/
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--開始製作增加標題按鈕--%>
    <div class="container mb-4">
  <div class="row">
    <div class="col-2">
        
      <div type="text" class="btn alert-dark" style="width:100%">標題</div>
    </div>
    <div class="col-8">
      <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg">
    </div>
    <div class="col-2">
     <button type="button" class="btn btn-primary">增加</button>
    </div>
  </div>
</div>
    <%--開始製作增加標題按鈕--%>

    <%--檢視資料--%>
    <%--圖表--%>
    <table class="table table-bordered table-hover">
        <thead>
            <tr>
                <th>PostID</th>
                <th>標題</th>
                <th>創建日期</th>              
                <th>內文</th>
                <th>刪除</th>
                <th>編輯</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>


    <%--檢視資料--%>


</asp:Content>
