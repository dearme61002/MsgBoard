<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        span {
            position: absolute;
            left: 50%;
            top: 100px;
            transform: translate(-50%, -50%);
            display: block;
            color: #cf1b1b;
            font-size: 47px; /*文字大小*/
            letter-spacing: 8px;
            cursor: pointer;
        }

            span::before {
                content: "設定首頁熱門標題及刪除標題"; /*設定文字*/
                position: absolute;
                color: transparent;
                background-image: repeating-linear-gradient( 45deg, transparent 0, transparent 2px, white 2px, white 4px );
                -webkit-background-clip: text;
                top: 0px;
                left: 0;
                z-index: -1;
                transition: 1s;
            }

            span::after {
                content: "設定首頁熱門標題及刪除標題"; /*設定文字*/
                position: absolute;
                color: transparent;
                background-image: repeating-linear-gradient( 135deg, transparent 0, transparent 2px, white 2px, white 4px );
                -webkit-background-clip: text;
                top: 0px;
                left: 0px;
                transition: 1s;
            }

            span:hover:before {
                top: 10px;
                left: 10px;
            }

            span:hover:after {
                top: -10px;
                left: -10px;
            }
    </style>
    <script>
        $(function () {
            function getAlldata() {
                $.ajax({
                    type: 'POST',
                    url: 'api/back/GetIndex',
                    success: function (res) {
                        var rows = [];
                        $.each(res, function (i, item) {
                            var CreateDate = new Date(item.CreateDate);
                            
                            /*dataID是我自定義的屬性用來查ID用的*/
                            /*  *//*  var mybotton = "<button type = 'button' class='btn btn-primary edit'data-bs-toggle='modal'data-bs-target='#myModal'dataID="+ item.ID +">gg</button>"*/
                            rows.push('<tr><td>' + (i + 1) + '</td><td>' + CreateDate.getFullYear() + '年' + (CreateDate.getMonth() + 1) + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Title + '</td><td>' + item.Body + '</td><td>' + item.ismaincontent + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td><td><button type = "button" class="btn btn-primary edit" dataID=' + item.ID + '>設定</button></td><td><button type = "button" class="btn btn-primary CancelTop" dataID=' + item.ID + '>取消</button></td></tr>');
                          
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
                    url: 'api/back/DelIndex',
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
            /*為刪除綁上點擊功能用代理的方式*/

            /*為edit綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.CancelTop', function () {
                var dataID = $(this).attr('dataID');

                $.ajax({
                    type: 'POST',
                    url: 'api/back/CancelIndex',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(dataID),
                    success: function (res) {
                        if (res.state !== 200) {
                            return alert('設定失敗');
                        }
                        getAlldata();
                        alert(res.msg);
                    },
                    error: function (res) {
                        return alert('設定失敗');
                    }

                })
            });
            /*為edit綁上點擊功能用代理的方式*/

            /*為edit綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.edit', function () {
                var dataID = $(this).attr('dataID');

                $.ajax({
                    type: 'POST',
                    url: 'api/back/setIndex',
                    contentType: 'application/json; charset=utf-8',
                    data: JSON.stringify(dataID),
                    success: function (res) {
                        if (res.state !== 200) {
                            return alert('設定失敗');
                        }
                        getAlldata();
                        alert(res.msg);
                    },
                    error: function (res) {
                        return alert('設定失敗');
                    }

                })
            });
            /*為edit綁上點擊功能用代理的方式*/

        })
    </script>
</asp:Content>
<%--接收標題--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--設定標題--%>
    <span>設定首頁熱門標題及刪除標題</span>
    <%--設定標題--%>
    <%--重點資料--%>
    <table class="table table-bordered table-hover" style="margin-top:130px">
        <thead>
            <tr>
               <th>#</th>
                <th>創建日期</th>
                <th>標題</th>
                <th>內文</th>
                <th>是否顯示在首頁</th>
                <th>刪除</th>
                <th>設定</th>
                 <th>取消設定</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>
    <%--重點資料--%>
</asp:Content>

