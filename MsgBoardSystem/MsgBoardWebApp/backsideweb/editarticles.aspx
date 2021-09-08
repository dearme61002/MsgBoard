<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="editarticles.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.editarticles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        $(function () {
            /* 增加初始資料*/
            function getAlldata() {
                $.ajax({
                    type: 'POST',
                    url: 'api/back/GetEditArticles',/*ffff*/
                    success: function (res) {
                        var rows = [];
                        $.each(res, function (i, item) {
                            var CreateDate = new Date(item.CreateDate);
                           
                            /*dataID是我自定義的屬性用來查ID用的*/
                            /*  *//*  var mybotton = "<button type = 'button' class='btn btn-primary edit'data-bs-toggle='modal'data-bs-target='#myModal'dataID="+ item.ID +">gg</button>"*/
                            rows.push('<tr><td>' + item.Name + '</td><td>' + CreateDate.getFullYear() + '年' + (CreateDate.getMonth() + 1) + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Account + '</td><td>' + item.UserID + '</td><td>' + item.Title + '</td><td>' + item.Body + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td><td><button type = "button" class="btn btn-primary edit" dataID=' + item.ID + '>只刪除內文回應刪除訊息</button></td></tr>');
                        })
                        var dd = rows.join('');
                        $('#tb').empty().append(rows.join(''));


                    },
                    error: function () {
                        alert('獲取資料失敗');
                    },
                    beforeSend: function (request) {
                        request.setRequestHeader("key", "<%=enCodedataID%>");
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
                    url: 'api/back/DelMessage',
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
                        request.setRequestHeader("key", "<%=enCodedataID%>");
                     }

                })
            });


            /*為刪除綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.edit', function () {
                var dataID = $(this).attr('dataID');
                $.ajax({
                    type: 'POST',
                    url: 'api/back/DelEditMessage',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(dataID),
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
                        request.setRequestHeader("key", "<%=enCodedataID%>");
                     }

                })
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
                <th>發文者姓名</th>
                <th>創建日期</th>
                <th>發文者帳號</th>
                <th>發文者UserID</th>
                <th>發文者回應的主題</th>
                <th>發文者的發表留言內容</th>
                <th>從資料庫刪除</th>
                <th>刪除資料但資料庫還保有發文者訊息</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>

     <div class="d-flex justify-content-center"><%--我的等預覽--%>
  <div class="spinner-border text-primary" role="status" id="myLoading" style="width:500px;height:500px;border-width:20px; display:none">  <%--我的等預覽--%>
    <span class="visually-hidden">Loading...</span><%--我的等預覽--%>
  </div>
</div><%--我的等預覽--%>
</asp:Content>
