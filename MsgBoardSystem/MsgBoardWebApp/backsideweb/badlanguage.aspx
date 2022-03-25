<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="badlanguage.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.badlanguage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {

            /*初始化資料*/
            function getAlldata() {
                $.ajax({
                    type: 'POST',
                    url: 'api/back/GetSwear',
                    success: function (res) {
                        var rows = [];
                        $.each(res, function (i, item) {
                           
                            /*dataID是我自定義的屬性用來查ID用的*/
                            rows.push('<tr><td>' + (i + 1) + '</td><td>' + item.Body + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td></tr>');
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
            /*初始化資料*/
            /*驗證表單增加按鈕*/
            /*檢驗*/
            document.querySelector('#topTitle').addEventListener('blur', validatetopTitle);
            function validatetopTitle() {
                const Titletop = document.querySelector('#topTitle');
                const re = /^\S+$/;
                if (re.test(Titletop.value)) {
                    Titletop.classList.remove('is-invalid');
                    Titletop.classList.add('is-valid');

                    return true;
                } else {
                    Titletop.classList.add('is-invalid');
                    Titletop.classList.remove('is-valid');

                    return false;
                }
            }

            /*增加功能按鈕*/
            /*  Topadd Modal js*/
            $("#topAdd").on("click", function () {

               
                var addData = document.getElementById("topTitle").value;

                const obj = {
                   
                    addData: addData
            
                };
                data = JSON.stringify(obj);
                $.ajax({

                    type: 'POST',
                    url: 'api/back/addswearing',

                    data: "=" + data,
                    success: function (res) {
                        if (res.state !== 200) {
                            return alert(res.msg);
                        }
                        getAlldata();
                        alert(res.msg);
                    },
                    error: function (res) {
                        if (res === undefined) {
                            return alert("增加失敗");
                        }
                        return alert(res.msg);
                    },
                    beforeSend: function (request) {
                        request.setRequestHeader("key", "<%=enCodedataID%>");
                    }
                })
                document.getElementById("topTitle").value = "";
           

            })

            /*  Topadd Modal js*/
            /*驗證表單增加按鈕*/


            /*為刪除綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.del', function () {
                var dataID = $(this).attr('dataID');
                $.ajax({
                    type: 'POST',
                    url: 'api/back/DelSwear',
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

            $(document).ajaxStop(function () {
                $('#myLoading').hide(); <%--我的等預覽--%>
             });
        })
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <%--開始製作增加標題按鈕--%>
    <div class="container mb-4">
        <form id="topform" action="#" class="needs-validation" novalidate>
            <div class="row">
                <div class="col-2">

                    <div type="text" class="btn alert-dark" style="width: 100%; cursor: default">禁言關鍵字</div>
                </div>
                <div class="col-8">

                    <input type="text" class="form-control" aria-label="Sizing example input" aria-describedby="inputGroup-sizing-lg" id="topTitle" required>
                    <div class="invalid-feedback">不有空白</div>
                </div>
                <div class="col-2">
                    <button type="button" class="btn btn-primary"  id="topAdd">增加</button>

                </div>
            </div>
        </form>
    </div>
    <%--內文製作--%>
    <%--圖表--%>
    <table class="table table-bordered table-hover">
        <thead>
            <tr>
              
                <th>#</th>
                <th>關鍵字</th>              
                <th>刪除</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>
    <div class="d-flex justify-content-center"><%--我的等預覽--%>
  <div class="spinner-border text-primary" role="status" id="myLoading" style="width:500px;height:500px;border-width:20px; display:none">  <%--我的等預覽--%>
    <span class="visually-hidden">Loading...</span><%--我的等預覽--%>
  </div>
</div>
</asp:Content>
