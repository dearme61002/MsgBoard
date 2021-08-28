<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="board.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.board" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
