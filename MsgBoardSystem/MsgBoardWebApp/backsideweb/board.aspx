<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="board.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.board" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script>
        $(function () {
            /*取得資料*/
            var getdataID = '<%=dataID %>'
            //const obj = {
            //    dataID: getdataID
            //};
            //data = JSON.stringify(obj);
            function getAlldata() {

                const obj = {
                    dataID: getdataID
                };
                data = JSON.stringify(obj);

                $.ajax({
                    type: 'POST',
                    url: 'api/back/Getbord',
                    data: "=" + data,
                    success: function (res) {
                        var rows = [];
                        $.each(res, function (i, item) {
                            var CreateDate = new Date(item.CreateDate);
                          
                            /*dataID是我自定義的屬性用來查ID用的*/
                            /*  *//*  var mybotton = "<button type = 'button' class='btn btn-primary edit'data-bs-toggle='modal'data-bs-target='#myModal'dataID="+ item.ID +">gg</button>"*/
                            rows.push('<tr><td>' + item.PostID + '</td><td>' + item.Title + '</td><td>' + CreateDate.getFullYear() + '年' + (CreateDate.getMonth() + 1) + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Body + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">刪除</a></td><td><button type = "button" class="btn btn-primary edit"data-bs-toggle="modal"data-bs-target="#myModal"dataID=' + item.ID + '>編輯</button></td></tr>');
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
 /*取得資料*/

            /*為刪除綁上點擊功能用代理的方式*/
            $('#tb').on('click', '.del', function () {
                var dataID = $(this).attr('dataID');
                $.ajax({
                    type: 'POST',
                    url: 'api/back/DelPosting',
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
            /*為刪除綁上點擊功能用代理的方式 編輯功能*/
            $('#tb').on('click', '.edit', function () {
                dataID = $(this).attr('dataID');

            });
            /*為刪除綁上點擊功能用代理的方式 編輯功能*/


            /* Modal js*/

            $("#yesdo").on("click", function () {

                var modalTitle = document.getElementById("modalTitle").value;
                var modalTextarea = document.getElementById("modalTextarea").value;
              
                const obj = {
                    Title: modalTitle,
                    Textarea: modalTextarea,
                    dataID: dataID
                };
                data = JSON.stringify(obj);
                $.ajax({

                    type: 'POST',
                    url: 'api/back/editPosting',

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
                            return alert("更新失敗");
                        }
                        return alert(res.msg);
                    }

                })
                /*變回空值*/
                document.getElementById("modalTitle").value = "";
                document.getElementById("modalTextarea").value = "";

                validatemodalTitle()

               


                $("#myModal").modal('hide');
            })
            /*Modal js*/







            /*驗證表單*/
            document.querySelector('#modalTitle').addEventListener('blur', validatemodalTitle);
            function validatemodalTitle() {
                const Title = document.querySelector('#modalTitle');
                const re = /^\S+$/;
                if (re.test(Title.value)) {
                    Title.classList.remove('is-invalid');
                    Title.classList.add('is-valid');

                    return true;
                } else {
                    Title.classList.add('is-invalid');
                    Title.classList.remove('is-valid');

                    return false;
                }
            }
            /*驗證表單*/

            /*變為空值*/
            $('.myisNull').on('click', function () {
                document.getElementById("modalTitle").value = "";
                document.getElementById("modalTextarea").value = "";
              
                validatemodalTitle()
            });

        })
       
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--開始製作增加標題按鈕--%>
    <div class="container mb-4">
  <div class="row">
    <div class="col-2">
        
      <div type="text" class="btn alert-dark" style="width:100%;cursor:default">標題</div>
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

 <!-- Modal -->
   <div class="modal fade" id="myModal" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="staticBackdropLabel">編輯公告</h5>
        <button type="button" class="btn-close myisNull" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
       
          <form id="newform" action="#" class="needs-validation" novalidate>


            <div class="form-group p-1">
                <label for="username">標題</label>

                <input type="text" class="form-control" id="modalTitle" name="username" required />

                <div class="invalid-feedback">不能有空白</div>
                

            </div>
            <div class="form-group p-1">
               
                <div class="form-floating">
            <textarea class="form-control"  id="modalTextarea" style="height: 350px"></textarea>
                <label for="floatingTextarea2">內文</label>
                  </div>
            </div>
        </form>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary myisNull" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary" id="yesdo">更正</button>
      </div>
    </div>
  </div>
</div>
    <!-- Modal -->

</asp:Content>
