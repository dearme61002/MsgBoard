<%@ Page Title="" Language="C#" MasterPageFile="~/backsideweb/backside.Master" AutoEventWireup="true" CodeBehind="passwordchange.aspx.cs" Inherits="MsgBoardWebApp.backsideweb.passwordchange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

   
    <style>

    #form {
        background-color: white;
        border-radius: 5px;
        padding: 20px;
        margin-top: 5%;
    }
</style>
<div class="row">
    <div id='form' class="col-8  mx-auto">

        <form id="newform" action="#" class="needs-validation" novalidate>


            <div class="form-group p-1">
                <label for="username">姓名</label>

                <input type="text" class="form-control" id="inputname" name="username" required />

                <div class="invalid-feedback">姓名不能有空白</div>
                    <div class="valid-feedback">OK</div>

            </div>

             <div class="form-group p-1">
                <label for="username">帳戶</label>

                <input type="text" class="form-control" id="inputaccount" name="inputaccount" required />

                <div class="invalid-feedback">帳號不能有空白</div>
                    <div class="valid-feedback">OK</div>

            </div>
             <div class="form-group p-1">
                <label for="username">密碼</label>

                <input type="password" class="form-control" id="inputpassword" name="inputpassword" required />

               <div class="invalid-feedback">密碼格式不對</div>
                    <div class="valid-feedback">OK</div>

            </div>
             <div class="form-group p-1">
                <label for="username">Email</label>

                <input type="email" class="form-control" id="inputemail" name="inputemail" required />

                 <div id="email-validation" class="invalid-feedback">請再次檢查電子郵件是否正確</div>

            </div>
            <div class="form-group p-1">
                <label for="date">生日</label>
                 <input type="date" class="form-control" id="date" name="date">
            </div>
            
            <button type="button" class=" mt-4 d-block w-100 btn btn-primary">Submit</button>
        </form>
    </div>
</div>

    <script src="../jquery/mycheck.js"></script>
</asp:Content>
