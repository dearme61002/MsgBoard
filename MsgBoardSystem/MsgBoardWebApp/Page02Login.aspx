<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Page02Login.aspx.cs" Inherits="MsgBoardWebApp.Page02Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <body class="bg-light">
        <div class="container">
            <main>
                <div class="py-5 text-center">
                </div>
                <div class="row g-5">
                    <div class="col-md-5 col-lg-4 order-md-last">
                        <form class="card p-2">
                            <div class="input-group">
                            </div>
                        </form>
                    </div>
                    <div class="col-md-7 col-lg-8">
                        <h3 class="mb-3">會員登入</h3>
                        <hr class="my-4">
                        <form class="needs-validation" novalidate>
                            <div class="row g-3">
                                <div class="col-sm-3">
                                    <label for="txtAcc" class="form-label">
                                        <h4>帳號 : </h4>
                                    </label>
                                </div>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="txtAcc" placeholder="" value="" required>
                                    <div class="invalid-feedback">
                                        請輸入帳號
                                    </div>
                                </div>
                            </div>
                            <div class="row g-3">
                                <div class="col-sm-3">
                                    <label for="txtPwd" class="form-label">
                                        <h4>密碼 : </h4>
                                    </label>
                                </div>
                                <div class="col-sm-9">
                                    <input type="text" class="form-control" id="txtPwd" placeholder="" value="" required>
                                    <div class="invalid-feedback">
                                        請輸入密碼
                                    </div>
                                </div>
                            </div>
                            <hr class="my-4">
                            <button class="btn btn-primary btn-lg" type="submit">送出</button>
                        </form>
                    </div>
                </div>
            </main>
        </div>

        <script src="Bootstrap/js/bootstrap.bundle.min.js"></script>
        <script src="Bootstrap/Page02Login/form-validation.js"></script>
    </body>
</asp:Content>
