<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="blackmember.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.blackmember" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        $(function () {


            var bb;
            var bd;
            /*取得所有資料*/
            function getAlldata() {
                $.ajax({
                    type: 'POST',
                    url: 'api/back/GetEditMember',
                    success: function (res) {
                        var rows = [];
                        var bucketDate;
                        $.each(res, function (i, item) {

                            if (item.Bucket == undefined) {
                                bucketDate = '沒有設定成黑名單'
                            } else {
                                var BucketDay = new Date(item.Bucket);
                                var ToDay = new Date();
                                if (BucketDay > ToDay) {
                                    bucketDate = BucketDay.getFullYear() + '年' + (BucketDay.getMonth() + 1) + '月' + BucketDay.getDate() + '日';
                                } else {
                                    bucketDate = '黑名單已過期';
                                }
                                
                            }
                          
                            var CreateDate = new Date(item.CreateDate);
                            bb = item.Bucket;/*ss*/

                            
                            bd = BucketDay;/*ss*/
                            /*dataID是我自定義的屬性用來查ID用的*/
                            /*  *//*  var mybotton = "<button type = 'button' class='btn btn-primary edit'data-bs-toggle='modal'data-bs-target='#myModal'dataID="+ item.ID +">gg</button>"*/
                            rows.push('<tr><td>' + item.Name + '</td><td>' + CreateDate.getFullYear() + '年' + (CreateDate.getMonth()+1) + '月' + CreateDate.getDate() + '日' + CreateDate.getHours() + '點' + CreateDate.getMinutes() + '分' + CreateDate.getSeconds() + '秒' + '</td><td>' + item.Account + '</td><td>' + item.UserID + '</td><td>' + item.Email + '</td><td>' + bucketDate + '</td><td><a href="javascript:;"class="del" dataID="' + item.ID + '">取消</a></td><td><button type = "button" class="btn btn-primary edit"data-bs-toggle="modal"data-bs-target="#myModal"dataID=' + item.ID + '>編輯</button></td></tr>');
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
            /*取得所有資料*/

        
            $('#tb').on('click', '.del', function () {
                var dataID = $(this).attr('dataID');
                $.ajax({
                    type: 'POST',
                    url: 'api/back/CancelBucket',
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
                <th>UserID</th>
                <th>Email</th>
                <th>關閉期間</th>
                <th>取消</th>
                <th>編輯</th>
            </tr>
        </thead>
        <tbody id="tb"></tbody>
    </table>
</asp:Content>
